using System;
using System.Collections.Generic;
using System.Text;

namespace ATIS.Ui.Core.Interfaces_Dapper
{
    public interface IRepository<T>
    {
        void Insert(T entity);
        void Delete(T entity);
        void Update(T entity);
        T GetById(int id);
        void Commit();
    }
}
