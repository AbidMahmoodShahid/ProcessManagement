using Microsoft.EntityFrameworkCore.Storage;
using Process.Editor.Elements;
using Process.Editor.Repo;
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

        // Repos
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
