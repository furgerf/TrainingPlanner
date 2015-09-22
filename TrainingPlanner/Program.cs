using System;
using System.IO;
using System.Windows.Forms;
using TrainingPlanner.Model;
using TrainingPlanner.Presenter;
using TrainingPlanner.View;

namespace TrainingPlanner
{
  internal static class Program
  {
    private const string ApplicationDataDirectoryWindows = @"D:\data\training-planner-data";
    private const string ApplicationDataDirectoryLinux = "/data/data/training-planner-data";
    private const string TrainingPlanFileName = "plan.json";
    private const string WorkoutsDirectoryName = "workouts";
    private const string WorkoutCategoriesDirectoryName = "workout-categories";

    private static readonly bool IsLinux = !Environment.OSVersion.Platform.ToString().ToLower().StartsWith("win");

    private static readonly string ApplicationDataDirectory = IsLinux
      ? ApplicationDataDirectoryLinux
      : ApplicationDataDirectoryWindows;

    public static string WorkoutsDirectory { get { return ApplicationDataDirectory + Path.DirectorySeparatorChar + WorkoutsDirectoryName; } }
    public static string WorkoutCategoriesDirectory { get { return ApplicationDataDirectory + Path.DirectorySeparatorChar + WorkoutCategoriesDirectoryName; } }
    public static string TrainingPlanFile { get { return ApplicationDataDirectory + Path.DirectorySeparatorChar + TrainingPlanFileName; } }

    /// <summary>
    /// The main entry point for the application.
    /// </summary>
    [STAThread]
    private static void Main()
    {
      Application.EnableVisualStyles();
      Application.SetCompatibleTextRenderingDefault(false);

      var data = Data.Load(WorkoutCategoriesDirectory, WorkoutsDirectory, TrainingPlanFile);
      var view = new MainForm(data);
      var presenter = new MainFormPresenter(view, data);

      Application.Run(view);
    }
  }
}
