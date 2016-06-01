using System;

namespace TrainingPlanner.View.Interfaces
{
  /// <summary>
  /// Defines the functionality required to interact with the NewTrainingPlanForm.
  /// </summary>
  public interface INewTrainingPlanForm
  {
    /// <summary>
    /// Gets the name of the new training plan.
    /// </summary>
    string NewTrainingPlanName { get; }

    /// <summary>
    /// Gets the number of weeks of the new training plan.
    /// </summary>
    int NumberOfTrainingWeeks { get; }

    /// <summary>
    /// Gets or sets the path to the directory from where to import training data.
    /// This can be either the path to another training plan or the sample data directory.
    /// </summary>
    string PathToTrainingPlanToImportDataFrom { get; set; }

    /// <summary>
    /// Gets the day when the training plan starts.
    /// </summary>
    DateTime StartOfTrainingPlan { get; }

    /// <summary>
    /// Triggered when the user requests to select the training plan from where to import the data.
    /// </summary>
    event EventHandler SelectPlanToImportWorkoutsClick;

    /// <summary>
    /// Triggered when the user requests to create the new training plan.
    /// </summary>
    event EventHandler OkButtonClick;

    /// <summary>
    /// Triggered when the user requests to cancel the creation of the new training plan.
    /// </summary>
    event EventHandler CancelButtonClick;

    /// <summary>
    /// Tells the form to close.
    /// </summary>
    void Close();
  }
}