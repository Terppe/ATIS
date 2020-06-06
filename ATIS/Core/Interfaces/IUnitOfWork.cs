using System;
using System.Collections.Generic;
using System.Text;

namespace ATIS.Ui.Core.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        ITbl03RegnumRepository Tbl03Regnums { get; }
        ITbl06PhylumRepository Tbl06Phylums { get; }
        int Complete();
    }
}
