using System;
using System.Linq;
using System.Windows.Forms;
using TrainingPlanner.Model;

namespace TrainingPlanner.View
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
      this._weekControls = new WeekControl[Data.TrainingWeeks];
      for (var i = 0; i < _weekControls.Length; i++)
      {
        // create control
        this._weekControls[i] = new WeekControl(this._data) {Top = i*WeeklyControlHeight, Parent = foregroundPanel};
        // register to event (to retrigger)
        this._weekControls[i].WeeklyPlanChanged += (sender, workout) =>
        {
          if (WeeklyPlansChanged != null)
          {
            WeeklyPlansChanged(this, new EventArgs<WeeklyPlan[]>(_weekControls.Select(wc => wc.WeeklyPlan).ToArray()));
          }
        };
      }

      // some more own UI stuff
      this.foregroundPanel.Width = _weekControls[0].Width + 16;
      this.butAddWorkout.Left = this.foregroundPanel.Right + 6;
      this.butPaces.Left = this.foregroundPanel.Right + 6;

      // register to more events (to retrigger)
      this.FormClosing += (s, e) => MainFormClosing(this, e);
      this.butAddWorkout.Click += (s, e) => AddWorkoutButtonClick(this, e);
    }

    public event EventHandler AddWorkoutButtonClick;
    public event EventHandler ConfigurePacesButtonClick;

    public event EventHandler MainFormClosing;

    public event EventHandler<EventArgs<WeeklyPlan[]>> WeeklyPlansChanged;

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

    public EditWorkoutForm GetEditWorkoutForm()
    {
      return new EditWorkoutForm(this._data);
    }

    public PaceForm GetPaceForm()
    {
      return new PaceForm();
    }

    private void butPaces_Click(object sender, EventArgs e)
    {
      if (ConfigurePacesButtonClick != null)
      {
        ConfigurePacesButtonClick(this, e);
      }
    }
  }
}
