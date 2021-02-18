using ATIS.Ui.Models;
using ATIS.Ui.Core.Interfaces_UOW;

namespace ATIS.Ui.Core.Repositories_UOW
{
    public class Tbl24SubclassRepository : Repository<Tbl24Subclass>, ITbl24SubclassRepository
    {
        private readonly AtisDbContext _atisDbContext;

        public Tbl24SubclassRepository(AtisDbContext context) : base(context)
        {
            _atisDbContext = context;

        }
    }
}
