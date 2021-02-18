using ATIS.Ui.Models;
using ATIS.Ui.Core.Interfaces_UOW;

namespace ATIS.Ui.Core.Repositories_UOW
{
    public class Tbl81ImageRepository : Repository<Tbl81Image>, ITbl81ImageRepository
    {
        private readonly AtisDbContext _atisDbContext;

        public Tbl81ImageRepository(AtisDbContext context) : base(context)
        {
            _atisDbContext = context;

        }
    }
}
