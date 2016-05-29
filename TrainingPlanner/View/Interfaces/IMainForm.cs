using System;
using TrainingPlanner.Model;
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
    event EventHandler<string> OpenSpecificPlanClick;
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

    /// <summary>
    /// Changes the "active" status of a week (-control).
    /// </summary>
    /// <param name="week">Week number.</param>
    /// <param name="isActive">True if the week is the active week.</param>
    void SetWeekActivity(int week, bool isActive);

    /// <summary>
    /// Scrolls to the specified week.
    /// </summary>
    /// <param name="week">Week number to scroll to.</param>
    void ScrollToWeek(int week);

    /// <summary>
    /// Assigns a new data instance to the view.
    /// </summary>
    /// <param name="data">The new data instance.</param>
    void SetNewData(Data data);

    /// <summary>
    /// Enables or disables the training-plan related menu entries.
    /// </summary>
    /// <param name="isEnbabled">True if the menu entrires should be enabled.</param>
    void SetTrainingPlanMenusEnabled(bool isEnbabled);
  }
}