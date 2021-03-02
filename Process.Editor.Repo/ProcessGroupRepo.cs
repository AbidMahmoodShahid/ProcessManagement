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

        public async Task Attach(ProcessGroupModel processGroupModel)
        {
            _pMDataContext.ProcessGroup.Attach(processGroupModel);
        }

        public async Task AttachRange(ObservableCollection<ProcessGroupModel> processGroupList)
        {
            _pMDataContext.AttachRange(processGroupList);
        }

        public async Task Update(ProcessGroupModel processGroupModel)
        {
            _pMDataContext.ProcessGroup.Update(processGroupModel);
        }

        public async Task UpdateRange(ObservableCollection<ProcessGroupModel> processGroupList)
        {
            _pMDataContext.UpdateRange(processGroupList);
        }

        public async Task Delete(ProcessGroupModel processGroupModel)
        {
            _pMDataContext.ProcessGroup.Remove(processGroupModel);
        }

    }
}
