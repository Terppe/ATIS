using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace ATIS.Ui.Core.Interfaces_UOW
{
    public interface IRepository<T> : IDisposable where T : class
    {
        T GetById(int id);

        IEnumerable<T> GetAll();

        IEnumerable<T> Find(Expression<Func<T, bool>> predicate);

        void Add(T entity);

        void AddRange(IEnumerable<T> entities);

        void Update(T entity);

        void UpdateRange(IEnumerable<T> entities);

        void Remove(T entity);

        void RemoveRange(IEnumerable<T> entities);

        //--------------------------------------
        IQueryable<T> GetQuery();
        IQueryable<T> AsNoTracking();
        T Single(Expression<Func<T, bool>> predicate);
        T First(Expression<Func<T, bool>> predicate);
        T FirstOrDefault(Expression<Func<T, bool>> predicate);
        void Delete(T entity);
        void Attach(T entity);
        void Detach(T entity);
        void MarkModified(T entity);
        void SaveChanges();
    }
}