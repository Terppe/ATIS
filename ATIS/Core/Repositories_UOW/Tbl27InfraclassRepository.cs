using ATIS.Ui.Models;
using ATIS.Ui.Core.Interfaces_UOW;

namespace ATIS.Ui.Core.Repositories_UOW
{
    public class Tbl27InfraclassRepository : Repository<Tbl27Infraclass>, ITbl27InfraclassRepository
    {
        private readonly AtisDbContext _atisDbContext;

        public Tbl27InfraclassRepository(AtisDbContext context) : base(context)
        {
            _atisDbContext = context;

        }
    }
}
