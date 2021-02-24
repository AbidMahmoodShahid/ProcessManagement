using inotech.Core;
using Microsoft.Win32;
using Process.Editor.Elements;
using Process.Editor.Repo;
using Process.Editor.Services;
using ProcessManagement.Core;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;

namespace Process.Editor.ViewModels
{
    public partial class ProcessEditorViewModel : PropertyChangedNotifier, IClonableCollectionContainer<ProcessModel>, IImportableCollectionContainer<ProcessModel>
    {
        public event EventHandler ProcessChangedEvent;

        private ObservableCollection<ProcessModel> _itemCollection;
        public ObservableCollection<ProcessModel> ItemCollection
        {
            get { return _itemCollection; }
            set
            {
                SetField(ref _itemCollection, value);
            }
        }

        private ProcessModel _selectedProcess;
        public ProcessModel SelectedProcess
        {
            get { return _selectedProcess; }
            set
            {
                SetField(ref _selectedProcess, value);
                DeleteProcessCommand.RaiseCanExecuteChanged();
                AddProcessGroupCommand.RaiseCanExecuteChanged();
                ExportProcessCommand.RaiseCanExecuteChanged();
                ProcessChangedEvent?.Invoke(this, new EventArgs());
            }
        }

        private ProcessModel _clonedItem;
        public ProcessModel ClonedItem
        {
            get { return _clonedItem; }
            set
            {
                SetField(ref _clonedItem, value);
            }
        }

        private ProcessModel _importedItem;
        public ProcessModel ImportedItem
        {
            get { return _importedItem; }
            set
            {
                SetField(ref _importedItem, value);
            }
        }

        public Visibility SortingNumberVisibility { get; private set; }


        #region Import process

        public DelegateCommand ImportProcessCommand { get; }

        private void ExecuteImportProcess(object obj)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "XML file (*.xml)|*.xml|Text file (*.txt)|*.txt";

            if(openFileDialog.ShowDialog() == true)
            {
                if(ItemCollection == null)
                    return;

                Dictionary<string, string> newNameAndIdDictionary = ServiceLocator.Default.GetService<ISetItemInformation>().SetNewNameAndID(_getterService.GetIDList(ItemCollection));

                if(newNameAndIdDictionary == null)
                    return;

                if(string.IsNullOrWhiteSpace(newNameAndIdDictionary["Name"]))
                    return;

                if(string.IsNullOrWhiteSpace(newNameAndIdDictionary["Id"]))
                    return;

                if(!(File.Exists(openFileDialog.FileName)))
                    return;

                ImportService.Import(this, openFileDialog.FileName, newNameAndIdDictionary);
            }
        }

        #endregion

        #region Copy process command

        private DelegateCommand _copyProcessCommand;
        public DelegateCommand CopyProcessCommand
        {
            get
            {
                return _copyProcessCommand = new DelegateCommand(param => ExecuteCopyProcess(param));
            }
        }

        private void ExecuteCopyProcess(object parameter)
        {
            ClonedItem = (parameter as ProcessModel);
        }

        #endregion

        #region Paste process command

        private DelegateCommand _pasteProcessCommand;

        public DelegateCommand PasteProcessCommand
        {
            get
            {
                return _pasteProcessCommand = new DelegateCommand(param => ExecutePasteProcess(param));
            }
        }

        private void ExecutePasteProcess(object obj)
        {
            if(ClonedItem == null)
                return;

            if(ItemCollection == null)
                return;

            if(ItemCollection.Count < 1)
                return;

            string newProcessId = ServiceLocator.Default.GetService<ISetItemInformation>().SetNewID(_getterService.GetIDList(ItemCollection));

            if(newProcessId == null)
                return;

            DeepCloneService.DeepClone(this, newProcessId);
        }

        #endregion

        #region Export process

        public DelegateCommand ExportProcessCommand { get; }

        private bool CanExecuteExportProcess(object obj)
        {
            return !(SelectedProcess == null);
        }

        private void ExecuteExportProcess(object obj)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "XML file (*.xml)|*.xml|Text file (*.txt)|*.txt";

            if(SelectedProcess == null)
                return;

            if(saveFileDialog.ShowDialog() == true)
            {
                ExportService.Export(SelectedProcess, saveFileDialog.FileName);
            }
        }

        #endregion


        #region Add process Command

        public DelegateCommand AddProcessCommand { get; }

        private void ExecuteAddProcess(object obj)
        {
            if(ItemCollection == null)
                return;

            ISortableItem newProcess = ServiceLocator.Default.GetService<ICreateNewItem>().SetNewProcess(_getterService.GetIDList(ItemCollection));

            if(newProcess == null)
                return;

            AddItemService addItemService = new AddItemService();
            addItemService.AddService(ItemCollection, (ProcessModel)newProcess);
            _unitOfWork.AttachProcess((ProcessModel)newProcess);
        }

        #endregion

        #region delete process Command

        public DelegateCommand DeleteProcessCommand { get; private set; }

        private bool CanExecuteDeleteProcess(object obj)
        {
            return !(SelectedProcess == null);
        }

        private void ExecuteDeleteProcess(object obj)
        {
            MessageBoxResult result = MessageBox.Show("Are you sure you want to delete the process?", "Delete Process", MessageBoxButton.YesNo);
            if(result == MessageBoxResult.Yes)
            {
                _unitOfWork.DeleteProcess(SelectedProcess);
                ItemCollection.Remove(SelectedProcess);
            }
        }

        #endregion

        #region Save process Command

        public DelegateCommand SaveCommand { get; }

        private void ExecuteSave(object obj)
        {
            _unitOfWork.SaveChanges();
        }

        #endregion

    }
}
