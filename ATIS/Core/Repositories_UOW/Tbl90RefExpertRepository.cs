using ATIS.Dal.Models;
using ATIS.Ui.Core.Interfaces_UOW;

namespace ATIS.Ui.Core.Repositories_UOW
{
    public class Tbl90RefExpertRepository : Repository<Tbl90RefExpert>, ITbl90RefExpertRepository
    {
        private readonly AtisDbContext _atisDbContext;

        public Tbl90RefExpertRepository(AtisDbContext context) : base(context)
        {
            _atisDbContext = context;
        }
    }
}
