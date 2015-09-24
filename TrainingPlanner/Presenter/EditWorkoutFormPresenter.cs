using System.IO;
using System.Linq;
using System.Windows.Forms;
using TrainingPlanner.Model;
using TrainingPlanner.View;

namespace TrainingPlanner.Presenter
{
  public class EditWorkoutFormPresenter : IEditWorkoutFormPresenter
  {
    private readonly EditWorkoutForm _view;

    private readonly Data _data;

    private bool _dontAskToSave;
    private bool _cancelClosing;

    public EditWorkoutFormPresenter(EditWorkoutForm view, Data data)
    {
      this._view = view;
      this._data = data;
      
      this._data.CategoriesChanged += (s, e) => SetCategories();
      SetCategories();

      this._view.AddStepButtonClick += (s, e) => this._view.AddStep();
      this._view.RemoveStepButtonClick += (s, e) => this._view.RemoveStep();
      this._view.EditWorkoutFormClosing += OnEditWorkoutFormClosing;
      this._view.SaveButtonClick += (s, e) => SaveWorkout();
      this._view.DeleteButtonClick += (s, e) => DeleteWorkout();

      // only add a step if there is none yet (there already are steps when editing
      // but not when creating workouts)
      if (this._view.Steps == null || this._view.Steps.Length == 0)
      {
        this._view.AddStep();
      }
    }

    private void SetCategories()
    {
      this._view.SetCategories(new []{""}.Concat(this._data.Categories.Select(c => c.Name)).ToArray());
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

      var workout = new Workout(this._view.WorkoutName, this._data.WorkoutCategoryFromName(this._view.CategoryName), steps);

      File.WriteAllText(
        Program.WorkoutsDirectory + Path.DirectorySeparatorChar + this._view.WorkoutName.ToLower().Replace(' ', '-') +
        ".json", workout.Json);

      this._dontAskToSave = true;

      this._data.AddOrUpdateWorkout(workout);

      this._view.Close();
    }

    private void DeleteWorkout()
    {
      if (MessageBox.Show("Are you sure you want to delete the workout?", "Delete?", MessageBoxButtons.YesNo) !=
          DialogResult.Yes)
      {
        return;
      }

      this._data.RemoveWorkout(this._data.WorkoutFromName(this._view.WorkoutName));
      File.Delete(Program.WorkoutsDirectory + Path.DirectorySeparatorChar + this._view.WorkoutName.ToLower().Replace(' ', '-') + ".json");

      this._dontAskToSave = true;
      this._view.Close();
    }
  }
}