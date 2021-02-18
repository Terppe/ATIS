using ATIS.Ui.Models;
using System.Collections.Generic;

namespace ATIS.Ui.Core.Interfaces_UOW
{
    public interface ITbl09DivisionRepository : IRepository<Tbl09Division>
    {
        IEnumerable<Tbl09Division> ListTbl09DivisionsOnlyPlantaeOrderBy(string search);

    }
}