namespace Process.Editor.Elements
{
    public class ProcessPointC : ProcessPoint
    {
        public ProcessPointC(string processPointName, string processPointId, int sortingNumber)
        {
            ProcessPointTypeName = "ProcessPoint C";
            Id = processPointId;
            Name = processPointName;
            SortingNumber = sortingNumber;
            SimulationStatus = "Unprocessed";
        }

        public ProcessPointC()
        {
            ProcessPointTypeName = "ProcessPoint C";
        }

        #region copy
        public override IClonableItem DeepCopy(string newId, int sortingNumber)
        {
            return new ProcessPointC(Name, newId, sortingNumber);
        }

        public override ProcessPoint DeepCopyDuplicateID()
        {
            return new ProcessPointC(Name, Id, SortingNumber);
        }

        #endregion
    }
}
