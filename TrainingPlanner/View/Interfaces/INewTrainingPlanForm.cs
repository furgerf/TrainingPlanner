using System;

namespace TrainingPlanner.View.Interfaces
{
  public interface INewTrainingPlanForm
  {
    string NewTrainingPlanName { get; }

    int NumberOfTrainingWeeks { get; }

    string PathToTrainingPlanToImportDataFrom { get; set; }

    DateTime StartOfTrainingPlan { get; }

    event EventHandler SelectPlanToImportWorkoutsClick;

    event EventHandler OkButtonClick;

    event EventHandler CancelButtonClick;

    void Close();
  }
}