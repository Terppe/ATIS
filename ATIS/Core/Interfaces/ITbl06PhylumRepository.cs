using System;
using System.Collections.Generic;
using System.Text;
using ATIS.DAL.Models;

namespace ATIS.Ui.Core.Interfaces
{
    public interface ITbl06PhylumRepository : IRepository<Tbl06Phylum>
    {
        IEnumerable<Tbl06Phylum> GetTopPhylums(int count);
        IEnumerable<Tbl06Phylum> GetPhylumsWithRegnums(int pageIndex, int pageSize);

    }
}
