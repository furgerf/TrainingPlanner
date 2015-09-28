using System;

namespace TrainingPlanner.View.Interfaces
{
  public interface ISelectWorkoutCategoryForm
  {
    event EventHandler<string> WorkoutCategorySelected;

    void SetCategories(string[] categories);

    void Close();
  }
}