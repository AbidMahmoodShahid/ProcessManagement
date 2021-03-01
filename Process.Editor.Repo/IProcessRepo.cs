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

        void Attach(ProcessModel processModel);

        void AttachRange(ObservableCollection<ProcessModel> processModel);

        void Update(ProcessModel processModel);

        void UpdateRange(ObservableCollection<ProcessModel> processModel);

        void Delete(ProcessModel processModel);
    }
}
