using System.Collections.ObjectModel;
using System.Windows;

namespace Process.Editor.Elements
{
    public interface IClonableCollectionContainer<T> where T : IClonableItem
    {
        ObservableCollection<T> ItemCollection { get; set; }
        T ClonedItem { get; set; }
        Visibility SortingNumberVisibility { get; }
    }
}
