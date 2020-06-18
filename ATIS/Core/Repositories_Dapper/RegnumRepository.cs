using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ATIS.Dal.Models;
using Dapper;

namespace ATIS.Ui.Core.Repositories_Dapper
{
    public class RegnumRepository : RepositoryBase<Tbl03Regnum>
    {
        public override void Insert(Tbl03Regnum regnum)
        {
            var regnumSql = @"insert into Tbl03Regnums VALUES (" +
                            regnum.RegnumId + ", '" + regnum.RegnumName + "', " + Convert.ToInt32(regnum.CountId) + ");";
            _connection.Query(regnumSql);

        }

        public override void Update(Tbl03Regnum regnum)
        {
            var regnumSql = @"update Tbl03Regnums set RegnumName = '" + regnum.RegnumName + " where Id = " + regnum.RegnumId + ";";
            _connection.Query(regnumSql);
        }

        public override void Delete(Tbl03Regnum regnum)
        {
            var regnumSql = @"delete from Tbl03Regnums where Id = " + regnum.RegnumId + ";";
            _connection.Query(regnumSql);
        }

        public override Tbl03Regnum GetById(int regnumId)
        {
            return _connection.Query<Tbl03Regnum>("select * from Tbl03Regnums where RegnumId = " + regnumId + ";").FirstOrDefault();
        }

        public IEnumerable<Tbl03Regnum> GetAll()
        {
            return _connection.Query<Tbl03Regnum>("select * from Tbl03Regnums");
        }

    }
}
