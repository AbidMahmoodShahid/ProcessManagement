﻿using Process.Editor.Elements;
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

        void Attach(ProcessGroupModel processGroupModel);

        void AttachRange(ObservableCollection<ProcessGroupModel> processGroupModel);

        void Update(ProcessGroupModel processGroupModel);

        void UpdateRange(ObservableCollection<ProcessGroupModel> processGroupList);

        void Delete(ProcessGroupModel processGroupModel);
    }
}
