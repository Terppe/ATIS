using ATIS.Dal.Models;
using ATIS.Ui.Core.Interfaces_UOW;

namespace ATIS.Ui.Core.Repositories_UOW
{
    public class TblCountryRepository : Repository<TblCountry>, ITblCountryRepository
    {
        private readonly AtisDbContext _atisDbContext;

        public TblCountryRepository(AtisDbContext context) : base(context)
        {
            _atisDbContext = context;

        }
    }
}
