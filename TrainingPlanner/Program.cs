using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace TrainingPlanner
{
  internal static class Program
  {
    public static List<Workout>  Workouts;

    public const string WorkoutsDirectory = "workouts";

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
      Application.Run(new MainForm());
    }
  }
}
