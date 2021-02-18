using ATIS.Ui.Models;
using ATIS.Ui.Core.Interfaces_UOW;

namespace ATIS.Ui.Core.Repositories_UOW
{
    public class TblUserProfileRepository : Repository<TblUserProfile>, ITblUserProfileRepository
    {
        private readonly AtisDbContext _atisDbContext;

        public TblUserProfileRepository(AtisDbContext context) : base(context)
        {
            _atisDbContext = context;

        }
    }
}
