using System.Collections.ObjectModel;
using System.Windows;

namespace Process.Elements.Models
{
    public interface IClonableCollectionContainer<T> where T : IClonableItem
    {
        ObservableCollection<T> ItemCollection { get; set; }
        T ClonedItem { get; set; }
        T ImportedItem { get; set; }
        Visibility Visibility { get; }
    }
}
