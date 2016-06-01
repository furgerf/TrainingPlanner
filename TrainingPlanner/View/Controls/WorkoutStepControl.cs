using System;
using System.Globalization;
using System.Windows.Forms;
using TrainingPlanner.Model;
using TrainingPlanner.Model.Serializable;

namespace TrainingPlanner.View.Controls
{
  public partial class WorkoutStepControl : UserControl
  {
    private bool _distanceRecentlyCalculated;

    private bool _dontRecalculate;

    public WorkoutStepControl()
    {
      InitializeComponent();

      comName.Items.AddRange(new object[] {"Warmup", "Cooldown"});
      comPace.Items.AddRange(Enum.GetNames(typeof (Pace.Names)));
      comPace.SelectedIndex = 0;
    }

    public bool IsValid
    {
      get { return (Duration.HasValue || Distance.HasValue) && comName.Text != null; }
    }

    public Step Step
    {
      get
      {
        if (!IsValid)
        {
          return Step.Empty;
        }

        return _distanceRecentlyCalculated
          ? new Step(comName.Text, Distance.Value, Pace, Rest, (int) numRepetitions.Value)
          : new Step(comName.Text, Duration.Value, Pace, Rest, (int) numRepetitions.Value);
      }
      set
      {
        comName.Text = value.Name;
        if (value.IsDistanceCalculated)
        {
          txtDuration.Text = value.Duration.ToString("hh':'mm':'ss");
        }
        else
        {
          txtDistance.Text = value.Distance.ToString(CultureInfo.InvariantCulture);
        }
        Pace = value.Pace;

        if (value.Rest != TimeSpan.Zero)
        {
          txtRest.Text = value.Rest.ToString("mm':'ss");
        }
        numRepetitions.Value = value.Repetitions;
      }
    }

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

    private Pace.Names Pace
    {
      get { return (Pace.Names) Enum.Parse(typeof (Pace.Names), comPace.Text); }
      set { comPace.Text = value.ToString(); }
    }

    private TimeSpan? Rest
    {
      get
      {
        TimeSpan rest;
        var success = TimeSpan.TryParse("00:" + txtRest.Text, out rest);

        if (success)
        {
          return rest;
        }

        return null;
      }
    }

    private void txtDuration_TextChanged(object sender, EventArgs e)
    {
      if (!Duration.HasValue || _dontRecalculate)
      {
        return;
      }

      _dontRecalculate = true;
      txtDistance.Text = string.Format("{0}", Math.Round(Duration.Value.TotalMinutes/Data.Instance.GetDurationFromPace(Pace).TotalMinutes, 2));
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
      txtDuration.Text = TimeSpan.FromMinutes(Data.Instance.GetDurationFromPace(Pace).TotalMinutes*Distance.Value).ToString();
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

    private void comName_TextChanged(object sender, EventArgs e)
    {
      Step step;

      if ("warmup".Equals(comName.Text.ToLower()))
      {
        step = Step.Warmup;
      }
      else if ("cooldown".Equals(comName.Text.ToLower()))
      {
        step = Step.Cooldown;
      }
      else
      {
        return;
      }

      comPace.Text = step.PaceName;
      txtRest.Text = step.Rest.TotalSeconds == 0 ? "" : step.Rest.ToString(Model.Serializable.Pace.PaceFormat);
      txtDuration.Text = step.Duration.ToString(Model.Serializable.Pace.PaceFormat);
      numRepetitions.Value = step.Repetitions;
    }
  }
}
