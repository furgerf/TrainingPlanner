using System;
using System.Collections.Generic;
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
    /// Actual data.
    /// </summary>
    private readonly List<WorkoutCategory> _categories;
    private readonly List<Workout> _workouts;
    private readonly TrainingPlan _trainingPlan;
    #endregion

    #region Constructor
    public Data()
    {
      // create persistence handler
      var persistence = new DataPersistence(this);

      // load persisted data
      this._categories = new List<WorkoutCategory>(persistence.LoadCategories());
      this._workouts = new List<Workout>(persistence.LoadWorkouts());

      this._trainingPlan = persistence.LoadPlan("sempacherseelauf-2016");
      if (TrainingPlanLoaded != null)
      {
        Logger.Debug("Triggering TrainingPlanLoaded event");
        TrainingPlanLoaded(this, null);
      }

      this._trainingPlan.SetData(this);

      Logger.Info("Data instantiated");
    }
    #endregion

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
    public event EventHandler<TrainingPlanChangedEventArgs> TrainingPlanModified;

    /// <summary>
    /// Triggered whenever the value of any of the paces changes.
    /// </summary>
    public event EventHandler<PaceChangedEventArgs> PaceChanged;

    public event EventHandler TrainingPlanLoaded;
    #endregion

    #region Data access
    /// <summary>
    /// Gets the workouts.
    /// </summary>
    public Workout[] Workouts
    {
      get { return _workouts.ToArray(); }
    }

    /// <summary>
    /// Gets the workout categories.
    /// </summary>
    public WorkoutCategory[] Categories
    {
      get { return _categories.ToArray(); }
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

    public static TimeSpan GetDurationFromPace(PaceNames pace)
    {
      switch (pace)
      {
        case PaceNames.Easy:
          return Paces.Default.Easy;
        case PaceNames.Base:
          return Paces.Default.Base;
        case PaceNames.Steady:
          return Paces.Default.Steady;
        case PaceNames.Marathon:
          return Paces.Default.Marathon;
        case PaceNames.Halfmarathon:
          return Paces.Default.Halfmarathon;
        case PaceNames.Threshold:
          return Paces.Default.Threshold;
        case PaceNames.TenK:
          return Paces.Default.TenK;
        case PaceNames.FiveK:
          return Paces.Default.FiveK;
        default:
          throw new ArgumentOutOfRangeException("pace");
      }
    }

    public static PaceNames GetPaceFromDuration(TimeSpan duration)
    {
      if (duration == Paces.Default.Easy)
        return PaceNames.Easy;
      if (duration == Paces.Default.Base)
        return PaceNames.Base;
      if (duration == Paces.Default.Steady)
        return PaceNames.Steady;
      if (duration == Paces.Default.Marathon)
        return PaceNames.Marathon;
      if (duration == Paces.Default.Halfmarathon)
        return PaceNames.Halfmarathon;
      if (duration == Paces.Default.Threshold)
        return PaceNames.Threshold;
      if (duration == Paces.Default.TenK)
        return PaceNames.TenK;
      if (duration == Paces.Default.FiveK)
        return PaceNames.FiveK;

      throw new ArgumentException("unkown pace");
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
      var index = this._workouts.FindIndex(w => string.Compare(w.Name, workout.Name, StringComparison.InvariantCulture) > 0);
      this._workouts.Insert(index == -1 ? this._workouts.Count : index, workout);

      if (WorkoutChanged != null)
      {
        Logger.Debug("Triggering WorkoutChanged event");
        WorkoutChanged(this, new WorkoutChangedEventArgs(workout, true));
      }
    }

    public void AddOrUpdateWorkout(Workout workout)
    {
      // TODO: (add/edit/update) Add proper updating of workout
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
        Logger.Debug("Triggering WorkoutChanged event");
        WorkoutChanged(this, new WorkoutChangedEventArgs(workout, false));
      }
    }

    public void AddWorkoutCategory(WorkoutCategory category)
    {
      var index = this._categories.FindIndex(c => string.Compare(c.Name, category.Name, StringComparison.InvariantCulture) > 0);
      this._categories.Insert(index == -1 ? this._categories.Count : index, category);

      if (CategoryChanged != null)
      {
        Logger.Debug("Triggering CategoryChanged event");
        CategoryChanged(this, new WorkoutCategoryChangedEventArgs(category, true));
      }
    }

    public void AddOrUpdateWorkoutCategory(WorkoutCategory category)
    {
      // TODO: (add/edit/update) add proper updating of workout category
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
        Logger.Debug("Triggering CategoryChanged event");
        CategoryChanged(this, new WorkoutCategoryChangedEventArgs(category, false));
      }
    }

    /// <summary>
    /// Updates the value of a pace.
    /// </summary>
    /// <param name="key">Description of the pace.</param>
    /// <param name="value">New value of the pace.</param>
    public void ChangePace(PaceNames key, TimeSpan value)
    {
      // nothing to do here because paces aren't saved in ram
      // just trigger event to persist the new value

      if (PaceChanged != null)
      {
        Logger.Debug("Triggering PaceChanged event");
        PaceChanged(this, new PaceChangedEventArgs(key, value));
      }
    }

    public void UpdateTrainingPlan(WeeklyPlan newWeeklyPlan)
    {
      _trainingPlan.WeeklyPlans[newWeeklyPlan.WeekNumber] = newWeeklyPlan;

      if (TrainingPlanModified != null)
      {
        Logger.Debug("Triggering TrainingPlanModified event");
        TrainingPlanModified(this, new TrainingPlanChangedEventArgs());
      }
    }
    #endregion
  }
}