using System.Globalization;
using System.Windows.Forms;

namespace TrainingPlanner
{
  public partial class WorkoutControl : UserControl
  {
    public delegate void OnWorkoutChanged(Workout newWorkout);

    public event OnWorkoutChanged WorkoutChanged;

    private Workout _workout;

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
      labWorkoutName.Text = Workout.Name;
      txtDuration.Text = Workout.Duration.ToString();
      txtDistance.Text = Workout.Distance.ToString(CultureInfo.InvariantCulture);
      txtDescription.Text = Workout.Description;
    }

    public WorkoutControl()
    {
      InitializeComponent();

      WorkoutChanged += workout =>
      {
        UpdateWorkoutData();

        Visible = workout != null;
      };

      Visible = false;
    }
  }
}
