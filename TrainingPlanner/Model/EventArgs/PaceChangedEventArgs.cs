using System;

namespace TrainingPlanner.Model.EventArgs
{
  public class PaceChangedEventArgs : System.EventArgs
  {
    public readonly PaceNames ModifiedPace;

    public readonly TimeSpan NewPace;

    public PaceChangedEventArgs(PaceNames modifiedPace, TimeSpan newPace)
    {
      ModifiedPace = modifiedPace;
      NewPace = newPace;
    }
  }
}
