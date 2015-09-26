using System;
using System.Drawing;

namespace TrainingPlanner.View.Interfaces
{
  public interface IEditWorkoutCategoryForm
  {
    /// <summary>
    /// Gets the currently entered category name.
    /// </summary>
    string CategoryName { get; }

    /// <summary>
    /// Gets the currently entered category color.
    /// </summary>
    Color CategoryColor { get; }

    /// <summary>
    /// Triggered when the user clicks the "save" button.
    /// </summary>
    event EventHandler SaveButtonClick;
  }
}