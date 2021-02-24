using Process.Editor.Elements;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Process.Editor.Repo
{
    public interface IProcessRepo
    {
        List<ProcessModel> GetAll();

        void AddProcess(ProcessModel processModel);

        void DeleteProcess(ProcessModel processModel);

        void SaveToDatabase();
    }
}
