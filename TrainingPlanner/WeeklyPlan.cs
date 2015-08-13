using System;

namespace TrainingPlanner
{
  public struct WeeklyPlan
  {
    private string[] _workouts;

    public DateTime WeekStart { get; set; }

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
