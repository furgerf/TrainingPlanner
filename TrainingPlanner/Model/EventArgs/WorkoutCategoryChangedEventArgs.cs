using TrainingPlanner.Model.Serializable;

namespace TrainingPlanner.Model.EventArgs
{
  /// <summary>
  /// Contains information about a WorkoutCategory that was added or removed.
  /// </summary>
  public class WorkoutCategoryChangedEventArgs : System.EventArgs
  {
    /// <summary>
    /// The category that was added or removed.
    /// </summary>
    public readonly WorkoutCategory WorkoutCategory;

    /// <summary>
    /// True if the category was added, false otherwise.
    /// </summary>
    public readonly bool CategoryAdded;

    public WorkoutCategoryChangedEventArgs(WorkoutCategory category, bool categoryAdded)
    {
      WorkoutCategory = category;
      CategoryAdded = categoryAdded;
    }
  }
}