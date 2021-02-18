using ATIS.Ui.Models;
using ATIS.Ui.Core.Interfaces_UOW;

namespace ATIS.Ui.Core.Repositories_UOW
{
    public class Tbl68SpeciesgroupRepository : Repository<Tbl68Speciesgroup>, ITbl68SpeciesgroupRepository
    {
        private readonly AtisDbContext _atisDbContext;

        public Tbl68SpeciesgroupRepository(AtisDbContext context) : base(context)
        {
            _atisDbContext = context;

        }
    }
}
