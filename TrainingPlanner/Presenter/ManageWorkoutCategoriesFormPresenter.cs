using System.Windows.Forms;
using TrainingPlanner.Model;
using TrainingPlanner.Presenter.Interfaces;
using TrainingPlanner.View.Forms;
using TrainingPlanner.View.Interfaces;

namespace TrainingPlanner.Presenter
{
  public class ManageWorkoutCategoriesFormPresenter : IManageWorkoutCategoriesFormPresenter
  {
    private readonly Data _data;

    public ManageWorkoutCategoriesFormPresenter(IManageWorkoutCategoriesForm view, Data data)
    {
      _data = data;

      view.DisplayCategories(data.Categories);

      view.AddCategoryButtonClick += (s, e) => EditCategory(null);
      view.EditCategoryButtonClick += (s, e) => EditCategory(e);
      view.DeleteCategoryButtonClick += (s, e) => DeleteCategory(e);
      view.ExitButtonClick += (s, e) => view.Close();

      data.CategoryChanged += (s, e) => view.DisplayCategories(data.Categories);
    }

    private void EditCategory(string categoryName)
    {
      var form = categoryName == null ? new EditWorkoutCategoryForm() : new EditWorkoutCategoryForm(_data.WorkoutCategoryFromName(categoryName));
      var presenter = new EditWorkoutCategoryFormPresenter(form, _data);
      form.Show();
    }

    private void DeleteCategory(string categoryName)
    {
      if (MessageBox.Show("Are you sure you want to delete the workout category?", "Delete?", MessageBoxButtons.YesNo) !=
          DialogResult.Yes)
      {
        return;
      }

      _data.RemoveWorkoutCategory(_data.WorkoutCategoryFromName(categoryName));
    }
  }
}