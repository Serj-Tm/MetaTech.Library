using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MetaTech.Library
{
  public static class FlowHelper
  {
    public static void MultiTry(this Action block, int maxTry = 3)
    {
      foreach (var tryIndex in Enumerable.Range(0, maxTry))
      {
        try
        {
          block();
          break;
        }
        catch (Exception exc)
        {
          TraceHlp.WriteException(exc);
          if (tryIndex == maxTry - 1)
            throw;
        }
      }
    }
    public static T MultiTry<T>(this Func<T> block, int maxTry = 3)
    {
      foreach (var tryIndex in Enumerable.Range(0, maxTry))
      {
        try
        {
          return block();
        }
        catch (Exception exc)
        {
          TraceHlp.WriteException(exc);
          if (tryIndex == maxTry - 1)
            throw;
        }
      }
      return default(T);
    }

  }
}
