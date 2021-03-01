using DataAccess;
using inotech.Core;
using Process.Editor.Elements;
using Process.Simulation.Elements;
using Process.Simulation.Services;
using ProcessManagement.Core;
using System.Collections.ObjectModel;
using System.Threading;
using System.Windows;

namespace Process.Simulation.ViewModels
{
    public class SimulationViewModel : PropertyChangedNotifier, ISimulationViewModel
    {
        #region Constructor

        private ISimulationViewModel _processSimulation;

        public SimulationViewModel(ProcessModel processModel)
        {
            _processSimulation = this;
            ProcessModel = processModel;
            ProcessName = processModel.Name;
            ProcessID = processModel.Id;
            StartCommand = new DelegateCommand(ExecuteStart, CanExecuteStart);
            PauseCommand = new DelegateCommand(ExecutePause, CanExecutePause);
            CancelCommand = new DelegateCommand(ExecuteCancel, CanExecuteCancel);
            ResetCommand = new DelegateCommand(ExecuteReset, CanExecuteReset);
            SaveCommand = new DelegateCommand(ExecuteSave, CanExecuteSave);
            CTS = new CancellationTokenSource();
            CreateSimulationList();
        }
        public SimulationViewModel()
        {
            SimulationUCVisibility = Visibility.Collapsed;
        }

        #endregion

        #region Properties

        private Thread _pointSimulationThread;

        public CancellationTokenSource CTS { get; set; }

        private ProcessModel _processModel;
        public ProcessModel ProcessModel
        {
            get { return _processModel; }
            set { SetField(ref _processModel, value); }
        }

        private string _processName;
        public string ProcessName
        {
            get { return _processName; }
            set { SetField(ref _processName, value); }
        }

        private string _processID;
        public string ProcessID
        {
            get { return _processID; }
            set { SetField(ref _processID, value); }
        }

        public SimulationPointModel CurrentPoint { get; set; }

        private Visibility _simulationUCVisibility;
        public Visibility SimulationUCVisibility
        {
            get { return _simulationUCVisibility; }
            set { SetField(ref _simulationUCVisibility, value); }
        }

        private bool _simulationStarted;
        public bool SimulationStarted
        {
            get { return _simulationStarted; }
            set
            {
                SetField(ref _simulationStarted, value);
                ResetCommand.RaiseCanExecuteChanged();
            }
        }

        private bool _simulationRunning;
        public bool SimulationRunning
        {
            get { return _simulationRunning; }
            set
            {
                SetField(ref _simulationRunning, value);
                StartCommand.RaiseCanExecuteChanged();
                PauseCommand.RaiseCanExecuteChanged();
                CancelCommand.RaiseCanExecuteChanged();
                ResetCommand.RaiseCanExecuteChanged();
            }
        }

        public bool SimulationCancelled { get; set; }

        private bool _simulationComplete;
        public bool SimulationComplete
        {
            get { return _simulationComplete; }
            set
            {
                SetField(ref _simulationComplete, value);
                StartCommand.RaiseCanExecuteChanged();
            }
        }

        private ObservableCollection<SimulationPointModel> _processPointSimulationList;
        public ObservableCollection<SimulationPointModel> ProcessPointSimulationList
        {
            get { return _processPointSimulationList; }
            set { SetField(ref _processPointSimulationList, value); }
        }

        public SimulationProcessModel SimulationProcess { get; set; }

        #endregion

        //private void LoadAllSimulationPoints()
        //{
        //    SimulationProcess.ProcessModelId = ProcessModel.ProcessModelId;
        //    SimulationProcess.Id = ProcessModel.Id;
        //    SimulationProcess.Name = ProcessModel.Name;

        //    foreach(ProcessGroupModel processGroupModel in ProcessModel.ItemCollection)
        //    {
        //        foreach(ProcessPoint processPointModel in processGroupModel.ItemCollection)
        //        {
        //            SimulationPointModel processSimulationModel = new SimulationPointModel(processPointModel.Id, processPointModel.Name, processPointModel.SortingNumber);
        //            SimulationProcess.SimulationPointModelList.Add(processSimulationModel);
        //        }
        //    }
        //}

        #region create list

        public void CreateSimulationList()
        {
            ProcessPointSimulationList = new ObservableCollection<SimulationPointModel>();

            foreach(ProcessGroupModel processGroupModel in ProcessModel.ItemCollection)
            {
                foreach(ProcessPoint processPointModel in processGroupModel.ItemCollection)
                {
                    SimulationPointModel processSimulationModel = new SimulationPointModel(processPointModel.Id, processPointModel.Name, processPointModel.SortingNumber);
                    processSimulationModel.SimulationPointId = processPointModel.ProcessPointID;
                    ProcessPointSimulationList.Add(processSimulationModel);
                }
            }

            using(UnitOfWork uow = new UnitOfWork())
            {
                uow.SimulationPointRepo.AttachRange(ProcessPointSimulationList);
                uow.SaveChangesAsync();
            }

        }

        #endregion

        #region start

        public DelegateCommand StartCommand { get; private set; }

        private bool CanExecuteStart(object obj)
        {
            return !(SimulationRunning || SimulationComplete);
        }

        private void ExecuteStart(object obj)
        {
            SimulationService simulationServices = new SimulationService();
            _pointSimulationThread = new Thread(simulationServices.SetupSimulation);
            CTS = new CancellationTokenSource();
            _pointSimulationThread.Start(_processSimulation);
        }

        #endregion

        #region pause

        public DelegateCommand PauseCommand { get; private set; }

        private bool CanExecutePause(object obj)
        {
            return SimulationRunning;
        }

        private void ExecutePause(object obj)
        {
            SimulationService simulationServices = new SimulationService();
            simulationServices.PauseSimulation(this);
        }
        #endregion

        #region cancel

        public DelegateCommand CancelCommand { get; private set; }

        private bool CanExecuteCancel(object obj)
        {
            return SimulationRunning;
        }

        private void ExecuteCancel(object obj)
        {
            CTS.Cancel();
        }

        #endregion

        #region reset

        public DelegateCommand ResetCommand { get; private set; }

        private bool CanExecuteReset(object obj)
        {
            return ((SimulationComplete || !SimulationRunning) && SimulationStarted);
        }

        private void ExecuteReset(object obj)
        {
            SimulationService simulationServices = new SimulationService();
            simulationServices.ResetSimulation(this);
        }

        #endregion

        #region save

        public DelegateCommand SaveCommand { get; private set; }

        private bool CanExecuteSave(object obj)
        {
            return true;
        }

        private void ExecuteSave(object obj)
        {
            using(UnitOfWork uow = new UnitOfWork())
            {
                uow.SimulationPointRepo.UpdateAll(ProcessPointSimulationList);
                uow.SaveChangesAsync();
            }
        }

        #endregion
    }
}
