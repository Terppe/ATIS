using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ATIS.Dal.Models;
using ATIS.Ui.Helper;
using Dapper;

namespace ATIS.Ui.Core.Repositories_Dapper
{
    public class PhylumRepository : RepositoryBase<Tbl06Phylum>
    {
        public override void Insert(Tbl06Phylum entity)
        {
            string insertQuery = @"INSERT INTO [dbo].[Tbl06Phylums]([PhylumName], [RegnumId], [CountId], [Valid], [ValidYear], [Synonym], [Author], [AuthorYear], [Info], [EngName], [GerName], [FraName], [PorName], [Writer], [WriterDate], [Updater], [UpdaterDate], [Memo]) 
                        VALUES (@PhylumName, @RegnumId, @CountId, @Valid, @ValidYear, @Synonym, @Author, @AuthorYear, @Info, @EngName, @GerName, @FraName, @PorName, @Writer, @WriterDate, @Updater, @UpdaterDate, @Memo)";

            _connection.Execute(insertQuery, new
            {
                entity.PhylumName,
                entity.RegnumId,
                CountId = RandomHelper.Randomnumber(),
                entity.Valid,
                entity.ValidYear,
                entity.Synonym,
                entity.Author,
                entity.AuthorYear,
                entity.Info,
                entity.EngName,
                entity.GerName,
                entity.FraName,
                entity.PorName,
                Writer = Environment.UserName,
                WriterDate = DateTime.Now,
                Updater = Environment.UserName,
                UpdaterDate = DateTime.Now,
                entity.Memo,
            });
        }

        public override void Update(Tbl06Phylum entity)
        {
            var updateQuery = @"UPDATE [dbo].[Tbl06Phylums] SET PhylumName = @PhylumName, RegnumId = @RegnumId, Valid = @Valid, ValidYear = @ValidYear, Synonym = @Synonym, Author = @Author, AuthorYear = @AuthorYear, Info = @Info, EngName = @EngName, GerName = @GerName, FraName = @FraName, PorName = @PorName, Updater = @Updater, UpdaterDate = @UpdaterDate, Memo = @Memo WHERE PhylumId = @PhylumId";

            _connection.Execute(updateQuery, new
            {
                entity.PhylumName,
                entity.RegnumId,
                entity.Valid,
                entity.ValidYear, 
                entity.Synonym,
                entity.Author,
                entity.AuthorYear,
                entity.Info,
                entity.EngName,
                entity.GerName,
                entity.FraName,
                entity.PorName,
                Updater = Environment.UserName,
                UpdaterDate = DateTime.Now,
                entity.Memo,
                entity.PhylumId
            });
        }

        public override void Delete(Tbl06Phylum entity)
        {
            var phylumSql = @"delete from Tbl06Phylums where PhylumId = " + entity.PhylumId + ";";
            _connection.Query(phylumSql);
        }

        public override Tbl06Phylum GetById(int id)
        {
            return _connection.Query<Tbl06Phylum>("select * from Tbl06Phylums where PhylumId = " + id + ";").FirstOrDefault();
        }

        public IEnumerable<Tbl06Phylum> GetAll()
        {
            return _connection.Query<Tbl06Phylum>("select * from Tbl06Phylums");
        }

    }
}
