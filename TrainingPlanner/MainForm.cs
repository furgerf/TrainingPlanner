using System;
using System.Windows.Forms;

namespace TrainingPlanner
{
  public partial class MainForm : Form
  {
    private const int TrainingWeeks = 11;

    private const int WeeklyControlHeight = 338;

    private WeekControl[] weekControls = new WeekControl[11];

    public MainForm()
    {
      InitializeComponent();

      WindowState = FormWindowState.Maximized;
      vScrollBar1.Height = 1000;
      panel1.Height = TrainingWeeks*WeeklyControlHeight;
      vScrollBar1.Maximum = panel1.Height;
      vScrollBar1.ValueChanged += (s, e) => panel1.Top = -vScrollBar1.Value;

      for (var i = 0; i < weekControls.Length; i++)
      {
        weekControls[i] = new WeekControl {Top = i*WeeklyControlHeight};

        panel1.Controls.Add(weekControls[i]);
      }

    }

    private void butAddWorkout_Click(object sender, EventArgs e)
    {
      var ewf = new EditWorkoutForm();
      foreach (var wc in weekControls)
      {
        var wc1 = wc;
        ewf.Closed += (s, ee) => wc1.ReloadWorkouts();
      }
      ewf.Show();
    }
  }
}
