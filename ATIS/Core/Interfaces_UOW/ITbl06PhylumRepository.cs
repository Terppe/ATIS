using System.Collections.Generic;
using ATIS.Dal.Models;

namespace ATIS.Ui.Core.Interfaces_UOW
{
    public interface ITbl06PhylumRepository : IRepository<Tbl06Phylum>
    {
        IEnumerable<Tbl06Phylum> GetTopPhylums(int count);
        IEnumerable<Tbl06Phylum> GetPhylumsWithRegnums(int pageIndex, int pageSize);

    }
}
