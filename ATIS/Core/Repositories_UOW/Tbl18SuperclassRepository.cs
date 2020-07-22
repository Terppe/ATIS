using System;
using System.Collections.Generic;
using System.Text;
using ATIS.Dal.Models;
using ATIS.Ui.Core.Interfaces_UOW;
using Microsoft.EntityFrameworkCore;

namespace ATIS.Ui.Core.Repositories_UOW
{
    public class Tbl18SuperclassRepository : Repository<Tbl18Superclass>, ITbl18SuperclassRepository
    {
        private readonly AtisDbContext _atisDbContext;

        public Tbl18SuperclassRepository(AtisDbContext context) : base(context)
        {
            _atisDbContext = context;
        }
    }
}
