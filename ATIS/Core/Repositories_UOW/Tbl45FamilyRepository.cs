using ATIS.Ui.Models;
using ATIS.Ui.Core.Interfaces_UOW;

namespace ATIS.Ui.Core.Repositories_UOW
{
    public class Tbl45FamilyRepository : Repository<Tbl45Family>, ITbl45FamilyRepository
    {
        private readonly AtisDbContext _atisDbContext;

        public Tbl45FamilyRepository(AtisDbContext context) : base(context)
        {
            _atisDbContext = context;

        }
    }
}
