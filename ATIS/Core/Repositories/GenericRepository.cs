using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ATIS.Ui.Core.Interfaces;
using Microsoft.EntityFrameworkCore;


namespace ATIS.Ui.Core.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly AtisDbContext _db = null;
        private readonly DbSet<T> _table = null;

        public GenericRepository()
        {
            this._db = new AtisDbContext();
            _table = _db.Set<T>();
        }

        public GenericRepository(AtisDbContext db)
        {
            this._db = db;
            _table = db.Set<T>();
        }

        public IEnumerable<T> SelectAll()
        {
            return _table.ToList();
        }

        public T SelectById(object id)
        {
            return _table.Find(id);
        }

        public void Insert(T obj)
        {
            _table.Add(obj);
        }

        public void Update(T obj)
        {
            _table.Attach(obj);
            _db.Entry(obj).State = EntityState.Modified;
        }

        public void Delete(object id)
        {
            T existing = _table.Find(id);
            _table.Remove(existing);
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
