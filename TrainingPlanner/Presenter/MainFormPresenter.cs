using System;
using TrainingPlanner.Model;
using TrainingPlanner.Presenter.Interfaces;
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

      view.NewPlanClick += (s, e) => OnNewPlanClick();
      view.OpenPlanClick += (s, e) => OnOpenPlanClick();
      view.ClosePlanClick += (s, e) => OnClosePlanClick();
      view.AddWorkoutClick += (s, e) => OnAddWorkoutClick();
      view.EditWorkoutClick += (s, e) => OnEditWorkoutClick(e);
      view.DeleteWorkoutClick += (s, e) => OnDeleteWorkoutClick(e);
      view.ManageWorkoutsClick += (s, e) => OnManageWorkoutsClick();
      view.AddWorkoutCategoryClick += (s, e) => OnAddWorkoutCategoryClick();
      view.EditWorkoutCategoryClick += (s, e) => OnEditWorkoutCategoryClick(e);
      view.DeleteWorkoutCategoryClick += (s, e) => OnDeleteWorkoutCategoryClick(e);
      view.ManageWorkoutCategoriesClick += (s, e) => OnManageWorkoutCategoriesClick();
      view.ConfigurePacesClick += (s, e) => OnConfigurePacesClick();
      view.InfoClick += (s, e) => OnInfoClick();

      view.WeeklyPlanChanged += (s, e) => this._data.UpdateTrainingPlan(e.Value);
    }

    private void OnNewPlanClick()
    {
      throw new NotImplementedException();
    }

    private void OnOpenPlanClick()
    {
      throw new NotImplementedException();
    }

    private void OnClosePlanClick()
    {
      throw new NotImplementedException();
    }

    private void OnAddWorkoutClick()
    {
      var form = new EditWorkoutForm(this._data);
      var presenter = new EditWorkoutFormPresenter(form, this._data);
      form.Show();
    }

    private void OnEditWorkoutClick(string workoutName)
    {
      var form = new EditWorkoutForm(this._data, this._data.WorkoutFromName(workoutName));
      var presenter = new EditWorkoutFormPresenter(form, this._data);
      // TODO: (add/edit/update) maybe notify the user that if he modifies the workout but leaves the name,
      // the workout will be overwritten but if it gets a new name, a new workout is created?
      form.Show();
    }

    private void OnDeleteWorkoutClick(string workoutName)
    {
      throw new NotImplementedException();
    }

    private void OnManageWorkoutsClick()
    {
      throw new NotImplementedException();
    }

    private void OnAddWorkoutCategoryClick()
    {
      throw new NotImplementedException();
    }

    private void OnEditWorkoutCategoryClick(string categoryName)
    {
      throw new NotImplementedException();
    }

    private void OnDeleteWorkoutCategoryClick(string categoryName)
    {
      throw new NotImplementedException();
    }

    private void OnManageWorkoutCategoriesClick()
    {
      var form = new ManageWorkoutCategoriesForm();
      var presenter = new ManageWorkoutCategoriesFormPresenter(form, this._data);
      form.Show();
    }

    private void OnConfigurePacesClick()
    {
      var form = new PaceForm();
      var presenter = new PaceFormPresenter(form, this._data);
      form.Show();
    }

    private void OnInfoClick()
    {
      throw new NotImplementedException();
    }
  }
}