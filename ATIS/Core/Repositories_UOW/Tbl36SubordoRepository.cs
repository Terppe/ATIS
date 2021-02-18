using ATIS.Ui.Models;
using ATIS.Ui.Core.Interfaces_UOW;

namespace ATIS.Ui.Core.Repositories_UOW
{
    public class Tbl36SubordoRepository : Repository<Tbl36Subordo>, ITbl36SubordoRepository
    {
        private readonly AtisDbContext _atisDbContext;

        public Tbl36SubordoRepository(AtisDbContext context) : base(context)
        {
            _atisDbContext = context;

        }
    }
}
