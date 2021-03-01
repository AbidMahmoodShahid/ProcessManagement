using Microsoft.EntityFrameworkCore;
using Process.Editor.Elements;
using ProcessManagement.DataStorage.EF;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Process.Editor.Repo
{
    public class ProcessGroupRepo : IProcessGroupRepo
    {
        private PMDataContext _pMDataContext;

        public ProcessGroupRepo(PMDataContext pMDataContext)
        {
            _pMDataContext = pMDataContext;
        }


        public async Task<List<ProcessGroupModel>> GetAll()
        {
            return _pMDataContext.ProcessGroup.Include(pP => pP.ItemCollection).ToList();
        }

        public void Add(ProcessGroupModel processGroupModel)
        {
            _pMDataContext.ProcessGroup.Attach(processGroupModel);
        }

        public void AddRange(ObservableCollection<ProcessGroupModel> processGroupList)
        {
            _pMDataContext.AttachRange(processGroupList);
        }

        public void Update(ProcessGroupModel processGroupModel)
        {
            _pMDataContext.ProcessGroup.Update(processGroupModel);
        }

        public void UpdateRange(ObservableCollection<ProcessGroupModel> processGroupList)
        {
            _pMDataContext.UpdateRange(processGroupList);
        }

        public void Delete(ProcessGroupModel processGroupModel)
        {
            _pMDataContext.ProcessGroup.Remove(processGroupModel);
        }

    }
}
