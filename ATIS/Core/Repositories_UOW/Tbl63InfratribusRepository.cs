using ATIS.Dal.Models;
using ATIS.Ui.Core.Interfaces_UOW;

namespace ATIS.Ui.Core.Repositories_UOW
{
    public class Tbl63InfratribusRepository : Repository<Tbl63Infratribus>, ITbl63InfratribusRepository
    {
        private readonly AtisDbContext _atisDbContext;

        public Tbl63InfratribusRepository(AtisDbContext context) : base(context)
        {
            _atisDbContext = context;

        }
    }
}
