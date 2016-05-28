using System.IO;
using System.Linq;
using System.Windows.Forms;
using TrainingPlanner.Model;
using TrainingPlanner.Model.Serializable;
using TrainingPlanner.Presenter.Interfaces;
using TrainingPlanner.View.Interfaces;

namespace TrainingPlanner.Presenter
{
  public class EditWorkoutFormPresenter : IEditWorkoutFormPresenter
  {
    private readonly IEditWorkoutForm _view;

    private readonly Data _data;

    private bool _dontAskToSave;

    public EditWorkoutFormPresenter(IEditWorkoutForm view, Data data)
    {
      _view = view;
      _data = data;
      
      _data.CategoryChanged += (s, e) => SetCategories();
      SetCategories();

      _view.AddStepButtonClick += (s, e) => _view.AddStep();
      _view.RemoveStepButtonClick += (s, e) => _view.RemoveStep();
      _view.EditWorkoutFormClosing += OnEditWorkoutFormClosing;
      _view.SaveButtonClick += (s, e) => OnSaveButtonClick();
      _view.DeleteButtonClick += (s, e) => OnDeleteButtonClick();

      // only add a step if there is none yet (there already are steps when editing
      // but not when creating workouts)
      if (_view.Steps == null || _view.Steps.Length == 0)
      {
        _view.AddStep();
      }
    }

    private void OnSaveButtonClick()
    {
      if (string.IsNullOrEmpty(_view.WorkoutName))
      {
        MessageBox.Show("Please enter a workout name.");
        return;
      }
      if (_view.WorkoutName.IndexOfAny(Path.GetInvalidFileNameChars()) >= 0)
      {
        MessageBox.Show("The workout name is invalid, please enter a valid file name.");
        return;
      }
      if (_view.Steps == null)
      {
        MessageBox.Show("One or more of the steps are invalid, please fix.");
        return;
      }
      if (_view.Steps.Length == 0)
      {
        MessageBox.Show("Please add steps to the workout.");
        return;
      }
      if (_data.WorkoutFromName(_view.WorkoutName) == null)
      {
        if (MessageBox.Show("Do you want to save a new workout?", "Create new workout?", MessageBoxButtons.YesNo) ==
            DialogResult.No)
        {
          return;
        }
      }
      else
      {
        if (
          MessageBox.Show("Do you want to overwrite an existing workout?", "Overwrite existing workout?",
            MessageBoxButtons.YesNo) ==
          DialogResult.No)
        {
          return;
        }
      }

      SaveWorkout();
    }

    private void OnDeleteButtonClick()
    {
      if (_data.WorkoutFromName(_view.WorkoutName) == null)
      {
        MessageBox.Show("There is no workout with this name.");
        return;
      }
      DeleteWorkout();
    }

    private void SetCategories()
    {
      _view.SetCategories(new []{""}.Concat(_data.Categories.Select(c => c.Name)).ToArray());
    }

    private void OnEditWorkoutFormClosing(object sender, FormClosingEventArgs e)
    {
      if (_dontAskToSave)
      {
        return;
      }

      var steps = _view.Steps;

      // check steps
      if (steps == null)
      {
        // invalid steps are entered - ask to discard
        if (
          MessageBox.Show("One or more of the steps currently is invalid. Do you want to discard the workout?",
            "Discard?", MessageBoxButtons.YesNo) == DialogResult.No)
        {
          e.Cancel = true;
        }
        return;
      }
      if (steps.Length == 0)
      {
        // no steps are entered - close form
        return;
      }

      // check workout name
      if (string.IsNullOrEmpty(_view.WorkoutName) || _view.WorkoutName.IndexOfAny(Path.GetInvalidFileNameChars()) >= 0)
      {
        // no (valid) workout name - ask to discard
        if (
          MessageBox.Show("The workout has no name or an invalid name. Do you want to discard the workout?",
            "Discard?", MessageBoxButtons.YesNo) == DialogResult.No)
        {
          e.Cancel = true;
        }
        return;
      }

      // check category name
      if (string.IsNullOrEmpty(_view.CategoryName))
      {
        // no category name - ask to continue editing
        if (
          MessageBox.Show("The workout has no category. Do you want to keep editing?",
            "Keep editing?", MessageBoxButtons.YesNo) == DialogResult.Yes)
        {
          e.Cancel = true;
          return;
        }
      }

      // workout data is ok, ask to save
      var message = _data.WorkoutFromName(_view.WorkoutName) == null
        ? "Do you want to create a new workout?"
        : "Do you want to overwrite an existing workout?";
      if (MessageBox.Show(message, "Save?", MessageBoxButtons.YesNo) == DialogResult.Yes)
      {
        SaveWorkout();
      }
    }

    private void SaveWorkout()
    {
      _dontAskToSave = true;

      _data.AddOrUpdateWorkout(new Workout(_view.WorkoutName, _view.WorkoutShortName,
        string.IsNullOrEmpty(_view.CategoryName)
          ? null
          : _data.WorkoutCategoryFromName(_view.CategoryName), _view.Steps));

      _view.Close();
    }

    private void DeleteWorkout()
    {
      if (MessageBox.Show("Are you sure you want to delete the workout?", "Delete?", MessageBoxButtons.YesNo) !=
          DialogResult.Yes)
      {
        return;
      }

      _data.RemoveWorkout(_data.WorkoutFromName(_view.WorkoutName));

      _dontAskToSave = true;
      _view.Close();
    }
  }
}