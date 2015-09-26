using System;
using System.Runtime.Serialization;

namespace TrainingPlanner.Model
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
    public int WeekNumber;

    /// <summary>
    /// The names of the workouts of the week.
    /// </summary>
    [DataMember(Name = "Workouts", IsRequired = true)]
    private string[] _workouts;

    /// <summary>
    /// Date of the start of the week (Monday).
    /// </summary>
    [DataMember(Name = "WeekStart", IsRequired = true)]
    public DateTime WeekStart { get; set; }

    /// <summary>
    /// Notes for the week.
    /// </summary>
    [DataMember(Name = "Notes")]
    public string Notes { get; set; }

    /// <summary>
    /// Wrapper around the backing field with the sole purpose to ensure that
    /// arrays are assigned with the right dimension.
    /// </summary>
    public string[] Workouts
    {
      get { return _workouts; }
      set
      {
        if (value == null || value.Length != 14)
        {
          throw new ArgumentException("Invalid workout array");
        }
        _workouts = value;
      }
    }
  }
}
