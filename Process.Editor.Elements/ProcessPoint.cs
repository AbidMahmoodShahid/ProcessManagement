using inotech.Core;
using System.ComponentModel.DataAnnotations;

namespace Process.Editor.Elements
{
    public abstract class ProcessPoint : PropertyChangedNotifier, ISortableItem, IClonableItem, INewItem
    {
        #region properties

        [Key]
        public int ProcessPointID { get; set; }

        public string ProcessPointTypeName { get; set; }

        public string Name { get; set; }

        public string Id { get; set; }

        private int _sortingNumber;
        public int SortingNumber
        {
            get { return _sortingNumber; }
            set { SetField(ref _sortingNumber, value); }
        }


        //(Kann mit Shadow Property auch gemacht werden) mit Fluent API
        // Reference Navigation Property(OneToMany)
        public ProcessGroupModel ProcessGroupModel { get; set; }
        // Foriegn Key Property
        public int ProcessGroupModelId { get; set; }

        #endregion

        #region copy

        // deep copy when user copies a processpoint directly
        public abstract IClonableItem DeepCopy(string newId, int sortingNumber);

        // deep copy when user copies a processgroup or a process
        public abstract ProcessPoint DeepCopyDuplicateID();

        #endregion
    }
}
