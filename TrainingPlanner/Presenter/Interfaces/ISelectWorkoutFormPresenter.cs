using System;

namespace TrainingPlanner.Presenter.Interfaces
{
  public interface ISelectWorkoutFormPresenter
  {
    event EventHandler<string> WorkoutSelected;
  }
}