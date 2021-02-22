namespace Process.Editor.Elements
{
    public class ProcessPointD : ProcessPoint
    {
        public ProcessPointD(string processPointName, string processPointId, int sortingNumber)
        {
            ProcessPointTypeName = "ProcessPoint D";
            Id = processPointId;
            Name = processPointName;
            SortingNumber = sortingNumber;
            SimulationStatus = "Unprocessed";
        }

        public ProcessPointD()
        {
            ProcessPointTypeName = "ProcessPoint D";
        }

        #region copy

        public override IClonableItem DeepCopy(string newId, int sortingNumber)
        {
            return new ProcessPointD(Name, newId, sortingNumber);
        }

        public override ProcessPoint DeepCopyDuplicateID()
        {
            return new ProcessPointD(Name, Id, SortingNumber);
        }

        #endregion

    }
}
