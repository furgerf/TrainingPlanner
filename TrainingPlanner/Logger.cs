using System;
using System.IO;
using System.Runtime.CompilerServices;

namespace TrainingPlanner
{
  internal static class Logger
  {
    private const string DebugString = "DEBUG";
    private const string InfoString = "INFO";
    private const string WarnString = "WARN";
    private const string ErrorString = "ERROR";
    private const int SeverityCharacters = 5;

    public static Severity  LogSeverity = Severity.Debug;

    public enum Severity
    {
      Debug, Info, Warn, Error
    }

    public static void DebugFormat(string message, object arg0, [CallerFilePath] string callerFilePath = "",
      [CallerMemberName] string callerMemberName = "")
    {
      Debug(string.Format(message, arg0), callerMemberName, callerFilePath);
    }

    public static void DebugFormat(string message, object arg0, object arg1, [CallerFilePath] string callerFilePath = "",
      [CallerMemberName] string callerMemberName = "")
    {
      Debug(string.Format(message, arg0, arg1), callerMemberName, callerFilePath);
    }

    public static void DebugFormat(string message, object arg0, object arg1, object arg2, [CallerFilePath] string callerFilePath = "",
      [CallerMemberName] string callerMemberName = "")
    {
      Debug(string.Format(message, arg0, arg1, arg2), callerMemberName, callerFilePath);
    }

    public static void Debug(string message, [CallerFilePath] string callerFilePath = "",
      [CallerMemberName] string callerMemberName = "")
    {
      if (LogSeverity > Severity.Debug)
      {
        return;
      }
      Write(message, DebugString, callerFilePath, callerMemberName);
    }

    public static void InfoFormat(string message, object arg0, [CallerFilePath] string callerFilePath = "",
      [CallerMemberName] string callerMemberName = "")
    {
      Info(string.Format(message, arg0), callerMemberName, callerFilePath);
    }

    public static void InfoFormat(string message, object arg0, object arg1, [CallerFilePath] string callerFilePath = "",
      [CallerMemberName] string callerMemberName = "")
    {
      Info(string.Format(message, arg0, arg1), callerMemberName, callerFilePath);
    }

    public static void InfoFormat(string message, object arg0, object arg1, object arg2, [CallerFilePath] string callerFilePath = "",
      [CallerMemberName] string callerMemberName = "")
    {
      Info(string.Format(message, arg0, arg1, arg2), callerMemberName, callerFilePath);
    }

    public static void Info(string message, [CallerFilePath] string callerFilePath = "",
      [CallerMemberName] string callerMemberName = "")
    {
      if (LogSeverity > Severity.Info)
      {
        return;
      }
      Write(message, InfoString, callerFilePath, callerMemberName);
    }

    public static void WarnFormat(string message, object arg0, [CallerFilePath] string callerFilePath = "",
      [CallerMemberName] string callerMemberName = "")
    {
      Warn(string.Format(message, arg0), callerMemberName, callerFilePath);
    }

    public static void WarnFormat(string message, object arg0, object arg1, [CallerFilePath] string callerFilePath = "",
      [CallerMemberName] string callerMemberName = "")
    {
      Warn(string.Format(message, arg0, arg1), callerMemberName, callerFilePath);
    }

    public static void WarnFormat(string message, object arg0, object arg1, object arg2, [CallerFilePath] string callerFilePath = "",
      [CallerMemberName] string callerMemberName = "")
    {
      Warn(string.Format(message, arg0, arg1, arg2), callerMemberName, callerFilePath);
    }

    public static void Warn(string message, [CallerFilePath] string callerFilePath = "",
      [CallerMemberName] string callerMemberName = "")
    {
      if (LogSeverity > Severity.Warn)
      {
        return;
      }
      Write(message, WarnString, callerFilePath, callerMemberName);
    }

    public static void ErrorFormat(string message, object arg0, [CallerFilePath] string callerFilePath = "",
      [CallerMemberName] string callerMemberName = "")
    {
      Error(string.Format(message, arg0), callerMemberName, callerFilePath);
    }

    public static void ErrorFormat(string message, object arg0, object arg1, [CallerFilePath] string callerFilePath = "",
      [CallerMemberName] string callerMemberName = "")
    {
      Error(string.Format(message, arg0, arg1), callerMemberName, callerFilePath);
    }

    public static void ErrorFormat(string message, object arg0, object arg1, object arg2, [CallerFilePath] string callerFilePath = "",
      [CallerMemberName] string callerMemberName = "")
    {
      Error(string.Format(message, arg0, arg1, arg2), callerMemberName, callerFilePath);
    }

    public static void Error(string message, [CallerFilePath] string callerFilePath = "",
      [CallerMemberName] string callerMemberName = "")
    {
      if (LogSeverity > Severity.Error)
      {
        return;
      }
      Write(message, ErrorString, callerFilePath, callerMemberName);
    }

    private static void Write(string message, string severity, string callerFilePath, string callerMemberName)
    {
      Console.WriteLine("[{0}] {1}{2} - {3}.{4} - {5}", DateTime.Now, severity, GetWhitespaces(SeverityCharacters - severity.Length), GetClassNameFromPath(callerFilePath), callerMemberName, message);
    }

    private static string GetWhitespaces(int count)
    {
      return new string(' ', count);
    }

    private static string GetClassNameFromPath(string path)
    {
      var fileName = Path.GetFileName(path);
      if (fileName == null)
      {
        throw new Exception(string.Format("Path \"{0}\" doesn't appear to be a valid path", path));
      }

      return fileName.Contains(".") ? fileName.Substring(0, fileName.IndexOf('.')) : fileName;
    }
  }
}
