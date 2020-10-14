using ATIS.Dal.Models;
using ATIS.Ui.Core.Interfaces_UOW;

namespace ATIS.Ui.Core.Repositories_UOW
{
    public class Tbl15SubdivisionRepository : Repository<Tbl15Subdivision>, ITbl15SubdivisionRepository
    {
        private readonly AtisDbContext _atisDbContext;

        public Tbl15SubdivisionRepository(AtisDbContext context) : base(context)
        {
            _atisDbContext = context;
        }
    }
}
