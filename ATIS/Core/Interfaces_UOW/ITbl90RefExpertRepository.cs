using ATIS.Dal.Models;
using System.Collections.Generic;

namespace ATIS.Ui.Core.Interfaces_UOW
{
    public interface ITbl90RefExpertRepository : IRepository<Tbl90RefExpert>
    {
        IEnumerable<Tbl90RefExpert> ListTbl90RefExpertsOrderBy();
    }
}
