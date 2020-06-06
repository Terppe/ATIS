using System;
using System.Collections.Generic;
using System.Text;
using ATIS.DAL.Models;

namespace ATIS.Ui.Core.Interfaces
{
    public interface ITbl03RegnumRepository : IRepository<Tbl03Regnum>
    {
        IEnumerable<Tbl03Regnum> GetBestRegnums(int countRegnum);

        IEnumerable<Tbl03Regnum> ListTbl03RegnumsByFilterTextAboutAllFields(string filterText);
        IEnumerable<Tbl03Regnum> ListRegnumsBySearchName(object searchName);
    }
}
