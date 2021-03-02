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

        Task Attach(ProcessPoint processPointModel);

        Task AttachRange(List<ProcessPoint> processPointList);

        Task Update(ProcessPoint processPointModel);

        Task UpdateRange(List<ProcessPoint> processPointList);

        Task Delete(ProcessPoint processPointModel);
    }
}
