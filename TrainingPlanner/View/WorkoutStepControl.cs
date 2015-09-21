﻿using System;
using System.Linq;
using System.Windows.Forms;
using TrainingPlanner.Model;

namespace TrainingPlanner.View
{
  public partial class WorkoutStepControl : UserControl
  {
    private bool _distanceRecentlyCalculated;

    private bool _dontRecalculate;

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
        var success = TimeSpan.TryParse("00:" + txtRest.Text, out rest);

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
      txtDistance.Text = string.Format("{0}", Math.Round(Duration.Value.TotalMinutes/Pace.TotalMinutes, 2));
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

    private void comName_TextChanged(object sender, EventArgs e)
    {
      if (!comName.Text.Equals("Warmup") && !comName.Text.Equals("Cooldown"))
      {
        return;
      }

      comPace.Text = "Easy";
      txtRest.Text = "";
      txtDuration.Text = new TimeSpan(0, 10, 0).ToString();
      numRepetitions.Value = 1;
    }
  }
}