using System;
using System.Collections.Generic;
using System.Text;
using ATIS.Dal.Models;
using ATIS.Ui.Core.Interfaces_UOW;
using Microsoft.EntityFrameworkCore;

namespace ATIS.Ui.Core.Repositories_UOW
{
    public class Tbl12SubphylumRepository : Repository<Tbl12Subphylum>, ITbl12SubphylumRepository
    {
        private readonly AtisDbContext _atisDbContext;

        public Tbl12SubphylumRepository(AtisDbContext context) : base(context)
        {
            _atisDbContext = context;

        }
    }
}
