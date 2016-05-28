using System;
using System.Linq;
using TrainingPlanner.Model;
using TrainingPlanner.Presenter.Interfaces;
using TrainingPlanner.View.Interfaces;

namespace TrainingPlanner.Presenter
{
  public class SelectWorkoutCategoryFormPresenter : ISelectWorkoutCategoryFormPresenter
  {
    private readonly Data _data;
    private readonly ISelectWorkoutCategoryForm _view;

    public SelectWorkoutCategoryFormPresenter(ISelectWorkoutCategoryForm view, Data data)
    {
      _data = data;
      _view = view;

      _view.WorkoutCategorySelected += (s, e) => OnWorkoutCategorySelected(e);

      // when the workout categories change, reload the category list
      _data.CategoryChanged += (s, e) => _view.SetCategories(_data.Categories.Select(c => c.Name).ToArray());

      _view.SetCategories(_data.Categories.Select(c => c.Name).ToArray());
    }

    private void OnWorkoutCategorySelected(string workoutName)
    {
      if (CategorySelected != null)
      {
        Logger.Debug("Triggering CategorySelected event");
        CategorySelected(this, workoutName);
      }
    }

    public event EventHandler<string> CategorySelected;
  }
}