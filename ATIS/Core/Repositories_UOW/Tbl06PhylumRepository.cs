using ATIS.Dal.Models;
using ATIS.Ui.Core.Interfaces_UOW;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

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
            var plantaeId = 0;
            var archaeaId = 0;
            var protozoaId = 0;

            //var animalia = _atisDbContext.Tbl03Regnums.FirstOrDefault(e => e.RegnumName == "Animalia");
            //if (animalia != null)
            //    var animaliaId = animalia.RegnumId;

            var plantae = _atisDbContext.Tbl03Regnums.FirstOrDefault(e => e.RegnumName == "Plantae");
            if (plantae != null)
                plantaeId = plantae.RegnumId;

            var archaea = _atisDbContext.Tbl03Regnums.FirstOrDefault(e => e.RegnumName == "Archaea");
            if (archaea != null)
                archaeaId = archaea.RegnumId;

            var protozoa = _atisDbContext.Tbl03Regnums.FirstOrDefault(e => e.RegnumName == "Protozoa");
            if (protozoa != null)
                protozoaId = protozoa.RegnumId;


            return _atisDbContext.Tbl06Phylums
                .Where(
                    e => e.PhylumName.StartsWith(search) &&
                         e.RegnumId.Equals(plantaeId) == false &&     //Plantae
                         e.RegnumId.Equals(archaeaId) == false &&     //Archaea
                         e.RegnumId.Equals(protozoaId) == false       //Protozoa
                )
                .OrderBy(r => r.PhylumName)
                .ToList();
        }

    }
}