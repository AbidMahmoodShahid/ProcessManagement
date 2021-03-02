using Process.Editor.Elements;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace Process.Editor.Repo
{
    public interface IProcessGroupRepo
    {
        Task<List<ProcessGroupModel>> GetAll();

        Task Attach(ProcessGroupModel processGroupModel);

        Task AttachRange(ObservableCollection<ProcessGroupModel> processGroupModel);

        Task Update(ProcessGroupModel processGroupModel);

        Task UpdateRange(ObservableCollection<ProcessGroupModel> processGroupList);

        Task Delete(ProcessGroupModel processGroupModel);
    }
}
