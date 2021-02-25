using System;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Windows;
using System.Xml.Serialization;

namespace Process.Editor.Elements
{
    public class ProcessModel : ISortableCollectionContainer<ProcessGroupModel>, IClonableItem, IClonableCollectionContainer<ProcessGroupModel>, IImportableItem, IExportableItem, ISortableItem, INewItem
    {
        //(Kann mit Shadow Property auch gemacht werden) mit Fluent API
        [Key]
        public int ProcessModelId { get; set; }

        public string Name { get; set; }

        public string Id { get; set; }


        //Collection Navigation Property(OneToMany)
        public ObservableCollection<ProcessGroupModel> ItemCollection { get; set; }

        public ProcessModel(string processname, string processId, int sortingNumber)
        {
            Name = processname;
            Id = processId;
            ItemCollection = new ObservableCollection<ProcessGroupModel>();
            SortingNumberVisibility = Visibility.Visible;
        }
        public ProcessModel()
        {
            ItemCollection = new ObservableCollection<ProcessGroupModel>();
            ItemCollection.CollectionChanged += ItemCollection_CollectionChanged;
        }

        private void ItemCollection_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            string a = "";
        }

        [NotMapped]
        public int SortingNumber { get; set; }

        [NotMapped]
        [XmlIgnore]
        public ProcessGroupModel SelectedItem { get; set; }

        [NotMapped]
        [XmlIgnore]
        public ProcessGroupModel ClonedItem { get; set; }

        [NotMapped]
        [XmlIgnore]
        public ProcessGroupModel ImportedItem { get; set; }

        [NotMapped]
        [XmlIgnore]
        public Visibility SortingNumberVisibility { get; private set; }

        public string ProcessPointTypeName
        {
            get
            {
                throw new NotImplementedException();
            }
        }

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
            if(ItemCollection == null || ItemCollection.Count < 1)
                return new ObservableCollection<ProcessGroupModel>();

            ObservableCollection<ProcessGroupModel> newProcessGroupCollection = new ObservableCollection<ProcessGroupModel>();
            foreach(ProcessGroupModel processGroupModelToCopy in ItemCollection)
            {
                ProcessGroupModel newProcessGroupModel = processGroupModelToCopy.DeepCopyDuplicateID();
                newProcessGroupCollection.Add(newProcessGroupModel);
            }
            return newProcessGroupCollection;
        }

        #endregion
    }
}
