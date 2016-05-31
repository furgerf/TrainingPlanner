using System;

namespace TrainingPlanner.Model.EventArgs
{
  /// <summary>
  /// Contains information required to create a new training plan.
  /// </summary>
  public class NewTrainingPlanEventArgs : System.EventArgs
  {
    /// <summary>
    /// Name of the new plan.
    /// </summary>
    public readonly string Name;

    /// <summary>
    /// Number of weeks of the plan.
    /// </summary>
    public readonly int TrainingWeeks;

    /// <summary>
    /// Name of another training plan from where to import various data
    /// such as Paces, Workouts, WorkoutCategories.
    /// </summary>
    public readonly string OtherTrainingPlanToImportDataFrom;

    /// <summary>
    /// Day when the training plan starts.
    /// </summary>
    public readonly DateTime StartOfTrainingPlan;

    public NewTrainingPlanEventArgs(string name, int trainingWeeks, string otherTrainingPlanToImportDataFrom, DateTime startOfTrainingPlan)
    {
      Name = name;
      TrainingWeeks = trainingWeeks;
      OtherTrainingPlanToImportDataFrom = otherTrainingPlanToImportDataFrom;
      StartOfTrainingPlan = startOfTrainingPlan;
    }
  }
}
