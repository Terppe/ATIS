using ATIS.Dal.Models;
using ATIS.Ui.Core.Interfaces_UOW;

namespace ATIS.Ui.Core.Repositories_UOW
{
    public class Tbl09DivisionRepository : Repository<Tbl09Division>, ITbl09DivisionRepository
    {
        private readonly AtisDbContext _atisDbContext;

        public Tbl09DivisionRepository(AtisDbContext context) : base(context)
        {
            _atisDbContext = context;

        }
    }
}
