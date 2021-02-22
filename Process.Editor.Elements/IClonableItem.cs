namespace Process.Editor.Elements
{
    public interface IClonableItem
    {
        string Name { get; set; }
        string Id { get; set; }
        int SortingNumber { get; set; }
        IClonableItem DeepCopy(string newId, int newSortingNumber);
    }
}
