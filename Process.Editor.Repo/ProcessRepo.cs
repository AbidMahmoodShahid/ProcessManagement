using Process.Editor.Elements;
using ProcessManagement.DataStorage.EF;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Process.Editor.Repo
{
    public class ProcessRepo : IProcessRepo
    {
        private PMDataContext _pMDataContext;

        public ProcessRepo()
        {
            _pMDataContext = new PMDataContext();
        }

        public List<ProcessModel> GetAll()
        {
            return _pMDataContext.Process.ToList();
        }

        public void AddProcess(ProcessModel processModel)
        {
            _pMDataContext.Process.Attach(processModel);
        }

        public void DeleteProcess(ProcessModel processModel)
        {
            _pMDataContext.Process.Remove(processModel);
        }

        public void Update()
        {
            _pMDataContext.SaveChanges();
        }
    }
}
