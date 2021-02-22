namespace Process.Editor.Elements
{
    public interface IExportableItem
    {
        string Name { get; set; }
        string Id { get; set; }
        int SortingNumber { get; set; }
    }
}
