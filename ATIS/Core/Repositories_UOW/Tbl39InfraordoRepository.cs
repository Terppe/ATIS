using ATIS.Ui.Models;
using ATIS.Ui.Core.Interfaces_UOW;

namespace ATIS.Ui.Core.Repositories_UOW
{
    public class Tbl39InfraordoRepository : Repository<Tbl39Infraordo>, ITbl39InfraordoRepository
    {
        private readonly AtisDbContext _atisDbContext;

        public Tbl39InfraordoRepository(AtisDbContext context) : base(context)
        {
            _atisDbContext = context;

        }
    }
}
