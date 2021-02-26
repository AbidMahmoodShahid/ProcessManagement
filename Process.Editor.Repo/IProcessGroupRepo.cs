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

        void AddOrUpdate(ProcessGroupModel processGroupModel);

        void AddOrUpdateRange(ObservableCollection<ProcessGroupModel> processGroupModel);

        void Update(ProcessGroupModel processGroupModel);

        void UpdateRange(ObservableCollection<ProcessGroupModel> processGroupModel);

        void Delete(ProcessGroupModel processGroupModel);
    }
}
