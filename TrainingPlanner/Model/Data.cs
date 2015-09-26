using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using TrainingPlanner.Model.EventArgs;
using TrainingPlanner.Model.Serializable;

namespace TrainingPlanner.Model
{
  public class Data
  {
    #region Data
    /// <summary>
    /// Color to use for the background of controls that don't have a specific color.
    /// TODO: Move to settings
    /// </summary>
    public static readonly Color DefaultBackgroundColor = Color.Beige;

    /// <summary>
    /// Actual data.
    /// </summary>
    private readonly List<WorkoutCategory> _categories;
    private readonly List<Workout> _workouts;
    private readonly TrainingPlan _trainingPlan;
    #endregion

    public Data()
    {
      var persistence = new DataPersistence(this);

      this._categories = new List<WorkoutCategory>(persistence.LoadCategories());
      this._workouts = new List<Workout>(persistence.LoadWorkouts());
      this._trainingPlan = persistence.LoadPlan();
    }

    #region Events
    /// <summary>
    /// Triggered whenever one of the workout changes or when one is added or removed.
    /// </summary>
    public event EventHandler<WorkoutChangedEventArgs> WorkoutChanged;

    /// <summary>
    /// Triggered whenever one of the categories changes or when one is added or removed.
    /// </summary>
    public event EventHandler<WorkoutCategoryChangedEventArgs> CategoryChanged;

    /// <summary>
    /// Triggered whenever one of the training plan entries changes.
    /// </summary>
    public event EventHandler<TrainingPlanChangedEventArgs> TrainingPlanChanged;

    /// <summary>
    /// Triggered whenever the value of any of the paces changes.
    /// </summary>
    public event EventHandler<PaceChangedEventArgs> PacesChanged;
    #endregion

    #region Data access
    public static TimeSpan GetDurationFromPace(Pace pace)
    {
      switch (pace)
      {
        case Pace.Easy:
          return Paces.Default.Easy;
        case Pace.Long:
          return Paces.Default.Long;
        case Pace.Marathon:
          return Paces.Default.Marathon;
        case Pace.Halfmarathon:
          return Paces.Default.Halfmarathon;
        case Pace.Threshold:
          return Paces.Default.Threshold;
        case Pace.Tenk:
          return Paces.Default.TenK;
        case Pace.Fivek:
          return Paces.Default.FiveK;
        default:
          throw new ArgumentOutOfRangeException("pace");
      }
    }

    public static Pace GetPaceFromDuration(TimeSpan duration)
    {
      if (duration == Paces.Default.Easy)
        return Pace.Easy;
      if (duration == Paces.Default.Long)
        return Pace.Long;
      if (duration == Paces.Default.Marathon)
        return Pace.Marathon;
      if (duration == Paces.Default.Halfmarathon)
        return Pace.Halfmarathon;
      if (duration == Paces.Default.Threshold)
        return Pace.Threshold;
      if (duration == Paces.Default.TenK)
        return Pace.Tenk;
      if (duration == Paces.Default.FiveK)
        return Pace.Fivek;

      throw new ArgumentException("unkown pace");
    }

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
    public TrainingPlan TrainingPlan
    {
      get { return _trainingPlan; }
    }

    /// <summary>
    /// Gets a context menu based on the current workoutouts and categories.
    /// The menu is created on each call so that there are no outdated event listeners...
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

    #endregion

    #region Data modification
    /// <summary>
    /// Adds a new workout to the data model.
    /// </summary>
    /// <param name="workout">New workout.</param>
    public void AddWorkout(Workout workout)
    {
      // TODO: Change "add + sort" to "insert"
      this._workouts.Add(workout);
      this._workouts.Sort((a, b) => string.Compare(a.Name, b.Name, StringComparison.InvariantCulture));

      if (WorkoutChanged != null)
      {
        WorkoutChanged(this, new WorkoutChangedEventArgs(workout, true));
      }
    }

    public void AddOrUpdateWorkout(Workout workout)
    {
      // TODO: Add proper updating of workout
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

      if (WorkoutChanged != null)
      {
        WorkoutChanged(this, new WorkoutChangedEventArgs(workout, false));
      }
    }


    public void AddWorkoutCategory(WorkoutCategory category)
    {
      // TODO: Change "add + sort" to "insert"
      this._categories.Add(category);
      this._categories.Sort((a, b) => string.Compare(a.Name, b.Name, StringComparison.InvariantCulture));

      if (CategoryChanged != null)
      {
        CategoryChanged(this, new WorkoutCategoryChangedEventArgs(category, true));
      }
    }

    public void AddOrUpdateWorkoutCategory(WorkoutCategory category)
    {
      // TODO: add proper updating of workout category
      var existing = this._categories.FirstOrDefault(c => c.Name == category.Name);

      if (existing != null)
      {
        this._categories.Remove(existing);
      }
      AddWorkoutCategory(category);
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

      if (CategoryChanged != null)
      {
        CategoryChanged(this, new WorkoutCategoryChangedEventArgs(category, false));
      }
    }

    /// <summary>
    /// Updates the value of a pace.
    /// </summary>
    /// <param name="key">Description of the pace.</param>
    /// <param name="value">New value of the pace.</param>
    public void ChangePace(Pace key, TimeSpan value)
    {
      if (PacesChanged != null)
      {
        PacesChanged(this, new PaceChangedEventArgs(key, value));
      }
    }

    public void UpdateTrainingPlan(WeeklyPlan newWeeklyPlan)
    {
      _trainingPlan.WeeklyPlans[newWeeklyPlan.WeekNumber] = newWeeklyPlan;

      if (TrainingPlanChanged != null)
      {
        TrainingPlanChanged(this, new TrainingPlanChangedEventArgs());
      }
    }
    #endregion
  }
}