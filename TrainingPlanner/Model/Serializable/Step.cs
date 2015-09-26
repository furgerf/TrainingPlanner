using System;
using System.Runtime.Serialization;

namespace TrainingPlanner.Model.Serializable
{
  /// <summary>
  /// Describes one segment in a workout.
  /// </summary>
  [DataContract(Name = "Step")]
  public struct Step
  {
    /// <summary>
    /// Name of the step.
    /// </summary>
    [DataMember(Name = "Name", IsRequired = true)]
    public readonly string Name;

    /// <summary>
    /// Duration of the step.
    /// </summary>
    [DataMember(Name = "Duration", IsRequired = true)]
    public readonly TimeSpan Duration;

    /// <summary>
    /// Pace at which to run during the step.
    /// TODO: Store enum name instead of timespan
    /// </summary>
    [DataMember(Name = "Pace", IsRequired = true)]
    public readonly TimeSpan Pace;

    /// <summary>
    /// Distance to cover during the step.
    /// </summary>
    [DataMember(Name = "Distance", IsRequired = true)]
    public readonly double Distance;

    /// <summary>
    /// Rest after the step.
    /// </summary>
    [DataMember(Name = "Rest", IsRequired = true)]
    public readonly TimeSpan Rest;

    /// <summary>
    /// Number of times this step is repeated.
    /// </summary>
    [DataMember(Name = "Repetitions", IsRequired = true)]
    public readonly int Repetitions;

    /// <summary>
    /// True if distance is the manually entered value and duration was calculated.
    /// TODO: Rename
    /// </summary>
    [DataMember(Name = "_durationCalculated", IsRequired = true)]
    public readonly bool DurationCalculated;

    /// <summary>
    /// True if duration is the manually entered value and distance was calculated.
    /// TODO: Rename
    /// </summary>
    [DataMember(Name = "_distanceCalculated", IsRequired = true)]
    public readonly bool DistanceCalculated;

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
      DurationCalculated = durationCalculated;
      DistanceCalculated = distanceCalculated;
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

      if (DurationCalculated)
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