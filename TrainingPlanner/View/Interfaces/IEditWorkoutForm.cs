using System;
using System.Windows.Forms;
using TrainingPlanner.Model.Serializable;

namespace TrainingPlanner.View.Interfaces
{
  /// <summary>
  /// Defines the functionality required to interact with the EditWorkoutForm.
  /// </summary>
  public interface IEditWorkoutForm
  {
    /// <summary>
    /// The current name of the workout.
    /// </summary>
    string WorkoutName { get; }

    /// <summary>
    /// The current short name of the workout.
    /// </summary>
    string WorkoutShortName { get; }

    /// <summary>
    /// The current name of the category.
    /// </summary>
    string CategoryName { get; }

    /// <summary>
    /// Gets the steps that are currently entered by the user.
    /// </summary>
    Step[] Steps { get; }

    /// <summary>
    /// Triggered when the user presses the "add step"-button.
    /// </summary>
    event EventHandler AddStepButtonClick;

    /// <summary>
    /// Triggered when the user presses the "remove step"-button.
    /// </summary>
    event EventHandler RemoveStepButtonClick;

    /// <summary>
    /// Triggered when the user presses the "save"-button.
    /// </summary>
    event EventHandler SaveButtonClick;

    /// <summary>
    /// Triggered when the user presses the "delete"-button.
    /// </summary>
    event EventHandler DeleteButtonClick;

    /// <summary>
    /// Triggered when the main form is closing.
    /// </summary>
    event EventHandler<FormClosingEventArgs> EditWorkoutFormClosing;

    /// <summary>
    /// Tells the form to add a new step.
    /// </summary>
    void AddStep();

    /// <summary>
    /// Tells the form to remove a step.
    /// </summary>
    void RemoveStep();

    /// <summary>
    /// Updates the list of workout categories.
    /// </summary>
    void SetCategories(string[] categories);

    /// <summary>
    /// Tells the form to close.
    /// </summary>
    void Close();
  }
}