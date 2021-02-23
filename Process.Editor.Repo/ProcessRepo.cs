using Process.Editor.Elements;
using ProcessManagement.DataStorage.EF;

namespace Process.Editor.Repo
{
    public class ProcessRepo
    {
        private PMDataContext _pMDataContext;

        public ProcessRepo()
        {
            _pMDataContext = new PMDataContext();
        }

        public void LoadProcess()
        {

        }

        public void AddProcess(ProcessModel processModel)
        {
            _pMDataContext.Process.Add(processModel);
            _pMDataContext.SaveChanges();
        }

        public void UpdateProcess()
        {
            _pMDataContext.SaveChanges();
        }

        public void RemoveProcess(ProcessModel processModel)
        {
            _pMDataContext.Process.Remove(processModel);
            _pMDataContext.SaveChanges();
        }
    }
}
