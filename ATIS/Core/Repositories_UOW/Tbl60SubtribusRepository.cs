using ATIS.Ui.Models;
using ATIS.Ui.Core.Interfaces_UOW;

namespace ATIS.Ui.Core.Repositories_UOW
{
    public class Tbl60SubtribusRepository : Repository<Tbl60Subtribus>, ITbl60SubtribusRepository
    {
        private readonly AtisDbContext _atisDbContext;

        public Tbl60SubtribusRepository(AtisDbContext context) : base(context)
        {
            _atisDbContext = context;

        }
    }
}
