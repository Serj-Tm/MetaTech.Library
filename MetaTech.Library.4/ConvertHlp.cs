﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MetaTech.Library
{
  public class ConvertHlp
  {
    [System.Diagnostics.DebuggerStepThrough]
    public static double? ToDouble(object value)
    {
      try
      {
        if (value == null || value is DBNull)
          return null;
        if (value is string)
        {
          string s = (string)value;
          return double.Parse(s.Replace(',', '.'), System.Globalization.CultureInfo.InvariantCulture);
        }
        return Convert.ToDouble(value);
      }
      catch (Exception)
      {
        //    TraceHlp2.WriteException(exc);
      }
      return null;
    }

    [System.Diagnostics.DebuggerStepThrough]
    public static Guid? ToGuid(object value)
    {
      try
      {
        if (value == null || value is DBNull)
          return null;
        if (value is Guid)
          return (Guid)value;
        else if (value is byte[])
          return new Guid((byte[])value);
        else if (value is string)
        {
          var s = (string)value;
          if (s == "")
            return null;
          return new Guid(s);
        }
        else
          return new Guid(value.ToString());
      }
      catch (Exception)
      {
      }
      return null;
    }

    [System.Diagnostics.DebuggerStepThrough]
    public static int? ToInt(object value)
    {
      try
      {
        if (value == null || value == DBNull.Value)
          return null;
        return Convert.ToInt32(value);
      }
      catch (Exception)
      {
      }
      return null;
    }

    [System.Diagnostics.DebuggerStepThrough]
    public static long? ToLong(object value)
    {
      try
      {
        if (value == null || value == DBNull.Value)
          return null;
        return Convert.ToInt64(value);
      }
      catch (Exception)
      {
      }
      return null;
    }

    [System.Diagnostics.DebuggerStepThrough]
    public static T? ToEnum<T>(object value) where T : struct
    {
      var text = value.ToString_Fair();
      if (text == null)
        return null;
      try
      {
        return (T)Enum.Parse(typeof(T), text, true);
      }
      catch
      {
        return null;
      }
    }
  }
}
