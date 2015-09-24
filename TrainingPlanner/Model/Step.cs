using System;
using System.Runtime.Serialization;

namespace TrainingPlanner.Model
{
  /// <summary>
  /// Describes one segment in a workout.
  /// </summary>
  [DataContract]
  public struct Step
  {
    /// <summary>
    /// Name of the step.
    /// </summary>
    [DataMember]
    public readonly string Name;

    /// <summary>
    /// Duration of the step.
    /// </summary>
    [DataMember]
    public readonly TimeSpan Duration;

    /// <summary>
    /// Pace at which to run during the step.
    /// </summary>
    [DataMember]
    public readonly TimeSpan Pace;

    /// <summary>
    /// Distance to cover during the step.
    /// </summary>
    [DataMember]
    public readonly double Distance;

    /// <summary>
    /// Rest after the step.
    /// </summary>
    [DataMember]
    public readonly TimeSpan Rest;

    /// <summary>
    /// Number of times this step is repeated.
    /// </summary>
    [DataMember]
    public readonly int Repetitions;

    /// <summary>
    /// True if distance is the manually entered value and duration was calculated.
    /// TODO: Rename...
    /// </summary>
    [DataMember]
    public readonly bool _durationCalculated;

    /// <summary>
    /// True if duration is the manually entered value and distance was calculated.
    /// TODO: Rename...
    /// </summary>
    [DataMember]
    public readonly bool _distanceCalculated;

    // TODO: Add note

    /// <summary>
    /// Format with which to convert the pace to string.
    /// </summary>
    private const string PaceFormat = @"mm\:ss";

    /// <summary>
    /// Gets a new Warmup-step.
    /// </summary>
    public static Step Warmup { get { return new Step("Warmup", new TimeSpan(0, 10, 0), Paces.Default.Warmup); } }

    /// <summary>
    /// Gets a new Cooldown-step.
    /// </summary>
    public static Step Cooldown { get { return new Step("Cooldown", new TimeSpan(0, 5, 0), Paces.Default.Cooldown); } }

    /// <summary>
    /// Gets a new empty step.
    /// </summary>
    public static Step Empty { get { return new Step(); } }

    /// <summary>
    /// Returns true if the step has no information.
    /// </summary>
    public bool IsEmpty { get { return Equals(Empty); } }

    private Step(string name, TimeSpan duration, TimeSpan pace, double distance, bool durationCalculated, bool distanceCalculated, TimeSpan? rest = null, int repetitions = 1)
      : this()
    {
      if (!durationCalculated ^ distanceCalculated)
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

    /// <summary>
    /// Create a new step with the length being determined by the duration.
    /// </summary>
    public Step(string name, TimeSpan duration, TimeSpan pace, TimeSpan? rest = null, int repetitions = 1)
      : this(name, duration, pace, duration.TotalSeconds / pace.TotalSeconds, false, true, rest, repetitions)
    {
    }

    /// <summary>
    /// Create a new step with the length being determined by the distance.
    /// </summary>
    public Step(string name, double distance, TimeSpan pace, TimeSpan? rest = null, int repetitions = 1)
      : this(name, TimeSpan.FromSeconds(distance / pace.TotalSeconds), pace, distance, true, false, rest, repetitions)
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
