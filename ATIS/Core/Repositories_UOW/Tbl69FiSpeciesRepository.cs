using ATIS.Ui.Models;
using ATIS.Ui.Core.Interfaces_UOW;

namespace ATIS.Ui.Core.Repositories_UOW
{
    public class Tbl69FiSpeciesRepository : Repository<Tbl69FiSpecies>, ITbl69FiSpeciesRepository
    {
        private readonly AtisDbContext _atisDbContext;

        public Tbl69FiSpeciesRepository(AtisDbContext context) : base(context)
        {
            _atisDbContext = context;

        }
    }
}
