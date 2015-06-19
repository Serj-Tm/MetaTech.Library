using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetaTech.Library
{
  class Program
  {
    static void Main(string[] args)
    {
      if (true)
      {
        ImmutableArray<int>? items = null;
        var items2 = items.Else_Empty();
        foreach (var item in items2)
        {
          Console.WriteLine(item);
        }
      }
      if (true)
      {
        ImmutableList<int> items = null;
        var items2 = items.Else_Empty();
        foreach (var item in items2)
        {
          Console.WriteLine(item);
        }
      }
    }
  }
}
