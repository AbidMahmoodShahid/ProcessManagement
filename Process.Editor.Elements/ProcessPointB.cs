namespace Process.Editor.Elements
{
    public class ProcessPointB : ProcessPoint
    {
        public ProcessPointB(string processPointName, string processPointId, int sortingNumber)
        {
            ProcessPointTypeName = "ProcessPoint B";
            Id = processPointId;
            Name = processPointName;
            SortingNumber = sortingNumber;
            SimulationStatus = "Unprocessed";
        }

        public ProcessPointB()
        {
            ProcessPointTypeName = "ProcessPoint B";
        }

        #region copy

        public override IClonableItem DeepCopy(string newId, int sortingNumber)
        {
            return new ProcessPointB(Name, newId, sortingNumber);
        }

        public override ProcessPoint DeepCopyDuplicateID()
        {
            return new ProcessPointB(Name, Id, SortingNumber);
        }

        #endregion
    }
}
