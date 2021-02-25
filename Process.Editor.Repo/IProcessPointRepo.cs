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

        void Attach(ProcessPoint processPointModel);

        void Update(ProcessPoint processPointModel);

        void UpdateAll(ObservableCollection<ProcessPoint> processPointList);

        void Delete(ProcessPoint processPointModel);
    }
}
