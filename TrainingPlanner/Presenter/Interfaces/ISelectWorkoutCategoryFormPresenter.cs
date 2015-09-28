using System;

namespace TrainingPlanner.Presenter.Interfaces
{
  public interface ISelectWorkoutCategoryFormPresenter
  {
    event EventHandler<string> CategorySelected;
  }
}