using inotech.Core;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Windows;
using System.Xml.Serialization;

namespace Process.Editor.Elements
{
    public class ProcessGroupModel : PropertyChangedNotifier, ISortableCollectionContainer<ProcessPoint>, ISortableItem, INewItem, IClonableItem, IClonableCollectionContainer<ProcessPoint>
    {
        //(Kann mit Shadow Property auch gemacht werden) mit Fluent API
        [Key]
        public int ProcessGroupId { get; set; }

        public string Name { get; set; }

        public string Id { get; set; }

        private int _sortingNumber;
        public int SortingNumber
        {
            get { return _sortingNumber; }
            set { SetField(ref _sortingNumber, value); }
        }


        //Collection Navigation Property(OneToMany)
        public ObservableCollection<ProcessPoint> ItemCollection { get; set; }
        //public ICollection<ProcessPoint> ProcessPoints { get; set; }







        //(Kann mit Shadow Property auch gemacht werden) mit Fluent API
        // Reference Navigation Property(OneToMany)
        public ProcessModel ProcessModel { get; set; }
        // Foriegn Key Property
        public int ProcessModelId { get; set; }


        public ProcessGroupModel(string processGroupName, string processGroupId, int processGroupSortingNumber)
        {
            Name = processGroupName;
            Id = processGroupId;
            SortingNumber = processGroupSortingNumber;
            ItemCollection = new ObservableCollection<ProcessPoint>();
            SortingNumberVisibility = Visibility.Visible;
        }
        public ProcessGroupModel()
        {
            ItemCollection = new ObservableCollection<ProcessPoint>();
        }

        [NotMapped]
        [XmlIgnore]
        public ProcessPoint SelectedItem { get; set; }

        [NotMapped]
        [XmlIgnore]
        public ProcessPoint ClonedItem { get; set; }

        [NotMapped]
        [XmlIgnore]
        public ProcessPoint ImportedItem { get; set; }

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

        #region deep copies

        // deep copy when user copies a processgroup directly
        public IClonableItem DeepCopy(string newId, int newSortingNumber)
        {
            ProcessGroupModel copiedProcessGroupModel = new ProcessGroupModel(Name, newId, newSortingNumber);
            copiedProcessGroupModel.ItemCollection = CopyProcessPointCollection();
            return copiedProcessGroupModel;
        }

        // deep copy when user copies a process
        public ProcessGroupModel DeepCopyDuplicateID()
        {
            ProcessGroupModel copiedProcessGroupModel = new ProcessGroupModel(Name, Id, SortingNumber);
            copiedProcessGroupModel.ItemCollection = CopyProcessPointCollection();

            return copiedProcessGroupModel;
        }

        #endregion

        #region private methods

        private ObservableCollection<ProcessPoint> CopyProcessPointCollection()
        {
            if(ItemCollection == null || ItemCollection.Count == 0)
            {
                return new ObservableCollection<ProcessPoint>();
            }

            ObservableCollection<ProcessPoint> newProcessPointCollection = new ObservableCollection<ProcessPoint>();
            foreach(ProcessPoint processPointModelToCopy in ItemCollection)
            {
                ProcessPoint newProcessPointModel = processPointModelToCopy.DeepCopyDuplicateID();
                newProcessPointCollection.Add(newProcessPointModel);
            }
            return newProcessPointCollection;
        }

        #endregion
    }
}
