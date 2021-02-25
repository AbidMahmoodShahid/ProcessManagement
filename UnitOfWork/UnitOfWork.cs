using Microsoft.EntityFrameworkCore.Storage;
using Process.Editor.Elements;
using Process.Editor.Repo;
using Process.Simulation.Repo;
using ProcessManagement.DataStorage.EF;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Transactions;

namespace DataAccess
{
    public class UnitOfWork : IDisposable
    {
        private PMDataContext _pMDataContext;
        private IDbContextTransaction _transaction;
        //private Task<IDbContextTransaction> transaction;

        public UnitOfWork()
        {
            _pMDataContext = new PMDataContext();
            _transaction = _pMDataContext.Database.BeginTransaction(); //TODO Am: read why we should use this

            //transaction = _pMDataContext.Database.BeginTransactionAsync(); //TODO Am: read why we should use this
        }

        #region Repos

        private IProcessRepo _processRepo;
        public IProcessRepo ProcessRepo
        {
            get
            {
                if(_processRepo == null)
                    _processRepo = new ProcessRepo(_pMDataContext);

                return _processRepo;
            }
        }

        private IProcessGroupRepo _processGroupRepo;
        public IProcessGroupRepo ProcessGroupRepo
        {
            get
            {
                if(_processGroupRepo == null)
                    _processGroupRepo = new ProcessGroupRepo(_pMDataContext);

                return _processGroupRepo;
            }
        }

        private IProcessPointRepo _processPointRepo;
        public IProcessPointRepo ProcessPointRepo
        {
            get
            {
                if(_processPointRepo == null)
                    _processPointRepo = new ProcessPointRepo(_pMDataContext);

                return _processPointRepo;
            }
        }

        private ISimulationPointRepo _simulationPointRepo;
        public ISimulationPointRepo SimulationPointRepo
        {
            get
            {
                if(_simulationPointRepo == null)
                    _simulationPointRepo = new SimulationPointRepo(_pMDataContext);

                return _simulationPointRepo;
            }
        }

        #endregion

        public void SaveChanges()
        {
            _pMDataContext.SaveChanges();
            //transaction.Result.Commit();
            _transaction.Commit();
        }

        public void Dispose()
        {
            _pMDataContext.Dispose();
        }
    }
}
