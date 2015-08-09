using System;
using System.Linq;
using System.Windows.Forms;

namespace TrainingPlanner
{
  public partial class WorkoutControl : UserControl
  {
    public delegate void OnWorkoutChanged(Workout newWorkout);

    public event OnWorkoutChanged WorkoutChanged;

    private Workout _workout;

    private readonly Control[] _emptyWorkoutControls;

    private readonly Control[] _nonemptyWorkoutControls;

    private bool HasWorkout { get { return _workout != null; } }

    //private Control[] CurrentlyVisibleControls { get { return HasWorkout ? _nonemptyWorkoutControls : _emptyWorkoutControls; } }

    private Control[] AllControls { get { return _emptyWorkoutControls.Concat(_nonemptyWorkoutControls).ToArray(); } }

    public Workout Workout
    {
      get { return _workout; }
      set
      {
        _workout = value;

        if (WorkoutChanged != null)
        {
          WorkoutChanged(_workout);
        }
      }
    }

    private void UpdateWorkoutData()
    {
      if (HasWorkout)
      {
        labWorkoutName.Text = Workout.Name;
        txtDuration.Text = Workout.Duration.ToString();
        txtDistance.Text = Math.Round(Workout.Distance, 2) + " km";
        txtDescription.Text = Workout.Description;
      }
      else
      {
        labWorkoutName.Text = "";
        txtDuration.Text = "";
        txtDistance.Text = "";
        txtDescription.Text = "";
      }

      foreach (var c in _emptyWorkoutControls)
      {
        c.Visible = !HasWorkout;
      }
      foreach (var c in _nonemptyWorkoutControls)
      {
        c.Visible = HasWorkout;
      }
    }

    public WorkoutControl()
    {
      InitializeComponent();

      _emptyWorkoutControls = new Control[] { labSelectWorkout, comWorkouts };
      _nonemptyWorkoutControls = new Control[] { labWorkoutName, txtDescription, txtDistance, txtDuration, butRemove };

      WorkoutChanged += workout => UpdateWorkoutData();

      UpdateWorkoutData();

      // TODO: Fill combobox
    }

    private void butRemove_Click(object sender, EventArgs e)
    {
      Workout = null;
    }
  }
}
