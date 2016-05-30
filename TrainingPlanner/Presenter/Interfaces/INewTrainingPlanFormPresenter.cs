using System;
using TrainingPlanner.Model.EventArgs;

namespace TrainingPlanner.Presenter.Interfaces
{
  public interface INewTrainingPlanFormPresenter
  {
    /// <summary>
    /// Triggered when information to create a new training plan has been entered.
    /// </summary>
    event EventHandler<NewTrainingPlanEventArgs> NewTrainingPlanDataEntered;
  }
}