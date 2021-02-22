using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Process.Elements.Models
{
    public class ProcessPointD : ProcessPoint
    {
        public ProcessPointD(string processPointId, string processPointName, int sortingNumber)
        {
            ProcessPointType = "ProcessPoint D";
            Id = processPointId;
            Name = processPointName;
            SortingNumber = sortingNumber;
            SimulationStatus = "Unprocessed";
        }

        public ProcessPointD()
        {
            ProcessPointType = "ProcessPoint D";
        }

        #region copy

        public override IClonableItem DeepCopy(string newId, int sortingNumber)
        {
            return new ProcessPointD(newId, Name, sortingNumber);
        }

        public override ProcessPoint DeepCopyDuplicateID()
        {
            return new ProcessPointD(Id, Name, SortingNumber);
        }

        #endregion

    }
}
