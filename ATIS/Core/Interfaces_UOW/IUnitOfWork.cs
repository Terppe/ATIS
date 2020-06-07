using System;

namespace ATIS.Ui.Core.Interfaces_UOW
{
    public interface IUnitOfWork : IDisposable
    {
        ITbl03RegnumRepository Tbl03Regnums { get; }
        ITbl06PhylumRepository Tbl06Phylums { get; }
        int Complete();
    }
}
