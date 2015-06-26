using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace MetaTech.Library
{
  public static class XLinqHelper
  {
    public static string Value_Fair(this XAttribute attribute)
    {
      if (attribute == null)
        return null;
      return attribute.Value;
    }
    public static string Attribute_Get(this XElement element, XName name)
    {
      if (element == null)
        return null;
      return element.Attribute(name).Value_Fair();
    }
    public static string Value_Get(this XElement element)
    {
      if (element == null)
        return null;
      return element.Value;
    }
    public static void Attribute_Set(this XElement element, XName name, string value)
    {
      if (element == null)
        return;
      var attribute = element.Attribute(name);
      var currentValue = attribute.Value_Fair();
      if (currentValue != value)
      {
        if (value == null)
        {
          if (attribute != null)
            attribute.Remove();
        }
        else
        {
          if (attribute != null)
            attribute.Value = value;
          else
            element.Add(new XAttribute(name, value));
        }
      }
    }
    public static string InnerXml(this XElement element)
    {
      if (element == null)
        return null;
      var reader = element.CreateReader();
      reader.MoveToContent();
      return reader.ReadInnerXml();
    }

    public static string InnerText(this XElement element)
    {
      if (element == null)
        return null;
      return element.DescendantNodesAndSelf()
        .Where(node => node.NodeType == System.Xml.XmlNodeType.Text)
        .Select(node => node.ToString())
        .JoinToString(" ");
    }

  }
}
