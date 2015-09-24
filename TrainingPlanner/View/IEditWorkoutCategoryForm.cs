using System;
using System.Drawing;

namespace TrainingPlanner.View
{
  public interface IEditWorkoutCategoryForm
  {
    event EventHandler SaveButtonClick;

    string CategoryName { get; }

    Color CategoryColor { get; }
  }
}