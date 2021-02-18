using ATIS.Ui.Models;
using ATIS.Ui.Core.Interfaces_UOW;

namespace ATIS.Ui.Core.Repositories_UOW
{
    public class Tbl12SubphylumRepository : Repository<Tbl12Subphylum>, ITbl12SubphylumRepository
    {
        private readonly AtisDbContext _atisDbContext;

        public Tbl12SubphylumRepository(AtisDbContext context) : base(context)
        {
            _atisDbContext = context;

        }
    }
}
