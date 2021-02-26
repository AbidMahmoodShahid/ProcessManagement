using Process.Editor.Elements;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Process.Editor.Repo
{
    public interface IProcessPointRepo
    {
        List<ProcessPoint> GetAll();

        void AttachOrUpdate(ProcessPoint processPointModel);

        void AttachOrUpdateRange(ObservableCollection<ProcessPoint> processPointList);

        void Delete(ProcessPoint processPointModel);
    }
}
