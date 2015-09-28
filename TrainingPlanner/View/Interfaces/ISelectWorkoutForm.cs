using System;

namespace TrainingPlanner.View.Interfaces
{
  public interface ISelectWorkoutForm
  {
    event EventHandler<string> WorkoutSelected;

    event EventHandler<string> WorkoutCategoryChanged;

    string CurrentCategory { get; }

    void SetCategories(string[] categories);

    void SetWorkouts(string[] workouts);

    void Close();
  }
}