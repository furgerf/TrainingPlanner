﻿using System;
using TrainingPlanner.Model.Serializable;

namespace TrainingPlanner.View.Interfaces
{
  public interface IManageWorkoutCategoriesForm
  {
    /// <summary>
    /// Triggered when the user requests to add a category.
    /// </summary>
    event EventHandler AddCategoryButtonClick;
     
    /// <summary>
    /// Triggered when the user requests to edit a category.
    /// </summary>
    event EventHandler<string> EditCategoryButtonClick;
     
    /// <summary>
    /// Triggered when the user requests to delete a category.
    /// </summary>
    event EventHandler<string> DeleteCategoryButtonClick;
     
    /// <summary>
    /// Triggered when the user requests to exit.
    /// </summary>
    event EventHandler ExitButtonClick;

    /// <summary>
    /// Displays the supplied workout categories.
    /// </summary>
    /// <param name="categories">Workout categories to display.</param>
    void DisplayCategories(WorkoutCategory[] categories);

    /// <summary>
    /// Tells the form to close.
    /// </summary>
    void Close();
  }
}