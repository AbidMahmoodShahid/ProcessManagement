using Process.Editor.Elements;
using ProcessManagement.DataStorage.EF;

namespace Process.Editor.Repo
{
    public class Process
    {
        PMDataContext _pMDataContext = new PMDataContext();

        public Process()
        {
            PMDataContext pMDataContext = new PMDataContext();
        }

        public void LoadProcess()
        {

        }

        public void AddProcess(ProcessModel processModel)
        {
            _pMDataContext.Process.Add(processModel);
        }

        public void RemoveProcess(ProcessModel processModel)
        {
            _pMDataContext.Process.Remove(processModel);
        }
    }
}
