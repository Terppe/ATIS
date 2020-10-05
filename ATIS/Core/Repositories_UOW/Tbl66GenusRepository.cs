using ATIS.Dal.Models;
using ATIS.Ui.Core.Interfaces_UOW;

namespace ATIS.Ui.Core.Repositories_UOW
{
    public class Tbl66GenusRepository : Repository<Tbl66Genus>, ITbl66GenusRepository
    {
        private readonly AtisDbContext _atisDbContext;

        public Tbl66GenusRepository(AtisDbContext context) : base(context)
        {
            _atisDbContext = context;

        }
    }
}
