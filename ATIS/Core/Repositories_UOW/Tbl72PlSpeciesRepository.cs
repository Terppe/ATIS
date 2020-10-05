using ATIS.Dal.Models;
using ATIS.Ui.Core.Interfaces_UOW;

namespace ATIS.Ui.Core.Repositories_UOW
{
    public class Tbl72PlSpeciesRepository : Repository<Tbl72PlSpecies>, ITbl72PlSpeciesRepository
    {
        private readonly AtisDbContext _atisDbContext;

        public Tbl72PlSpeciesRepository(AtisDbContext context) : base(context)
        {
            _atisDbContext = context;

        }
    }
}
