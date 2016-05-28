using TrainingPlanner.Model.Serializable;

namespace TrainingPlanner.Model.EventArgs
{
  public class WorkoutChangedEventArgs : System.EventArgs
  {
    public readonly Workout Workout;

    public readonly bool WorkoutAdded;

    public bool WorkoutRemoved { get { return !WorkoutAdded; } }

    public WorkoutChangedEventArgs(Workout workout, bool workoutAdded)
    {
      Workout = workout;
      WorkoutAdded = workoutAdded;
    }
  }
}