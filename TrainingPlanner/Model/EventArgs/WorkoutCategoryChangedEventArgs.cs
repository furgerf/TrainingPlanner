using TrainingPlanner.Model.Serializable;

namespace TrainingPlanner.Model.EventArgs
{
  public class WorkoutCategoryChangedEventArgs : System.EventArgs
  {
    public readonly WorkoutCategory WorkoutCategory;

    public readonly bool CategoryAdded;

    public bool CategoryRemoved { get { return !CategoryAdded; } }

    public WorkoutCategoryChangedEventArgs(WorkoutCategory category, bool categoryAdded)
    {
      WorkoutCategory = category;
      CategoryAdded = categoryAdded;
    }
  }
}