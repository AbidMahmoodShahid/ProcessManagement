using Microsoft.EntityFrameworkCore;
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

        public ProcessRepo(PMDataContext pMDataContext)
        {
            _pMDataContext = pMDataContext;
        }

        public List<ProcessModel> GetAll()
        {
            //return _pMDataContext.Process.Include(pM => pM.ItemCollection).ThenInclude(pG => pG.ItemCollection).ToList(); //TODO AM: ThenInclude not working
            var x = _pMDataContext.Process.Include(pM => pM.ItemCollection).ThenInclude(pG => pG.ItemCollection).ToList();
            return x;
        }

        public void AttachProcess(ProcessModel processModel)
        {
            _pMDataContext.Process.Attach(processModel); //TODO AM: see difference between add and attach
        }

        public void DeleteProcess(ProcessModel processModel)
        {
            _pMDataContext.Process.Remove(processModel);
        }
    }
}
