using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Process.Elements.Models
{
    public class ProcessPointB : ProcessPoint
    {
        public ProcessPointB(string processPointId, string processPointName, int sortingNumber)
        {
            ProcessPointType = "ProcessPoint B";
            Id = processPointId;
            Name = processPointName;
            SortingNumber = sortingNumber;
            SimulationStatus = "Unprocessed";
        }

        public ProcessPointB()
        {
            ProcessPointType = "ProcessPoint B";
        }

        #region copy

        public override IClonableItem DeepCopy(string newId, int sortingNumber)
        {
            return new ProcessPointB(newId, Name, sortingNumber);
        }

        public override ProcessPoint DeepCopyDuplicateID()
        {
            return new ProcessPointB(Id, Name, SortingNumber);
        }

        #endregion
    }
}
