using Process.Editor.Elements;
using System;

namespace Process.Editor.Services
{
    public static class DeepCloneService
    {
        public static void DeepClone<T>(IClonableCollectionContainer<T> clonableCollectionContainer, string newId) where T : IClonableItem
        {
            if(clonableCollectionContainer == null)
                throw new ArgumentNullException("clonableCollectionContainer is null!");

            if(clonableCollectionContainer.ItemCollection == null)
                throw new ArgumentNullException("ItemCollection is null!");

            if(clonableCollectionContainer.ItemCollection.Count < 1)
                throw new ArgumentException("ItemCollection is empty. No Item to Clone.");

            if(clonableCollectionContainer.ClonedItem == null)
                throw new ArgumentNullException("ClonedItem is null!");

            if(newId == null)
                throw new ArgumentNullException("newId is null!");

            int sortingNumber = clonableCollectionContainer.ItemCollection.Count + 1;

            IClonableItem clonableItem = clonableCollectionContainer.ClonedItem.DeepCopy(newId, sortingNumber);
            clonableCollectionContainer.ItemCollection.Add((T)clonableItem);
        }

    }
}
