namespace Process.Editor.Elements
{
    public interface INewItem
    {
        string Name { get; }
        string Id { get; }
        int SortingNumber { get; }
        string ProcessPointTypeName { get; }
    }
}
