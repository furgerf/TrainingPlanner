using System;
using System.Runtime.Serialization;

namespace TrainingPlanner.Model.Serializable
{
  [DataContract(Name = "TrainingPlan")]
  public class TrainingPlan
  {
    /// <summary>
    /// Number of weeks of the training plan.
    /// </summary>
    public const int TrainingWeeks = 11;

    [DataMember(Name = "WeeklyPlans", IsRequired = true)]
    public WeeklyPlan[] WeeklyPlans;

    public static TrainingPlan NewTrainingPlan
    {
      get
      {
        Logger.Info("Creating new empty TrainingPlan");
        var diff = DateTime.Today.DayOfWeek - DayOfWeek.Sunday;
        if (diff < 0)
        {
          diff += 7;
        }
        var monday = DateTime.Today.AddDays(-1 * diff).Date;

        var weeks = new WeeklyPlan[TrainingWeeks];
        for (var i = 0; i < TrainingWeeks; i++)
        {
          weeks[i] = new WeeklyPlan { WeekNumber = i, WeekStart = monday.AddDays(i * 7), Workouts = new string[14] };
        }

        return new TrainingPlan { WeeklyPlans = weeks };
      }
    }
  }
}
