using Process.Editor.Elements;
using Process.Editor.Repo;
using ProcessManagement.DataStorage.EF;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace UnitOfWork
{
    public class UnitOfWork
    {
        private IProcessRepo _processRepo;

        public UnitOfWork()
        {
            _processRepo = new ProcessRepo();
        }


        public List<ProcessModel> GetAll()
        {
            return _processRepo.GetAll();
        }

        public void AddProcess(ProcessModel processModel)
        {
            _processRepo.AddProcess(processModel);
        }

        public void DeleteProcess(ProcessModel processModel)
        {
            _processRepo.DeleteProcess(processModel);
        }

        public void SaveToDatabase()
        {
            _processRepo.SaveToDatabase();
        }

    }
}
