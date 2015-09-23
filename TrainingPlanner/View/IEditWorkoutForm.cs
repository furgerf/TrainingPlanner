using System;
using System.Windows.Forms;
using TrainingPlanner.Model;

namespace TrainingPlanner.View
{
  public interface IEditWorkoutForm
  {
    /// <summary>
    /// Tells the form to close.
    /// </summary>
    void Close();

    /// <summary>
    /// Tells the form to add a new step.
    /// </summary>
    void AddStep();

    /// <summary>
    /// Tells the form to remove a step.
    /// </summary>
    void RemoveStep();

    /// <summary>
    /// The steps that are currently entered by the user.
    /// </summary>
    Step[] Steps { get; }

    /// <summary>
    /// The current name of the workout.
    /// </summary>
    string WorkoutName { get; }

    /// <summary>
    /// The current name of the category.
    /// </summary>
    string CategoryName { get; }

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
    /// Triggered when the main form is closing.
    /// </summary>
    event EventHandler<FormClosingEventArgs> EditWorkoutFormClosing;

    /// <summary>
    /// Updates the list of workout categories.
    /// </summary>
    void SetCategories(string[] categories);
  }
}