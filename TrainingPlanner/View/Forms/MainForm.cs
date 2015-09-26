using System;
using System.Windows.Forms;
using TrainingPlanner.Model;
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
      InitializeComponent();

      this._data = data;

      // prepare UI
      this.WindowState = FormWindowState.Maximized;

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
      this._weekControls = new WeekControl[TrainingPlan.TrainingWeeks];
      for (var i = 0; i < _weekControls.Length; i++)
      {
        // create control
        this._weekControls[i] = new WeekControl(this._data) {Top = i*WeeklyControlHeight, Parent = foregroundPanel};
        // register to event (to retrigger)
        this._weekControls[i].WeeklyPlanChanged += (sender, plan) =>
        {
          if (WeeklyPlanChanged != null)
          {
            WeeklyPlanChanged(this, new EventArgs<WeeklyPlan>(plan.Value));
          }
        };
      }

      // some more own UI stuff
      this.foregroundPanel.Width = _weekControls[0].Width + 16;
      this.butAddWorkout.Left = this.foregroundPanel.Right + 6;
      this.butPaces.Left = this.foregroundPanel.Right + 6;
      this.butEditWorkout.Left = this.foregroundPanel.Right + 6;
      this.butEditCategories.Left = this.foregroundPanel.Right + 6;

      // edit workout button menu
      this._data.WorkoutChanged += (s, e) => CreateContextMenu();
      CreateContextMenu();

      // register to more events (to retrigger)
      this.butAddWorkout.Click += (s, e) => AddWorkoutButtonClick(this, e);
    }

    private void CreateContextMenu()
    {
      // retrieve menu
      this.butEditWorkout.ContextMenu = this._data.WorkoutContextMenu;

      // add event listeners
      foreach (MenuItem category in this.butEditWorkout.ContextMenu.MenuItems)
      {
        foreach (MenuItem workout in category.MenuItems)
        {
          var workout1 = workout;
          workout.Click += (s, e) =>
          {
            if (EditWorkoutButtonClick != null)
            {
              EditWorkoutButtonClick(this, workout1.Text);
            }
          };
        }
      }
    }

    public event EventHandler AddWorkoutButtonClick;
    public event EventHandler ConfigurePacesButtonClick;
    public event EventHandler<string> EditWorkoutButtonClick;
    public event EventHandler EditCategoriesButtonClick;
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

    private void butPaces_Click(object sender, EventArgs e)
    {
      if (ConfigurePacesButtonClick != null)
      {
        ConfigurePacesButtonClick(this, e);
      }
    }

    private void butEditWorkout_Click(object sender, EventArgs e)
    {
      this.butEditWorkout.ContextMenu.Show(this.butEditWorkout, ((MouseEventArgs)e).Location);
    }

    private void butEditCategories_Click(object sender, EventArgs e)
    {
      if (EditCategoriesButtonClick != null)
      {
        EditCategoriesButtonClick(this, e);
      }
    }
  }
}
