using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using TrainingPlanner.Model;
using TrainingPlanner.Model.Serializable;
using TrainingPlanner.Presenter.Interfaces;
using TrainingPlanner.View.Forms;
using TrainingPlanner.View.Interfaces;

namespace TrainingPlanner.Presenter
{
  public class MainFormPresenter : IMainFormPresenter
  {
    private readonly IMainForm _view;
    private Data _data;

    private Data Data
    {
      get { return _data; }
      set
      {
        _data = value;
        _view.SetTrainingPlanMenusEnabled(Data != null);
      }
    }

    public MainFormPresenter(IMainForm view)
    {
      _view = view;

      view.NewPlanClick += (s, e) => OnNewPlanClick();
      view.OpenPlanClick += (s, e) => OnOpenPlanClick();
      view.OpenSpecificPlanClick += (s, e) => LoadTrainingPlan(e);
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

      view.WeeklyPlanChanged += (s, e) => Data.UpdateTrainingPlan(e.Value);
    }

    private void OnNewPlanClick()
    {
      var form = new NewTrainingPlanForm();
      var presenter = new NewTrainingPlanFormPresenter(form);
      presenter.NewTrainingPlanDataEntered += (s, e) =>
      {
        // we've received all information required to create the new plan
        // start by creating the directory
        Directory.CreateDirectory(DataPersistence.ApplicationDataDirectory + Path.DirectorySeparatorChar + e.Name);

        // copy workouts, categories, and paces
        DataPersistence.CopyExistingTrainingPlanDataToNewPlan(e.OtherTrainingPlanToImportDataFrom, e.Name);

        // create and store new empty plan
        DataPersistence.CreateNewTrainingPlanFile(TrainingPlan.NewTrainingPlan(e.Name, e.TrainingWeeks));

        // load the now newly-created plan
        LoadTrainingPlan(e.Name);
      };
      form.Show();
    }

    private void OnOpenPlanClick()
    {
      var dlg = new OpenFileDialog
      {
        AddExtension = true,
        DefaultExt = "json",
        Filter = "JSON plan files|plan.json",
        InitialDirectory = DataPersistence.ApplicationDataDirectory,
        Title = "Select Training Plan file to open"
      };

      // abort if no plan was selected
      if (dlg.ShowDialog() != DialogResult.OK)
      {
        return;
      }

      // get plan name and create stuff
      var planName = new DirectoryInfo(dlg.FileName).Parent.Name;

      LoadTrainingPlan(planName);
    }

    private void OnClosePlanClick()
    {
      Data = null;
      _view.SetNewData(null);

      Logger.Info("Closed current training plan");
    }

    private void OnAddWorkoutClick()
    {
      var form = new EditWorkoutForm(Data);
      var presenter = new EditWorkoutFormPresenter(form, Data);
      form.Show();
    }

    private void OnEditWorkoutClick(string workoutName)
    {
      var form = new EditWorkoutForm(Data, Data.WorkoutFromName(workoutName));
      var presenter = new EditWorkoutFormPresenter(form, Data);
      // TODO: (add/edit/update) maybe notify the user that if he modifies the workout but leaves the name,
      // the workout will be overwritten but if it gets a new name, a new workout is created?
      form.Show();
    }

    private void OnDeleteWorkoutClick(string workoutName)
    {
      if (Data.WorkoutFromName(workoutName) == null)
      {
        MessageBox.Show("There is no workout with this name.");
        return;
      }

      if (MessageBox.Show("Are you sure you want to delete the workout?", "Delete?", MessageBoxButtons.YesNo) !=
          DialogResult.Yes)
      {
        return;
      }

      Data.RemoveWorkout(Data.WorkoutFromName(workoutName));
    }

    private void OnManageWorkoutsClick()
    {
      var form = new ManageWorkoutsForm(Data);
      var presenter = new ManageWorkoutsFormPresenter(form, Data);
      form.Show();
    }

    private void OnAddWorkoutCategoryClick()
    {
      var form = new EditWorkoutCategoryForm();
      var presenter = new EditWorkoutCategoryFormPresenter(form, Data);
      form.Show();
    }

    private void OnEditWorkoutCategoryClick(string categoryName)
    {
      var form = new EditWorkoutCategoryForm(Data.WorkoutCategoryFromName(categoryName));
      var presenter = new EditWorkoutCategoryFormPresenter(form, Data);
      // TODO: (add/edit/update) maybe notify the user that if he modifies the workout but leaves the name,
      // the workout will be overwritten but if it gets a new name, a new workout is created?
      form.Show();
    }

    private void OnDeleteWorkoutCategoryClick(string categoryName)
    {
      if (Data.WorkoutCategoryFromName(categoryName) == null)
      {
        MessageBox.Show("There is no workout category with this name.");
        return;
      }

      if (MessageBox.Show("Are you sure you want to delete the workout category?", "Delete?", MessageBoxButtons.YesNo) !=
          DialogResult.Yes)
      {
        return;
      }

      Data.RemoveWorkoutCategory(Data.WorkoutCategoryFromName(categoryName));
    }

    private void OnManageWorkoutCategoriesClick()
    {
      var form = new ManageWorkoutCategoriesForm(Data);
      var presenter = new ManageWorkoutCategoriesFormPresenter(form, Data);
      form.Show();
    }

    private void OnConfigurePacesClick()
    {
      var form = new PaceForm();
      var presenter = new PaceFormPresenter(form, Data);
      form.Show();
    }

    private void OnInfoClick()
    {
      new AboutForm().Show();
    }

    private void LoadTrainingPlan(string planName)
    {
      // load plan
      var data = new Data(planName);
      Data = data;
      _view.SetNewData(data);
      _view.UpdateWeeklyPlan(Data.TrainingPlan.WeeklyPlans);

      // update recent training plans
      var recentPlans = Misc.Default.LastTrainingPlans.Split(';').ToList();
      if (recentPlans.Contains(planName))
      {
        // the newly-loaded plan is already among the list of recent plans - remove it
        recentPlans.Remove(planName);
      }
      else
      {
        while (recentPlans.Count >= 5)
        {
          // the plan isn't already in the list but we may need to remove one
          // to avoid having too many recent plans
          recentPlans.RemoveAt(4);
        }
      }

      // insert newly-loaded plan and save
      recentPlans.Insert(0, planName);
      Misc.Default.LastTrainingPlans = recentPlans.Aggregate((a, b) => a + ";" + b).TrimEnd(';');
      Misc.Default.Save();

      // scroll to current week - this probably doesn't work
      for (var i = 0; i < Data.TrainingPlan.WeeklyPlans.Length; i++)
      {

        if (Data.TrainingPlan.WeeklyPlans[i].WeekStart > DateTime.Today || Data.TrainingPlan.WeeklyPlans[i].WeekEnd < DateTime.Today)
        {
          continue;
        }

        _view.SetWeekActivity(i, true);
        //Task.Factory.StartNew(() =>
        //{
        //  Thread.Sleep(1);
          //view.ScrollToWeek(i);
        //});
        break;
      }

      Logger.InfoFormat("Opened new training plan '{0}'", data.TrainingPlan.Name);
    }
  }
}