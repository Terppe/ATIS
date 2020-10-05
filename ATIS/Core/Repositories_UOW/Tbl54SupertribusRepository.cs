using ATIS.Dal.Models;
using ATIS.Ui.Core.Interfaces_UOW;

namespace ATIS.Ui.Core.Repositories_UOW
{
    public class Tbl54SupertribusRepository : Repository<Tbl54Supertribus>, ITbl54SupertribusRepository
    {
        private readonly AtisDbContext _atisDbContext;

        public Tbl54SupertribusRepository(AtisDbContext context) : base(context)
        {
            _atisDbContext = context;

        }
    }
}
