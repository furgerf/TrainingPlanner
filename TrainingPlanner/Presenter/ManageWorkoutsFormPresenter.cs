using System.Windows.Forms;
using TrainingPlanner.Model;
using TrainingPlanner.Presenter.Interfaces;
using TrainingPlanner.View.Forms;
using TrainingPlanner.View.Interfaces;

namespace TrainingPlanner.Presenter
{
  public class ManageWorkoutsFormPresenter : IManageWorkoutsFormPresenter
  {
    private readonly Data _data;

    public ManageWorkoutsFormPresenter(IManageWorkoutsForm view, Data data)
    {
      _data = data;

      view.DisplayWorkouts(_data.Workouts);

      view.AddWorkoutButtonClick += (s, e) => EditWorkout(null);
      view.EditWorkoutButtonClick += (s, e) => EditWorkout(e);
      view.DeleteWorkoutButtonClick += (s, e) => DeleteWorkout(e);
      view.ExitButtonClick += (s, e) => view.Close();

      data.WorkoutChanged += (s, e) => view.DisplayWorkouts(_data.Workouts);
    }

    private void EditWorkout(string workoutName)
    {
      // TODO: (add/edit/update) maybe notify the user that if he modifies the workout but leaves the name,
      // the workout will be overwritten but if it gets a new name, a new workout is created?
      var form = workoutName == null ? new EditWorkoutForm(_data) : new EditWorkoutForm(_data, _data.WorkoutFromName(workoutName));
      var presenter = new EditWorkoutFormPresenter(form, _data);
      form.Show();
    }

    private void DeleteWorkout(string workoutName)
    {
      if (MessageBox.Show("Are you sure you want to delete the workout?", "Delete?", MessageBoxButtons.YesNo) !=
          DialogResult.Yes)
      {
        return;
      }

      _data.RemoveWorkout(_data.WorkoutFromName(workoutName));
    }
  }
}