using inotech.Core;
using Process.Editor.Elements;
using ProcessManagement.Core;
using System;
using System.Collections.Generic;
using System.Windows;

namespace Process.Editor.ViewModels
{
    public class SetItemIdAndNameViewModel : PropertyChangedNotifier
    {
        public SetItemIdAndNameViewModel(Type itemType, List<string> idList, IImportableItem itemToImport)
        {
            OkCommand = new DelegateCommand(ExecuteOk, CanExecuteOk);
            _itemIDList = idList;
            NameVisibility = Visibility.Visible;
            ItemToImport = itemToImport;
        }

        #region Properties

        private List<string> _itemIDList;

        public IImportableItem ItemToImport { get; set; }

        private string _itemName;
        public string ItemName
        {
            get { return _itemName; }
            set
            {
                SetField(ref _itemName, value);
                OkCommand.RaiseCanExecuteChanged();
            }
        }

        private string _itemID;
        public string ItemID
        {
            get { return _itemID; }
            set
            {
                SetField(ref _itemID, value);
                OkCommand.RaiseCanExecuteChanged();
            }
        }

        private Visibility _nameVisibility;
        public Visibility NameVisibility
        {
            get { return _nameVisibility; }
            set { SetField(ref _nameVisibility, value); }
        }

        #endregion

        #region Ok command

        public DelegateCommand OkCommand { get; }

        private bool CanExecuteOk(object obj)
        {
            return !(String.IsNullOrWhiteSpace(ItemName) || String.IsNullOrWhiteSpace(ItemID) || _itemIDList.Contains(ItemID));
        }

        private void ExecuteOk(object obj)
        {
            SetImportedItemInformation();

            Window window = (Window)obj;
            window.DialogResult = true;
        }

        private void SetImportedItemInformation()
        {
            ItemToImport.Id = ItemID;
            ItemToImport.Name = ItemName;
        }

        #endregion
    }
}
