using TrainingPlanner.Model;
using TrainingPlanner.Model.Serializable;
using TrainingPlanner.Presenter.Interfaces;
using TrainingPlanner.View.Interfaces;

namespace TrainingPlanner.Presenter
{
  public class EditWorkoutCategoryFormPresenter : IEditWorkoutCategoryFormPresenter
  {
    private readonly Data _data;

    private readonly IEditWorkoutCategoryForm _view;

    public EditWorkoutCategoryFormPresenter(IEditWorkoutCategoryForm view, Data data)
    {
      _data = data;
      _view = view;

      _view.SaveButtonClick += (s, e) => SaveWorkoutCategory();
    }

    public void SaveWorkoutCategory()
    {
      var category = new WorkoutCategory(_view.CategoryName, _view.CategoryColor);
      _data.AddOrUpdateWorkoutCategory(category);

      _view.Close();
    }
  }
}
