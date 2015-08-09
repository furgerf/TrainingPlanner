using System;
using System.Linq;

namespace TrainingPlanner
{
  public class Workout
  {
    #region Public Fields

    public readonly string Name;

    public Step[] Steps { get; private set; }

    public TimeSpan Duration { get { return TimeSpan.FromSeconds(Steps.Sum(s => s.Duration.TotalSeconds)); }}

    public double Distance { get { return Steps.Sum(s => s.Distance); }}

    public TimeSpan AveragePace { get { return TimeSpan.FromSeconds(Steps.Sum(s => s.Duration.TotalSeconds) / Steps.Length); }}

    #endregion

    #region Constructor

    private Workout(string name, Step[] steps)
    {
      Name = name;
      Steps = steps;
    }

    #endregion

    #region Main Methods

    public static Workout Parse(string data)
    {
      return null;
    }

    #endregion
  }
}
