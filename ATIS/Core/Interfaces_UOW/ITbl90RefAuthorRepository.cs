using ATIS.Dal.Models;
using System.Collections.Generic;

namespace ATIS.Ui.Core.Interfaces_UOW
{
    public interface ITbl90RefAuthorRepository : IRepository<Tbl90RefAuthor>
    {
        IEnumerable<Tbl90RefAuthor> ListTbl90RefAuthorsOrderBy();
    }
}
