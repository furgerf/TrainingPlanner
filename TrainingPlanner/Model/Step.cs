using System;
using System.Runtime.Serialization;

namespace TrainingPlanner.Model
{
  [DataContract]
  public struct Step
  {
    [DataMember]
    public readonly string Name;

    [DataMember]
    public readonly TimeSpan Duration;

    [DataMember]
    public readonly TimeSpan Pace;

    [DataMember]
    public readonly double Distance;

    [DataMember]
    public readonly TimeSpan Rest;

    [DataMember]
    public readonly int Repetitions;

    [DataMember]
    private readonly bool _durationCalculated;

    [DataMember]
    private readonly bool _distanceCalculated;

    private const string PaceFormat = @"mm\:ss";

    public static Step Warmup { get { return new Step("Warmup", new TimeSpan(0, 10, 0), Paces.Default.Warmup); } }

    public static Step Cooldown { get { return new Step("Cooldown", new TimeSpan(0, 5, 0), Paces.Default.Cooldown);} }

    public static Step Empty { get { return new Step(); } }

    public bool IsEmpty { get { return Equals(Empty); } }

    private Step(string name, TimeSpan duration, TimeSpan pace, double distance, bool durationCalculated, bool distanceCalculated, TimeSpan? rest = null, int repetitions = 1) : this()
    {
      if (! durationCalculated ^ distanceCalculated)
      {
        throw new ArgumentException("Exactly one of duration and distance can have been calculated");
      }

      Name = name;
      Duration = duration;
      Pace = pace;
      Distance = distance;
      Rest = rest ?? TimeSpan.Zero;
      Repetitions = repetitions;
      _durationCalculated = durationCalculated;
      _distanceCalculated = distanceCalculated;
    }

    public Step(string name, TimeSpan duration, TimeSpan pace, TimeSpan? rest = null, int repetitions = 1)
      : this(name, duration, pace, duration.TotalSeconds/pace.TotalSeconds, false, true, rest, repetitions)
    {
    }

    public Step(string name, double distance, TimeSpan pace, TimeSpan? rest = null, int repetitions = 1)
      : this(name, TimeSpan.FromSeconds(distance/pace.TotalSeconds), pace, distance, true, false, rest, repetitions)
    {
    }

    public override string ToString()
    {
      var result = "";

      if (_durationCalculated)
      {
        result += Distance + " km";
      }
      else
      {
        if (Duration.Hours >= 1)
        {
          result += Duration.ToString(@"h\:mm") + " h";
        }
        else
        {
          result += Duration.ToString(@"mm") + " min";
        }
      }

      result += " at " + Pace.ToString(PaceFormat);

      if (Rest != TimeSpan.Zero)
      {
        result += string.Format(" with {0} rest", Rest.ToString(@"m\:ss"));
      }

      if (Repetitions > 1)
      {
        result = string.Format("({0}) x {1}", result, Repetitions);
      }

      return result;
    }
  }
}
