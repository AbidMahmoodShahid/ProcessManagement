using Process.Editor.Elements;
using ProcessManagement.DataStorage.EF;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Process.Editor.Repo
{
    public class ProcessPointRepo : IProcessPointRepo
    {
        private PMDataContext _pMDataContext;

        public ProcessPointRepo(PMDataContext pMDataContext)
        {
            _pMDataContext = pMDataContext;
        }


        public List<ProcessPoint> GetAll()
        {
            return _pMDataContext.ProcessPoint.ToList();
        }

        public void AttachOrUpdate(ProcessPoint processPointModel)
        {
            _pMDataContext.ProcessPoint.Attach(processPointModel);
        }

        public void AttachOrUpdateRange(ObservableCollection<ProcessPoint> processPointList)
        {
            _pMDataContext.ProcessPoint.AttachRange(processPointList);
        }

        public void Delete(ProcessPoint processPointModel)
        {
            _pMDataContext.ProcessPoint.Remove(processPointModel);
        }
    }
}
