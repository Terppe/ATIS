using System;
using System.Collections.Generic;
using System.Text;
using ATIS.Ui.Core;
using ATIS.Ui.Helper;
using Common.Logging;

namespace ATIS.Ui.Views.Database.D12Subphylum
{
    public class SubphylumsViewModel : ViewModelBase
    {
        #region "Private Data Members"
        private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        //      private static IBusinessLayer _businessLayer;
        //    private static DbEntityException _entityException;
        private readonly UnitOfWork _uow = new UnitOfWork(new AtisDbContext());
        private readonly AtisDbContext _context = new AtisDbContext();
        private int _position;

        #endregion "Private Data Members"               

        public SubphylumsViewModel()
        {
            
        }
    }
}
