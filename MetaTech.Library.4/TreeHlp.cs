using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace MetaTech.Library
{
  public static class TreeHlp
  {
    public static TreeNode<TTargetItem> Select<TTargetItem, TSourceItem>(TTargetItem parent, TSourceItem root, Func<TTargetItem, TSourceItem, TTargetItem> converter,
      Func<TSourceItem, IEnumerable<TSourceItem>> childs)
    {
      var target = converter(parent, root);
      return new TreeNode<TTargetItem>(target,
        childs(root)
       .Else_Empty()
       .Select(item => Select(target, item, converter, childs))
       .ToArray()
      );
    }
    public static IEnumerable<PlainTreeNode<TItem>> PlainTree<TItem>(TItem root, Func<TItem, IEnumerable<TItem>> childs)
    {
      var stack = new Stack<PlainTreeNode<TItem>>();
      var rootNode = new PlainTreeNode<TItem>(root, null, 0);
      yield return rootNode;
      stack.Push(rootNode);

      for (; stack.Count > 0; )
      {
        var node = stack.Pop();
        foreach (var child in childs(node.Item))
        {
          var childNode = new PlainTreeNode<TItem>(child, node, node.Level + 1);
          yield return childNode;
          stack.Push(childNode);
        }
      }

    }
  }
  public class TreeNode<TItem>
  {
    public TreeNode(TItem item, TreeNode<TItem>[] childs)
    {
      this.Item = item;
      this.Childs = childs;
    }
    public readonly TItem Item;
    //public readonly TreeNode<TItem> Parent;
    public readonly TreeNode<TItem>[] Childs;
  }
  public class PlainTreeNode<TItem>
  {
    public PlainTreeNode(TItem item, PlainTreeNode<TItem> parent, int level)
    {
      this.Level = level;
      this.Item = item;
      this.ParentNode = parent;
    }
    public readonly TItem Item;
    public readonly PlainTreeNode<TItem> ParentNode;
    public readonly int Level;
  }
}
