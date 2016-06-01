using System;
using System.Windows.Forms;
using TrainingPlanner.Model.EventArgs;
using TrainingPlanner.Model.Serializable;

namespace TrainingPlanner.Model
{
  public interface IData
  {
    /// <summary>
    /// Triggered whenever one of the workout changes or when one is added or removed.
    /// </summary>
    event EventHandler<WorkoutChangedEventArgs> WorkoutChanged;

    /// <summary>
    /// Triggered whenever one of the categories changes or when one is added or removed.
    /// </summary>
    event EventHandler<WorkoutCategoryChangedEventArgs> CategoryChanged;

    /// <summary>
    /// Triggered whenever one of the training plan entries changes.
    /// </summary>
    event EventHandler TrainingPlanModified;

    /// <summary>
    /// Triggered whenever the value of any of the paces changes.
    /// </summary>
    event EventHandler PaceChanged;

    /// <summary>
    /// Gets the workouts.
    /// </summary>
    Workout[] Workouts { get; }

    /// <summary>
    /// Gets the workout categories.
    /// </summary>
    WorkoutCategory[] Categories { get; }

    /// <summary>
    /// Gets the training plan.
    /// </summary>
    TrainingPlan TrainingPlan { get; }

    /// <summary>
    /// Gets a context menu based on the current workoutouts and categories.
    /// The menu is created on each call so that there are no outdated event listeners...
    /// </summary>
    ContextMenu WorkoutContextMenu { get; }

    TimeSpan GetDurationFromPace(Pace.Names pace);

    /// <summary>
    /// Gets the workout from the workout's name.
    /// </summary>
    /// <param name="workoutName">Name of the workout.</param>
    /// <returns>Workout.</returns>
    Workout WorkoutFromName(string workoutName);

    /// <summary>
    /// Gets the workout category from the workout category's name.
    /// </summary>
    /// <param name="categoryName">Name of the workout category.</param>
    /// <returns>Workout category.</returns>
    WorkoutCategory WorkoutCategoryFromName(string categoryName);

    /// <summary>
    /// Adds a new workout if no workout exists with the provided workout's name or
    /// replaces the existing workout with the same name.
    /// </summary>
    /// <param name="workout">Workout to add or update.</param>
    void AddOrUpdateWorkout(Workout workout);

    /// <summary>
    /// Removes a workout from the data model.
    /// </summary>
    /// <param name="workout">Workout to remove.</param>
    void RemoveWorkout(Workout workout);

    /// <summary>
    /// Adds a new workout category if no category exists with the provided category's name or
    /// replaces the existing category with the same name.
    /// </summary>
    /// <param name="category">The new workout category to add or update.</param>
    void AddOrUpdateWorkoutCategory(WorkoutCategory category);

    /// <summary>
    /// Removes a workout category from the data model.
    /// </summary>
    /// <param name="category">Category to remove.</param>
    void RemoveWorkoutCategory(WorkoutCategory category);

    /// <summary>
    /// Updates the value of several paces.
    /// </summary>
    /// <param name="keys">Description of the changed paces.</param>
    /// <param name="values">New value of the changed paces.</param>
    void ChangePaces(Pace.Names[] keys, TimeSpan[] values);

    void UpdateTrainingPlan(WeeklyPlan newWeeklyPlan);
  }
}