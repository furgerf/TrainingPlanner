using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace TrainingPlanner.Model
{
  public class Data
  {
    public static readonly Color DefaultBackgroundColor = Color.Beige;

    private static readonly Dictionary<Pace, TimeSpan> PaceMap = new Dictionary<Pace, TimeSpan>
    {
      {Pace.Easy, TrainingPlanner.Paces.Default.Easy},
      {Pace.Long, TrainingPlanner.Paces.Default.Long},
      {Pace.Marathon, TrainingPlanner.Paces.Default.Marathon},
      {Pace.Threshold, TrainingPlanner.Paces.Default.Threshold},
      {Pace.Halfmarathon, TrainingPlanner.Paces.Default.Halfmarathon},
      {Pace.Tenk, TrainingPlanner.Paces.Default.TenK},
      {Pace.Fivek, TrainingPlanner.Paces.Default.FiveK}
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

    public static Dictionary<Pace, TimeSpan> Paces { get { return PaceMap; } } 

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

    public ContextMenu WorkoutContextMenu
    {
      get
      {
        var menu = new ContextMenu();

        // add categories and their workouts
        menu.MenuItems.AddRange(Categories.Select(c => new MenuItem(c.Name)).ToArray());
        foreach (MenuItem mi in menu.MenuItems)
        {
          mi.MenuItems.AddRange(
            Workouts.Where(w => mi.Text.Equals(w.CategoryName)).Select(w => new MenuItem(w.Name)).ToArray());
        }

        // add uncategorized workouts
        var uncategorizedMenu = new MenuItem("(uncategorized)");
        uncategorizedMenu.MenuItems.AddRange(
          Workouts.Where(w => w.CategoryName == null).Select(w => new MenuItem(w.Name)).ToArray());
        if (uncategorizedMenu.MenuItems.Count > 0)
        {
          menu.MenuItems.Add(uncategorizedMenu);
        }

        return menu;
      }
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

    public void RemoveWorkout(Workout workout)
    {
      this._workouts.Remove(workout);

      // (no need to sort)

      if (WorkoutsChanged != null)
      {
        WorkoutsChanged(this, EventArgs.Empty);
      }
    }

    public void ChangePace(Pace key, TimeSpan value)
    {
      if (!PaceMap.ContainsKey(key))
      {
        throw new ArgumentException(string.Format("Unkown pace name ({0})!", key));
      }

      PaceMap[key] = value;

      SavePacesToSettings();

      if (PacesChanged != null)
      {
        PacesChanged(this, EventArgs.Empty);
      }
    }

    private static void SavePacesToSettings()
    {
      TrainingPlanner.Paces.Default.Easy = PaceMap[Pace.Easy];
      TrainingPlanner.Paces.Default.Long = PaceMap[Pace.Long];
      TrainingPlanner.Paces.Default.Marathon = PaceMap[Pace.Marathon];
      TrainingPlanner.Paces.Default.Threshold = PaceMap[Pace.Threshold];
      TrainingPlanner.Paces.Default.Halfmarathon = PaceMap[Pace.Halfmarathon];
      TrainingPlanner.Paces.Default.TenK = PaceMap[Pace.Tenk];
      TrainingPlanner.Paces.Default.FiveK = PaceMap[Pace.Fivek];

      TrainingPlanner.Paces.Default.Save();
    }
  }
}