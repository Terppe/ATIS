using System;
using System.Collections.Generic;
using System.Text;
using ATIS.Ui.Core.Interfaces_Dapper;
using Dapper;
using Microsoft.Data.SqlClient;

namespace ATIS.Ui.Core.Repositories_Dapper
{
    public abstract class RepositoryBase<T> : IRepository<T>, IDisposable
    {


        /// <summary>
        /// Just using a SQLite database for the purposes of this demo.
        /// </summary>
        protected SqlConnection _connection = new SqlConnection(@"Data Source=.\SQLEXPRESS;Initial Catalog=ATIS35;Integrated Security=True;");
        private bool _disposed;
        /// <summary>
        /// Opens a connection for each object created.
        /// </summary>
        protected internal RepositoryBase()
        {
            _connection.Open();
        }
        public abstract void Delete(T entity);
        public abstract T GetById(int id);
        public abstract void Insert(T entity);
        public abstract void Update(T entity);
        /// <summary>
        /// Cleans up after a transaction is complete.
        /// </summary>
        public void Commit()
        {
        }
        public void Dispose()
        {
            Disposable(true);
            _connection.Dispose();
            GC.SuppressFinalize(this);
        }

        private void Disposable(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                }

                _disposed = true;
            }
        }

        public void CreateDatabase()
        {
            const string sqlCmd = @"create table Dogs (Id int PRIMARY KEY, Name varchar(50), Breed varchar(50)); "
                                  + "create table Cats (Id int PRIMARY KEY, Name varchar(50), ShortHair bit); "
                                  + "insert into Dogs VALUES (1, 'Fido', 'Chocolate Lab');"
                                  + "insert into Dogs VALUES (2, 'Spot', 'Dalmation');"
                                  + "insert into Cats VALUES (1, 'Mittens', 1);"
                                  + "insert into Cats VALUES (2, 'Mr. Floof', 0);";
            _connection.Query(sqlCmd);
        }

    }
}
