namespace Process.Editor.Elements
{
    public class ProcessPointA : ProcessPoint
    {
        public ProcessPointA(string processPointName, string processPointId, int sortingNumber)
        {
            ProcessPointTypeName = "ProcessPoint A";
            Id = processPointId;
            Name = processPointName;
            SortingNumber = sortingNumber;
            SimulationStatus = "Unprocessed";
        }

        public ProcessPointA()
        {
            ProcessPointTypeName = "ProcessPoint A";
        }

        #region copy

        public override IClonableItem DeepCopy(string newId, int sortingNumber)
        {
            return new ProcessPointA(Name, newId, sortingNumber);
        }

        public override ProcessPoint DeepCopyDuplicateID()
        {
            return new ProcessPointA(Name, Id, SortingNumber);
        }

        #endregion
    }
}
