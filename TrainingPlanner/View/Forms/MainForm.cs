using System;
using System.Windows.Forms;
using TrainingPlanner.Model;
using TrainingPlanner.Model.EventArgs;
using TrainingPlanner.Model.Serializable;
using TrainingPlanner.View.Controls;
using TrainingPlanner.View.Interfaces;

namespace TrainingPlanner.View.Forms
{
  public partial class MainForm : Form, IMainForm
  {
    private const int WeeklyControlHeight = 218;

    private readonly WeekControl[] _weekControls;

    private readonly Data _data;

    public MainForm(Data data)
    {
      this._data = data;

      InitializeComponent();

      this._weekControls = new WeekControl[TrainingPlan.TrainingWeeks];
      InitializeDynamicControls();

      // register to more events (to retrigger)
      this.newPlanToolStripMenuItem.Click += (s, e) => OnNewPlanClick();
      this.openPlanToolStripMenuItem.Click += (s, e) => OnOpenPlanClick();
      this.closePlanToolStripMenuItem.Click += (s, e) => OnClosePlanClick();
      this.addToolStripMenuItem.Click += (s, e) => OnAddWorkoutClick();
      this.editToolStripMenuItem.Click += (s, e) => OnEditWorkoutClick();
      this.deleteToolStripMenuItem.Click += (s, e) => OnDeleteWorkoutClick();
      this.manageToolStripMenuItem.Click += (s, e) => OnManageWorkoutsClick();
      this.addToolStripMenuItem1.Click += (s, e) => OnAddWorkoutCategoryClick();
      this.editToolStripMenuItem1.Click += (s, e) => OnEditWorkoutCategoryClick();
      this.deleteToolStripMenuItem1.Click += (s, e) => OnDeleteWorkoutCategoryClick();
      this.manageToolStripMenuItem1.Click += (s, e) => OnManageWorkoutCategoriesClick();
      this.configureToolStripMenuItem.Click += (s, e) => OnConfigurePacesClick();
      this.infoToolStripMenuItem.Click += (s, e) => OnInfoClick();

      foreach (var wc in this._weekControls)
      {
        wc.Activate();
      }
    }

    private void InitializeDynamicControls()
    {
      // prepare UI
      var screenHeight = Screen.PrimaryScreen.WorkingArea.Height;
      var panelHeight = screenHeight - 50;
      this.foregroundPanel.Height = panelHeight;
      this.foregroundPanel.AutoScroll = true;
      this.foregroundPanel.SizeChanged += (s, e) =>
      {
        this.foregroundPanel.VerticalScroll.SmallChange = this.foregroundPanel.VerticalScroll.Maximum/25;
        this.foregroundPanel.VerticalScroll.LargeChange = this.foregroundPanel.VerticalScroll.Maximum/10;
      };

      // prepare WeekControls
      for (var i = 0; i < _weekControls.Length; i++)
      {
        // create control
        this._weekControls[i] = new WeekControl(this._data) {Top = i*WeeklyControlHeight, Parent = foregroundPanel};
        // register to event (to retrigger)
        this._weekControls[i].WeeklyPlanChanged += (sender, plan) =>
        {
          if (WeeklyPlanChanged != null)
          {
            Logger.Debug("Triggering WeeklyPlanChanged event");
            WeeklyPlanChanged(this, new EventArgs<WeeklyPlan>(plan.Value));
          }
        };
      }

      // some more own UI stuff
      this.foregroundPanel.Width = _weekControls[0].Width + 16;
    }

    public event EventHandler NewPlanClick;
    public event EventHandler OpenPlanClick;
    public event EventHandler ClosePlanClick;
    public event EventHandler AddWorkoutClick;
    public event EventHandler<string> EditWorkoutClick;
    public event EventHandler<string> DeleteWorkoutClick;
    public event EventHandler ManageWorkoutsClick;
    public event EventHandler AddWorkoutCategoryClick;
    public event EventHandler<string> EditWorkoutCategoryClick;
    public event EventHandler<string> DeleteWorkoutCategoryClick;
    public event EventHandler ManageWorkoutCategoriesClick;
    public event EventHandler ConfigurePacesClick;
    public event EventHandler InfoClick;
    public event EventHandler<EventArgs<WeeklyPlan>> WeeklyPlanChanged;

    public void UpdateWeeklyPlan(WeeklyPlan[] weeklyPlans)
    {
      if (weeklyPlans.Length != this._weekControls.Length)
      {
        throw new ArgumentException("Array size mismatch");
      }

      for (var i = 0; i < weeklyPlans.Length; i++)
      {
        _weekControls[i].WeeklyPlan = weeklyPlans[i];
      }
    }

    private void OnNewPlanClick()
    {
      if (NewPlanClick != null)
      {
        Logger.Debug("Triggering NewPlanClick event");
        NewPlanClick(this, null);
      }
    }

    private void OnOpenPlanClick()
    {
      if (OpenPlanClick != null)
      {
        Logger.Debug("Triggering OpenPlanClick event");
        OpenPlanClick(this, null);
      }
    }

    private void OnClosePlanClick()
    {
      if (ClosePlanClick != null)
      {
        Logger.Debug("Triggering ClosePlanClick event");
        ClosePlanClick(this, null);
      }
    }

    private void OnAddWorkoutClick()
    {
      if (AddWorkoutClick != null)
      {
        Logger.Debug("Triggering AddWorkoutClick event");
        AddWorkoutClick(this, null);
      }
    }

    private void OnEditWorkoutClick()
    {
      throw new NotImplementedException();
    }

    private void OnDeleteWorkoutClick()
    {
      throw new NotImplementedException();
    }

    private void OnManageWorkoutsClick()
    {
      if (ManageWorkoutsClick != null)
      {
        Logger.Debug("Triggering ManageWorkoutsClick event");
        ManageWorkoutsClick(this, null);
      }
    }

    private void OnAddWorkoutCategoryClick()
    {
      if (AddWorkoutCategoryClick != null)
      {
        Logger.Debug("Triggering AddWorkoutCategoryClick event");
        AddWorkoutCategoryClick(this, null);
      }
    }

    private void OnEditWorkoutCategoryClick()
    {
      throw new NotImplementedException();
    }

    private void OnDeleteWorkoutCategoryClick()
    {
      throw new NotImplementedException();
    }

    private void OnManageWorkoutCategoriesClick()
    {
      if (ManageWorkoutCategoriesClick != null)
      {
        Logger.Debug("Triggering ManageWorkoutCategoriesClick event");
        ManageWorkoutCategoriesClick(this, null);
      }
    }

    private void OnConfigurePacesClick()
    {
      if (ConfigurePacesClick != null)
      {
        Logger.Debug("Triggering ConfigurePacesClick event");
        ConfigurePacesClick(this, null);
      }
    }

    private void OnInfoClick()
    {
      if (InfoClick != null)
      {
        Logger.Debug("Triggering InfoClick event");
        InfoClick(this, null);
      }
    }
  }
}
