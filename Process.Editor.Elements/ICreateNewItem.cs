using System.Collections.Generic;

namespace Process.Editor.Elements
{
    public interface ICreateNewItem
    {
        ISortableItem SetNewProcess(List<string> itemIdList);

        ISortableItem SetNewProcessGroup(List<string> itemIdList, int initialSortingNumber);

        ISortableItem SetNewProcessPoint(List<string> itemIdList, int initialSortingNumber, List<string> processPointTypeNameList);
    }
}
