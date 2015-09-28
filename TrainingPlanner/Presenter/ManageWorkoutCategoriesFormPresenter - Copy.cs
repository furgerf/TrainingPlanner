using System.Windows.Forms;
using TrainingPlanner.Model;
using TrainingPlanner.Presenter.Interfaces;
using TrainingPlanner.View.Forms;
using TrainingPlanner.View.Interfaces;

namespace TrainingPlanner.Presenter
{
  public class ManageWorkoutsFormPresenter : IManageWorkoutsFormPresenter
  {
    private readonly Data _data;

    public ManageWorkoutsFormPresenter(IManageWorkoutsForm view, Data data)
    {
      this._data = data;

      view.DisplayWorkouts(data.Workouts);

      view.AddWorkoutButtonClick += (s, e) => EditCategory(null);
      view.EditWorkoutButtonClick += (s, e) => EditCategory(e);
      view.DeleteWorkoutButtonClick += (s, e) => DeleteCategory(e);
      view.ExitButtonClick += (s, e) => view.Close();

      data.CategoryChanged += (s, e) => view.DisplayWorkouts(data.Workouts);
    }

    private void EditCategory(string categoryName)
    {
      // TODO: (add/edit/update) maybe notify the user that if he modifies the workout but leaves the name,
      // the workout will be overwritten but if it gets a new name, a new workout is created?
      var form = categoryName == null ? new EditWorkoutCategoryForm() : new EditWorkoutCategoryForm(this._data.WorkoutCategoryFromName(categoryName));
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