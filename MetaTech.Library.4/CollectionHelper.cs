using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MetaTech.Library
{
  public static partial class CollectionHelper
  {
    public static IEnumerable<TItem> Else_Empty<TItem>(this IEnumerable<TItem> items)
    {
      if (items == null)
        return Array<TItem>.Empty;
      return items;
    }
    public static ICollection<TItem> Else_Empty<TItem>(this ICollection<TItem> items)
    {
      if (items == null)
        return Array<TItem>.Empty;
      return items;
    }
    public static IList<TItem> Else_Empty<TItem>(this IList<TItem> items)
    {
      if (items == null)
        return Array<TItem>.Empty;
      return items;
    }
    public static TItem[] Else_Empty<TItem>(this TItem[] items)
    {
      if (items == null)
        return Array<TItem>.Empty;
      return items;
    }
    public static List<TItem> Else_Empty<TItem>(this List<TItem> items)
    {
      if (items == null)
        return new List<TItem>();
      return items;
    }

    public static IEnumerable<TItem> RemoveNulls<TItem>(this IEnumerable<TItem?> items) where TItem:struct
    {
      if (items != null)
        foreach (var item in items)
          if (item != null)
            yield return item.Value;
    }
    public static Dictionary<TKey, TValue[]> ToDictionaryGrouped<TKey, TValue>(this IEnumerable<IGrouping<TKey, TValue>> groups)
    {
      return groups.ToDictionary(group => group.Key,
        group => group.ToArray());
    }

    public static T MinObject<T>(this IEnumerable<T> items, Func<T, IComparable> keyer) where T : class
    {
      IComparable min = null;
      T minItem = null;
      foreach (var item in items)
      {
        var value = keyer(item);
        if (min == null || value.CompareTo(min) < 0)
        {
          min = value;
          minItem = item;
        }
      }
      return minItem;
    }
    public static T? MinValue<T>(this IEnumerable<T> items, Func<T, IComparable> keyer) where T : struct
    {
      IComparable min = null;
      T? minItem = null;
      foreach (var item in items)
      {
        var value = keyer(item);
        if (min == null || value.CompareTo(min) < 0)
        {
          min = value;
          minItem = item;
        }
      }
      return minItem;
    }

    public static T MaxObject<T>(this IEnumerable<T> items, Func<T, IComparable> keyer) where T : class
    {
      IComparable max = null;
      T maxItem = null;
      foreach (var item in items)
      {
        var value = keyer(item);
        if (max == null || value.CompareTo(max) > 0)
        {
          max = value;
          maxItem = item;
        }
      }
      return maxItem;
    }
    public static T? MaxValue<T>(this IEnumerable<T> items, Func<T, IComparable> keyer) where T : struct
    {
      IComparable max = null;
      T? maxItem = null;
      foreach (var item in items)
      {
        var value = keyer(item);
        if (max == null || value.CompareTo(max) > 0)
        {
          max = value;
          maxItem = item;
        }
      }
      return maxItem;
    }
    public static IEnumerable<TItem> DistinctBy<TItem, TKey>(this IEnumerable<TItem> items, Func<TItem, TKey> keyer)
    {
      return items
        .GroupBy(keyer)
        .Select(group => group.First());
    }

    static readonly Random random = new Random();
    public static T ElementAtRandomOrDefault<T>(this IList<T> items, Random _random = null)
    {
      if (items.Count == 0)
        return default(T);
      return items[_random.Else_Default(random).Next(items.Count)];
    }

    public static IEnumerable<TResult> OuterJoin<TLeft, TRight, TKey, TResult>(this IEnumerable<TLeft> lefts, IEnumerable<TRight> rights, Func<TLeft, TKey> leftKeyer, Func<TRight, TKey> rightKeyer, Func<TLeft, TRight, TResult> result)
    {
      var rightIndex = rights.GroupBy(rightKeyer).ToDictionary(group => group.Key, group => group.First());
      var rightMarkers = new Dictionary<TKey, bool>();
      foreach (var left in lefts)
      {
        TRight right;
        var key = leftKeyer(left);
        if (rightIndex.TryGetValue(key, out right))
        {
          yield return result(left, right);
          rightMarkers[key] = true;
        }
        else
          yield return result(left, default(TRight));
      }
      foreach (var right in rights.Where(_right => !rightMarkers.ContainsKey(rightKeyer(_right))))
      {
        yield return result(default(TLeft), right);
      }
    }

    public static IEnumerable<SplitGroup<T, TKey>> SplitBy<T, TKey>(this IEnumerable<T> items, Func<T, TKey> keyer)
    {
      var prevs = new List<T>();
      var prevKey = default(TKey);
      foreach (var item in items)
      {
        var key = keyer(item);
        if (prevs.Count != 0)
        {
          if (!object.Equals(prevKey, key))
          {
            yield return new SplitGroup<T, TKey>(prevKey, prevs.ToArray());
            prevs.Clear();
            prevKey = key;
          }
        }
        else
        {
          prevKey = key;
        }
        prevs.Add(item);
      }
      if (prevs.Count > 0)
        yield return new SplitGroup<T, TKey>(prevKey, prevs.ToArray());
    }
  }
  public class SplitGroup<T, TKey>
  {
    public SplitGroup(TKey key, T[] items)
    {
      this.Key = key;
      this.Items = items;
    }
    public readonly TKey Key;
    public readonly T[] Items;
  }

  public static class Array<TItem>
  {
    // ReSharper disable StaticFieldInGenericType
    public static readonly TItem[] Empty = new TItem[] { };
    // ReSharper restore StaticFieldInGenericType
  }
}
