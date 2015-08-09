using System;
using System.Linq;
using System.Runtime.Serialization;

namespace TrainingPlanner
{
  [DataContract]
  public class Workout
  {
    #region Public Fields

    [DataMember]
    public readonly string Name;

    [DataMember]
    public Step[] Steps { get; private set; }

    public TimeSpan Duration { get { return TimeSpan.FromSeconds(Steps.Sum(s => (s.Duration.TotalSeconds + s.Rest.TotalSeconds) * s.Repetitions)); }}

    public double Distance { get { return Steps.Sum(s => s.Distance * s.Repetitions); }}

    public TimeSpan AveragePace { get { return TimeSpan.FromSeconds(Steps.Sum(s => s.Pace.TotalSeconds * s.Distance) / Distance); }}

    public string Description
    {
      get
      {
          var result = Steps.Aggregate("", (current, s) => current + (Environment.NewLine + s.Name + ": " + s));

          return result.TrimStart();
      }
    }

    #endregion

    #region Constructor

    public Workout(string name, Step[] steps)
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

    public override string ToString()
    {
      return "Workout: " + Name;
    }

    #endregion
  }
}
