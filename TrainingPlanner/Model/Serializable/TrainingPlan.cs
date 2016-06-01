using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace TrainingPlanner.Model.Serializable
{
  [DataContract(Name = "TrainingPlan")]
  public sealed class TrainingPlan
  {
    [DataMember(Name = "Name", IsRequired = true)]
    public string Name { get; set; }

    /// <summary>
    /// Number of weeks of the training plan.
    /// </summary>
    [DataMember(Name = "TrainingWeeks", IsRequired = true)]
    public readonly int TrainingWeeks;

    [DataMember(Name = "WeeklyPlans", IsRequired = true)]
    public WeeklyPlan[] WeeklyPlans;

    private Data _data;

    public TrainingPlan(string name, WeeklyPlan[] plans)
    {
      Name = name;
      TrainingWeeks = plans.Length;
      WeeklyPlans = plans;
    }

    public IEnumerable<Workout> AllWorkouts
    {
      get
      {
        var workouts = new List<Workout>();
        foreach (var w in WeeklyPlans)
        {
          workouts.AddRange(w.Workouts.Where(ww => ww != null).Select(ww => _data.WorkoutFromName(ww)));
        }

        return workouts.ToArray();
      }
    }

    public void SetData(Data data)
    {
      _data = data;
    }

    public static TrainingPlan NewTrainingPlan(string name, int weekCount, DateTime startOfTrainingPlan)
    {
      if (startOfTrainingPlan.DayOfWeek != DayOfWeek.Monday)
      {
        throw new ArgumentException();
      }

      Logger.Info("Creating new empty TrainingPlan");
      var monday = startOfTrainingPlan.Date;

      var weeks = new WeeklyPlan[weekCount];
      for (var i = 0; i < weekCount; i++)
      {
        weeks[i] = new WeeklyPlan(new string[14], monday.AddDays(i*7), i);
      }

      return new TrainingPlan(name, weeks);
    }

    public override string ToString()
    {
      return string.Format("{0}-week plan starting {1}", TrainingWeeks, WeeklyPlans[0].WeekStart);
    }
  }
}
