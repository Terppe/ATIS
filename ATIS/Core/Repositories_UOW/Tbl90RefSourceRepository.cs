using ATIS.Ui.Models;
using ATIS.Ui.Core.Interfaces_UOW;
using System.Collections.Generic;
using System.Linq;

namespace ATIS.Ui.Core.Repositories_UOW
{
    public class Tbl90RefSourceRepository : Repository<Tbl90RefSource>, ITbl90RefSourceRepository
    {
        private readonly AtisDbContext _atisDbContext;

        public Tbl90RefSourceRepository(AtisDbContext context) : base(context)
        {
            _atisDbContext = context;

        }

        public IEnumerable<Tbl90RefSource> ListTbl90RefSourcesOrderBy()
        {
            return _atisDbContext.Tbl90RefSources.OrderBy(x => x.RefSourceName).ToList();

        }
    }
}