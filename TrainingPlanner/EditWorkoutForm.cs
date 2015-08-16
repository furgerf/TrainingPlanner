using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace TrainingPlanner
{
  public partial class EditWorkoutForm : Form
  {
    private readonly List<WorkoutStepControl> _stepControls = new List<WorkoutStepControl>();

    private int _stepControlWidth;

    private bool _dontAskToSave;
    private bool _cancelClosing;

    public EditWorkoutForm()
    {
      InitializeComponent();

      txtName.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
      txtName.AutoCompleteSource = AutoCompleteSource.CustomSource;
      //txtName.AutoCompleteCustomSource = new AutoCompleteStringCollection();
      txtName.AutoCompleteCustomSource.AddRange(Program.Workouts.Select(w => w.Name).ToArray());

      butAddStep_Click();
    }

    private void butAddStep_Click(object sender = null, EventArgs e = null)
    {
      var wsc = new WorkoutStepControl();

      if (_stepControlWidth == 0)
      {
        _stepControlWidth = wsc.Width + 5;
      }

      wsc.Location = new Point(15 + _stepControlWidth * _stepControls.Count, butAddStep.Top);

      Controls.Add(wsc);
      _stepControls.Add(wsc);

      butAddStep.Left += _stepControlWidth;
      butRemoveStep.Left += _stepControlWidth;

      wsc.Focus();
    }

    private void butRemoveStep_Click(object sender, EventArgs e)
    {
      var wsc = _stepControls[_stepControls.Count - 1];
      Controls.Remove(wsc);
      _stepControls.Remove(wsc);
      wsc.Dispose();

      butAddStep.Left -= _stepControlWidth;
      butRemoveStep.Left -= _stepControlWidth;
    }


    private void EditWorkoutForm_FormClosing(object sender, FormClosingEventArgs e)
    {
      if (_dontAskToSave)
      {
        return;
      }

      if (MessageBox.Show("Do you want to save the workout?", "Save?", MessageBoxButtons.YesNo) == DialogResult.Yes)
      {
        butSave_Click();
        if (_cancelClosing)
        {
          e.Cancel = true;
        }
      }
    }

    private void butSave_Click(object sender = null, EventArgs e = null)
    {
      var steps = new Step[_stepControls.Count];

      for (var i = 0; i < _stepControls.Count; i++)
      {
        if (!_stepControls[i].IsValid)
        {
          MessageBox.Show(string.Format("Step {0} isn't valid, please fix...", i + 1));
          _cancelClosing = true;
          return;
        }

        steps[i] = _stepControls[i].Step;
      }

      var workout = new Workout(txtName.Text, steps);

      File.WriteAllText(Program.WorkoutsDirectory + Path.DirectorySeparatorChar + txtName.Text.ToLower().Replace(' ', '-') + ".json", workout.Json);

      _dontAskToSave = true;

      Program.Workouts.Add(workout);
      Program.Workouts.Sort((a, b) => string.CompareOrdinal(a.Name, b.Name));

      Close();
    }
  }
}
