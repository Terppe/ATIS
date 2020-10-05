using ATIS.Dal.Models;
using ATIS.Ui.Core.Interfaces_UOW;

namespace ATIS.Ui.Core.Repositories_UOW
{
    public class Tbl30LegioRepository : Repository<Tbl30Legio>, ITbl30LegioRepository
    {
        private readonly AtisDbContext _atisDbContext;

        public Tbl30LegioRepository(AtisDbContext context) : base(context)
        {
            _atisDbContext = context;

        }
    }
}
