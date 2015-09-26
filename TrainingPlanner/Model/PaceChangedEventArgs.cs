using System;

namespace TrainingPlanner.Model
{
  public class PaceChangedEventArgs : EventArgs
  {
    public readonly Pace ModifiedPace;

    public PaceChangedEventArgs(Pace modifiedPace)
    {
      this.ModifiedPace = modifiedPace;
    }
  }
}
