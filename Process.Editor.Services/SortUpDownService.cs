using Process.Editor.Elements;
using System;

namespace Process.Editor.Services
{
    public static class SortUpDownService
    {
        public static void ItemDown<T>(ISortableCollectionContainer<T> sortableCollectionContainer) where T : ISortableItem
        {
            if(sortableCollectionContainer == null)
                throw new ArgumentNullException("sortableCollectionContainer is null. ItemDown can not be implemented!");

            if(sortableCollectionContainer.SelectedItem == null)
                throw new ArgumentNullException("SelectedItem is null. ItemDown can not be implemented!");

            if(sortableCollectionContainer.ItemCollection == null)
                throw new ArgumentNullException("ItemCollection is null. ItemDown can not be implemented!");

            if(sortableCollectionContainer.ItemCollection.Count < 2)
                throw new ArgumentException("Less than 1 item in ItemCollection. ItemDown can not be implemented!");

            int currentIndex = sortableCollectionContainer.ItemCollection.IndexOf(sortableCollectionContainer.SelectedItem);
            if(currentIndex >= sortableCollectionContainer.ItemCollection.Count - 1)
                throw new ArgumentException("Already the last item in ItemCollection. ItemDown can not be implemented!");

            // remove item
            sortableCollectionContainer.ItemCollection.Remove(sortableCollectionContainer.SelectedItem);
            // insert in correct position
            sortableCollectionContainer.ItemCollection.Insert(currentIndex + 1, sortableCollectionContainer.SelectedItem);
            // edit sortnumber of all items
            sortableCollectionContainer.ItemCollection[currentIndex].SortingNumber--;
            sortableCollectionContainer.ItemCollection[currentIndex + 1].SortingNumber++;
        }

        public static void ItemUp<T>(ISortableCollectionContainer<T> sortableCollectionContainer) where T : ISortableItem
        {
            if(sortableCollectionContainer == null)
                throw new ArgumentNullException("sortableCollectionContainer is null. ItemUp can not be implemented!");

            if(sortableCollectionContainer.SelectedItem == null)
                throw new ArgumentNullException("SelectedItem is null. ItemUp can not be implemented!");

            if(sortableCollectionContainer.ItemCollection == null)
                throw new ArgumentNullException("ItemCollection is null. ItemUp can not be implemented!");

            if(sortableCollectionContainer.ItemCollection.Count < 2)
                throw new ArgumentException("Less than 1 item in ItemCollection. ItemUp can not be implemented!");

            int currentIndex = sortableCollectionContainer.ItemCollection.IndexOf(sortableCollectionContainer.SelectedItem);
            if(currentIndex < 1)
                throw new ArgumentException("Already the first item in ItemCollection. ItemUp can not be implemented!");

            // remove item
            sortableCollectionContainer.ItemCollection.Remove(sortableCollectionContainer.SelectedItem);
            // insert in correct position
            sortableCollectionContainer.ItemCollection.Insert(currentIndex - 1, sortableCollectionContainer.SelectedItem);
            // edit sortnumber of all items
            sortableCollectionContainer.ItemCollection[currentIndex].SortingNumber++;
            sortableCollectionContainer.ItemCollection[currentIndex - 1].SortingNumber--;
        }
    }
}
