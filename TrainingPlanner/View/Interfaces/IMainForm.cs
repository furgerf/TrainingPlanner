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
    event EventHandler NewPlanClick;
    event EventHandler OpenPlanClick;
    event EventHandler ClosePlanClick;
    event EventHandler AddWorkoutClick;
    event EventHandler<string> EditWorkoutClick;
    event EventHandler<string> DeleteWorkoutClick;
    event EventHandler ManageWorkoutsClick;
    event EventHandler AddWorkoutCategoryClick;
    event EventHandler<string> EditWorkoutCategoryClick;
    event EventHandler<string> DeleteWorkoutCategoryClick;
    event EventHandler ManageWorkoutCategoriesClick;
    event EventHandler ConfigurePacesClick;
    event EventHandler InfoClick;

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