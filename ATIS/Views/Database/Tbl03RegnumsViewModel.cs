using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Data;
using ATIS.Ui.Core;
using ATIS.Ui.Core.Models;
using ATIS.Ui.Helper;

namespace ATIS.Ui.Views.Database
{
    public class Tbl03RegnumsViewModel : ViewModelBase
    {
        public override string Name => "Regnum";

     //   public static string ConnectionString = @"Server=localhost\SQLEXPRESS;Database=ATIS34; Trusted_Connection=True; Integrated Security=SSPI";

        public Tbl03RegnumsViewModel()
        {
            ConnectToRegnumsList();

        }

        public ObservableCollection<Tbl03Regnum> ConnectToRegnumsList()
        {
            using (var context = new AtisDbContext())
            {
                Tbl03RegnumsList = new ObservableCollection<Tbl03Regnum>(context.Tbl03Regnums
                    .OrderBy(r => r.RegnumName)
                    .ThenBy(p => p.Subregnum));
                // or         .OrderBy(r => r.RegnumName + r.Subregnum)
                var t = Tbl03RegnumsList;
                return Tbl03RegnumsList;
            }
        }
        #region "Public Properties Tbl03Regnum"

        public string SearchRegnumName { get; set; }
        public Tbl03Regnum CurrentTbl03Regnum => Tbl03RegnumsView?.CurrentItem as Tbl03Regnum;
        public ObservableCollection<Tbl03Regnum> Tbl03RegnumsList { get; set; }
        public ObservableCollection<Tbl03Regnum> Tbl03RegnumsAllList { get; set; }
        public CollectionView Tbl03RegnumsView { get; set; }

        #endregion "Public Properties Tbl03Regnum"

    }
}
