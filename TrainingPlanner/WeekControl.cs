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
  }
}
