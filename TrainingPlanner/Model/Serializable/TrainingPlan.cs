using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace TrainingPlanner.Model.Serializable
{
  [DataContract(Name = "TrainingPlan")]
  public sealed class TrainingPlan
  {
    /// <summary>
    /// Number of weeks of the training plan.
    /// </summary>
    [DataMember(Name = "TrainingWeeks", IsRequired = true)]
    public readonly int TrainingWeeks;

    [DataMember(Name = "WeeklyPlans", IsRequired = true)]
    public WeeklyPlan[] WeeklyPlans;

    private Data _data;

    public TrainingPlan(WeeklyPlan[] plans)
    {
      this.TrainingWeeks = plans.Length;
      this.WeeklyPlans = plans;
    }

    public Workout[] AllWorkouts
    {
      get
      {
        var workouts = new List<Workout>();
        foreach (var w in WeeklyPlans)
        {
          workouts.AddRange(w.Workouts.Where(ww => ww != null).Select(ww => this._data.WorkoutFromName(ww)));
        }

        return workouts.ToArray();
      }
    }

    public void SetData(Data data)
    {
      this._data = data;
    }

    public static TrainingPlan NewTrainingPlan(int weekCount)
    {
        Logger.Info("Creating new empty TrainingPlan");
        var diff = DateTime.Today.DayOfWeek - DayOfWeek.Sunday;
        if (diff < 0)
        {
          diff += 7;
        }
        var monday = DateTime.Today.AddDays(-1 * diff).Date;

        var weeks = new WeeklyPlan[weekCount];
        for (var i = 0; i < weekCount; i++)
        {
          weeks[i] = new WeeklyPlan(new string[14], monday.AddDays(i*7), i);
        }

      return new TrainingPlan(weeks);
    }

    public override string ToString()
    {
      return string.Format("{0}-week plan starting {1}", TrainingWeeks, this.WeeklyPlans[0].WeekStart);
    }
  }
}
