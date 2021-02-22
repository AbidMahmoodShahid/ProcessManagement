using inotech.Core;
using ProcessManagement.Core;
using System.Collections.Generic;
using System.Windows;

namespace Process.Editor.ViewModels
{
    public class SetItemIdViewModel : PropertyChangedNotifier
    {
        public SetItemIdViewModel(List<string> idList, Visibility nameVisibility)
        {
            OkCommand = new DelegateCommand(ExecuteOk, CanExecuteOk);
            _itemIDList = idList;
            NameVisibility = nameVisibility;
        }


        private List<string> _itemIDList;

        private string _itemName;
        public string ItemName
        {
            get { return _itemName; }
            set
            {
                SetField(ref _itemName, value);
                HandleOkButtonIsEnabled();
            }
        }

        private string _itemID;
        public string ItemID
        {
            get { return _itemID; }
            set
            {
                SetField(ref _itemID, value);
                HandleOkButtonIsEnabled();
            }
        }

        private string _itemIDInfo;
        public string ItemIDInfo
        {
            get { return _itemIDInfo; }
            set
            {
                SetField(ref _itemIDInfo, value);
            }
        }

        private Visibility _nameVisibility;
        public Visibility NameVisibility
        {
            get { return _nameVisibility; }
            set { SetField(ref _nameVisibility, value); }
        }

        #region  OK Command

        public DelegateCommand OkCommand { get; }

        private bool CanExecuteOk(object obj)
        {
            if(System.String.IsNullOrWhiteSpace(ItemID))
            {
                ItemIDInfo = "Please Select a new Id.";
                return false;
            }
            if(!(System.String.IsNullOrWhiteSpace(ItemID) || _itemIDList.Contains(ItemID)))
            {
                ItemIDInfo = "This Id can be used.";
                return true;
            }
            else
            {
                ItemIDInfo = "This Id has already been used. Please Select a new Id.";
                return false;
            }
        }

        private void ExecuteOk(object obj)
        {
            HandleOk(obj);
        }

        // set ok button to enabled when all necessary information filled
        private void HandleOkButtonIsEnabled()
        {
            OkCommand.RaiseCanExecuteChanged();
        }
        // send result as true on Ok Pressed
        private void HandleOk(object obj)
        {
            Window window = (Window)obj;
            window.DialogResult = true;
        }

        #endregion
    }
}

