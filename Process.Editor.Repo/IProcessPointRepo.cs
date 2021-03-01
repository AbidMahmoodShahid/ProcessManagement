using Process.Editor.Elements;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace Process.Editor.Repo
{
    public interface IProcessPointRepo
    {
        Task<List<ProcessPoint>> GetAll();

        void Attach(ProcessPoint processPointModel);

        void AttachRange(List<ProcessPoint> processPointList);

        void Update(ProcessPoint processPointModel);

        void UpdateRange(List<ProcessPoint> processPointList);

        void Delete(ProcessPoint processPointModel);
    }
}
