using TrainingPlanner.Model;
using TrainingPlanner.Model.Serializable;
using TrainingPlanner.Presenter.Interfaces;
using TrainingPlanner.View.Interfaces;

namespace TrainingPlanner.Presenter
{
  public class EditWorkoutCategoryFormPresenter : IEditWorkoutCategoryFormPresenter
  {
    public EditWorkoutCategoryFormPresenter(IEditWorkoutCategoryForm view, Data data)
    {
      view.SaveButtonClick += (s, e) =>
      {
        var category = new WorkoutCategory(view.CategoryName, view.CategoryColor);
        data.AddOrUpdateWorkoutCategory(category);

        view.Close();
      };
    }
  }
}
