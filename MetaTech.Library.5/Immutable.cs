using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetaTech.Library
{
    public static class Immutable
    {
      public static ImmutableArray<T> Else_Empty<T>(this ImmutableArray<T>? items)
      {
        return items ?? ImmutableArray<T>.Empty;
      }
      public static ImmutableList<T> Else_Empty<T>(this ImmutableList<T> items)
      {
        return items ?? ImmutableList<T>.Empty;
      }
    }
}
