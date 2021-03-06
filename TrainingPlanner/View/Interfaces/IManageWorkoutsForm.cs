﻿using System;
using System.Collections.Generic;
using TrainingPlanner.Model.Serializable;

namespace TrainingPlanner.View.Interfaces
{
  /// <summary>
  /// Defines the functionality required to interact with the ManageWorkoutsForm.
  /// </summary>
  public interface IManageWorkoutsForm
  {
    /// <summary>
    /// Triggered when the user requests to add a workout.
    /// </summary>
    event EventHandler AddWorkoutButtonClick;
     
    /// <summary>
    /// Triggered when the user requests to edit a workout.
    /// </summary>
    event EventHandler<string> EditWorkoutButtonClick;
     
    /// <summary>
    /// Triggered when the user requests to delete a workout.
    /// </summary>
    event EventHandler<string> DeleteWorkoutButtonClick;
     
    /// <summary>
    /// Triggered when the user requests to exit.
    /// </summary>
    event EventHandler ExitButtonClick;

    /// <summary>
    /// Displays the supplied workouts.
    /// </summary>
    /// <param name="workouts">Workouts to display.</param>
    void DisplayWorkouts(IEnumerable<Workout> workouts);

    /// <summary>
    /// Tells the form to close.
    /// </summary>
    void Close();
  }
}