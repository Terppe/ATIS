using System;

namespace ATIS.Ui.Helper
{
    public abstract class RandomHelper
    {
        public static int Randomnumber()
        {
            var rnum = new Random();
            return rnum.Next(99, 999999999);
        }
    }
}
