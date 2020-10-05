using ATIS.Dal.Models;
using ATIS.Ui.Core.Interfaces_UOW;

namespace ATIS.Ui.Core.Repositories_UOW
{
    public class Tbl51InfrafamilyRepository : Repository<Tbl51Infrafamily>, ITbl51InfrafamilyRepository
    {
        private readonly AtisDbContext _atisDbContext;

        public Tbl51InfrafamilyRepository(AtisDbContext context) : base(context)
        {
            _atisDbContext = context;

        }
    }
}
