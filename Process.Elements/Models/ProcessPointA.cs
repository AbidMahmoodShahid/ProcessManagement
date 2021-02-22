using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Process.Elements.Models
{
    public class ProcessPointA : ProcessPoint
    {
        public ProcessPointA(string processPointId, string processPointName, int sortingNumber)
        {
            ProcessPointType = "ProcessPoint A";
            Id = processPointId;
            Name = processPointName;
            SortingNumber = sortingNumber;
            SimulationStatus = "Unprocessed";
        }

        public ProcessPointA()
        {
            ProcessPointType = "ProcessPoint A";
        }

        #region copy

        public override IClonableItem DeepCopy(string newId, int sortingNumber)
        {
            return new ProcessPointA(newId, Name, sortingNumber);
        }

        public override ProcessPoint DeepCopyDuplicateID()
        {
            return new ProcessPointA(Id, Name, SortingNumber);
        }

        #endregion
    }
}
