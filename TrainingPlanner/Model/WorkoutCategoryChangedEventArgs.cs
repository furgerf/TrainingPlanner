using System;

namespace TrainingPlanner.Model
{
  public class WorkoutCategoryChangedEventArgs : EventArgs
  {
    public readonly WorkoutCategory WorkoutCategory;

    public readonly bool CategoryAdded;

    public bool CategoryRemoved { get { return !CategoryAdded; } }

    public WorkoutCategoryChangedEventArgs(WorkoutCategory category, bool categoryAdded)
    {
      this.WorkoutCategory = category;
      this.CategoryAdded = categoryAdded;
    }
  }
}