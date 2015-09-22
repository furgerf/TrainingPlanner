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
    /// The number of training weeks in the plan. NOTE: move this to the presenter.
    /// </summary>
    int TrainingWeeks { get; }

    /// <summary>
    /// Triggered when the user requests to add new workouts.
    /// </summary>
    event EventHandler AddWorkoutButtonClick;

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
    /// Lets the main form show the EditWorkoutForm.
    /// </summary>
    void ShowEditWorkoutForm(EditWorkoutForm form);

    /// <summary>
    /// Gets a new EditWorkoutForm.
    /// </summary>
    EditWorkoutForm GetEditWorkoutForm();
  }
}