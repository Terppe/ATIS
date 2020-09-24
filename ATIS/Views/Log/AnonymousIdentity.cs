using System;
using System.Collections.Generic;
using System.Text;

namespace ATIS.Ui.Views.Log
{
    public class AnonymousIdentity : CustomIdentity
    {
        public AnonymousIdentity()
            : base(string.Empty, string.Empty)
        { }
    }
}
