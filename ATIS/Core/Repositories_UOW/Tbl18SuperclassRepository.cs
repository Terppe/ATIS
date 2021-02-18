using ATIS.Ui.Models;
using ATIS.Ui.Core.Interfaces_UOW;

namespace ATIS.Ui.Core.Repositories_UOW
{
    public class Tbl18SuperclassRepository : Repository<Tbl18Superclass>, ITbl18SuperclassRepository
    {
        private readonly AtisDbContext _atisDbContext;

        public Tbl18SuperclassRepository(AtisDbContext context) : base(context)
        {
            _atisDbContext = context;
        }
    }
}
