using Process.Editor.Elements;
using System;
using System.Collections.ObjectModel;

namespace Process.Editor.Services
{
    public class AddItemService
    {
        public void AddService<T>(ObservableCollection<T> itemCollection, T newItem) where T : ISortableItem
        {
            if(itemCollection == null)
                throw new ArgumentNullException("itemCollection is null!");

            if(newItem == null)
                throw new ArgumentNullException("newItem is null!");

            if(newItem.SortingNumber > 0 && newItem.SortingNumber <= itemCollection.Count)
            {
                itemCollection.Insert(newItem.SortingNumber - 1, newItem);
                ResetAllSortingNumbers(itemCollection);
            }
            else
            {
                ResetAllSortingNumbers(itemCollection);
                newItem.SortingNumber = itemCollection.Count + 1;
                itemCollection.Add(newItem);
            }
        }

        private void ResetAllSortingNumbers<T>(ObservableCollection<T> itemCollection) where T : ISortableItem
        {
            for(int i = 0; i < itemCollection.Count; i++)
            {
                itemCollection[i].SortingNumber = i + 1;
            }
        }

    }
}
