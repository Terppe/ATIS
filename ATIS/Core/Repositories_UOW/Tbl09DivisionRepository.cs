using ATIS.Ui.Models;
using ATIS.Ui.Core.Interfaces_UOW;
using System.Collections.Generic;
using System.Linq;

namespace ATIS.Ui.Core.Repositories_UOW
{
    public class Tbl09DivisionRepository : Repository<Tbl09Division>, ITbl09DivisionRepository
    {
        private readonly AtisDbContext _atisDbContext;

        public Tbl09DivisionRepository(AtisDbContext context) : base(context)
        {
            _atisDbContext = context;

        }

        public IEnumerable<Tbl09Division> ListTbl09DivisionsOnlyPlantaeOrderBy(string search)
        {
            return _atisDbContext.Tbl09Divisions
                .Where(
                    e => e.DivisionName.StartsWith(search) &&
                         e.RegnumId.Equals(112) == false &&     //Animalia
                         e.RegnumId.Equals(114) == false &&     //Archaea
                         e.RegnumId.Equals(115) == false        //Protozoa
                )
                .OrderBy(r => r.DivisionName)
                .ToList();
        }
    }
}