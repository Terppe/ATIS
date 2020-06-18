using System.Collections.ObjectModel;
using System.Linq;
using ATIS.Dal.Models;
using ATIS.Ui.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ATIS.UiTests.Views.Database
{
    [TestClass()]
    public class DatabaseTests
    {
        [TestMethod()]
        public void CountOfTbl03Regnums()
        {
            //Tbl03RegnumsViewModel model = new Tbl03RegnumsViewModel();
            //var count = model.ConnectToRegnumsList().Count;
            int count;

            using (var context = new AtisDbContext())
            {
                var regnumsList = new ObservableCollection<Tbl03Regnum>(context.Tbl03Regnums
                       .OrderBy(r => r.RegnumName)
                       .ThenBy(p => p.Subregnum));
                //or          .OrderBy(r => r.RegnumName + r.Subregnum)

                count = regnumsList.Count;
            }

            Assert.AreEqual(count, 9);
        }
        [TestMethod()]
        public void ConnectToRegnumsListByStartwithRegnumName()
        {
            //Tbl03RegnumsViewModel model = new Tbl03RegnumsViewModel();
            //var count = model.ConnectToRegnumsList().Count;
            int count;

            using (var context = new AtisDbContext())
            {
                var regnumsList = new ObservableCollection<Tbl03Regnum>(context.Tbl03Regnums
                    .Where(e => e.RegnumName.StartsWith("a"))
                    .OrderBy(r => r.RegnumName)
                    .ThenBy(p => p.Subregnum));
                //or          .OrderBy(r => r.RegnumName + r.Subregnum)

                count = regnumsList.Count;
            }

            Assert.AreEqual(count, 2);
        }
        [TestMethod()]
        public void ConnectToRegnumsListByStartwithRegnumId()
        {
            //Tbl03RegnumsViewModel model = new Tbl03RegnumsViewModel();
            //var count = model.ConnectToRegnumsList().Count;
            int count;

            using (var context = new AtisDbContext())
            {
                var regnumsList = new ObservableCollection<Tbl03Regnum>(context.Tbl03Regnums
                    .Where(e => e.RegnumId == 115)
                    .OrderBy(r => r.RegnumName + r.Subregnum));

                count = regnumsList.Count;
            }

            Assert.IsTrue(count == 1);
        }

    }
}