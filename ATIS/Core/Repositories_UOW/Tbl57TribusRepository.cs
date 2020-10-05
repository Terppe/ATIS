using System;
using System.Collections.Generic;
using System.Text;
using ATIS.Dal.Models;
using ATIS.Ui.Core.Interfaces_UOW;
using Microsoft.EntityFrameworkCore;

namespace ATIS.Ui.Core.Repositories_UOW
{
    public class Tbl57TribusRepository : Repository<Tbl57Tribus>, ITbl57TribusRepository
    {
        private readonly AtisDbContext _atisDbContext;

        public Tbl57TribusRepository(AtisDbContext context) : base(context)
        {
            _atisDbContext = context;

        }
    }
}
