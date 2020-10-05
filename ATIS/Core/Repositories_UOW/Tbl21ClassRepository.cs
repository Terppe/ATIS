using ATIS.Dal.Models;
using ATIS.Ui.Core.Interfaces_UOW;

namespace ATIS.Ui.Core.Repositories_UOW
{
    public class Tbl21ClassRepository : Repository<Tbl21Class>, ITbl21ClassRepository
    {
        private readonly AtisDbContext _atisDbContext;

        public Tbl21ClassRepository(AtisDbContext context) : base(context)
        {
            _atisDbContext = context;

        }
    }
}
