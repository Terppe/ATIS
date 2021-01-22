using ATIS.Ui.Core.Interfaces_UOW;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace ATIS.Ui.Core.Repositories_UOW
{
    public class Repository<T> : IRepository<T> where T : class
    {    /// <summary>
         /// The context object for the database
         /// </summary>
        private DbContext _context;

        /// <summary>
        /// The IObjectSet that represents the current entity.
        /// </summary>
        private readonly DbSet<T> _entities;

        public Repository(DbContext context)
        {
            _context = context;
            _entities = _context.Set<T>();
        }

        public T GetById(int id)
        {
            return _entities.Find(id);
        }

        public IEnumerable<T> GetAll()
        {
            return _entities.ToList();
        }

        public IEnumerable<T> Find(Expression<Func<T, bool>> predicate)
        {
            return _entities.Where<T>(predicate);
        }

        public void Add(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            _entities.Add(entity);
        }

        public void AddRange(IEnumerable<T> entities)
        {
            _entities.AddRange(entities);
        }

        public void Update(T entity)
        {
            _entities.Update(entity);
        }

        public void UpdateRange(IEnumerable<T> entities)
        {
            _entities.UpdateRange(entities);
        }

        public void Remove(T entity)
        {
            _entities.Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
            _entities.RemoveRange(entities);
        }

        public IQueryable<T> GetQuery()
        {
            return _entities;
        }

        public IQueryable<T> AsNoTracking()
        {
            return _entities.AsNoTracking<T>();
        }

        public T Single(Expression<Func<T, bool>> predicate)
        {
            return _entities.Single<T>(predicate);
        }

        public T First(Expression<Func<T, bool>> predicate)
        {
            return _entities.First<T>(predicate);
        }

        public T FirstOrDefault(Expression<Func<T, bool>> predicate)
        {
            return _entities.FirstOrDefault<T>(predicate);
        }

        public void Delete(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            _entities.Remove(entity);
        }

        public void Attach(T entity)
        {
            _entities.Attach(entity);
        }

        public void Detach(T entity)
        {
            _context.Entry(entity).State = EntityState.Detached;
        }

        public void MarkModified(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!disposing) return;
            if (_context == null) return;
            _context.Dispose();

            _context = null;
        }
    }
}
