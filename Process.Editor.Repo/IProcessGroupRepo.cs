using Process.Editor.Elements;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Process.Editor.Repo
{
    public interface IProcessGroupRepo
    {
        List<ProcessGroupModel> GetAll();

        void Attach(ProcessGroupModel processGroupModel);

        void Update(ProcessGroupModel processGroupModel);

        void UpdateAll(ObservableCollection<ProcessGroupModel> processGroupModel);

        void Delete(ProcessGroupModel processGroupModel);
    }
}
