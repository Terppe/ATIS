using System;
using System.Collections.Generic;
using System.Text;

namespace ATIS.Dal.Models
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
