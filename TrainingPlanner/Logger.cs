using System;
using System.IO;
using System.Runtime.CompilerServices;

namespace TrainingPlanner
{
  public static class Logger
  {
    private const string DebugString = "DEBUG";
    private const string InfoString = "INFO";
    private const string WarnString = "WARN";
    private const string ErrorString = "ERROR";
    private const int SeverityCharacters = 5;

    public static void Debug(string message, [CallerFilePath] string callerFilePath = "",
      [CallerMemberName] string callerMemberName = "")
    {
      Write(message, DebugString, callerFilePath, callerMemberName);
    }

    public static void Info(string message, [CallerFilePath] string callerFilePath = "",
      [CallerMemberName] string callerMemberName = "")
    {
      Write(message, InfoString, callerFilePath, callerMemberName);
    }

    public static void Warn(string message, [CallerFilePath] string callerFilePath = "",
      [CallerMemberName] string callerMemberName = "")
    {
      Write(message, WarnString, callerFilePath, callerMemberName);
    }

    public static void Error(string message, [CallerFilePath] string callerFilePath = "",
      [CallerMemberName] string callerMemberName = "")
    {
      Write(message, ErrorString, callerFilePath, callerMemberName);
    }

    private static void Write(string message, string severity, string callerFilePath, string callerMemberName)
    {
      Console.WriteLine("{0}{1} - {2}.{3} - {4}", severity, GetWhitespaces(SeverityCharacters - severity.Length), GetClassNameFromPath(callerFilePath), callerMemberName, message);
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
      
      return fileName.Substring(0, fileName.IndexOf('.'));
    }
  }
}
