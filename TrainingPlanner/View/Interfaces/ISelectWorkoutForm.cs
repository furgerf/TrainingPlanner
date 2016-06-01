using System;

namespace TrainingPlanner.View.Interfaces
{
  /// <summary>
  /// Defines the functionality required to interact with the SelectWorkoutCategoryForm.
  /// </summary>
  public interface ISelectWorkoutForm
  {
    /// <summary>
    /// Triggered when the user selects a workout.
    /// </summary>
    event EventHandler<string> WorkoutSelected;

    /// <summary>
    /// Triggered when the user selects a workout category.
    /// </summary>
    event EventHandler<string> WorkoutCategoryChanged;

    /// <summary>
    /// Gets the name of the current workout category.
    /// </summary>
    string CurrentCategory { get; }

    /// <summary>
    /// Sets the workout categories.
    /// </summary>
    /// <param name="categories">The names of the workout categories to set.</param>
    void SetCategories(string[] categories);

    /// <summary>
    /// Sets the workouts.
    /// </summary>
    /// <param name="workouts">The names of the workouts to set.</param>
    void SetWorkouts(string[] workouts);
  }
}