using System;
using System.Collections.Generic;
using System.Text;

namespace ATIS.DAL.Models
{
    public interface IEntity
    {
        EntityState EntityState { get; set; }
    }

    public enum EntityState
    {
        Unchanged,
        Added,
        Modified,
        Deleted
    }
}
