using Process.Editor.Elements;
using System.Collections.ObjectModel;

namespace Process.Editor.Services
{
    public static class MockDataService
    {
        public static void CreateMockData(ObservableCollection<ProcessModel> itemCollection)
        {
            // create mock process data
            for(int i = 1; i < 4; i++)
            {
                string processName = "Process " + i;
                string processId = "P_" + i;
                itemCollection.Add(new ProcessModel(processName, processId, i));
            }

            // create mock processgroup data
            foreach(ProcessModel processModel in itemCollection)
            {
                for(int i = 1; i < 4; i++)
                {
                    string processGroupName = processModel.Id + " Process Group " + i;
                    string processGroupId = processModel.Id + "_PG_" + i;
                    processModel.ItemCollection.Add(new ProcessGroupModel(processGroupName, processGroupId, i));
                }
            }

            // create mock processpoint data
            foreach(ProcessModel processModel in itemCollection)
            {
                foreach(ProcessGroupModel processGroupModel in processModel.ItemCollection)
                {
                    for(int i = 1; i < 6; i++)
                    {
                        string processPointName = processGroupModel.Id + " Process Point " + i;
                        string processPointId = processGroupModel.Id + "_PP_" + i;
                        processGroupModel.ItemCollection.Add(new ProcessPointA(processPointName, processPointId, i));
                    }
                    for(int i = 6; i < 11; i++)
                    {
                        string processPointName = processGroupModel.Id + " Process Point " + i;
                        string processPointId = processGroupModel.Id + "_PP_" + i;
                        processGroupModel.ItemCollection.Add(new ProcessPointB(processPointName, processPointId, i));
                    }
                    for(int i = 11; i < 16; i++)
                    {
                        string processPointName = processGroupModel.Id + " Process Point " + i;
                        string processPointId = processGroupModel.Id + "_PP_" + i;
                        processGroupModel.ItemCollection.Add(new ProcessPointC(processPointName, processPointId, i));
                    }
                    for(int i = 16; i < 21; i++)
                    {
                        string processPointName = processGroupModel.Id + " Process Point " + i;
                        string processPointId = processGroupModel.Id + "_PP_" + i;
                        processGroupModel.ItemCollection.Add(new ProcessPointD(processPointName, processPointId, i));
                    }
                }
            }
        }
    }
}
