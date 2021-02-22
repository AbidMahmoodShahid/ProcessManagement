using System.Collections.ObjectModel;

namespace Process.Elements.Models
{
    public interface ISortableCollectionContainer<T> where T : ISortableItem
    {
        ObservableCollection<T> ItemCollection { get; set; }
        T SelectedItem { get; set; }
    }
}
