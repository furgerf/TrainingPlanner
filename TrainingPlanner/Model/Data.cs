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
    /// <summary>
    /// Color to use for the background of controls that don't have a specific color.
    /// </summary>
    public static readonly Color DefaultBackgroundColor = Color.Beige;

    /// <summary>
    /// Maps entries in the Pace-enum to the timespans stored in the settings.
    /// </summary>
    public static readonly Dictionary<Pace, TimeSpan> Paces = new Dictionary<Pace, TimeSpan>
    {
      {Pace.Easy, TrainingPlanner.Paces.Default.Easy},
      {Pace.Long, TrainingPlanner.Paces.Default.Long},
      {Pace.Marathon, TrainingPlanner.Paces.Default.Marathon},
      {Pace.Threshold, TrainingPlanner.Paces.Default.Threshold},
      {Pace.Halfmarathon, TrainingPlanner.Paces.Default.Halfmarathon},
      {Pace.Tenk, TrainingPlanner.Paces.Default.TenK},
      {Pace.Fivek, TrainingPlanner.Paces.Default.FiveK}
    };

    /// <summary>
    /// Number of weeks of the training plan.
    /// </summary>
    public const int TrainingWeeks = 11;

    /// <summary>
    /// Data.
    /// </summary>
    private readonly List<WorkoutCategory> _categories;
    private readonly List<Workout> _workouts;
    private readonly List<WeeklyPlan> _trainingPlan;

    private Data(IEnumerable<WorkoutCategory> categories, IEnumerable<Workout> workouts, IEnumerable<WeeklyPlan> trainingPlan)
    {
      this._trainingPlan = new List<WeeklyPlan>(trainingPlan);
      this._workouts = new List<Workout>(workouts);
      this._categories = new List<WorkoutCategory>(categories);
    }

    /// <summary>
    /// Triggered whenever one of the workout changes or when one is added or removed.
    /// </summary>
    public event EventHandler WorkoutsChanged;

    /// <summary>
    /// Triggered whenever one of the categories changes or when one is added or removed.
    /// </summary>
    public event EventHandler CategoriesChanged;

    /// <summary>
    /// Triggered whenever the value of any of the paces changes.
    /// TODO: figure out what to do with the existing workouts when a pace changes...
    /// </summary>
    public event EventHandler PacesChanged;

    /// <summary>
    /// Gets the workout categories.
    /// </summary>
    public WorkoutCategory[] Categories
    {
      get { return _categories.ToArray(); }
    }

    /// <summary>
    /// Gets the workouts.
    /// </summary>
    public Workout[] Workouts
    {
      get { return _workouts.ToArray(); }
    }

    /// <summary>
    /// Gets the training plan.
    /// </summary>
    public WeeklyPlan[] TrainingPlan
    {
      get { return _trainingPlan.ToArray(); }
    }

    /// <summary>
    /// Gets a context menu based on the current workoutouts and categories.
    /// </summary>
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

    /// <summary>
    /// Creates a new instance, loading the data from the provided directories and files.
    /// </summary>
    /// <param name="categoryDirectory">Directory where workout categories are stored.</param>
    /// <param name="workoutDirectory">Directory where workouts are stored.</param>
    /// <param name="planFile">File where the training plan is stored.</param>
    /// <returns>Data instance.</returns>
    public static Data Load(string categoryDirectory, string workoutDirectory, string planFile)
    {
      return new Data(
        Directory.GetFiles(categoryDirectory, "*.json").Select(WorkoutCategory.ParseJsonFile).ToArray(),
        Directory.GetFiles(workoutDirectory, "*.json").Select(Workout.ParseJsonFile).ToArray(),
        File.ReadAllLines(planFile).Select(WeeklyPlan.FromJson).ToArray());
    }

    /// <summary>
    /// Gets the workout from the workout's name.
    /// </summary>
    /// <param name="workoutName">Name of the workout.</param>
    /// <returns>Workout.</returns>
    public Workout WorkoutFromName(string workoutName)
    {
      return Workouts.FirstOrDefault(w => w.Name == workoutName);
    }

    /// <summary>
    /// Gets the workout category from the workout category's name.
    /// </summary>
    /// <param name="categoryName">Name of the workout category.</param>
    /// <returns>Workout category.</returns>
    public WorkoutCategory WorkoutCategoryFromName(string categoryName)
    {
      return Categories.FirstOrDefault(w => w.Name == categoryName);
    }

    /// <summary>
    /// Adds a new workout to the data model.
    /// </summary>
    /// <param name="workout">New workout.</param>
    public void AddWorkout(Workout workout)
    {
      // TODO: Change "add + sort" to "insert"
      this._workouts.Add(workout);
      this._workouts.Sort((a, b) => string.Compare(a.Name, b.Name, StringComparison.InvariantCulture));

      if (WorkoutsChanged != null)
      {
        WorkoutsChanged(this, EventArgs.Empty);
      }
    }

    public void AddOrUpdateWorkout(Workout workout)
    {
      var existing = this._workouts.FirstOrDefault(c => c.Name == workout.Name);

      if (existing != null)
      {
        this._workouts.Remove(existing);
      }
      AddWorkout(workout);
    }

    /// <summary>
    /// Removes a workout from the data model.
    /// </summary>
    /// <param name="workout">Workout to remove.</param>
    public void RemoveWorkout(Workout workout)
    {
      if (!this._workouts.Contains(workout))
      {
        return;
      }

      this._workouts.Remove(workout);

      // (no need to sort)

      if (WorkoutsChanged != null)
      {
        WorkoutsChanged(this, EventArgs.Empty);
      }
    }

    /// <summary>
    /// Updates the value of a pace.
    /// </summary>
    /// <param name="key">Description of the pace.</param>
    /// <param name="value">New value of the pace.</param>
    public void ChangePace(Pace key, TimeSpan value)
    {
      if (!Paces.ContainsKey(key))
      {
        throw new ArgumentException(string.Format("Unkown pace name ({0})!", key));
      }

      Paces[key] = value;
      SavePacesToSettings();

      if (PacesChanged != null)
      {
        PacesChanged(this, EventArgs.Empty);
      }
    }

    /// <summary>
    /// Stores the current values of the Dictionary in the settings.
    /// </summary>
    private static void SavePacesToSettings()
    {
      TrainingPlanner.Paces.Default.Easy = Paces[Pace.Easy];
      TrainingPlanner.Paces.Default.Long = Paces[Pace.Long];
      TrainingPlanner.Paces.Default.Marathon = Paces[Pace.Marathon];
      TrainingPlanner.Paces.Default.Threshold = Paces[Pace.Threshold];
      TrainingPlanner.Paces.Default.Halfmarathon = Paces[Pace.Halfmarathon];
      TrainingPlanner.Paces.Default.TenK = Paces[Pace.Tenk];
      TrainingPlanner.Paces.Default.FiveK = Paces[Pace.Fivek];

      TrainingPlanner.Paces.Default.Save();
    }

    /// <summary>
    /// Removes a workout category from the data model.
    /// </summary>
    /// <param name="category">Category to remove.</param>
    public void RemoveWorkoutCategory(WorkoutCategory category)
    {
      if (!this._categories.Contains(category))
      {
        return;
      }

      this._categories.Remove(category);

      // (no need to sort)

      if (CategoriesChanged != null)
      {
        CategoriesChanged(this, EventArgs.Empty);
      }
    }

    public void AddOrUpdateWorkoutCategory(WorkoutCategory category)
    {
      var existing = this._categories.FirstOrDefault(c => c.Name == category.Name);

      if (existing != null)
      {
        this._categories.Remove(existing);
      }
      AddWorkoutCategory(category);
    }

    public void AddWorkoutCategory(WorkoutCategory category)
    {
      // TODO: Change "add + sort" to "insert"
      this._categories.Add(category);
      this._categories.Sort((a, b) => string.Compare(a.Name, b.Name, StringComparison.InvariantCulture));

      if (CategoriesChanged != null)
      {
        CategoriesChanged(this, EventArgs.Empty);
      }
    }
  }
}