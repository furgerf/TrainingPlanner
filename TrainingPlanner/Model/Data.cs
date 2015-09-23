using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;

namespace TrainingPlanner.Model
{
  public class Data
  {
    public static readonly Color DefaultBackgroundColor = Color.Beige;

    private static readonly Dictionary<string, TimeSpan> PaceMap = new Dictionary<string, TimeSpan>
    {
      {"Easy", TrainingPlanner.Paces.Default.Easy},
      {"Long", TrainingPlanner.Paces.Default.Long},
      {"Marathon", TrainingPlanner.Paces.Default.Marathon},
      {"Threshold", TrainingPlanner.Paces.Default.Threshold},
      {"Halfmarathon", TrainingPlanner.Paces.Default.Halfmarathon},
      {"TenK", TrainingPlanner.Paces.Default.TenK},
      {"FiveK", TrainingPlanner.Paces.Default.FiveK}
    };

    public const int TrainingWeeks = 11;

    private readonly List<WorkoutCategory> _categories;
    private readonly List<Workout> _workouts;
    private readonly List<WeeklyPlan> _trainingPlan;

    private Data(IEnumerable<WorkoutCategory> categories, IEnumerable<Workout> workouts, IEnumerable<WeeklyPlan> trainingPlan)
    {
      this._trainingPlan = new List<WeeklyPlan>(trainingPlan);
      this._workouts= new List<Workout>(workouts);
      this._categories= new List<WorkoutCategory>(categories);
    }

    public event EventHandler WorkoutsChanged;

    public event EventHandler CategoriesChanged;

    public event EventHandler PacesChanged;

    public Dictionary<string, TimeSpan> Paces { get { return PaceMap; } } 

    public WorkoutCategory[] Categories
    {
      get { return _categories.ToArray(); }
    }

    public Workout[] Workouts
    {
      get { return _workouts.ToArray(); }
    }

    public WeeklyPlan[] TrainingPlan
    {
      get { return _trainingPlan.ToArray(); }
    }

    public static Data Load(string categoryDirectory, string workoutDirectory, string planFile)
    {
      return new Data(
        Directory.GetFiles(categoryDirectory, "*.json").Select(WorkoutCategory.ParseJsonFile).ToArray(),
        Directory.GetFiles(workoutDirectory, "*.json").Select(Workout.ParseJsonFile).ToArray(),
        File.ReadAllLines(planFile).Select(WeeklyPlan.FromJson).ToArray());
    }

    public Workout WorkoutFromName(string workoutName)
    {
      return Workouts.First(w => w.Name == workoutName);
    }

    public WorkoutCategory WorkoutCategoryFromName(string categoryName)
    {
      return Categories.First(w => w.Name == categoryName);
    }

    public void AddWorkout(Workout workout)
    {
      this._workouts.Add(workout);
      this._workouts.Sort((a, b) => string.Compare(a.Name, b.Name, StringComparison.InvariantCulture));

      if (WorkoutsChanged != null)
      {
        WorkoutsChanged(this, EventArgs.Empty);
      }
    }

    public void ChangePace(string paceName, TimeSpan pace)
    {
      if (!PaceMap.ContainsKey(paceName))
      {
        throw new ArgumentException(string.Format("Unkown pace name ({0})!", paceName));
      }

      PaceMap[paceName] = pace;

      SavePacesToSettings();

      if (PacesChanged != null)
      {
        PacesChanged(this, EventArgs.Empty);
      }
    }

    private static void SavePacesToSettings()
    {
      TrainingPlanner.Paces.Default.Easy = PaceMap["Easy"];
      TrainingPlanner.Paces.Default.Long = PaceMap["Long"];
      TrainingPlanner.Paces.Default.Marathon = PaceMap["Marathon"];
      TrainingPlanner.Paces.Default.Threshold = PaceMap["Threshold"];
      TrainingPlanner.Paces.Default.Halfmarathon = PaceMap["Halfmarathon"];
      TrainingPlanner.Paces.Default.TenK = PaceMap["TenK"];
      TrainingPlanner.Paces.Default.FiveK = PaceMap["FiveK"];

      TrainingPlanner.Paces.Default.Save();
    }
  }
}