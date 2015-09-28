using System;
using System.Linq;
using TrainingPlanner.Model;
using TrainingPlanner.Presenter.Interfaces;
using TrainingPlanner.View.Interfaces;

namespace TrainingPlanner.Presenter
{
  public class SelectWorkoutFormPresenter : ISelectWorkoutFormPresenter
  {
    private readonly Data _data;
    private readonly ISelectWorkoutForm _view;

    public SelectWorkoutFormPresenter(ISelectWorkoutForm view, Data data)
    {
      this._data = data;
      this._view = view;

      this._view.WorkoutSelected += (s, e) => OnWorkoutSelected(e);
      this._view.WorkoutCategoryChanged += (s, e) => OnWorkoutCategoryChanged(e);

      // when the workouts change, reload the workouts list
      this._data.WorkoutChanged += (s, e) => OnWorkoutCategoryChanged(this._view.CurrentCategory);
      // when the workout categories change, reload the category list and potentially the workout list too
      this._data.CategoryChanged += (s, e) => this._view.SetCategories(this._data.Categories.Select(c => c.Name).ToArray());

      this._view.SetCategories(this._data.Categories.Select(c => c.Name).ToArray());
    }

    private void OnWorkoutCategoryChanged(string categoryName)
    {
      this._view.SetWorkouts(this._data.Workouts.Where(w => w.CategoryName == categoryName).Select(w => w.Name).ToArray());
    }

    private void OnWorkoutSelected(string workoutName)
    {
      if (WorkoutSelected != null)
      {
        Logger.Debug("Triggering WorkoutSelected event");
        WorkoutSelected(this, workoutName);
      }
    }

    public event EventHandler<string> WorkoutSelected;
  }
}