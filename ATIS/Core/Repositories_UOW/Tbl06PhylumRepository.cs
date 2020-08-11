using System.Collections.Generic;
using System.Linq;
using ATIS.Dal.Models;
using ATIS.Ui.Core.Interfaces_UOW;
using Microsoft.EntityFrameworkCore;

namespace ATIS.Ui.Core.Repositories_UOW
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

        public IEnumerable<Tbl06Phylum> ListTbl06PhylumsOnlyAnimaliaOrderBy(string search)
        {
            return _atisDbContext.Tbl06Phylums
                .Where(
                    e => e.PhylumName.StartsWith(search) &&
                         e.RegnumId.Equals(113) == false &&     //Plantae
                         e.RegnumId.Equals(114) == false &&     //Archaea
                         e.RegnumId.Equals(115) == false        //Protozoa
                )
                .OrderBy(r => r.PhylumName)
                .ToList();
        }

    }
}