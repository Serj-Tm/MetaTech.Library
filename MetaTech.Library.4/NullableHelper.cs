using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MetaTech.Library
{
  public static class NullableHelper
  {
// ReSharper disable InconsistentNaming
// ReSharper disable CompareNonConstrainedGenericWithNull

    [System.Diagnostics.DebuggerStepThrough]
    public static TValue _f<TItem, TValue>(this TItem item, Func<TItem, TValue> getter)
    {
      if (item == null || getter == null)
        return default(TValue);
      return getter(item);
    }
    [System.Diagnostics.DebuggerStepThrough]
    public static TValue? _fv<TItem, TValue>(this TItem item, Func<TItem, TValue> getter)
      where TValue : struct
    {
      if (item == null || getter == null)
        return null;
      return getter(item);
    }
    [System.Diagnostics.DebuggerStepThrough]
    public static void _f<TItem>(this TItem item, Action<TItem> getter)
    {
      if (item == null || getter == null)
        return;
      getter(item);
    }

    //public static TValue _n<TItem, TValue>(this TItem? item, Func<TItem, TValue> getter) where TItem : struct
    //{
    //  if (item == null || getter == null)
    //    return default(TValue);
    //  return getter(item.Value);
    //}
    //public static TValue? _nv<TItem, TValue>(this TItem? item, Func<TItem, TValue> getter)
    //  where TItem : struct 
    //  where TValue : struct
    //{
    //  if (item == null || getter == null)
    //    return null;
    //  return getter(item.Value);
    //}
    //public static void _n<TItem>(this TItem? item, Action<TItem> getter) where TItem:struct
    //{
    //  if (item == null || getter == null)
    //    return;
    //  getter(item.Value);
    //}
// ReSharper restore CompareNonConstrainedGenericWithNull
// ReSharper restore InconsistentNaming

  }
}
