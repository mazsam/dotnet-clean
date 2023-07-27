using System;
namespace VendorBoilerplate.Application.Misc
{
  public class Utils
  {
    public static long DateToTimestamps(DateTime tm)
    {
      return (tm.Ticks - 621355968000000000) / 10000000  * 1000;
    }
  }
}