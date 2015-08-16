using System;
using System.IO;
using System.Windows.Forms;

namespace TrainingPlanner
{
  public partial class MainForm : Form
  {
    private const int TrainingWeeks = 11;

    private const int WeeklyControlHeight = 338;

    private readonly WeekControl[] _weekControls = new WeekControl[11];

    private const string TrainingPlanFile = "plan.json";

    public MainForm()
    {
      InitializeComponent();

      WindowState = FormWindowState.Maximized;

      backgroundPanel.Height = 1000;
      vScrollBar1.Height = 1000;
      foregroundPanel.Height = TrainingWeeks*WeeklyControlHeight;

      vScrollBar1.Maximum = foregroundPanel.Height;
      vScrollBar1.ValueChanged += (s, e) => foregroundPanel.Top = -vScrollBar1.Value;
      vScrollBar1.SmallChange = vScrollBar1.Maximum/100;
      vScrollBar1.LargeChange = vScrollBar1.Maximum/10;

      for (var i = 0; i < _weekControls.Length; i++)
      {
        _weekControls[i] = new WeekControl {Top = i*WeeklyControlHeight, Parent = foregroundPanel};
      }

      backgroundPanel.Width = _weekControls[0].Width;
      foregroundPanel.Width = _weekControls[0].Width;

      LoadTrainingPlan();
    }

    private void butAddWorkout_Click(object sender, EventArgs e)
    {
      var ewf = new EditWorkoutForm();
      foreach (var wc in _weekControls)
      {
        var wc1 = wc;
        ewf.Closed += (s, ee) => wc1.ReloadWorkouts();
      }
      ewf.Show();
    }

    private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
    {
      SaveTrainingPlan();
    }

    private void SaveTrainingPlan()
    {
      var data = new string[TrainingWeeks];
      for (var i = 0; i < TrainingWeeks; i++)
      {
        data[i] = _weekControls[i].WeeklyPlan.Json;
      }
      File.WriteAllLines(TrainingPlanFile, data);
    }
    
    private void LoadTrainingPlan()
    {
      if (!File.Exists(TrainingPlanFile))
      {
        return;
      }

      var data = File.ReadAllLines(TrainingPlanFile);
      for (var i = 0; i < TrainingWeeks; i++)
      {
        _weekControls[i].WeeklyPlan = WeeklyPlan.FromJson(data[i]);
      }
    }
  }
}
