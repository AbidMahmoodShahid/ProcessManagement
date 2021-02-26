using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace DataAccess.Contract
{
    interface IBaseRepo
    {
        void AddOrUpdate(T entity);

        void AddOrUpdateRange(ObservableCollection<T> entityList);

        void Delete(T entity);
    }
}
