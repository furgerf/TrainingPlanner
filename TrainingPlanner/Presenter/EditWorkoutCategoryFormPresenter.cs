using TrainingPlanner.Model;
using TrainingPlanner.Model.Serializable;
using TrainingPlanner.Presenter.Interfaces;
using TrainingPlanner.View;
using TrainingPlanner.View.Forms;

namespace TrainingPlanner.Presenter
{
  public class EditWorkoutCategoryFormPresenter : IEditWorkoutCategoryFormPresenter
  {
    public EditWorkoutCategoryFormPresenter(EditWorkoutCategoryForm view, Data data)
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
