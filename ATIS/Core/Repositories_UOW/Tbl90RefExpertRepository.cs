using ATIS.Ui.Models;
using ATIS.Ui.Core.Interfaces_UOW;
using System.Collections.Generic;
using System.Linq;

namespace ATIS.Ui.Core.Repositories_UOW
{
    public class Tbl90RefExpertRepository : Repository<Tbl90RefExpert>, ITbl90RefExpertRepository
    {
        private readonly AtisDbContext _atisDbContext;

        public Tbl90RefExpertRepository(AtisDbContext context) : base(context)
        {
            _atisDbContext = context;
        }

        public IEnumerable<Tbl90RefExpert> ListTbl90RefExpertsOrderBy()
        {
            return _atisDbContext.Tbl90RefExperts.OrderBy(x => x.RefExpertName).ToList();
        }
    }
}