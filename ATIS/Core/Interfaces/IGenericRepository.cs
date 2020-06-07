using System;
using System.Collections.Generic;
using System.Text;

namespace ATIS.Ui.Core.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        IEnumerable<T> SelectAll();
        T SelectById(object id);
        void Insert(T obj);
        void Update(T obj);
        void Delete(object id);
        void Save();


    }
}
