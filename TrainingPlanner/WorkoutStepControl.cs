using System;
using System.Linq;
using System.Windows.Forms;

namespace TrainingPlanner
{
  public partial class WorkoutStepControl : UserControl
  {
    private bool _distanceRecentlyCalculated;

    private bool _dontRecalculate;

    public bool IsValid
    {
      get { return (Duration != null || Distance != null) && comName.Text != null; }
    }

    //public Step Step
    //{
    //  get { return new Step(); }
    //}

    private double? Distance
    {
      get
      {
        double distance;
        var success = double.TryParse(txtDistance.Text, out distance);

        if (success)
        {
          return distance;
        }

        return null;
      }
    }

    private TimeSpan? Duration
    {
      get
      {
        TimeSpan duration;
        var success = TimeSpan.TryParse(txtDuration.Text, out duration);

        if (success)
        {
          return duration;
        }

        return null;
      }
    }

    private TimeSpan Pace
    {
      get
      {
        return Program.Paces[comPace.Text];
      }
    }

    private TimeSpan? Rest
    {
      get
      {
        TimeSpan rest;
        var success = TimeSpan.TryParse(txtRest.Text, out rest);

        if (success)
        {
          return rest;
        }

        return null;
      }
    }

    public WorkoutStepControl()
    {
      InitializeComponent();

      comName.Items.AddRange(new object[] {"Warmup", "Cooldown"});
      comPace.Items.AddRange(Program.Paces.Keys.ToArray());
      comPace.SelectedIndex = 0;
    }

    private void txtDuration_TextChanged(object sender, EventArgs e)
    {
      if (!Duration.HasValue || _dontRecalculate)
      {
        return;
      }

      _dontRecalculate = true;
      txtDistance.Text = string.Format("{0}", Duration.Value.TotalMinutes/Pace.TotalMinutes);
      _dontRecalculate = false;

      _distanceRecentlyCalculated = false;
    }

    private void txtDistance_TextChanged(object sender, EventArgs e)
    {
      if (!Distance.HasValue || _dontRecalculate)
      {
        return;
      }

      _dontRecalculate = true;
      txtDuration.Text = TimeSpan.FromMinutes(Pace.TotalMinutes*Distance.Value).ToString();
      _dontRecalculate = false;

      _distanceRecentlyCalculated = true;
    }

    private void comPace_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (_distanceRecentlyCalculated)
      {
        txtDistance_TextChanged(sender, e);
      }
      else
      {
        txtDuration_TextChanged(sender, e);
      }
    }
  }
}
