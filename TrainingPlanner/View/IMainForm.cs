using System;
using TrainingPlanner.Model;

namespace TrainingPlanner.View
{
  /// <summary>
  /// Describes the functionality required to interact with the main form.
  /// </summary>
  public interface IMainForm
  {
    /// <summary>
    /// Triggered when the user requests to add new workouts.
    /// </summary>
    event EventHandler AddWorkoutButtonClick;

    /// <summary>
    /// Triggered when the user requests to configure paces.
    /// </summary>
    event EventHandler ConfigurePacesButtonClick;

    /// <summary>
    /// Triggered when the main form is closing.
    /// </summary>
    event EventHandler MainFormClosing;

    /// <summary>
    /// Triggered when a workout in one of the weekly plans changed.
    /// </summary>
    event EventHandler<EventArgs<WeeklyPlan[]>> WeeklyPlansChanged;

    /// <summary>
    /// Updates the view with a new set of WeeklyPlans.
    /// </summary>
    /// <param name="weeklyPlans"></param>
    void UpdateWeeklyPlan(WeeklyPlan[] weeklyPlans);

    /// <summary>
    /// Gets a new EditWorkoutForm.
    /// </summary>
    EditWorkoutForm GetEditWorkoutForm();

    /// <summary>
    /// Gets a new PaceForm.
    /// </summary>
    /// <returns></returns>
    PaceForm GetPaceForm();
  }
}