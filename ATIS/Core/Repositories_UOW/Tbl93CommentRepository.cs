using ATIS.Dal.Models;
using ATIS.Ui.Core.Interfaces_UOW;

namespace ATIS.Ui.Core.Repositories_UOW
{
    public class Tbl93CommentRepository : Repository<Tbl93Comment>, ITbl93CommentRepository
    {
        private readonly AtisDbContext _atisDbContext;

        public Tbl93CommentRepository(AtisDbContext context) : base(context)
        {
            _atisDbContext = context;

        }
    }
}
