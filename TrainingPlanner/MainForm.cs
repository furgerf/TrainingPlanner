using System;
using System.Windows.Forms;

namespace TrainingPlanner
{
  public partial class MainForm : Form
  {
    public MainForm()
    {
      InitializeComponent();


      var foo = new Workout("Short Intervals", new Step[]
      {
        Step.Warmup, new Step("Work", TimeSpan.FromMinutes(5), Paces.Default.Threshold, TimeSpan.FromMinutes(1), 5),
        Step.Cooldown
      });

      weekControl1.Workouts = new[] {foo, foo, foo, foo, foo, foo, foo, foo, foo, foo, foo, foo, foo, foo};

    }
  }
}
