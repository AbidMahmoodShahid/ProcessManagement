using Microsoft.EntityFrameworkCore;
using Process.Editor.Elements;
using ProcessManagement.DataStorage.EF;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Process.Editor.Repo
{
    public class ProcessGroupRepo : IProcessGroupRepo
    {
        private PMDataContext _pMDataContext;

        public ProcessGroupRepo(PMDataContext pMDataContext)
        {
            _pMDataContext = pMDataContext;
        }


        public List<ProcessGroupModel> GetAll()
        {
            return _pMDataContext.ProcessGroup.Include(pP => pP.ItemCollection).ToList();
        }

        public void Attach(ProcessGroupModel processGroupModel)
        {
            _pMDataContext.ProcessGroup.Attach(processGroupModel);
        }

        public void Update(ProcessGroupModel processGroup)
        {
            _pMDataContext.Update(processGroup);
        }

        public void UpdateAll(ObservableCollection<ProcessGroupModel> processGroupList)
        {
            _pMDataContext.UpdateRange(processGroupList);
        }

        public void Delete(ProcessGroupModel processGroupModel)
        {
            _pMDataContext.ProcessGroup.Remove(processGroupModel);
        }

    }
}
