using System.IO;
using System.Windows.Forms;
using TrainingPlanner.Model;
using TrainingPlanner.View;

namespace TrainingPlanner.Presenter
{
  public class EditWorkoutFormPresenter : IEditWorkoutFormPresenter
  {
    private readonly IEditWorkoutForm _view;

    private readonly Data _data;

    private bool _dontAskToSave;
    private bool _cancelClosing;

    public EditWorkoutFormPresenter(IEditWorkoutForm view, Data data)
    {
      this._view = view;
      this._data = data;

      this._view.AddStepButtonClick += (s, e) => this._view.AddStep();
      this._view.RemoveStepButtonClick += (s, e) => this._view.RemoveStep();
      this._view.EditWorkoutFormClosing += OnEditWorkoutFormClosing;
      this._view.SaveButtonClick += (s, e) => SaveWorkout();
      this._view.AddStep();
    }

    private void OnEditWorkoutFormClosing(object sender, FormClosingEventArgs e)
    {
      if (_dontAskToSave)
      {
        return;
      }

      if (MessageBox.Show("Do you want to save the workout?", "Save?", MessageBoxButtons.YesNo) == DialogResult.Yes)
      {
        SaveWorkout();
        if (_cancelClosing)
        {
          e.Cancel = true;
        }
      }
    }

    private void SaveWorkout()
    {
      var steps = this._view.Steps;
      if (steps == null)
      {
        MessageBox.Show("One of the steps is invalid, please fix...");
        _cancelClosing = true;
        return;
      }

      if (string.IsNullOrEmpty(this._view.WorkoutName))
      {
        MessageBox.Show("Please provide a name for the workout...");
        _cancelClosing = true;
        return;
      }

      var workout = new Workout(this._view.WorkoutName, steps);

      File.WriteAllText(
        Program.WorkoutsDirectory + Path.DirectorySeparatorChar + this._view.WorkoutName.ToLower().Replace(' ', '-') +
        ".json", workout.Json);

      this._dontAskToSave = true;

      this._data.AddWorkout(workout);

      this._view.Close();
    }
  }
}