using Process.Elements.Common;
using System.Collections.ObjectModel;
using System.Windows;
using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Process.Elements.Models
{
    public class ProcessGroupModel : PropertyChangedNotifier, ISortableCollectionContainer<ProcessPoint>, ISortableItem, IClonableItem, IClonableCollectionContainer<ProcessPoint>
    {
        public ProcessGroupModel(string processGroupName, string processGroupId, int processGroupSortingNumber)
        {
            Name = processGroupName;
            Id = processGroupId;
            SortingNumber = processGroupSortingNumber;
            ItemCollection = new ObservableCollection<ProcessPoint>();
            Visibility = Visibility.Visible;
        }
        public ProcessGroupModel()
        {
            ItemCollection = new ObservableCollection<ProcessPoint>();
        }

        public string Name { get; set ; }
        public string Id { get; set; }

        private int _sortingNumber;
        public int SortingNumber
        {
            get { return _sortingNumber; }
            set { SetField(ref _sortingNumber, value); }
        }

        public ObservableCollection<ProcessPoint> ItemCollection { get; set; }

        [XmlIgnore]
        public ProcessPoint SelectedItem { get; set; }

        [XmlIgnore]
        public ProcessPoint ClonedItem { get; set; }

        [XmlIgnore]
        public ProcessPoint ImportedItem { get; set; }

        [XmlIgnore]
        public Visibility Visibility { get; private set; }

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
            if (ItemCollection == null || ItemCollection.Count == 0)
            {
                return new ObservableCollection<ProcessPoint>();
            }

            ObservableCollection<ProcessPoint> newProcessPointCollection = new ObservableCollection<ProcessPoint>();
            foreach (ProcessPoint processPointModelToCopy in ItemCollection)
            {
                ProcessPoint newProcessPointModel = processPointModelToCopy.DeepCopyDuplicateID();
                newProcessPointCollection.Add(newProcessPointModel);
            }
            return newProcessPointCollection;
        }

        #endregion
    }
}
