﻿using System;
using System.Linq;
using System.Windows.Forms;
using TrainingPlanner.Model;
using TrainingPlanner.Model.EventArgs;
using TrainingPlanner.Model.Serializable;

namespace TrainingPlanner.View.Controls
{
  public partial class WeekControl : UserControl
  {
    private const int MaxWeeklyWorkouts = 14;

    private readonly WorkoutControl[] _workoutControls;

    private bool _updateDateRange = true;

    public event EventHandler<EventArgs<WeeklyPlan>> WeeklyPlanChanged;

    private bool _triggerWeeklyPlanChangedEvent;

    private readonly Data _data;

    private WeeklyPlan _weeklyPlan;
    private bool _isActiveWeek;

    public bool IsActiveWeek
    {
      private get { return _isActiveWeek; }
      set
      {
        _isActiveWeek = value;
        BackColor = IsActiveWeek
          ? Colors.Default.ActiveWorkoutControlBackground
          : Colors.Default.DefaultWorkoutControlBackground;
      }
    }

    public WeeklyPlan WeeklyPlan
    {
      private get { return _weeklyPlan; }
      set
      {
        if (_weeklyPlan.Equals(value))
        {
          return;
        }

        _weeklyPlan = value;

        _triggerWeeklyPlanChangedEvent = false;

        for (var i = 0; i < MaxWeeklyWorkouts; i++)
        {
          if (value.Workouts[i] != null)
          {
            _workoutControls[i].Workout = _data.WorkoutFromName(value.Workouts[i]);
          }
        }

        txtNotes.Text = value.Notes;
        WeekStart = value.WeekStart;
        grpSummary.Text = "Summary - Week " + (value.WeekNumber + 1);

        _triggerWeeklyPlanChangedEvent = true;

        if (WeeklyPlanChanged != null && _triggerWeeklyPlanChangedEvent)
        {
          Logger.Debug("Triggering WeeklyPlanChanged event");
          WeeklyPlanChanged(this, new EventArgs<WeeklyPlan>(WeeklyPlan));
        }
      }
    }

    private DateTime WeekStart
    {
      get { return monthCalendar1.SelectionStart; }
      set { monthCalendar1.SelectionStart = value; }
    }

    public WeekControl(Data data)
    {
      InitializeComponent();

      _data = data;

      BackColor = Colors.Default.DefaultWorkoutControlBackground;

      _workoutControls = new[]
      {
        wrkMondayMorning, wrkMondayEvening, wrkTuesdayMorning, wrkTuesdayEvening, wrkWednesdayMorning,
        wrkWednesdayEvening, wrkThursdayMorning, wrkThursdayEvening, wrkFridayMorning, wrkFridayEvening,
        wrkSaturdayMorning, wrkSaturdayEvening, wrkSundayMorning, wrkSundayEvening
      };

      if (_workoutControls.Length != MaxWeeklyWorkouts)
      {
        throw new Exception("Must have exactly " + MaxWeeklyWorkouts + " workout contorls");
      }

      for (var i = 0; i < _workoutControls.Length; i++)
      {
        var i1 = i;
        _workoutControls[i].SetData(_data);
        _workoutControls[i].WorkoutChanged += (s, workout) =>
        {
          _weeklyPlan.Workouts[i1] = workout == null ? null : workout.Name;
          UpdateStatistics();

          if (WeeklyPlanChanged != null && _triggerWeeklyPlanChangedEvent)
          {
            Logger.Debug("Triggering WeeklyPlanChanged event");
            WeeklyPlanChanged(this, new EventArgs<WeeklyPlan>(WeeklyPlan));
          }
        };
      }

      // create a timer that triggers after 2s
      var noteChangeTriggerTimer = new Timer {Interval = 2000};

      noteChangeTriggerTimer.Tick += (s, e) =>
      {
        // when the timer triggers, stop it from re-triggering
        noteChangeTriggerTimer.Stop();

        // save the currently entered notes
        _weeklyPlan.Notes = txtNotes.Text;

        if (WeeklyPlanChanged != null)
        {
          Logger.Debug("Triggering WeeklyPlanChanged event");
          WeeklyPlanChanged(this, new EventArgs<WeeklyPlan>(WeeklyPlan));
        }
      };

      txtNotes.TextChanged += (s, e) =>
      {
        if (!_triggerWeeklyPlanChangedEvent)
        {
          return;
        }
        noteChangeTriggerTimer.Stop();
        noteChangeTriggerTimer.Start();
      };
    }

    private void UpdateStatistics()
    {
      txtWorkoutCount.Text = string.Format("{0} workouts", _weeklyPlan.Workouts.Count(w => w != null));
      txtTotalDuration.Text = string.Format("Total duration: {0}",
        TimeSpan.FromSeconds(_weeklyPlan.Workouts.Where(w => w != null).Sum(w => _data.WorkoutFromName(w).Duration.TotalSeconds)));
      txtTotalDistance.Text = string.Format("Total distance: {0}", Math.Round(_weeklyPlan.Workouts.Where(w => w != null).Sum(w => _data.WorkoutFromName(w).Distance), 1));
    }

    private void monthCalendar1_DateChanged(object sender, DateRangeEventArgs e)
    {
      if (!_updateDateRange)
      {
        return;
      }

      _updateDateRange = false;

      var diff = e.Start.DayOfWeek - DayOfWeek.Sunday;
      if (diff < 0)
      {
        diff += 7;
      }
      monthCalendar1.SelectionStart = e.Start.AddDays(-1 * diff).Date;
      monthCalendar1.SelectionEnd = monthCalendar1.SelectionStart.AddDays(7);

      _updateDateRange = true;

      _weeklyPlan.WeekStart = WeekStart;
      if (WeeklyPlanChanged != null && _triggerWeeklyPlanChangedEvent)
      {
        Logger.Debug("Triggering WeeklyPlanChanged event");
        WeeklyPlanChanged(this, new EventArgs<WeeklyPlan>(WeeklyPlan));
      }
    }

    public void Activate()
    {
      foreach (var wc in _workoutControls)
      {
        wc.Activate();
      }
    }
  }
}
