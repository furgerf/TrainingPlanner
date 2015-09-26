using System;

namespace TrainingPlanner.Model
{
  public class PaceChangedEventArgs : EventArgs
  {
    public readonly Pace ModifiedPace;

    public readonly TimeSpan NewPace;

    public PaceChangedEventArgs(Pace modifiedPace, TimeSpan newPace)
    {
      this.ModifiedPace = modifiedPace;
      this.NewPace = newPace;
    }
  }
}
