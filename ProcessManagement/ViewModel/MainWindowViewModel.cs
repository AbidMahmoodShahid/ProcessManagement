using inotech.Core;
using Process.Editor.Elements;
using Process.Editor.UI;
using Process.Editor.ViewModels;
using Process.Simulation.ViewModels;
using ProcessManagement.Core;
using ProcessManagement.DataStorage.EF;
using ProcessManagement.Elements;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace ProcessManagement.ViewModel
{
    public class MainWindowViewModel : PropertyChangedNotifier, ISimulationManager
    {
        public MainWindowViewModel()
        {
            SwitchModeCommand = new DelegateCommand(ExecuteSwitchMode, CanExecuteSwitchMode);
            ApplicationClosedCommand = new DelegateCommand(ExecuteApplicationClosed);

            ProcessEditorViewModel = new ProcessEditorViewModel(true);
            ProcessEditorViewModel.ProcessChangedEvent += HandleProcessChangedEvent;
            CurrentSimulationViewModel = new SimulationViewModel();
            SwitchModeButtonContent = "Simulation Mode";

            SimulationViewModelDictionary = new Dictionary<string, SimulationViewModel>();
            IsSimulationMode = false;

            #region  delete after databse setup correctly

            PMDataContext pMDC = new PMDataContext();
            pMDC.Test();
            pMDC.SaveChanges();

            #endregion

            ServiceLocator.Default.RegisterService<ICreateNewItem, CreateNewItem>(RegisterOption.Transient);
            ServiceLocator.Default.RegisterService<ISetItemInformation, SetItemInformation>(RegisterOption.Transient);
        }

        #region Properties

        private bool _canSwitchToSimulation;

        public Dictionary<string, SimulationViewModel> SimulationViewModelDictionary { get; set; }

        private ProcessEditorViewModel _processEditorViewModel;
        public ProcessEditorViewModel ProcessEditorViewModel
        {
            get { return _processEditorViewModel; }
            set { SetField(ref _processEditorViewModel, value); }
        }

        private SimulationViewModel _currentSimulationViewModel;
        public SimulationViewModel CurrentSimulationViewModel
        {
            get { return _currentSimulationViewModel; }
            set { SetField(ref _currentSimulationViewModel, value); }
        }

        private string _switchModeButtonContent;
        public string SwitchModeButtonContent
        {
            get { return _switchModeButtonContent; }
            set { SetField(ref _switchModeButtonContent, value); }
        }

        private bool _isSimulationMode;
        public bool IsSimulationMode
        {
            get { return _isSimulationMode; }
            set { SetField(ref _isSimulationMode, value); }
        }

        #endregion

        #region SwitchMode Command

        public DelegateCommand SwitchModeCommand { get; private set; }

        private bool CanExecuteSwitchMode(object obj)
        {
            return _canSwitchToSimulation;
        }

        private void ExecuteSwitchMode(object obj)
        {
            if(ProcessEditorViewModel.EditorUCVisibility == Visibility.Collapsed)
            {
                ProcessEditorViewModel.EditorUCVisibility = Visibility.Visible;
                CurrentSimulationViewModel.SimulationUCVisibility = Visibility.Collapsed;
                IsSimulationMode = false;
                SwitchModeButtonContent = "Simulation Mode";
            }
            else
            {
                ProcessEditorViewModel.EditorUCVisibility = Visibility.Collapsed;
                CurrentSimulationViewModel.SimulationUCVisibility = Visibility.Visible;
                IsSimulationMode = true;
                SwitchModeButtonContent = "Editor Mode";
                Services.SimulationUpdateServices.UpdateSimulationViewModelDictionary(this);
            }
        }

        #endregion

        #region Events

        private void HandleProcessChangedEvent(object sender, System.EventArgs e)
        {
            SetSwitchModeButtonIsEnabled();

            if(IsSimulationMode)
                SetCurrentSimulationViewModel();

        }

        private void SetSwitchModeButtonIsEnabled()
        {
            if(ProcessEditorViewModel.SelectedProcess == null)
            {
                _canSwitchToSimulation = false;
                SwitchModeCommand.RaiseCanExecuteChanged();
                return;
            }
            if(ProcessEditorViewModel.SelectedProcess.ItemCollection == null)
            {
                _canSwitchToSimulation = false;
                SwitchModeCommand.RaiseCanExecuteChanged();
                return;
            }
            if(ProcessEditorViewModel.SelectedProcess.ItemCollection.Count <= 0)
            {
                _canSwitchToSimulation = false;
                SwitchModeCommand.RaiseCanExecuteChanged();
                return;
            }

            foreach(ProcessGroupModel group in ProcessEditorViewModel.SelectedProcess.ItemCollection)
            {
                if(group.ItemCollection == null)
                    continue;
                if(group.ItemCollection.Count <= 0)
                    continue;

                _canSwitchToSimulation = true;
                SwitchModeCommand.RaiseCanExecuteChanged();
                return;
            }
        }

        private void SetCurrentSimulationViewModel()
        {
            Services.SimulationUpdateServices.SetCurrentSimulationViewModel(this);
        }

        #endregion

        #region Application closed command

        public DelegateCommand ApplicationClosedCommand { get; private set; }

        private void ExecuteApplicationClosed(object obj)
        {
            ProcessEditorViewModel.ProcessChangedEvent -= HandleProcessChangedEvent;
        }

        #endregion
    }
}
