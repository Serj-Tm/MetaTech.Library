using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Reflection;
using System.IO;


namespace MetaTech.Library
{
  /// <summary>
  /// Класс помощи для работы с Trace (сообщения, подписчики)
  /// </summary>
  public class TraceHlp
  {
    /// <summary>
    /// Пишет в лог форматированный текст, при этом не выкидывает наверх исключение, 
    /// если есть ошибки в формате
    /// </summary>
    /// <param name="formatText"></param>
    /// <param name="args"></param>
    public static void AddMessage(string formatText, params object[] args)
    {
      try
      {
        //Trace.WriteLine(getFormatString(format, args)); //string.Format(format, args));
        Trace.WriteLine(CreateDefaultTraceMessage(new StackTrace()).WithText(getFormatString(formatText, args)));
      }
      catch
      {
      }
    }

    public static bool IsTraceMessageWithMethod = true;
    public static bool IsTraceMessageWithThread = true;
    public static void IsTraceMessageWithoutExtraInfo()
    {
      IsTraceMessageWithMethod = false;
      IsTraceMessageWithThread = false;
    }

    private static TraceMessage CreateDefaultTraceMessage(StackTrace trace)
    {
      TraceMessage message = new TraceMessage();
      if (TraceHlp.IsTraceMessageWithMethod && trace.FrameCount >= 2)
        message.WithMethod(trace.GetFrame(1).GetMethod());
      if (TraceHlp.IsTraceMessageWithThread)
        message.WithThread();
      return message;
    }
    private static string getFormatString(string format, params object[] args)
    {
      try
      {
        if (args == null || args.Length == 0)
          return format;
        return string.Format(format, args);
      }
      catch (Exception exc)
      {
        return string.Format(@"Ошибка форматирования: '{1}' в '{0}', 
{2}", format, exc.Message, new StackTrace());
      }
    }
    public static void WriteMethod()
    {
      try
      {
        Trace.WriteLine(CreateDefaultTraceMessage(new StackTrace()));
      }
      catch
      {
      }
    }
    public static void WriteMethod(object obj)
    {
      try
      {
        Trace.WriteLine(CreateDefaultTraceMessage(new StackTrace()).WithHash(obj != null ? obj.GetHashCode() : 0));
      }
      catch
      {
      }
    }
    public static void WriteMethod(object obj, string format, params object[] args)
    {
      try
      {
        Trace.WriteLine(CreateDefaultTraceMessage(new StackTrace()).WithHash(obj != null ? obj.GetHashCode() : 0)
          .WithText(getFormatString(format, args)));
      }
      catch
      {
      }
    }
    [Obsolete("Используйте WriteMethodWithArgs")]
    public static void WriteArgs(params object[] args)
    {
      try
      {
        Trace.WriteLine(CreateDefaultTraceMessage(new StackTrace()).WithArgs(args));
      }
      catch
      {
      }
    }
    public static void WriteMethodWithArgs(params object[] args)
    {
      try
      {
        Trace.WriteLine(CreateDefaultTraceMessage(new StackTrace()).WithArgs(args));
      }
      catch
      {
      }
    }
    public static void WriteException(Exception exc)
    {
      try
      {
        Trace.WriteLine(CreateDefaultTraceMessage(new StackTrace()).WithExc(exc));
      }
      catch (Exception exc2)
      {
        Console.Error.WriteLine(exc2);
      }
    }
    public static void WriteException(Exception exc, object obj)
    {
      try
      {
        Trace.WriteLine(CreateDefaultTraceMessage(new StackTrace()).WithExc(exc)
          .WithHash(obj != null ? obj.GetHashCode() : 0));
      }
      catch
      {
      }
    }
    public static void WriteException(Exception ex, string formatText, params object[] args)
    {
      try
      {
        Trace.WriteLine(CreateDefaultTraceMessage(new StackTrace()).
          WithText(string.Format("{0}{1}{2}", getFormatString(formatText, args),
          Environment.NewLine, ex)));
      }
      catch
      {
      }
    }

    public static void Write(string formatText)
    {
      try
      {
        Trace.WriteLine(CreateDefaultTraceMessage(new StackTrace()).WithText(formatText));
        //Write(formatText, null);
      }
      catch
      {
      }
    }

    public static void Write(string formatText, params object[] args)
    {
      try
      {
        //todo добавить время вывода сообщения
        //Trace.WriteLine(getFormatString(formatText, args));
        Trace.WriteLine(CreateDefaultTraceMessage(new StackTrace()).WithText(getFormatString(formatText, args)));
      }
      catch (Exception)
      {
      }
    }
    public static void Write(string text, params TraceArg[] args)
    {
      try
      {
        //todo добавить время вывода сообщения
        StringBuilder builder = new StringBuilder();
        builder.Append(text);
        if (args != null)
        {
          builder.Append(": ");
          bool isSecond = false;
          foreach (TraceArg arg in args)
          {
            if (arg == null)
              continue;
            if (isSecond)
              builder.Append(", ");
            else
              isSecond = true;
            builder.AppendFormat("'{0}'-'{1}'", arg.Name, arg.Value);
            if (arg.MeasuringUnit != null)
              builder.AppendFormat(" {0}", arg.MeasuringUnit);
          }
        }
        //Trace.WriteLine(builder.ToString());
        Trace.WriteLine(CreateDefaultTraceMessage(new StackTrace()).WithText(builder.ToString()));
      }
      catch (Exception)
      {
      }
    }


    public static void MakeConsoleTraceListener()
    {
      Trace.Listeners.Add(new TextWriterTraceListener(Console.Out));
    }
    public static void MakeFileTraceListener()
    {
      Directory.CreateDirectory("Logs");
      Trace.AutoFlush = true;
      Trace.Listeners.Add(GetListener("Logs"));
      Trace.AutoFlush = true;
    }
    public static void MakeFileTraceListener(string dir, string name)
    {
      string path = Path.Combine(dir, "Logs");
      Directory.CreateDirectory(path);
      Trace.AutoFlush = true;
      Trace.Listeners.Add(GetListener(path, name));
    }

    static TextWriterTraceListener GetListener(string dir)
    {
      string name = Path.GetFileNameWithoutExtension(ApplicationHlp.ExecutablePath);
      return GetListener(dir, name);
    }


    static TextWriterTraceListener GetListener(string dir, string name)
    {
      for (int i = 0; i < 10; ++i)
      {
        try
        {
          return new TextWriterTraceListener(Path.Combine(dir, string.Format("{0}.{1}.log", name, i)));
        }
        catch (Exception)
        {
        }
      }
      return new TextWriterTraceListener(Path.Combine (dir, string.Format("{0}.{1}.log", name, DateTime.Now.Ticks)));
    }
    public class TraceArg
    {
      public TraceArg(string name, object value)
        :
        this(name, value , null)
      {
      }
      public TraceArg(string name, object value, string measuringUnit)
      {
        this.Name = name;
        this.Value = value;
        this.MeasuringUnit = measuringUnit;
      }
      public readonly string Name;
      public readonly object Value;
      public readonly string MeasuringUnit;
    }

    public class TraceMessage
    {
      //public TraceMessage(object message, MethodBase method, object[] args, int hash, DateTime messageTime)
      //{
      //  this.message = message;
      //  this._args = args;
      //  this._hash = hash;
      //  this._method = method;
      //  this.messageTime = messageTime;
      //}
      public TraceMessage(object message)
      {
        this.message = message;
      }
      public TraceMessage()
      {
        message = String.Empty;
      }

      public TraceMessage WithTime(DateTime time)
      {
        this.messageTime = time;
        return this;
      }
      public TraceMessage WithMethod(MethodBase method)
      {
        this._method = method;
        return this;
      }
      public TraceMessage WithArgs(params object[] args)
      {
        this._args = args;
        return this;
      }
      public TraceMessage WithHash(int hash)
      {
        this._hash = hash;
        return this;
      }
      public TraceMessage WithExc(Exception exc)
      {
        this.message = exc;
        return this;
      }
      public TraceMessage WithText(string text)
      {
        this.message = text;
        return this;
      }

      public TraceMessage WithThread()
      {
        this.ThreadName = System.Threading.Thread.CurrentThread.Name;
        this.ThreadId = System.Threading.Thread.CurrentThread.ManagedThreadId;
        this.IsWithThread = true;
        return this;
      }

      //public string ThreadName = System.Threading.Thread.CurrentThread.Name;
      //public int ThreadId = System.Threading.Thread.CurrentThread.ManagedThreadId;

      public bool IsWithThread = false;
      public string ThreadName = null;
      public int ThreadId = -1;

      private DateTime messageTime = DateTime.Now;
      public DateTime MessageTime
      {
        get
        {
          return messageTime;
        }
        set
        {
          messageTime = value;
        }
      }
      private object message;
      public object Message
      {
        get
        {
          return message;
        }
        set
        {
          message = value;
        }
      }
      private MethodBase _method = null;
      public MethodBase Method
      {
        get
        {
          return _method;
        }
      }
      private object[] _args = new object[] { };
      public object[] Args
      {
        get
        {
          return _args;
        }
      }
      private int _hash = 0;
      public int Hash
      {
        get
        {
          return _hash;
        }
      }

      string OutputHash
      {
        get
        {
          return (_hash == 0) ? "" : "(" + _hash + ")";
        }
      }

      public override string ToString()
      {
        var builder = new System.Text.StringBuilder();
        if (_method != null && message is Exception)
        {
          builder.AppendFormat("{0}{2}.{1} \r\n",
            _method.ReflectedType.FullName, _method.Name, OutputHash);
        }
        if (_args != null && _args.Length != 0)
        {
          builder.AppendFormat("Аргументы: ");
          foreach (object arg in _args)
          {
            builder.Append((arg == null ? "<null>" : arg.ToString()));
            builder.Append(", ");
          }
          builder.Append("\r\n");
        }
        if (ThreadName != null)
          builder.AppendFormat("{0}: ", ThreadName);
        if (message != null)
          builder.Append(message);
        return builder.ToString();
      }
    }
  }

}



