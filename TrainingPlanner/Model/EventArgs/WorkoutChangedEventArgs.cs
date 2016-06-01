using TrainingPlanner.Model.Serializable;

namespace TrainingPlanner.Model.EventArgs
{
  /// <summary>
  /// Contains information about a Workout that was added or removed.
  /// </summary>
  public class WorkoutChangedEventArgs : System.EventArgs
  {
    /// <summary>
    /// The workout that was added or removed.
    /// </summary>
    public readonly Workout Workout;

    /// <summary>
    /// True if the workout was added, false otherwise.
    /// </summary>
    public readonly bool WorkoutAdded;

    public WorkoutChangedEventArgs(Workout workout, bool workoutAdded)
    {
      Workout = workout;
      WorkoutAdded = workoutAdded;
    }
  }
}