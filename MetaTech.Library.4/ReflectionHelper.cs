using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace MetaTech.Library.ReflectionExtension
{
  /// <summary>
  /// Предоставляет для каждого объекта удобные методы для доступа через reflection: _P, _M
  /// </summary>
  static public class ReflectionHelper
  {
    /// <summary>
    /// Доступ к свойству или полю через reflection
    /// </summary>
    public static T _P<T>(this object item, string name)
    {
      if (item == null)
        return default(T);
      var type = item.GetType();

      var members = type.GetMembers(BindingFlags.GetField | BindingFlags.GetProperty | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
        .Where(_member => _member.Name == name)
        .ToArray();
      if (members.Length == 0)
        return default(T);
      if (members.Length > 1)
        throw new Exception(string.Format("У объекта полей/свойств с именем '{0}' больше чем один: '{1}'", name, members.Length));
      var member = members.First();
      object result;
      if (member is FieldInfo)
        result = ((FieldInfo)member).GetValue(item);
      else
        result = ((PropertyInfo)member).GetValue(item, null);
      if (result is T)
        return (T)result;
      return default(T);
    }
    /// <summary>
    /// Доступ к свойству или полю через reflection
    /// Кидает исключение, если типы не совпадут
    /// </summary>
    public static void _P<T>(this object item, string name, T value)
    {
      if (item == null)
        return;
      var type = item.GetType();

      var members = type.GetMembers(BindingFlags.GetField | BindingFlags.GetProperty | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
        .Where(_member => _member.Name == name)
        .ToArray();
      if (members.Length == 0)
        return;
      if (members.Length > 1)
        throw new Exception(string.Format("У объекта полей/свойств с именем '{0}' больше чем один: '{1}'", name, members.Length));
      var member = members.First();
      if (member is FieldInfo)
        ((FieldInfo)member).SetValue(item, value);
      else
        ((PropertyInfo)member).SetValue(item, value, null);
    }
    /// <summary>
    /// Доступ к методу через reflection
    /// </summary>
    public static void _M(this object item, string name, params object[] args)
    {
      _M<object>(item, name, args);
    }
    /// <summary>
    /// Доступ к методу через reflection
    /// </summary>
    public static T _M<T>(this object item, string name, params object[] args)
    {
      if (item == null)
        return default(T);
      var type = item.GetType();

      var methods = type.GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
        .Where(_member => _member.Name == name)
        .ToArray();
      if (methods.Length == 0)
        return default(T);
      if (methods.Length > 1)
        throw new Exception(string.Format("Вызов перегруженных методов не поддерживается, у объекта методов с именем '{0}' больше чем один: '{1}'.", name, methods.Length));
      var method = methods.First();
      var result = method.Invoke(item, args);
      if (result is T)
        return (T)result;
      return default(T);
    }
  }
}
