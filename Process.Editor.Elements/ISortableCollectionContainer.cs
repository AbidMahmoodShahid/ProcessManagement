using System.Collections.ObjectModel;

namespace Process.Editor.Elements
{
    public interface ISortableCollectionContainer<T> where T : ISortableItem
    {
        ObservableCollection<T> ItemCollection { get; set; }
        T SelectedItem { get; set; }
    }
}
