using DataAccess;
using inotech.Core;
using Process.Editor.Elements;
using Process.Editor.Services;
using ProcessManagement.Core;
using System.Collections.Generic;
using System.Windows;

namespace Process.Editor.ViewModels
{
    public partial class ProcessEditorViewModel : PropertyChangedNotifier
    {
        private ProcessGroupModel _selectedProcessGroup;
        public ProcessGroupModel SelectedProcessGroup
        {
            get { return _selectedProcessGroup; }
            set
            {
                SetField(ref _selectedProcessGroup, value);
                DeleteProcessGroupCommand.RaiseCanExecuteChanged();
                AddProcessPointCommand.RaiseCanExecuteChanged();
            }
        }

        private ProcessGroupModel _copiedProcessGroup;
        public ProcessGroupModel CopiedProcessGroup
        {
            get { return _copiedProcessGroup; }
            set
            {
                SetField(ref _copiedProcessGroup, value);
            }
        }

        #region Processgroup down Command

        // shift processgroup down
        public DelegateCommand ProcessGroupDownCommand
        { get; private set; }

        private void ExecuteProcessGroupDown(object processGroup)
        {
            if(SelectedProcess == null)
                return;

            if(SelectedProcess.ItemCollection == null)
                return;

            if(SelectedProcess.ItemCollection.Count < 2)
                return;

            if((processGroup as ProcessGroupModel) == null)
                return;

            ISortableCollectionContainer<ProcessGroupModel> sortableCollectionContainer = SelectedProcess;
            sortableCollectionContainer.SelectedItem = processGroup as ProcessGroupModel;

            int currentIndex = sortableCollectionContainer.ItemCollection.IndexOf(sortableCollectionContainer.SelectedItem);
            if(currentIndex >= sortableCollectionContainer.ItemCollection.Count - 1)
                return;

            SortUpDownService.ItemDown(sortableCollectionContainer);
        }

        #endregion

        #region Processgroup up Command

        // shift processgroup up
        public DelegateCommand ProcessGroupUpCommand
        { get; private set; }

        private void ExecuteProcessGroupUp(object processGroup)
        {
            if(SelectedProcess == null)
                return;

            if(SelectedProcess.ItemCollection == null)
                return;

            if(SelectedProcess.ItemCollection.Count < 2)
                return;

            if((processGroup as ProcessGroupModel) == null)
                return;

            ISortableCollectionContainer<ProcessGroupModel> sortableCollectionContainer = SelectedProcess;
            sortableCollectionContainer.SelectedItem = processGroup as ProcessGroupModel;

            int currentIndex = sortableCollectionContainer.ItemCollection.IndexOf(sortableCollectionContainer.SelectedItem);
            if(currentIndex < 1)
                return;

            SortUpDownService.ItemUp(sortableCollectionContainer);
        }

        #endregion

        #region Copy processgroup command

        private DelegateCommand _copyProcessGroupCommand;

        public DelegateCommand CopyProcessGroupCommand
        {
            get
            {
                return _copyProcessGroupCommand = new DelegateCommand(param => ExecuteCopyProcessGroup(param));
            }
        }

        private void ExecuteCopyProcessGroup(object parameter)
        {
            CopiedProcessGroup = (parameter as ProcessGroupModel);
        }

        #endregion

        #region Paste processgroup command

        private DelegateCommand _pasteProcessGroupCommand;

        public DelegateCommand PasteProcessGroupCommand
        {
            get
            {
                return _pasteProcessGroupCommand = new DelegateCommand(param => ExecutePasteProcessGroup(param));
            }
        }

        private void ExecutePasteProcessGroup(object parameter)
        {
            if(CopiedProcessGroup == null)
                return;

            if(SelectedProcess == null)
                return;

            if(SelectedProcess.ItemCollection == null)
                return;

            if(SelectedProcess.ItemCollection.Count < 1)
                return;

            string newProcessGroupId = ServiceLocator.Default.GetService<ISetItemInformation>().SetNewID(_getterService.GetIDList(SelectedProcess.ItemCollection));
            SelectedProcess.ClonedItem = CopiedProcessGroup;

            if(newProcessGroupId == null)
                return;

            DeepCloneService.DeepClone(SelectedProcess, newProcessGroupId);
        }

        #endregion

        #region Add processgroup command

        public DelegateCommand AddProcessGroupCommand { get; }

        private bool CanExecuteAddProcessGroup(object obj)
        {
            return !(SelectedProcess == null);
        }

        private async void ExecuteAddProcessGroup(object obj)
        {
            if(SelectedProcess == null)
                return;

            if(SelectedProcess.ItemCollection == null)
                return;

            ISortableItem newProcessGroup = ServiceLocator.Default.GetService<ICreateNewItem>().SetNewProcessGroup(_getterService.GetIDList(SelectedProcess.ItemCollection), SelectedProcess.ItemCollection.Count);

            if(newProcessGroup == null)
                return;

            AddItemService addItemService = new AddItemService();
            addItemService.AddService(SelectedProcess.ItemCollection, (ProcessGroupModel)newProcessGroup);

            using(UnitOfWork uow = new UnitOfWork())
            {
                await uow.ProcessRepo.Update(SelectedProcess);
                await uow.SaveChangesAsync();
            }
        }

        #endregion


        #region delete processgroup Command

        public DelegateCommand DeleteProcessGroupCommand { get; private set; }

        private bool CanExecuteDeleteProcessGroup(object obj)
        {
            return !(SelectedProcessGroup == null);
        }

        private async void ExecuteDeleteProcessGroup(object obj)
        {
            MessageBoxResult result = MessageBox.Show("Are you sure you want to delete the process group?", "Delete Processgroup", MessageBoxButton.YesNo);
            if(result != MessageBoxResult.Yes)
                return;

            //TODO AM: Reasearch Bug
            ProcessGroupModel processGroupModelToDelete = SelectedProcessGroup;

            SelectedProcess.ItemCollection.Remove(SelectedProcessGroup);
            if(SelectedProcess.ItemCollection.Count > 0)
            {
                for(int i = 0; i < SelectedProcess.ItemCollection.Count; i++)
                {
                    SelectedProcess.ItemCollection[i].SortingNumber = i + 1;
                }
            }

            using(UnitOfWork uow = new UnitOfWork())
            {
                await uow.ProcessGroupRepo.UpdateRange(SelectedProcess.ItemCollection);
                await uow.ProcessGroupRepo.Delete(processGroupModelToDelete);
                await uow.SaveChangesAsync();
            }

        }

        #endregion
    }
}
