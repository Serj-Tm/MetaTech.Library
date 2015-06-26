
namespace MetaTech.Library
{
  public static class ApplicationHlp
  {
    public static string ExecutablePath
    {
     get { return System.Reflection.Assembly.GetExecutingAssembly().Location; }
    }

    public static string StartupPath
    {
      get { return System.IO.Path.GetDirectoryName(ExecutablePath); }
    }

    public static string MapPath(string filename)
    {
      if (filename == null)
        return StartupPath;
      return System.IO.Path.Combine(StartupPath, filename);
    }
  }
}
