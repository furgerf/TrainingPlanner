using System;
using System.Linq;
using System.Windows.Forms;
using TrainingPlanner.Model;

namespace TrainingPlanner.View
{
  public partial class MainForm : Form, IMainForm
  {
    private const int WeeklyControlHeight = 338;

    private readonly WeekControl[] _weekControls;

    private readonly Data _data;

    public MainForm(Data data)
    {
      InitializeComponent();

      this._data = data;

      _weekControls = new WeekControl[Data.TrainingWeeks];

      WindowState = FormWindowState.Maximized;

      backgroundPanel.Height = 1000;
      vScrollBar1.Height = 1000;
      foregroundPanel.Height = Data.TrainingWeeks*WeeklyControlHeight;

      vScrollBar1.Maximum = foregroundPanel.Height;
      vScrollBar1.ValueChanged += (s, e) => foregroundPanel.Top = -vScrollBar1.Value;
      vScrollBar1.SmallChange = vScrollBar1.Maximum/100;
      vScrollBar1.LargeChange = vScrollBar1.Maximum/10;

      for (var i = 0; i < _weekControls.Length; i++)
      {
        _weekControls[i] = new WeekControl {Top = i*WeeklyControlHeight, Parent = foregroundPanel};
        _weekControls[i].SetData(this._data);
        _weekControls[i].WeeklyPlanChanged += (sender, workout) =>
        {
          if (WeeklyPlansChanged != null)
          {
            WeeklyPlansChanged(this, new EventArgs<WeeklyPlan[]>(_weekControls.Select(wc => wc.WeeklyPlan).ToArray()));
          }
        };
      }

      backgroundPanel.Width = _weekControls[0].Width;
      foregroundPanel.Width = _weekControls[0].Width;

      FormClosing += (s, e) => MainFormClosing(this, null);
      this.butAddWorkout.Click += (s, e) => AddWorkoutButtonClick(this, null);
    }

    public event EventHandler AddWorkoutButtonClick;

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

    public void ShowEditWorkoutForm(EditWorkoutForm form)
    {
      foreach (var wc in _weekControls)
      {
        var wc1 = wc;
        form.Closed += (s, ee) => wc1.ReloadWorkouts();
      }
      form.Show();
    }

    public EditWorkoutForm GetEditWorkoutForm()
    {
      return new EditWorkoutForm(this._data);
    }
  }
}
