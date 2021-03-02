using Process.Editor.Elements;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace Process.Editor.Repo
{
    public interface IProcessRepo
    {
        Task<List<ProcessModel>> GetAll();

        Task Attach(ProcessModel processModel);

        Task AttachRange(ObservableCollection<ProcessModel> processModel);

        Task Update(ProcessModel processModel);

        Task UpdateRange(ObservableCollection<ProcessModel> processModel);

        Task Delete(ProcessModel processModel);
    }
}
