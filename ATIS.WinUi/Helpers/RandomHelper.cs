namespace ATIS.WinUi.Helpers;
public class RandomHelper
{
    public static int Randomnumber()
    {
        var rnum = new Random();
        return rnum.Next(99, 999999999);
    }
}
