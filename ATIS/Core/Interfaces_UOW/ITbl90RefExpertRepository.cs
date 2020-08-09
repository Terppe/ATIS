using System;
using System.Collections.Generic;
using System.Text;
using ATIS.Dal.Models;

namespace ATIS.Ui.Core.Interfaces_UOW
{
    public interface ITbl90RefExpertRepository : IRepository<Tbl90RefExpert>
    {
        IEnumerable<Tbl90RefExpert> ListTbl90RefExpertsOrderBy();
    }
}
