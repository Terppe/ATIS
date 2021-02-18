using ATIS.Ui.Models;
using ATIS.Ui.Core.Interfaces_UOW;

namespace ATIS.Ui.Core.Repositories_UOW
{
    public class Tbl33OrdoRepository : Repository<Tbl33Ordo>, ITbl33OrdoRepository
    {
        private readonly AtisDbContext _atisDbContext;

        public Tbl33OrdoRepository(AtisDbContext context) : base(context)
        {
            _atisDbContext = context;

        }
    }
}
