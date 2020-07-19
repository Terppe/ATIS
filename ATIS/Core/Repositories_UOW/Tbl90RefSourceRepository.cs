using ATIS.Dal.Models;
using ATIS.Ui.Core.Interfaces_UOW;

namespace ATIS.Ui.Core.Repositories_UOW
{
    public class Tbl90RefSourceRepository : Repository<Tbl90RefSource>, ITbl90RefSourceRepository
    {
        private readonly AtisDbContext _atisDbContext;

        public Tbl90RefSourceRepository(AtisDbContext context) : base(context)
        {
            _atisDbContext = context;

        }
    }
}
