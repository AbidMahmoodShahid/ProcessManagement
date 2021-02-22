using System.Collections.ObjectModel;

namespace Process.Editor.Elements
{
    public interface IImportableCollectionContainer<T> where T : IImportableItem
    {
        ObservableCollection<T> ItemCollection { get; set; }
        T ImportedItem { get; set; }
    }
}
