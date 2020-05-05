using System.Collections.ObjectModel;
using System.Linq;
using ATIS.Ui.Core;
using ATIS.Ui.Core.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ATIS.UiTests.Views.Database
{
    [TestClass()]
    public class DatabaseTests
    {
        [TestMethod()]
        public void CountOfTbl03Regnums()
        {
            int count;

            using (var context = new AtisDbContext())
            {
                var tbl03RegnumsList = new ObservableCollection<Tbl03Regnum>(context.Tbl03Regnums
                    //.OrderBy(r => r.RegnumName)
                    //.ThenBy(r => r.Subregnum));
                    .OrderBy(r => r.RegnumName + r.Subregnum));

                count = tbl03RegnumsList.Count;
            }

            Assert.AreEqual(count, 9);
        }
    }
}