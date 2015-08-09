using System.Windows.Forms;

namespace TrainingPlanner
{
  public partial class WorkoutControl : UserControl
  {
    private Workout _workout;

    public Workout Workout
    {
      get { return _workout; }
      set
      {
        _workout = value;
        UpdateWorkoutData();
      }
    }

    private void UpdateWorkoutData()
    {
      labWorkoutName.Text = Workout.Name;
      txtDuration.Text = Workout.Duration.ToString();
      txtDistance.Text = Workout.Distance.ToString();
      txtDescription.Text = Workout.Description;
    }


    public WorkoutControl()
    {
      InitializeComponent();
    }
  }
}
