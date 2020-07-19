using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using ATIS.Dal.Models;
using ATIS.Ui.Core.Repositories_UOW;

namespace ATIS.Ui.Core.Interfaces_UOW
{
    public interface ITbl90RefAuthorRepository : IRepository<Tbl90RefAuthor>
    {

        IEnumerable<Tbl90RefAuthor> ListTbl90RefAuthorsToCombobox();

 
    }
}
