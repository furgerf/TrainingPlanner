using System.IO;
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

        File.WriteAllText(
          Program.WorkoutCategoriesDirectory + Path.DirectorySeparatorChar +
          view.CategoryName.ToLower().Replace(' ', '-') + ".json", category.Json);

        data.AddOrUpdateWorkoutCategory(category);

        view.Close();
      };
    }
  }
}
