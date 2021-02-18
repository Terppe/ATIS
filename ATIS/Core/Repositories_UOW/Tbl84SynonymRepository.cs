using ATIS.Ui.Models;
using ATIS.Ui.Core.Interfaces_UOW;

namespace ATIS.Ui.Core.Repositories_UOW
{
    public class Tbl84SynonymRepository : Repository<Tbl84Synonym>, ITbl84SynonymRepository
    {
        private readonly AtisDbContext _atisDbContext;

        public Tbl84SynonymRepository(AtisDbContext context) : base(context)
        {
            _atisDbContext = context;

        }
    }
}
