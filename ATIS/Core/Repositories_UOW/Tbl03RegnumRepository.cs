using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using ATIS.Dal.Models;
using ATIS.Ui.Core.Interfaces_UOW;

namespace ATIS.Ui.Core.Repositories_UOW
{
    public class Tbl03RegnumRepository : Repository<Tbl03Regnum>, ITbl03RegnumRepository
    {

        private readonly AtisDbContext _atisDbContext;

        public Tbl03RegnumRepository(AtisDbContext context) : base(context)
        {
            _atisDbContext = context;
        }


        public IEnumerable<Tbl03Regnum> GetBestRegnums(int countRegnum)
        {
            if (countRegnum > _atisDbContext.Tbl03Regnums.ToList().Count)
            {
                throw new IndexOutOfRangeException();
            }

            return _atisDbContext.Tbl03Regnums.OrderByDescending(x => x.RegnumName).Take(countRegnum).ToList();
        }

        public IEnumerable<Tbl03Regnum> ListTbl03RegnumsByFilterTextAboutAllFields(string filterText)
        {
            return _atisDbContext.Tbl03Regnums
                .Where(
                e => e.RegnumName.StartsWith(filterText) &&
                     e.RegnumName.Contains("#") == false ||
                     e.Subregnum.Contains(filterText) ||
                     e.EngName.Contains(filterText) ||
                     e.GerName.Contains(filterText) ||
                     e.FraName.Contains(filterText) ||
                     e.PorName.Contains(filterText))
                .OrderBy(r => r.RegnumName)
                .ThenBy(y => y.Subregnum)
                .ToList();
            //   p => p.Tbl06Phylums, k => k.Tbl09Divisions, r => r.Tbl90References, s => s.Tbl93Comments);
        }



        public IEnumerable<Tbl03Regnum> ListRegnumsBySearchName(object searchname)
        {
            var regnumsList = new ObservableCollection<Tbl03Regnum>();
            //        var context = new AtisDbContext();

            if (searchname == null)
            {
                MessageBox.Show("Input Requested", "SearchNameOrId",
                    MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                return regnumsList;
            }

            if (searchname.ToString() == "*")
            {
                //            using var context = new AtisDbContext();
                regnumsList = new ObservableCollection<Tbl03Regnum>(_atisDbContext.Tbl03Regnums.ToList());
            }
            else
            {
                //            using var context = new AtisDbContext();
                regnumsList = int.TryParse(searchname.ToString(), out var id)
                    ? new ObservableCollection<Tbl03Regnum>(_atisDbContext.Tbl03Regnums
                        .Where(e => e.RegnumId == id))
                    : new ObservableCollection<Tbl03Regnum>(_atisDbContext.Tbl03Regnums
                        .Where(e => e.RegnumName.StartsWith(searchname.ToString()))
                        .OrderBy(a => a.RegnumName)
                        .ThenBy(y => y.Subregnum)

                    );
            }

            if (regnumsList.Count == 0)
            {
                MessageBox.Show("No Dataset found ", "Tables",
                    MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
            }
            return regnumsList;
        }

        public IEnumerable<Tbl03Regnum> ListTbl03RegnumsOrderBy()
        {
            return _atisDbContext.Tbl03Regnums
                .OrderBy(x => x.RegnumName)
                .ThenBy(y => y.Subregnum)
                .ToList();
        }
    }
}
