using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Xml.Serialization;

namespace Process.Elements.Models
{
    public class ProcessModel: ISortableCollectionContainer<ProcessGroupModel>, IClonableItem, IClonableCollectionContainer<ProcessGroupModel>
    {
        public ProcessModel(string processname, string processId, int sortingNumber)
        {
            Name = processname;
            Id = processId;
            ItemCollection = new ObservableCollection<ProcessGroupModel>();
            Visibility = Visibility.Visible;
        }
        public ProcessModel()
        {
            ItemCollection = new ObservableCollection<ProcessGroupModel>();
        }

        public string Name { get; set; }

        public string Id { get; set; }

        public int SortingNumber { get; set; }

        public ObservableCollection<ProcessGroupModel> ItemCollection { get; set; }

        [XmlIgnore]
        public ProcessGroupModel SelectedItem { get; set; }

        [XmlIgnore]
        public ProcessGroupModel ClonedItem { get; set; }

        [XmlIgnore]
        public ProcessGroupModel ImportedItem { get; set; }

        [XmlIgnore]
        public Visibility Visibility { get; private set; }

        #region deep copy

        public IClonableItem DeepCopy(string newId, int unusedSortingNumber)
        {
            ProcessModel newProcessModel = new ProcessModel();
            newProcessModel.Id = newId;
            newProcessModel.Name = Name;
            newProcessModel.ItemCollection = CopyProcessGroupCollection();

            return newProcessModel;
        }

        #endregion

        #region private methods

        private ObservableCollection<ProcessGroupModel> CopyProcessGroupCollection()
        {
            if(ItemCollection==null || ItemCollection.Count < 1)
                return new ObservableCollection<ProcessGroupModel>();

            ObservableCollection<ProcessGroupModel> newProcessGroupCollection = new ObservableCollection<ProcessGroupModel>();
            foreach (ProcessGroupModel processGroupModelToCopy in ItemCollection)
            {
                ProcessGroupModel newProcessGroupModel = processGroupModelToCopy.DeepCopyDuplicateID();
                newProcessGroupCollection.Add(newProcessGroupModel);
            }
            return newProcessGroupCollection;
        }

        #endregion
    }
}
