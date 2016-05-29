using System;

namespace TrainingPlanner.Model.EventArgs
{
  public class NewTrainingPlanEventArgs : System.EventArgs
  {
    public readonly string PlanName;

    public readonly int TrainingWeeks;

    public readonly string PlanToImportWorkoutsFrom;

    public NewTrainingPlanEventArgs(string planName, int trainingWeeks, string planToImportWorkoutsFrom)
    {
      PlanName = planName;
      TrainingWeeks = trainingWeeks;
      PlanToImportWorkoutsFrom = planToImportWorkoutsFrom;
    }
  }
}
