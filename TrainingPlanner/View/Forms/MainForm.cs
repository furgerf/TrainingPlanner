using System;
using System.Drawing;
using System.Windows.Forms;
using TrainingPlanner.Model;
using TrainingPlanner.Model.EventArgs;
using TrainingPlanner.Model.Serializable;
using TrainingPlanner.Presenter;
using TrainingPlanner.View.Controls;
using TrainingPlanner.View.Interfaces;

namespace TrainingPlanner.View.Forms
{
  public partial class MainForm : Form, IMainForm
  {
    private const int WeeklyControlHeight = 218;

    private WeekControl[] _weekControls;
    private Data _data;

    private Data Data
    {
      get { return _data; }
      set
      {
        _data = value;
        Text = "Training Planner - " + (Data == null ? "<none>" : Data.PlanName);
      }
    }

    public MainForm()
    {
      InitializeComponent();
      SetTrainingPlanMenusEnabled(false);

      // register to more events (to retrigger)
      newPlanToolStripMenuItem.Click += (s, e) => OnNewPlanClick();
      openPlanToolStripMenuItem.Click += (s, e) => OnOpenPlanClick();
      closePlanToolStripMenuItem.Click += (s, e) => OnClosePlanClick();
      addToolStripMenuItem.Click += (s, e) => OnAddWorkoutClick();
      editToolStripMenuItem.Click += (s, e) => OnEditWorkoutClick();
      deleteToolStripMenuItem.Click += (s, e) => OnDeleteWorkoutClick();
      manageToolStripMenuItem.Click += (s, e) => OnManageWorkoutsClick();
      addToolStripMenuItem1.Click += (s, e) => OnAddWorkoutCategoryClick();
      editToolStripMenuItem1.Click += (s, e) => OnEditWorkoutCategoryClick();
      deleteToolStripMenuItem1.Click += (s, e) => OnDeleteWorkoutCategoryClick();
      manageToolStripMenuItem1.Click += (s, e) => OnManageWorkoutCategoriesClick();
      configureToolStripMenuItem.Click += (s, e) => OnConfigurePacesClick();
      infoToolStripMenuItem.Click += (s, e) => OnInfoClick();
      exitToolStripMenuItem.Click += (s, e) => Close();

      // TODO: create shown event in IMF and scroll on the event callback in MFP
      Shown += (s, e) =>
      {
        //this.foregroundPanel.ScrollControlIntoView(_weekControls[9]);
        foregroundPanel.AutoScrollPosition = new Point(100, 5*WeeklyControlHeight);
      };
    }

    private void InitializeDynamicControls()
    {
      // prepare UI
      var screenHeight = Screen.PrimaryScreen.WorkingArea.Height;
      var panelHeight = screenHeight - 50;
      foregroundPanel.Height = panelHeight;
      foregroundPanel.AutoScroll = true;
      foregroundPanel.SizeChanged += (s, e) =>
      {
        foregroundPanel.VerticalScroll.SmallChange = foregroundPanel.VerticalScroll.Maximum/25;
        foregroundPanel.VerticalScroll.LargeChange = foregroundPanel.VerticalScroll.Maximum/10;
      };

      // prepare WeekControls
      for (var i = 0; i < _weekControls.Length; i++)
      {
        // create control
        _weekControls[i] = new WeekControl(Data) {Top = i*WeeklyControlHeight, Parent = foregroundPanel};
        // register to event (to retrigger)
        _weekControls[i].WeeklyPlanChanged += (sender, plan) =>
        {
          if (WeeklyPlanChanged != null)
          {
            Logger.Debug("Triggering WeeklyPlanChanged event");
            WeeklyPlanChanged(this, new EventArgs<WeeklyPlan>(plan.Value));
          }
        };
      }

      // some more own UI stuff
      foregroundPanel.Width = _weekControls[0].Width + 16;
    }

    public event EventHandler NewPlanClick = (s, e) => { };
    public event EventHandler OpenPlanClick = (s, e) => { };
    public event EventHandler ClosePlanClick = (s, e) => { };
    public event EventHandler AddWorkoutClick = (s, e) => { };
    public event EventHandler<string> EditWorkoutClick = (s, e) => { };
    public event EventHandler<string> DeleteWorkoutClick = (s, e) => { };
    public event EventHandler ManageWorkoutsClick = (s, e) => { };
    public event EventHandler AddWorkoutCategoryClick = (s, e) => { };
    public event EventHandler<string> EditWorkoutCategoryClick = (s, e) => { };
    public event EventHandler<string> DeleteWorkoutCategoryClick = (s, e) => { };
    public event EventHandler ManageWorkoutCategoriesClick = (s, e) => { };
    public event EventHandler ConfigurePacesClick = (s, e) => { };
    public event EventHandler InfoClick = (s, e) => { };
    public event EventHandler<EventArgs<WeeklyPlan>> WeeklyPlanChanged = (s, e) => { };

    public void UpdateWeeklyPlan(WeeklyPlan[] weeklyPlans)
    {
      if (weeklyPlans.Length != _weekControls.Length)
      {
        throw new ArgumentException("Array size mismatch");
      }

      for (var i = 0; i < weeklyPlans.Length; i++)
      {
        _weekControls[i].WeeklyPlan = weeklyPlans[i];
      }
    }

    public void SetWeekActivity(int week, bool isActive)
    {
      _weekControls[week].IsActiveWeek = isActive;
    }

    public void ScrollToWeek(int week)
    {
      if (foregroundPanel.InvokeRequired)
      {
        foregroundPanel.Invoke((MethodInvoker) (() => ScrollToWeek(week)));
      }
      else
      {
        //this.foregroundPanel.ScrollControlIntoView(this._weekControls[Math.Min(week + 2, this._weekControls.Length)]);
        //this.foregroundPanel.AutoScrollPosition = new Point(0, -10);
      }
    }

    public void SetNewData(Data data)
    {
      // cleanup
      if (_weekControls != null)
      {
        foreach (var wc in _weekControls)
        {
          wc.Dispose();
        }
        _weekControls = null;
      }

      // assign new data
      Data = data;

      // no need for new setup if new data is null
      if (Data == null)
      {
        return;
      }

      // setup new data
      _weekControls = new WeekControl[Data.TrainingPlan.TrainingWeeks];
      InitializeDynamicControls();
      foreach (var wc in _weekControls)
      {
        wc.Activate();
      }
    }

    public void SetTrainingPlanMenusEnabled(bool isEnbabled)
    {
      workoutsToolStripMenuItem.Enabled = isEnbabled;
      workoutCategoriesToolStripMenuItem.Enabled = isEnbabled;
      pacesToolStripMenuItem.Enabled = isEnbabled;
    }

    private void OnNewPlanClick()
    {
      Logger.Debug("Triggering NewPlanClick event");
      NewPlanClick(this, null);
    }

    private void OnOpenPlanClick()
    {
      Logger.Debug("Triggering OpenPlanClick event");
      OpenPlanClick(this, null);
    }

    private void OnClosePlanClick()
    {
      Logger.Debug("Triggering ClosePlanClick event");
      ClosePlanClick(this, null);
    }

    private void OnAddWorkoutClick()
    {
      Logger.Debug("Triggering AddWorkoutClick event");
      AddWorkoutClick(this, null);
    }

    private void OnEditWorkoutClick()
    {
      var form = new SelectWorkoutForm();
      var presenter = new SelectWorkoutFormPresenter(form, Data);

      presenter.WorkoutSelected += (s, e) =>
      {
        form.Close();
        Logger.Debug("Triggering EditWorkoutClick event");
        EditWorkoutClick(this, e);
      };

      form.Show();
    }

    private void OnDeleteWorkoutClick()
    {
      var form = new SelectWorkoutForm();
      var presenter = new SelectWorkoutFormPresenter(form, Data);

      presenter.WorkoutSelected += (s, e) =>
      {
        form.Close();
        Logger.Debug("Triggering DeleteWorkoutClick event");
        DeleteWorkoutClick(this, e);
      };

      form.Show();
    }

    private void OnManageWorkoutsClick()
    {
      Logger.Debug("Triggering ManageWorkoutsClick event");
      ManageWorkoutsClick(this, null);
    }

    private void OnAddWorkoutCategoryClick()
    {
      Logger.Debug("Triggering AddWorkoutCategoryClick event");
      AddWorkoutCategoryClick(this, null);
    }

    private void OnEditWorkoutCategoryClick()
    {
      var form = new SelectWorkoutCategoryForm();
      var presenter = new SelectWorkoutCategoryFormPresenter(form, Data);

      presenter.CategorySelected += (s, e) =>
      {
        form.Close();
        Logger.Debug("Triggering EditWorkoutCategoryClick event");
        EditWorkoutCategoryClick(this, e);
      };

      form.Show();
    }

    private void OnDeleteWorkoutCategoryClick()
    {
      var form = new SelectWorkoutCategoryForm();
      var presenter = new SelectWorkoutCategoryFormPresenter(form, Data);

      presenter.CategorySelected += (s, e) =>
      {
        form.Close();
        Logger.Debug("Triggering DeleteWorkoutCategoryClick event");
        DeleteWorkoutCategoryClick(this, e);
      };

      form.Show();
    }

    private void OnManageWorkoutCategoriesClick()
    {
      Logger.Debug("Triggering ManageWorkoutCategoriesClick event");
      ManageWorkoutCategoriesClick(this, null);
    }

    private void OnConfigurePacesClick()
    {
      Logger.Debug("Triggering ConfigurePacesClick event");
      ConfigurePacesClick(this, null);
    }

    private void OnInfoClick()
    {
      Logger.Debug("Triggering InfoClick event");
      InfoClick(this, null);
    }
  }
}
