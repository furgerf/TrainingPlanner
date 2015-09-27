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
      this._data = data;

      view.DisplayCategories(data.Categories);

      view.AddCategoryButtonClick += (s, e) => AddCategory();
      view.EditCategoryButtonClick += (s, e) => EditCategory(e);
      view.DeleteCategoryButtonClick += (s, e) => DeleteCategory(e);
      view.ExitButtonClick += (s, e) => view.Close();

      data.CategoryChanged += (s, e) => view.DisplayCategories(data.Categories);
    }

    // TODO: remove
    private void AddCategory()
    {
      var form = new EditWorkoutCategoryForm();
      var presenter = new EditWorkoutCategoryFormPresenter(form, this._data);
      form.Show();
    }

    private void EditCategory(string categoryName)
    {
      // TODO: (add/edit/update) maybe notify the user that if he modifies the workout but leaves the name,
      // the workout will be overwritten but if it gets a new name, a new workout is created?
      var form = new EditWorkoutCategoryForm(this._data.WorkoutCategoryFromName(categoryName));
      var presenter = new EditWorkoutCategoryFormPresenter(form, this._data);
      form.Show();
    }

    private void DeleteCategory(string categoryName)
    {
      if (MessageBox.Show("Are you sure you want to delete the workout?", "Delete?", MessageBoxButtons.YesNo) !=
          DialogResult.Yes)
      {
        return;
      }

      this._data.RemoveWorkoutCategory(this._data.WorkoutCategoryFromName(categoryName));
    }
  }
}