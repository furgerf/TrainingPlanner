using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using TrainingPlanner.Model;
using TrainingPlanner.Presenter;
using TrainingPlanner.View;

namespace TrainingPlanner
{
  internal static class Program
  {
    public static List<Workout>  Workouts;

    public static Workout WorkoutFromName(string workoutName)
    {
      return Workouts.First(w => w.Name == workoutName);
    }

    private const string ApplicationDataDirectoryWindows = @"D:\data\training-planner-data";
    private const string ApplicationDataDirectoryLinux = "/data/data/training-planner-data";
    private const string TrainingPlanFileName = "plan.json";
    private const string WorkoutsDirectoryName = "workouts";

    private static readonly bool IsLinux = !Environment.OSVersion.Platform.ToString().ToLower().StartsWith("win");

    private static readonly string ApplicationDataDirectory = IsLinux
      ? ApplicationDataDirectoryLinux
      : ApplicationDataDirectoryWindows;

    public static string WorkoutsDirectory { get { return ApplicationDataDirectory + Path.DirectorySeparatorChar + WorkoutsDirectoryName; } }
    public static string TrainingPlanFile { get { return ApplicationDataDirectory + Path.DirectorySeparatorChar + TrainingPlanFileName; } }

    public static readonly Dictionary<string, TimeSpan> Paces = new Dictionary<string, TimeSpan>
    {
      {"Easy", TrainingPlanner.Paces.Default.Easy},
      {"Long", TrainingPlanner.Paces.Default.Long},
      {"Marathon", TrainingPlanner.Paces.Default.Marathon},
      {"Threshold", TrainingPlanner.Paces.Default.Threshold},
      {"Halfmarathon", TrainingPlanner.Paces.Default.Halfmarathon},
      {"TenK", TrainingPlanner.Paces.Default.TenK},
      {"FiveK", TrainingPlanner.Paces.Default.FiveK}
    };

    /// <summary>
    /// The main entry point for the application.
    /// </summary>
    [STAThread]
    private static void Main()
    {
      Workouts = Directory.GetFiles(WorkoutsDirectory, "*.json").Select(Workout.ParseJsonFile).ToList();
      Application.EnableVisualStyles();
      Application.SetCompatibleTextRenderingDefault(false);
      Application.EnableVisualStyles();
      Application.SetCompatibleTextRenderingDefault(false);

      var view = new MainForm();
      var presenter = new MainFormPresenter(view);

      Application.Run(view);
    }
  }
}
