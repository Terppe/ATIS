using System.Collections.Generic;
using ATIS.DAL.Models;

namespace ATIS.Ui.Core.Interfaces_UOW
{
    public interface ITbl03RegnumRepository : IRepository<Tbl03Regnum>
    {
        IEnumerable<Tbl03Regnum> GetBestRegnums(int countRegnum);

        IEnumerable<Tbl03Regnum> ListTbl03RegnumsByFilterTextAboutAllFields(string filterText);
        IEnumerable<Tbl03Regnum> ListRegnumsBySearchName(object searchName);
    }
}
