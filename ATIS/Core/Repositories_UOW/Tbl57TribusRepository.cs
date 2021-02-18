using ATIS.Ui.Models;
using ATIS.Ui.Core.Interfaces_UOW;

namespace ATIS.Ui.Core.Repositories_UOW
{
    public class Tbl57TribusRepository : Repository<Tbl57Tribus>, ITbl57TribusRepository
    {
        private readonly AtisDbContext _atisDbContext;

        public Tbl57TribusRepository(AtisDbContext context) : base(context)
        {
            _atisDbContext = context;

        }
    }
}
