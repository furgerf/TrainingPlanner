using System;
using TrainingPlanner.Model.EventArgs;

namespace TrainingPlanner.Presenter.Interfaces
{
  public interface INewTrainingPlanFormPresenter
  {
    event EventHandler<NewTrainingPlanEventArgs> NewTrainingPlanDataEntered;
  }
}