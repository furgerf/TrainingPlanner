using System;

namespace TrainingPlanner.Model
{
  public class WorkoutChangedEventArgs : EventArgs
  {
    public readonly Workout Workout;

    public readonly bool WorkoutAdded;

    public bool WorkoutRemoved { get { return !WorkoutAdded; } }

    public WorkoutChangedEventArgs(Workout workout, bool workoutAdded)
    {
      this.Workout = workout;
      this.WorkoutAdded = workoutAdded;
    }
  }
}