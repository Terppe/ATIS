using ATIS.Ui.Models;
using System.Collections.Generic;

namespace ATIS.Ui.Core.Interfaces_UOW
{
    public interface ITbl90RefSourceRepository : IRepository<Tbl90RefSource>
    {
        IEnumerable<Tbl90RefSource> ListTbl90RefSourcesOrderBy();
    }
}
