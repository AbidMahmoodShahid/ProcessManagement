using Process.Editor.Elements;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace DataAccess.Contract
{
    public interface IProcessGroupRepo
    {
        List<ProcessGroupModel> GetAll();

        void AddOrUpdate(ProcessGroupModel processGroupModel);

        void AddOrUpdateRange(ObservableCollection<ProcessGroupModel> processGroupModel);

        void Delete(ProcessGroupModel processGroupModel);
    }
}
