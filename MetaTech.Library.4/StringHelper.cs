using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MetaTech.Library
{
  static public partial class StringHelper
  {
    public static string JoinToString(this IEnumerable<string> items, string separator)
    {
      return string.Join(separator, items.ToArray());
    }

    public static bool IsNullOrEmpty(this string value)
    {
      return string.IsNullOrEmpty(value);
    }
    /// <summary>
    /// Пустая строка ("") преобразуется в null-строку(null)
    /// </summary>
    public static string EmptyAsNull(this string value)
    {
      if (value == "")
        return null;
      return value;
    }


    public static string Else_Empty(this string s)
    {
      return s ?? "";
    }

    public static bool All(this string s, Func<char, bool> f)
    {
      for (var i = 0; i < s.Length; ++i)
        if (!(f(s[i])))
          return false;
      return true;
    }
  }
}
