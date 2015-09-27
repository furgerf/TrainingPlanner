using TrainingPlanner.Model;
using TrainingPlanner.Presenter.Interfaces;
using TrainingPlanner.View;
using TrainingPlanner.View.Forms;
using TrainingPlanner.View.Interfaces;

namespace TrainingPlanner.Presenter
{
  public class MainFormPresenter : IMainFormPresenter
  {
    private readonly Data _data;

    public MainFormPresenter(IMainForm view, Data data)
    {
      this._data = data;

      view.UpdateWeeklyPlan(this._data.TrainingPlan.WeeklyPlans);

      view.AddWorkoutButtonClick += (s, e) =>
      {
        var form = new EditWorkoutForm(this._data);
        var presenter = new EditWorkoutFormPresenter(form, this._data);
        form.Show();
      };
      view.ConfigurePacesButtonClick += (s, e) =>
      {
        var form = new PaceForm();
        var presenter = new PaceFormPresenter(form, this._data);
        form.Show();
      };
      view.EditWorkoutButtonClick += (s, e) =>
      {
        var form = new EditWorkoutForm(this._data, this._data.WorkoutFromName(e));
        var presenter = new EditWorkoutFormPresenter(form, this._data);
        // TODO: (add/edit/update) maybe notify the user that if he modifies the workout but leaves the name,
        // the workout will be overwritten but if it gets a new name, a new workout is created?
        form.Show();
      };
      view.EditCategoriesButtonClick += (s, e) =>
      {
        var form = new ManageWorkoutCategoriesForm();
        var presenter = new ManageWorkoutCategoriesFormPresenter(form, this._data);
        form.Show();
      };
      view.WeeklyPlanChanged += (s, e) => this._data.UpdateTrainingPlan(e.Value);
    }
  }
}