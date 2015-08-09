using System;

namespace TrainingPlanner
{
  public struct Step
  {
    public readonly string Name;

    public readonly TimeSpan Duration;

    public readonly TimeSpan Pace;

    public readonly double Distance;

    public readonly TimeSpan Rest;

    public readonly int Repetitions;

    public static Step Warmup { get { return new Step("Warmup", new TimeSpan(0, 10, 0), new TimeSpan(0, 5, 15));} }

    public static Step Cooldown { get { return new Step("Cooldown", new TimeSpan(0, 5, 0), new TimeSpan(0, 5, 30));} }

    private Step(string name, TimeSpan duration, TimeSpan pace, double distance, TimeSpan? rest = null, int repetitions = 1) : this()
    {
      Name = name;
      Duration = duration;
      Pace = pace;
      Distance = distance;
      Rest = rest ?? TimeSpan.Zero;
      Repetitions = repetitions;
    }

    public Step(string name, TimeSpan duration, TimeSpan pace, TimeSpan? rest = null, int repetitions = 1)
      : this(name, duration, pace, duration.TotalSeconds/pace.TotalSeconds, rest, repetitions)
    {
    }

    public Step(string name, TimeSpan duration, double distance, TimeSpan? rest = null, int repetitions = 1)
      : this(name, duration, TimeSpan.FromSeconds(distance/duration.TotalSeconds), distance, rest, repetitions)
    {
    }

    public Step(string name, double distance, TimeSpan pace, TimeSpan? rest = null, int repetitions = 1)
      : this(name, TimeSpan.FromSeconds(distance/pace.TotalSeconds), pace, distance, rest, repetitions)
    {
    }

    public override string ToString()
    {
      var result = string.Format("{0} at {1}", Duration, Pace);

      if (Rest != TimeSpan.Zero)
      {
        result += string.Format(" with {0} rest", Rest);
      }

      if (Repetitions > 1)
      {
        result = string.Format("({0}) x {1}", result, Repetitions);
      }

      return result;
    }
  }
}