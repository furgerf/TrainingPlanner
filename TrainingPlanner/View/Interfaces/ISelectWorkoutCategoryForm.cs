using System;

namespace TrainingPlanner.View.Interfaces
{
  /// <summary>
  /// Defines the functionality required to interact with the SelectWorkoutCategoryForm.
  /// </summary>
  public interface ISelectWorkoutCategoryForm
  {
    /// <summary>
    /// Triggered when the user selects a workout category.
    /// </summary>
    event EventHandler<string> WorkoutCategorySelected;

    /// <summary>
    /// Sets the workout categories.
    /// </summary>
    /// <param name="categories">The names of the workout categories to set.</param>
    void SetCategories(string[] categories);
  }
}