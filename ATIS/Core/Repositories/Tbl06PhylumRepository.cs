using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ATIS.DAL.Models;
using ATIS.Ui.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ATIS.Ui.Core.Repositories
{
    public class Tbl06PhylumRepository : Repository<Tbl06Phylum>, ITbl06PhylumRepository
    {
        private readonly AtisDbContext _atisDbContext;

        public Tbl06PhylumRepository(AtisDbContext context) : base(context)
        {
            _atisDbContext = context;
        }

        public IEnumerable<Tbl06Phylum> GetTopPhylums(int count)
        {
            return _atisDbContext.Tbl06Phylums.OrderByDescending(c => c.PhylumName).Take(count).ToList();
        }

        public IEnumerable<Tbl06Phylum> GetPhylumsWithRegnums(int pageIndex, int pageSize)
        {
            return _atisDbContext.Tbl06Phylums
                .Include(c => c.RegnumId)
                .OrderBy(c => c.PhylumName)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToList();
        }


    }
}
