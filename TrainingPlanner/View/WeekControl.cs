using System;
using System.Linq;
using System.Windows.Forms;
using TrainingPlanner.Model;

namespace TrainingPlanner.View
{
  public partial class WeekControl : UserControl
  {
    private const int MaxWeeklyWorkouts = 14;

    private readonly Workout[] _workouts = new Workout[MaxWeeklyWorkouts];

    private readonly WorkoutControl[] _workoutControls;

    private bool _updateDateRange = true;

    public event EventHandler<EventArgs<WeeklyPlan>> WeeklyPlanChanged;

    private readonly Data _data;

    public WeeklyPlan WeeklyPlan
    {
      get
      {
        var workouts = new string[MaxWeeklyWorkouts];
        for (var i = 0; i < MaxWeeklyWorkouts; i++)
        {
          workouts[i] = _workouts[i] == null ? null : _workouts[i].Name;
        }
        return new WeeklyPlan {WeekStart = monthCalendar1.SelectionStart, Workouts = workouts, Notes = txtNotes.Text};
      }
      set
      {
        for (var i = 0; i < MaxWeeklyWorkouts; i++)
        {
          if (value.Workouts[i] != null)
          {
            _workoutControls[i].Workout = this._data.WorkoutFromName(value.Workouts[i]);
          }
        }
        txtNotes.Text = value.Notes;
        WeekStart = value.WeekStart;
      }
    }

    public DateTime WeekStart
    {
      get { return monthCalendar1.SelectionStart; }
      set { monthCalendar1.SelectionStart = value; }
    }

    public WeekControl(Data data)
    {
      InitializeComponent();

      this._data = data;

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
        _workoutControls[i].SetData(this._data);
        _workoutControls[i].WorkoutChanged += workout =>
        {
          _workouts[i1] = workout;
          UpdateStatistics();

          if (WeeklyPlanChanged != null)
          {
            WeeklyPlanChanged(this, new EventArgs<WeeklyPlan>(WeeklyPlan));
          }
        };
      }
    }

    private void UpdateStatistics()
    {
      txtWorkoutCount.Text = string.Format("{0} workouts", _workouts.Count(w => w != null));
      txtTotalDuration.Text = string.Format("Total duration: {0}",
        TimeSpan.FromSeconds(_workouts.Where(w => w != null).Sum(w => w.Duration.TotalSeconds)));
      txtTotalDistance.Text = string.Format("Total distance: {0}", Math.Round(_workouts.Where(w => w != null).Sum(w => w.Distance), 1));
    }

    private void monthCalendar1_DateChanged(object sender, DateRangeEventArgs e)
    {
      if (!_updateDateRange)
      {
        return;
      }

      _updateDateRange = false;

      var diff = e.Start.DayOfWeek - DayOfWeek.Sunday;
      if (diff < 0)
      {
        diff += 7;
      }
      monthCalendar1.SelectionStart = e.Start.AddDays(-1 * diff).Date;
      monthCalendar1.SelectionEnd = monthCalendar1.SelectionStart.AddDays(7);

      _updateDateRange = true;
    }
  }
}
