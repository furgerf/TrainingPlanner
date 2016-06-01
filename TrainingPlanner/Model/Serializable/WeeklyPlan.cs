using System;
using System.Runtime.Serialization;

namespace TrainingPlanner.Model.Serializable
{
  /// <summary>
  /// Describes a week's worth of training (up to 14 workouts).
  /// </summary>
  [DataContract(Name = "WeeklyPlan")]
  public struct WeeklyPlan
  {
    /// <summary>
    /// Number of the week in the plan. Should be zero-based.
    /// </summary>
    [DataMember(Name = "WeekNumber", IsRequired = true)]
    public readonly int WeekNumber;

    /// <summary>
    /// The names of the workouts of the week.
    /// </summary>
    [DataMember(Name = "Workouts", IsRequired = true)]
    private readonly string[] _workouts;

    /// <summary>
    /// Date of the start of the week (Monday).
    /// </summary>
    [DataMember(Name = "WeekStart", IsRequired = true)]
    public DateTime WeekStart;

    /// <summary>
    /// Notes for the week.
    /// </summary>
    [DataMember(Name = "Notes")]
    public string Notes;

    public WeeklyPlan(string[] workouts, DateTime weekStart, int weekNumber, string notes = "")
    {
      _workouts = workouts;
      WeekStart = weekStart;
      WeekNumber = weekNumber;
      Notes = notes;
    }

    /// <summary>
    /// Wrapper around the backing field with the sole purpose to ensure that
    /// arrays are assigned with the right dimension.
    /// </summary>
    public string[] Workouts
    {
      get { return _workouts; }
    }

    public DateTime WeekEnd
    {
      get { return WeekStart.AddDays(7); }
    }

    public override string ToString()
    {
      return string.Format("Week {0} starting {1}", WeekNumber, WeekStart);
    }
  }
}
