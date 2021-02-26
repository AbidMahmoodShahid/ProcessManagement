﻿using Microsoft.EntityFrameworkCore;
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

        //TODO AM: implement baseclass
        public List<ProcessModel> GetAll()
        {
            return _pMDataContext.Process.Include(pM => pM.ItemCollection).ThenInclude(pG => pG.ItemCollection).ToList();
        }

        public void AttachOrUpdate(ProcessModel processModel)
        {
            _pMDataContext.Process.Attach(processModel);
        }

        public void AttachOrUpdateRange(ObservableCollection<ProcessModel> processModelList)
        {
            _pMDataContext.Process.AttachRange(processModelList);
        }

        public void Delete(ProcessModel processModel)
        {
            _pMDataContext.Process.Remove(processModel);
        }
    }
}
