using System;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Windows.Forms;

namespace TrainingPlanner
{
  public partial class MainForm : Form
  {
    public MainForm()
    {
      InitializeComponent();


      var foo = new Workout("Short Intervals", new[]
      {
        Step.Warmup, new Step("Work", TimeSpan.FromMinutes(5), Paces.Default.Threshold, TimeSpan.FromMinutes(1), 5),
        Step.Cooldown
      });

      var stream1 = new MemoryStream();
      var ser = new DataContractJsonSerializer(typeof(Workout));
      ser.WriteObject(stream1, foo);
      stream1.Position = 0;
      var sr = new StreamReader(stream1);

      File.WriteAllText("foo.txt", sr.ReadToEnd());

      var foobar = Workout.ParseJsonFile("foo.txt");

      weekControl1.Workouts = new[] {foobar, foo, null, foo, foo, foo, foo, foo, foo, foo, foo, foo, foo, foo};
    }
  }
}
