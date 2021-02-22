using inotech.Core;
using Process.Editor.Elements;
using ProcessManagement.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows;

namespace Process.Editor.ViewModels
{
    public class NewItemViewModel : PropertyChangedNotifier
    {
        #region Properties

        public ISortableItem NewItem { get; set; }
        private string _newItemType;

        private List<string> _itemIDList;


        private Visibility _sortingVisibility;
        public Visibility SortingVisibility
        {
            get { return _sortingVisibility; }
            set { SetField(ref _sortingVisibility, value); }
        }

        private Visibility _itemTypeVisibility;
        public Visibility ItemTypeVisibility
        {
            get { return _itemTypeVisibility; }
            set { SetField(ref _itemTypeVisibility, value); }
        }

        private string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                SetField(ref _name, value);
                OkCommand.RaiseCanExecuteChanged();
            }
        }

        private string _id;
        public string Id
        {
            get { return _id; }
            set
            {
                SetField(ref _id, value);
                OkCommand.RaiseCanExecuteChanged();
            }
        }

        private int _sortingNumber;
        public int SortingNumber
        {
            get { return _sortingNumber; }
            set
            {
                SetField(ref _sortingNumber, value);
                OkCommand.RaiseCanExecuteChanged();
            }
        }

        private string _processPointTypeName;
        public string ProcessPointTypeName
        {
            get { return _processPointTypeName; }
            set
            {
                SetField(ref _processPointTypeName, value);
                OkCommand.RaiseCanExecuteChanged();
            }
        }

        private List<string> _processPointTypeNameList;
        public List<string> ProcessPointTypeNameList
        {
            get { return _processPointTypeNameList; }
            set { SetField(ref _processPointTypeNameList, value); }
        }

        #endregion


        #region Constructors

        public NewItemViewModel(List<string> processIdList)
        {
            OkCommand = new DelegateCommand(ExecuteOk, CanExecuteOk);

            _itemIDList = processIdList;
            SortingVisibility = Visibility.Collapsed;
            ItemTypeVisibility = Visibility.Collapsed;

            _newItemType = "Process";
        }

        public NewItemViewModel(List<string> processGroupIdList, int initialSortingNumber)
        {
            OkCommand = new DelegateCommand(ExecuteOk, CanExecuteOk);

            _itemIDList = processGroupIdList;
            SortingVisibility = Visibility.Visible;
            ItemTypeVisibility = Visibility.Collapsed;

            if(initialSortingNumber <= 0)
                SortingNumber = 1;
            else
                SortingNumber = initialSortingNumber + 1;

            _newItemType = "ProcessGroup";
        }

        public NewItemViewModel(List<string> processPointIdList, int initialSortingNumber, List<string> processPointTypeNameList)
        {
            OkCommand = new DelegateCommand(ExecuteOk, CanExecuteOk);

            _itemIDList = processPointIdList;
            SortingVisibility = Visibility.Visible;
            ItemTypeVisibility = Visibility.Visible;

            if(initialSortingNumber <= 0)
                SortingNumber = 1;
            else
                SortingNumber = initialSortingNumber + 1;

            ProcessPointTypeNameList = processPointTypeNameList;
            _newItemType = "ProcessPoint";

        }

        #endregion


        #region Ok command

        public DelegateCommand OkCommand { get; }

        private bool CanExecuteOk(object obj)
        {
            if(_newItemType == "ProcessPoint")
            {
                if(_itemIDList == null)
                    return !(String.IsNullOrWhiteSpace(Name) || String.IsNullOrWhiteSpace(Id) || ProcessPointTypeName == null);
                else
                    return !(String.IsNullOrWhiteSpace(Name) || String.IsNullOrWhiteSpace(Id) || ProcessPointTypeName == null || _itemIDList.Contains(Id));
            }
            else
            {
                if(_itemIDList == null)
                    return !(String.IsNullOrWhiteSpace(Name) || String.IsNullOrWhiteSpace(Id));
                else
                    return !(String.IsNullOrWhiteSpace(Name) || String.IsNullOrWhiteSpace(Id) || _itemIDList.Contains(Id));
            }
        }

        private void ExecuteOk(object obj)
        {
            switch(_newItemType)
            {
                case "Process":
                    SetNewProcess();
                    break;
                case "ProcessGroup":
                    SetNewProcessGroup();
                    break;
                case "ProcessPoint":
                    SetNewProcessPoint();
                    break;

            }

            Window window = (Window)obj;
            window.DialogResult = true;
        }

        #endregion

        #region private methods

        private void SetNewProcess()
        {
            ProcessModel newProcess = new ProcessModel(Name, Id, SortingNumber);
            NewItem = newProcess;
        }

        private void SetNewProcessGroup()
        {
            ProcessGroupModel newProcessGroup = new ProcessGroupModel(Name, Id, SortingNumber);
            NewItem = newProcessGroup;
        }

        private void SetNewProcessPoint()
        {
            Type[] processPointTypes = GetInheritedClassesArray(typeof(ProcessPoint));
            Type processPointType = GetTypeFromTypeName(ProcessPointTypeName, processPointTypes);
            //TODO AM: see if you can get Type from string of Type directly
            ProcessPoint processPoint = (ProcessPoint)Activator.CreateInstance(processPointType);
            processPoint.Id = Id;
            processPoint.Name = Name;
            processPoint.SortingNumber = SortingNumber;
            NewItem = processPoint;
        }

        private Type[] GetInheritedClassesArray(Type baseType)
        {
            return Assembly.GetAssembly(baseType).GetTypes().Where(theType => theType.IsClass && !theType.IsAbstract && theType.IsSubclassOf(baseType)).ToArray();
        }

        private Type GetTypeFromTypeName(string typeName, Type[] typeArray)
        {
            foreach(Type type in typeArray)
            {
                if(type.Name == typeName)
                {
                    return type;
                }
            }
            return null;
        }

        #endregion
    }
}
