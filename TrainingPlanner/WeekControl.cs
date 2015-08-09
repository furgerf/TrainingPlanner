using System;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace TrainingPlanner
{
  public partial class WeekControl : UserControl
  {
    private const int MaxWeeklyWorkouts = 14;

    private Workout[] _workouts;

    public Workout[] Workouts
    {
      get { return _workouts; }
      set
      {
        if (value.Length != MaxWeeklyWorkouts)
        {
          throw new ArgumentException("Must provide exactly " + MaxWeeklyWorkouts + " workouts");
        }

        _workouts = value;

        for (var i = 0; i < MaxWeeklyWorkouts; i++)
        {
          _workoutControls[i].Workout = _workouts[i];
        }
      }
    }

    private readonly WorkoutControl[] _workoutControls;

    public WeekControl()
    {
      InitializeComponent();

      _workoutControls = new[]
      {
        wrkMondayMorning, wrkMondayEvening, wrkTuesdayMorning, wrkThursdayEvening, wrkWednesdayMorning,
        wrkWednesdayEvening, wrkThursdayMorning, wrkThursdayEvening, wrkFridayMorning, wrkFridayEvening,
        wrkSaturdayMorning, wrkSaturdayEvening, wrkSundayMorning, wrkSundayEvening
      };

      if (_workoutControls.Length != MaxWeeklyWorkouts)
      {
        throw new Exception("Must have exactly " + MaxWeeklyWorkouts + " workout contorls");
      }

      foreach (var wc in _workoutControls)
      {
        wc.WorkoutChanged += workout => UpdateStatistics();
      }
    }

    private void UpdateStatistics()
    {
      txtWorkoutCount.Text = string.Format("{0} workouts", _workouts.Count(w => w != null));
      txtTotalDuration.Text = string.Format("Total duration: {0}",
        TimeSpan.FromSeconds(_workouts.Where(w => w != null).Sum(w => w.Duration.TotalSeconds)));
      txtTotalDistance.Text = string.Format("Total distance: {0}", _workouts.Where(w => w != null).Sum(w => w.Distance));
    }
  }
}
