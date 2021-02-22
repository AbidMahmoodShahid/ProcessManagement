using Process.Elements.Common;

namespace Process.Elements.Models
{
    public abstract class ProcessPoint : PropertyChangedNotifier, ISortableItem, IClonableItem
    {
        #region properties

        public string ProcessPointType { get; set; }

        public string Name { get; set; }
        
        public string Id { get; set; }

        private int _sortingNumber;
        public int SortingNumber
        {
            get { return _sortingNumber; }
            set { SetField(ref _sortingNumber, value); }
        }

        public string SimulationStatus { get; set; }

        #endregion
              
        #region copy

        // deep copy when user copies a processpoint directly
        public abstract IClonableItem DeepCopy(string newId, int sortingNumber);

        // deep copy when user copies a processgroup or a process
        public abstract ProcessPoint DeepCopyDuplicateID();

        #endregion
    }
}
