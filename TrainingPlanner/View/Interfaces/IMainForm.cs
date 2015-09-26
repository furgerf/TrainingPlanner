using System;
using TrainingPlanner.Model.EventArgs;
using TrainingPlanner.Model.Serializable;

namespace TrainingPlanner.View.Interfaces
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
    /// Triggered when the user requests to edit a workout.
    /// </summary>
    event EventHandler<string> EditWorkoutButtonClick;

    /// <summary>
    /// Triggered when the user requests to edit a workout.
    /// </summary>
    event EventHandler EditCategoriesButtonClick;

    /// <summary>
    /// Triggered when a workout in one of the weekly plans changed.
    /// </summary>
    event EventHandler<EventArgs<WeeklyPlan>> WeeklyPlanChanged;

    /// <summary>
    /// Updates the view with a new set of WeeklyPlans.
    /// </summary>
    /// <param name="weeklyPlans"></param>
    void UpdateWeeklyPlan(WeeklyPlan[] weeklyPlans);
  }
}