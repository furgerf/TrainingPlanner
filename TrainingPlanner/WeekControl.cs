using System;
using System.Linq;
using System.Windows.Forms;

namespace TrainingPlanner
{
  public partial class WeekControl : UserControl
  {
    private const int MaxWeeklyWorkouts = 14;

    private readonly Workout[] _workouts = new Workout[MaxWeeklyWorkouts];

    private readonly WorkoutControl[] _workoutControls;

    private bool _updateDateRange = true;

    public WeeklyPlan WeeklyPlan
    {
      get
      {
        var workouts = new string[MaxWeeklyWorkouts];
        for (var i = 0; i < MaxWeeklyWorkouts; i++)
        {
          workouts[i] = _workouts[i] == null ? null : _workouts[i].Name;
        }
        return new WeeklyPlan {WeekStart = monthCalendar1.SelectionStart, Workouts = workouts};
      }
    }

    public DateTime WeekStart
    {
      get { return monthCalendar1.SelectionStart; }
      set { monthCalendar1.SelectionStart = value; }
    }

    public WeekControl()
    {
      InitializeComponent();

      _workoutControls = new[]
      {
        wrkMondayMorning, wrkMondayEvening, wrkTuesdayMorning, wrkTuesdayEvening, wrkWednesdayMorning,
        wrkWednesdayEvening, wrkThursdayMorning, wrkThursdayEvening, wrkFridayMorning, wrkFridayEvening,
        wrkSaturdayMorning, wrkSaturdayEvening, wrkSundayMorning, wrkSundayEvening
      };

      if (_workoutControls.Length != MaxWeeklyWorkouts)
      {
        throw new Exception("Must have exactly " + MaxWeeklyWorkouts + " workout contorls");
      }

      for (var i = 0; i < _workoutControls.Length; i++)
      {
        var i1 = i;
        _workoutControls[i].WorkoutChanged += workout =>
        {
          _workouts[i1] = workout;
          UpdateStatistics();
        };
      }
    }

    public void ReloadWorkouts()
    {
      foreach (var wc in _workoutControls)
      {
        wc.UpdateWorkouts();
      }
    }

    private void UpdateStatistics()
    {
      txtWorkoutCount.Text = string.Format("{0} workouts", _workouts.Count(w => w != null));
      txtTotalDuration.Text = string.Format("Total duration: {0}",
        TimeSpan.FromSeconds(_workouts.Where(w => w != null).Sum(w => w.Duration.TotalSeconds)));
      txtTotalDistance.Text = string.Format("Total distance: {0}", _workouts.Where(w => w != null).Sum(w => w.Distance));
    }

    private void monthCalendar1_DateChanged(object sender, DateRangeEventArgs e)
    {
      if (!_updateDateRange)
      {
        return;
      }

      _updateDateRange = false;

      if (e.Start.DayOfWeek != DayOfWeek.Monday)
      {
        monthCalendar1.SelectionStart = e.Start.Subtract(new TimeSpan((int)e.Start.DayOfWeek, 0, 0, 0));
      }

      if (e.End.DayOfWeek != DayOfWeek.Sunday)
      {
        monthCalendar1.SelectionEnd = WeekStart.AddDays(7);
      }

      _updateDateRange = true;
    }
  }
}
