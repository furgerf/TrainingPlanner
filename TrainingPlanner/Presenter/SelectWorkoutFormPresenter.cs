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
      _data = data;
      _view = view;

      _view.WorkoutSelected += (s, e) => OnWorkoutSelected(e);
      _view.WorkoutCategoryChanged += (s, e) => OnWorkoutCategoryChanged(e);

      // when the workouts change, reload the workouts list
      _data.WorkoutChanged += (s, e) => OnWorkoutCategoryChanged(_view.CurrentCategory);
      // when the workout categories change, reload the category list and potentially the workout list too
      _data.CategoryChanged += (s, e) => _view.SetCategories(_data.Categories.Select(c => c.Name).ToArray());

      _view.SetCategories(_data.Categories.Select(c => c.Name).ToArray());
    }

    private void OnWorkoutCategoryChanged(string categoryName)
    {
      _view.SetWorkouts(_data.Workouts.Where(w => w.CategoryName == categoryName).Select(w => w.Name).ToArray());
    }

    private void OnWorkoutSelected(string workoutName)
    {
      Logger.Debug("Triggering WorkoutSelected event");
      WorkoutSelected(this, workoutName);
    }

    public event EventHandler<string> WorkoutSelected = (s, e) => { };
  }
}