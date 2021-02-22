using System.Collections.Generic;

namespace Process.Editor.Elements
{
    public interface ISetItemInformation
    {
        string SetNewID(List<string> usedIdList);

        Dictionary<string, string> SetNewNameAndID(List<string> usedIdList);
    }
}
