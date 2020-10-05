using ATIS.Dal.Models;
using ATIS.Ui.Core.Interfaces_UOW;

namespace ATIS.Ui.Core.Repositories_UOW
{
    public class Tbl42SuperfamilyRepository : Repository<Tbl42Superfamily>, ITbl42SuperfamilyRepository
    {
        private readonly AtisDbContext _atisDbContext;

        public Tbl42SuperfamilyRepository(AtisDbContext context) : base(context)
        {
            _atisDbContext = context;

        }
    }
}
