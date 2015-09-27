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
      this._data = data;
      this._view = view;

      this._view.SaveButtonClick += (s, e) => SaveWorkoutCategory();
    }

    public void SaveWorkoutCategory()
    {
      var category = new WorkoutCategory(this._view.CategoryName, this._view.CategoryColor);
      this._data.AddOrUpdateWorkoutCategory(category);

      this._view.Close();
    }
  }
}
