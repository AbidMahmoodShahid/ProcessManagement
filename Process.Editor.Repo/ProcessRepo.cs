﻿using Microsoft.EntityFrameworkCore;
using Process.Editor.Elements;
using ProcessManagement.DataStorage.EF;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace Process.Editor.Repo
{
    public class ProcessRepo : IProcessRepo
    {
        private PMDataContext _pMDataContext;

        public ProcessRepo(PMDataContext pMDataContext)
        {
            _pMDataContext = pMDataContext;
        }

        //TODO AM: implement baseclass
        public async Task<List<ProcessModel>> GetAll()
        {
            return _pMDataContext.Process.Include(pM => pM.ItemCollection).ThenInclude(pG => pG.ItemCollection).ToList();
        }

        public void Attach(ProcessModel processModel)
        {
            _pMDataContext.Process.Attach(processModel);
        }

        public void AttachRange(ObservableCollection<ProcessModel> processModelList)
        {
            _pMDataContext.Process.AttachRange(processModelList);
        }

        public void Update(ProcessModel processModel)
        {
            _pMDataContext.Process.Update(processModel);
        }

        public void UpdateRange(ObservableCollection<ProcessModel> processModelList)
        {
            _pMDataContext.Process.UpdateRange(processModelList);
        }

        public void Delete(ProcessModel processModel)
        {
            _pMDataContext.Process.Remove(processModel);
        }
    }
}
