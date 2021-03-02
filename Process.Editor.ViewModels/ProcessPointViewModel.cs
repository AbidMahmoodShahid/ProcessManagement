using DataAccess;
using inotech.Core;
using Process.Editor.Elements;
using Process.Editor.Services;
using ProcessManagement.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace Process.Editor.ViewModels
{
    public partial class ProcessEditorViewModel : PropertyChangedNotifier
    {
        private Type[] _processPointTypeList;
        private List<string> _pointTypeNameList;

        private ProcessPoint _selectedProcessPoint;
        public ProcessPoint SelectedProcessPoint
        {
            get { return _selectedProcessPoint; }
            set
            {
                SetField(ref _selectedProcessPoint, value);
                DeleteProcessPointCommand.RaiseCanExecuteChanged();
            }
        }

        private ProcessPoint _copiedProcessPoint;
        public ProcessPoint CopiedProcessPoint
        {
            get { return _copiedProcessPoint; }
            set { SetField(ref _copiedProcessPoint, value); }
        }


        #region Processpoint down Command

        // shift Processpoint down
        public DelegateCommand ProcessPointDownCommand
        { get; private set; }

        private void ExecuteProcessPointDown(object processPoint)
        {
            if(SelectedProcessGroup == null)
                return;

            if(SelectedProcessGroup.ItemCollection == null)
                return;

            if(SelectedProcessGroup.ItemCollection.Count < 2)
                return;

            if((processPoint as ProcessPoint) == null)
                return;

            ISortableCollectionContainer<ProcessPoint> sortableCollectionContainer = SelectedProcessGroup;
            sortableCollectionContainer.SelectedItem = processPoint as ProcessPoint;

            int currentIndex = sortableCollectionContainer.ItemCollection.IndexOf(sortableCollectionContainer.SelectedItem);
            if(currentIndex >= sortableCollectionContainer.ItemCollection.Count - 1)
                return;

            SortUpDownService.ItemDown(sortableCollectionContainer);
        }

        #endregion

        #region Processpoint up Command

        // shift Processpoint up
        public DelegateCommand ProcessPointUpCommand
        { get; private set; }

        private void ExecuteProcessPointUp(object processPoint)
        {
            if(SelectedProcessGroup == null)
                return;

            if(SelectedProcessGroup.ItemCollection == null)
                return;

            if(SelectedProcessGroup.ItemCollection.Count < 2)
                return;

            if((processPoint as ProcessPoint) == null)
                return;

            ISortableCollectionContainer<ProcessPoint> sortableCollectionContainer = SelectedProcessGroup;
            sortableCollectionContainer.SelectedItem = processPoint as ProcessPoint;

            int currentIndex = sortableCollectionContainer.ItemCollection.IndexOf(sortableCollectionContainer.SelectedItem);
            if(currentIndex < 1)
                return;

            SortUpDownService.ItemUp(sortableCollectionContainer);
        }

        #endregion

        #region Copy processpoint command

        private DelegateCommand _copyProcessPointCommand;
        public DelegateCommand CopyProcessPointCommand
        {
            get
            {
                return _copyProcessPointCommand = new DelegateCommand(param => ExecuteCopyProcessPoint(param));
            }
        }

        private void ExecuteCopyProcessPoint(object parameter)
        {
            CopiedProcessPoint = (parameter as ProcessPoint);
        }

        #endregion

        #region Paste processpoint command

        private DelegateCommand _pasteProcessPointCommand;
        public DelegateCommand PasteProcessPointCommand
        {
            get
            {
                return _pasteProcessPointCommand = new DelegateCommand(param => ExecutePasteProcessPoint(param));
            }
        }

        private void ExecutePasteProcessPoint(object obj)
        {
            if(CopiedProcessPoint == null)
                return;

            if(SelectedProcessGroup == null)
                return;

            if(SelectedProcessGroup.ItemCollection == null)
                return;

            if(SelectedProcessGroup.ItemCollection.Count < 1)
                return;

            string newProcessPointId = ServiceLocator.Default.GetService<ISetItemInformation>().SetNewID(_getterService.GetIDList(SelectedProcessGroup.ItemCollection));
            SelectedProcessGroup.ClonedItem = CopiedProcessPoint;

            if(newProcessPointId == null)
                return;

            DeepCloneService.DeepClone(SelectedProcessGroup, newProcessPointId);
        }

        #endregion

        #region Add processpoint command

        public DelegateCommand AddProcessPointCommand { get; }

        private bool CanExecuteAddProcessPoint(object obj)
        {
            return !(SelectedProcessGroup == null);
        }

        private async void ExecuteAddProcessPoint(object obj)
        {
            if(SelectedProcessGroup == null)
                return;

            if(SelectedProcessGroup.ItemCollection == null)
                return;

            ISortableItem newProcessPoint = ServiceLocator.Default.GetService<ICreateNewItem>().SetNewProcessPoint(_getterService.GetIDList(SelectedProcessGroup.ItemCollection), SelectedProcessGroup.ItemCollection.Count, _pointTypeNameList);

            if(newProcessPoint == null)
                return;

            AddItemService addItemService = new AddItemService();
            addItemService.AddService(SelectedProcessGroup.ItemCollection, (ProcessPoint)newProcessPoint);

            using(UnitOfWork uow = new UnitOfWork())
            {
                //uow.ProcessPointRepo.UpdateRange(SelectedProcessGroup.ItemCollection.ToList());

                uow.ProcessGroupRepo.Update(SelectedProcessGroup);
                await uow.SaveChangesAsync();
            }
        }

        #endregion

        #region delete process point Command

        public DelegateCommand DeleteProcessPointCommand { get; private set; }

        private bool CanExecuteDeleteProcessPoint(object obj)
        {
            return !(SelectedProcessPoint == null);
        }

        private async void ExecuteDeleteProcessPoint(object obj)
        {
            MessageBoxResult result = MessageBox.Show("Are you sure you want to delete the process point?", "Delete Processpoint", MessageBoxButton.YesNo);
            if(result == MessageBoxResult.Yes)
            {
                ProcessPoint processPointToDelete = SelectedProcessPoint;
                //SelectedProcessGroup.ItemCollection.Remove(SelectedProcessPoint);

                //if(SelectedProcessGroup.ItemCollection.Count > 0)
                //{
                //    for(int i = 0; i < SelectedProcessGroup.ItemCollection.Count; i++)
                //    {
                //        SelectedProcessGroup.ItemCollection[i].SortingNumber = i + 1;
                //        i++;
                //    }
                //}

                List<ProcessPoint> updatedProcessPointList = SelectedProcessGroup.ItemCollection.ToList();
                using(UnitOfWork uow = new UnitOfWork())
                {
                    //// Method 0
                    //uow.ProcessPointRepo.Delete(processPointToDelete);
                    //uow.ProcessPointRepo.UpdateRange(SelectedProcessGroup.ItemCollection);


                    //// Method 1
                    //uow.ProcessGroupRepo.Update(SelectedProcessGroup); Does not delete ProcessPoint from processPointTable (nor the foreignkey reference)

                    // Method 2
                    uow.ProcessPointRepo.Delete(processPointToDelete);

                    SelectedProcessGroup.ItemCollection.Remove(processPointToDelete); // TODO AM: check all three options in doku 1) doesnt load IC 2) Loads and deletes immediately 3) what we have now
                    for(int i = 0; i < SelectedProcessGroup.ItemCollection.Count; i++)
                    {
                        SelectedProcessGroup.ItemCollection[i].SortingNumber = i + 1;
                        i++;
                    }
                    uow.ProcessPointRepo.UpdateRange(SelectedProcessGroup.ItemCollection.ToList());

                    //// Method 3
                    //uow.ProcessPointRepo.UpdateRange(updatedProcessPointList);
                    //uow.ProcessPointRepo.Delete(processPointToDelete); //TODO AM: if item doesnt exist in DB, exception will be thrown. (TryCatch?)

                    //// Method 4
                    //uow.ProcessPointRepo.Delete(processPointToDelete); //TODO AM: if item doesnt exist in DB, exception will be thrown. (TryCatch?)
                    //uow.ProcessPointRepo.UpdateRange(updatedProcessPointList);

                    await uow.SaveChangesAsync();
                }

            }
        }

        #endregion
    }
}
