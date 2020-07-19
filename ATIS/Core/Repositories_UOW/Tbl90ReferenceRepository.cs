using ATIS.Dal.Models;
using ATIS.Ui.Core.Interfaces_UOW;

namespace ATIS.Ui.Core.Repositories_UOW
{
    public class Tbl90ReferenceRepository : Repository<Tbl90Reference>, ITbl90ReferenceRepository
    {
        private readonly AtisDbContext _atisDbContext;

        public Tbl90ReferenceRepository(AtisDbContext context) : base(context)
        {
            _atisDbContext = context;
        }
    }
}
