using System;
using System.Collections.Generic;
using System.Text;
using ATIS.Ui.Core.Interfaces;
using ATIS.Ui.Core.Interfaces_UOW;
using ATIS.Ui.Core.Repositories;
using ATIS.Ui.Core.Repositories_UOW;

namespace ATIS.Ui.Core
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AtisDbContext _context;

        public UnitOfWork(AtisDbContext context)
        {
            _context = context;
            Tbl03Regnums = new Tbl03RegnumRepository(_context);
            Tbl06Phylums = new Tbl06PhylumRepository(_context);
        }


        public ITbl03RegnumRepository Tbl03Regnums { get; private set; }
        public ITbl06PhylumRepository Tbl06Phylums { get; private set; }

        public int Complete()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
