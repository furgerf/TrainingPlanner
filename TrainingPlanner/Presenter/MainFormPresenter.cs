using System;
using System.Windows.Forms;
using TrainingPlanner.Model;
using TrainingPlanner.Presenter.Interfaces;
using TrainingPlanner.View.Forms;
using TrainingPlanner.View.Interfaces;

namespace TrainingPlanner.Presenter
{
  public class MainFormPresenter : IMainFormPresenter
  {
    private readonly IMainForm _view;
    private Data _data;

    public MainFormPresenter(IMainForm view, Data data)
    {
      _view = view;
      _data = data;

      view.UpdateWeeklyPlan(_data.TrainingPlan.WeeklyPlans);

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

      view.WeeklyPlanChanged += (s, e) => _data.UpdateTrainingPlan(e.Value);

      // find active week
      for (var i = 0; i < _data.TrainingPlan.WeeklyPlans.Length; i++)
      {

        if (_data.TrainingPlan.WeeklyPlans[i].WeekStart > DateTime.Today || _data.TrainingPlan.WeeklyPlans[i].WeekEnd < DateTime.Today)
        {
          continue;
        }

        view.SetWeekActivity(i, true);
        //Task.Factory.StartNew(() =>
        //{
        //  Thread.Sleep(1);
          //view.ScrollToWeek(i);
        //});
        break;
      }
    }

    private void OnNewPlanClick()
    {
      throw new NotImplementedException();
    }

    private void OnOpenPlanClick()
    {
      var data = new Data("Sempacherseelauf 2016");
      _data = data;
      _view.SetNewData(data);

      Logger.InfoFormat("Opened new training plan '{0}'", data.PlanName);
    }

    private void OnClosePlanClick()
    {
      throw new NotImplementedException();
    }

    private void OnAddWorkoutClick()
    {
      var form = new EditWorkoutForm(_data);
      var presenter = new EditWorkoutFormPresenter(form, _data);
      form.Show();
    }

    private void OnEditWorkoutClick(string workoutName)
    {
      var form = new EditWorkoutForm(_data, _data.WorkoutFromName(workoutName));
      var presenter = new EditWorkoutFormPresenter(form, _data);
      // TODO: (add/edit/update) maybe notify the user that if he modifies the workout but leaves the name,
      // the workout will be overwritten but if it gets a new name, a new workout is created?
      form.Show();
    }

    private void OnDeleteWorkoutClick(string workoutName)
    {
      if (_data.WorkoutFromName(workoutName) == null)
      {
        MessageBox.Show("There is no workout with this name.");
        return;
      }

      if (MessageBox.Show("Are you sure you want to delete the workout?", "Delete?", MessageBoxButtons.YesNo) !=
          DialogResult.Yes)
      {
        return;
      }

      _data.RemoveWorkout(_data.WorkoutFromName(workoutName));
    }

    private void OnManageWorkoutsClick()
    {
      var form = new ManageWorkoutsForm(_data);
      var presenter = new ManageWorkoutsFormPresenter(form, _data);
      form.Show();
    }

    private void OnAddWorkoutCategoryClick()
    {
      var form = new EditWorkoutCategoryForm();
      var presenter = new EditWorkoutCategoryFormPresenter(form, _data);
      form.Show();
    }

    private void OnEditWorkoutCategoryClick(string categoryName)
    {
      var form = new EditWorkoutCategoryForm(_data.WorkoutCategoryFromName(categoryName));
      var presenter = new EditWorkoutCategoryFormPresenter(form, _data);
      // TODO: (add/edit/update) maybe notify the user that if he modifies the workout but leaves the name,
      // the workout will be overwritten but if it gets a new name, a new workout is created?
      form.Show();
    }

    private void OnDeleteWorkoutCategoryClick(string categoryName)
    {
      if (_data.WorkoutCategoryFromName(categoryName) == null)
      {
        MessageBox.Show("There is no workout category with this name.");
        return;
      }

      if (MessageBox.Show("Are you sure you want to delete the workout category?", "Delete?", MessageBoxButtons.YesNo) !=
          DialogResult.Yes)
      {
        return;
      }

      _data.RemoveWorkoutCategory(_data.WorkoutCategoryFromName(categoryName));
    }

    private void OnManageWorkoutCategoriesClick()
    {
      var form = new ManageWorkoutCategoriesForm(_data);
      var presenter = new ManageWorkoutCategoriesFormPresenter(form, _data);
      form.Show();
    }

    private void OnConfigurePacesClick()
    {
      var form = new PaceForm();
      var presenter = new PaceFormPresenter(form, _data);
      form.Show();
    }

    private void OnInfoClick()
    {
      new AboutForm().Show();
    }
  }
}