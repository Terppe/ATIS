using ATIS.Ui.Models;
using ATIS.Ui.Core.Interfaces_UOW;

namespace ATIS.Ui.Core.Repositories_UOW
{
    public class Tbl48SubfamilyRepository : Repository<Tbl48Subfamily>, ITbl48SubfamilyRepository
    {
        private readonly AtisDbContext _atisDbContext;

        public Tbl48SubfamilyRepository(AtisDbContext context) : base(context)
        {
            _atisDbContext = context;

        }
    }
}
