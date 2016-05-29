using System;

namespace TrainingPlanner.View.Interfaces
{
  public interface INewTrainingPlanForm
  {
    // TODO: Add selectable start date of the plan

    string NewTrainingPlanName { get; }

    int NumberOfTrainingWeeks { get; }

    string TrainingPlanToImportWorkoutsFrom { get; set; }

    event EventHandler SelectPlanToImportWorkoutsClick;

    event EventHandler OkButtonClick;

    event EventHandler CancelButtonClick;

    void Close();
  }
}