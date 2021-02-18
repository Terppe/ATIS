using ATIS.Ui.Models;
using ATIS.Ui.Core.Interfaces_UOW;

namespace ATIS.Ui.Core.Repositories_UOW
{
    public class Tbl87GeographicRepository : Repository<Tbl87Geographic>, ITbl87GeographicRepository
    {
        private readonly AtisDbContext _atisDbContext;

        public Tbl87GeographicRepository(AtisDbContext context) : base(context)
        {
            _atisDbContext = context;

        }
    }
}
