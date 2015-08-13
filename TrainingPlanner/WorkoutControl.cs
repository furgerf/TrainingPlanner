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
      UpdateWorkouts();
    }

    public void UpdateWorkouts()
    {
      comWorkouts.Items.Clear();

      comWorkouts.Items.Add("");
      if (Program.Workouts != null)
      {
        foreach (var w in Program.Workouts)
        {
          comWorkouts.Items.Add(w.Name);
        }
      }
      comWorkouts.SelectedIndex = 0;
    }

    private void butRemove_Click(object sender, EventArgs e)
    {
      Workout = null;
      comWorkouts.SelectedIndex = 0;
    }

    private void comWorkouts_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (string.IsNullOrEmpty(comWorkouts.Text))
      {
        return;
      }

      Workout = Program.Workouts.First(w => w.Name.Equals(comWorkouts.Text));
    }
  }
}
