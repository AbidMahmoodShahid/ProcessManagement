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
    public class ProcessPointRepo : IProcessPointRepo
    {
        private PMDataContext _pMDataContext;

        public ProcessPointRepo(PMDataContext pMDataContext)
        {
            _pMDataContext = pMDataContext;
        }


        public async Task<List<ProcessPoint>> GetAll()
        {
            return _pMDataContext.ProcessPoint.ToList();
        }

        public void Attach(ProcessPoint processPointModel)
        {
            _pMDataContext.ProcessPoint.Attach(processPointModel);
        }

        public void AttachRange(List<ProcessPoint> processPointList)
        {
            _pMDataContext.ProcessPoint.AttachRange(processPointList);
        }

        public void Update(ProcessPoint processPointModel)
        {
            _pMDataContext.ProcessPoint.Update(processPointModel);
        }

        public void UpdateRange(List<ProcessPoint> processPointList)
        {
            _pMDataContext.ProcessPoint.UpdateRange(processPointList);
        }

        public void Delete(ProcessPoint processPointModel)
        {
            _pMDataContext.ProcessPoint.Remove(processPointModel);
        }
    }
}
