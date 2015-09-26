using TrainingPlanner.Model;
using TrainingPlanner.View;

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
