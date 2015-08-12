using System;
using System.Windows.Forms;

namespace TrainingPlanner
{
  public partial class MainForm : Form
  {
    public MainForm()
    {
      InitializeComponent();
    }

    private void butAddWorkout_Click(object sender, EventArgs e)
    {
      var ewf = new EditWorkoutForm();
      ewf.Closed += (s, ee) => weekControl1.ReloadWorkouts();
      ewf.Show();
    }
  }
}
