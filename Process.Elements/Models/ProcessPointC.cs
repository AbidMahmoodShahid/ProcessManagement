using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Process.Elements.Models
{
    public class ProcessPointC : ProcessPoint
    {
        public ProcessPointC(string processPointId, string processPointName, int sortingNumber)
        {
            ProcessPointType = "ProcessPoint C";
            Id = processPointId;
            Name = processPointName;
            SortingNumber = sortingNumber;
            SimulationStatus = "Unprocessed";
        }

        public ProcessPointC()
        {
            ProcessPointType = "ProcessPoint C";
        }

        #region copy
        public override IClonableItem DeepCopy(string newId, int sortingNumber)
        {
            return new ProcessPointC(newId, Name, sortingNumber);
        }

        public override ProcessPoint DeepCopyDuplicateID()
        {
            return new ProcessPointC(Id, Name, SortingNumber);
        }

        #endregion
    }
}
