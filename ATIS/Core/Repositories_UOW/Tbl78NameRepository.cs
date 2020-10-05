using ATIS.Dal.Models;
using ATIS.Ui.Core.Interfaces_UOW;

namespace ATIS.Ui.Core.Repositories_UOW
{
    public class Tbl78NameRepository : Repository<Tbl78Name>, ITbl78NameRepository
    {
        private readonly AtisDbContext _atisDbContext;

        public Tbl78NameRepository(AtisDbContext context) : base(context)
        {
            _atisDbContext = context;

        }
    }
}
