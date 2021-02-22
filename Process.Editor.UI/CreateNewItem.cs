using Process.Editor.Elements;
using Process.Editor.ViewModels;
using System.Collections.Generic;

namespace Process.Editor.UI
{
    public class CreateNewItem : ICreateNewItem
    {
        public ISortableItem SetNewProcess(List<string> processIdList)
        {
            NewItemViewModel newItemViewModel = new NewItemViewModel(processIdList);
            return SetNewItem(newItemViewModel);
        }

        public ISortableItem SetNewProcessGroup(List<string> processGroupIdList, int initialSortingNumber)
        {
            NewItemViewModel newItemViewModel = new NewItemViewModel(processGroupIdList, initialSortingNumber);
            return SetNewItem(newItemViewModel);
        }

        public ISortableItem SetNewProcessPoint(List<string> processPointIdList, int initialSortingNumber, List<string> processPointTypeNameList)
        {
            NewItemViewModel newItemViewModel = new NewItemViewModel(processPointIdList, initialSortingNumber, processPointTypeNameList);
            return SetNewItem(newItemViewModel);
        }

        private ISortableItem SetNewItem(NewItemViewModel newItemViewModel)
        {
            NewItemWindow newItemDialogBox = new NewItemWindow();
            newItemDialogBox.DataContext = newItemViewModel;

            newItemDialogBox.ShowDialog();

            if((bool)newItemDialogBox.DialogResult)
                return newItemViewModel.NewItem;
            else
                return null;
        }
    }
}
