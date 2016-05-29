using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using TrainingPlanner.Model.EventArgs;
using TrainingPlanner.Model.Serializable;

namespace TrainingPlanner.Model
{
  /// <summary>
  /// Contains all relevant data of a training plan.
  /// </summary>
  public class Data
  {
    /// <summary>
    /// Last `Data` instance that was created - sometimes static access
    /// to the current instance seems unavoidable...
    /// </summary>
    public static Data Instance;

    /// <summary>
    /// Paces which are used in the plan.
    /// </summary>
    public readonly Pace Pace;

    /// <summary>
    /// Training plan instance.
    /// </summary>
    private readonly TrainingPlan _trainingPlan;

    /// <summary>
    /// List of the different workouts.
    /// </summary>
    private readonly List<Workout> _workouts;

    /// <summary>
    /// List of the different workout categories.
    /// </summary>
    private readonly List<WorkoutCategory> _categories;

    /// <summary>
    /// Initializes a new Data instance with the given name.
    /// This name is used to resolve the path to the persisted data.
    /// </summary>
    /// <param name="planName">Name of the training plan.</param>
    public Data(string planName)
    {
      Instance = this;

      // create persistence handler for this `Data` instance.
      var persistence = new DataPersistence(this);

      // load persisted data
      // NOTE: Must load training plan first because its name is needed to find the other files
      _trainingPlan = persistence.LoadPlan(planName);
      _trainingPlan.SetData(this);

      Pace = persistence.LoadPace();

      _workouts = new List<Workout>(persistence.LoadWorkouts());
      _categories = new List<WorkoutCategory>(persistence.LoadCategories());

      Logger.Info("Data instantiated");
    }

    /// <summary>
    /// Triggered whenever one of the workout changes or when one is added or removed.
    /// </summary>
    public event EventHandler<WorkoutChangedEventArgs> WorkoutChanged = (s, e) => { };

    /// <summary>
    /// Triggered whenever one of the categories changes or when one is added or removed.
    /// </summary>
    public event EventHandler<WorkoutCategoryChangedEventArgs> CategoryChanged = (s, e) => { };

    /// <summary>
    /// Triggered whenever one of the training plan entries changes.
    /// </summary>
    public event EventHandler TrainingPlanModified = (s, e) => { };

    /// <summary>
    /// Triggered whenever the value of any of the paces changes.
    /// </summary>
    public event EventHandler PaceChanged = (s, e) => { };

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

    public TimeSpan GetDurationFromPace(Pace.Names pace)
    {
      switch (pace)
      {
        case Pace.Names.Easy:
          return Pace.Easy;
        case Pace.Names.Base:
          return Pace.Base;
        case Pace.Names.Steady:
          return Pace.Steady;
        case Pace.Names.Marathon:
          return Pace.Marathon;
        case Pace.Names.Halfmarathon:
          return Pace.Halfmarathon;
        case Pace.Names.Threshold:
          return Pace.Threshold;
        case Pace.Names.TenK:
          return Pace.TenK;
        case Pace.Names.FiveK:
          return Pace.FiveK;
        default:
          throw new ArgumentOutOfRangeException("pace");
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

    /// <summary>
    /// Adds a new workout if no workout exists with the provided workout's name or
    /// replaces the existing workout with the same name.
    /// </summary>
    /// <param name="workout">Workout to add or update.</param>
    public void AddOrUpdateWorkout(Workout workout)
    {
      // TODO: (add/edit/update) Add proper updating of workout
      var existing = _workouts.FirstOrDefault(c => c.Name == workout.Name);

      if (existing != null)
      {
        // NOTE: Removing workout "under the hood" which means that the
        // WorkoutChanged-event is not triggered (which will be triggered afterwards)
        _workouts.Remove(existing);
      }
      AddWorkout(workout);
    }

    /// <summary>
    /// Adds a new workout to the data model.
    /// </summary>
    /// <param name="workout">New workout to add.</param>
    private void AddWorkout(Workout workout)
    {
      var index = _workouts.FindIndex(w => string.Compare(w.Name, workout.Name, StringComparison.InvariantCulture) > 0);
      _workouts.Insert(index == -1 ? _workouts.Count : index, workout);

      Logger.Debug("Triggering WorkoutChanged event");
      WorkoutChanged(this, new WorkoutChangedEventArgs(workout, true));
    }

    /// <summary>
    /// Removes a workout from the data model.
    /// </summary>
    /// <param name="workout">Workout to remove.</param>
    public void RemoveWorkout(Workout workout)
    {
      if (!_workouts.Contains(workout))
      {
        return;
      }

      _workouts.Remove(workout);

      // (no need to sort)

      Logger.Debug("Triggering WorkoutChanged event");
      WorkoutChanged(this, new WorkoutChangedEventArgs(workout, false));
    }

    /// <summary>
    /// Adds a new workout category if no category exists with the provided category's name or
    /// replaces the existing category with the same name.
    /// </summary>
    /// <param name="category">The new workout category to add or update.</param>
    public void AddOrUpdateWorkoutCategory(WorkoutCategory category)
    {
      // TODO: (add/edit/update) add proper updating of workout category
      var existing = _categories.FirstOrDefault(c => c.Name == category.Name);

      if (existing != null)
      {
        // NOTE: Removing category "under the hood" which means that the
        // WorkoutCategoryChanged-event is not triggered (which will be triggered afterwards)
        _categories.Remove(existing);
      }
      AddWorkoutCategory(category);
    }

    /// <summary>
    /// Adds a new workout category to the data model.
    /// </summary>
    /// <param name="category">New workout category to add.</param>
    private void AddWorkoutCategory(WorkoutCategory category)
    {
      var index =
        _categories.FindIndex(c => string.Compare(c.Name, category.Name, StringComparison.InvariantCulture) > 0);
      _categories.Insert(index == -1 ? _categories.Count : index, category);

      Logger.Debug("Triggering CategoryChanged event");
      CategoryChanged(this, new WorkoutCategoryChangedEventArgs(category, true));
    }

    /// <summary>
    /// Removes a workout category from the data model.
    /// </summary>
    /// <param name="category">Category to remove.</param>
    public void RemoveWorkoutCategory(WorkoutCategory category)
    {
      if (!_categories.Contains(category))
      {
        return;
      }

      _categories.Remove(category);

      // (no need to sort)

      Logger.Debug("Triggering CategoryChanged event");
      CategoryChanged(this, new WorkoutCategoryChangedEventArgs(category, false));
    }

    /// <summary>
    /// Updates the value of a pace.
    /// </summary>
    /// <param name="key">Description of the pace.</param>
    /// <param name="value">New value of the pace.</param>
    public void ChangePace(Pace.Names key, TimeSpan value)
    {
      // save pace
      Pace.SetPace(key, value);

      Logger.Debug("Triggering PaceChanged event");
      PaceChanged(this, null);
    }

    public void UpdateTrainingPlan(WeeklyPlan newWeeklyPlan)
    {
      _trainingPlan.WeeklyPlans[newWeeklyPlan.WeekNumber] = newWeeklyPlan;

      Logger.Debug("Triggering TrainingPlanModified event");
      TrainingPlanModified(this, null);
    }
  }
}