using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace MetaTech.Library
{
  public static class TopologicSorter
  {
    public static IEnumerable<TItem> TopologicSort<TItem>(this IEnumerable<TItem> items, Func<TItem, TItem[]> getDependencies)
    {
      return TopologicSort(UnwindDependencies(items, getDependencies));
    }
    ////Wikipedia топологическая сортировка
    //public static IEnumerable<TItem> TopologicSort<TItem>(this Dictionary<TItem, TItem[]> edges)
    //{
    //  var matrix = edges.Select(pair => new { Item = pair.Key, Dependencies = new Set<TItem>(pair.Value) }).ToArray();

    //  var resultSet = new Set<TItem>();
    //  var result = new List<TItem>(matrix.Length);

    //  while (result.Count < edges.Count)
    //  {
    //    var nextPair = matrix.Where(pair => !resultSet.Contains(pair.Item)).FirstOrDefault(pair => pair.Dependencies.Count == 0);
    //    if (nextPair == null)
    //      throw new Exception(string.Format("Есть циклические зависимости между следующими элементами: {0}",
    //        matrix
    //         .Where(pair => !resultSet.Contains(pair.Item))
    //         .Select(pair => string.Format("'{0}'", pair.Item))
    //         .JoinToString(", ")));
    //    result.Add(nextPair.Item);
    //    resultSet.Add(nextPair.Item);
    //    foreach (var pair in matrix)
    //      pair.Dependencies.Remove(nextPair.Item);
    //  }
    //  return result;
    //}
    //Wikipedia топологическая сортировка
    //идея реализации: http://www.brpreiss.com/books/opus6/html/page559.html#SECTION0017331000000000000000
    public static IEnumerable<TItem> TopologicSort<TItem>(this Dictionary<TItem, TItem[]> edges)
    {
      var items = edges.Keys.ToArray();
      var itemIndex = edges.Keys.Select((edge, index) => new { Edge = edge, Index = index })
        .ToDictionary(indexedEdge => indexedEdge.Edge, indexedEdge => indexedEdge.Index);

      var reverseEdges = edges.SelectMany(edge => edge.Value.Select(target => new { Source = edge.Key, Target = target }))
        .GroupBy(edge => edge.Target, edge => edge.Source)
        .ToDictionary(group => group.Key, group => group.ToArray());

      var degree = new int[itemIndex.Count];
      foreach (var edge in reverseEdges)
        foreach (var source in edge.Value)
        {
          var index = itemIndex.FindValue(source);
          if (index != null)
            ++degree[index.Value];
        }

      var queue = new Queue<TItem>();
      for (int i = 0; i < degree.Length; ++i)
      {
        if (degree[i] == 0)
          queue.Enqueue(items[i]);
      }
      while (queue.Count > 0)
      {
        var item = queue.Dequeue();
        yield return item;
        foreach (var source in reverseEdges.Find(item).Else_Empty())
        {
          var index = itemIndex.FindValue(source);
          if (index != null)
          {
            if (--degree[index.Value] == 0)
              queue.Enqueue(source);
          }
        }
      }

      var cycledItems = items.Select((item, index) => new { Item = item, Index = index })
        .Where(indexedItem => degree[indexedItem.Index] != 0)
        .Select(indexedItem => indexedItem.Item)
        .ToArray();

      if (cycledItems.Length > 0)
      {
        throw new Exception(string.Format("Есть циклические зависимости между следующими элементами: {0}",
          cycledItems
           .Select(item => string.Format("'{0}'", item))
           .JoinToString(", ")));
      }
    }
    public static Dictionary<TItem, TItem[]> UnwindDependencies<TItem>(IEnumerable<TItem> items, Func<TItem, TItem[]> getDependencies)
    {
      var result = new Dictionary<TItem, TItem[]>();
      var queue = new Queue<TItem>(items);
      while (queue.Count > 0)
      {
        var item = queue.Dequeue();
        if (result.ContainsKey(item))
          continue;
        var dependencies = getDependencies(item);
        result[item] = dependencies;
        foreach (var dependency in dependencies)
          queue.Enqueue(dependency);
      }
      return result;
    }

  }
}
