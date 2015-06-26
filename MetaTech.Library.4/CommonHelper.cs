using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MetaTech.Library
{
  public static class CommonHelper
  {
    /// <summary>
    /// Используется вместо ??, т.к. у операции ?? низкий приоритет, и приходится неудобные скобки расставлять
    /// </summary>
    public static TItem Else_Default<TItem>(this TItem? item, TItem defaultValue) where TItem : struct
    {
      if (item == null)
        return defaultValue;
      return item.Value;
    }
    /// <summary>
    /// Используется вместо ??, т.к. у операции ?? низкий приоритет, и приходится неудобные скобки расставлять
    /// </summary>
    public static TItem Else_Default<TItem>(this TItem item, TItem defaultValue) where TItem : class
    {
      if (item == null)
        return defaultValue;
      return item;
    }

    /// <summary>
    /// Используется вместо as, т.к. у операции as низкий приоритет, и приходится неудобные скобки расставлять
    /// </summary>
    public static TItem As<TItem>(this object item) //where TItem : class
    {
      if (item is TItem)
        return (TItem) item;
      return default(TItem);
    }


    /// <summary>
    /// Преобразует в строку, и для null-я возвращает null
    /// </summary>
    public static string ToString_Fair<T>(this T item)
    {
// ReSharper disable CompareNonConstrainedGenericWithNull
      if (item == null)
        return null;
      return item.ToString();
// ReSharper restore CompareNonConstrainedGenericWithNull
    }

    public static TItem? AsNullable<TItem>(this TItem item) where TItem : struct
    {
      return item;
    }




  }


}
