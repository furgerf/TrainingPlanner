using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using TrainingPlanner.Model;

namespace TrainingPlanner.View
{
  public partial class EditWorkoutForm : Form, IEditWorkoutForm
  {
    private int _stepControlWidth;

    private readonly List<WorkoutStepControl> _stepControls = new List<WorkoutStepControl>();

    public EditWorkoutForm()
    {
      InitializeComponent();

      txtName.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
      txtName.AutoCompleteSource = AutoCompleteSource.CustomSource;
      txtName.AutoCompleteCustomSource.AddRange(Program.Workouts.Select(w => w.Name).ToArray());
    }

    public void AddStep()
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

    public void RemoveStep()
    {
      var wsc = _stepControls[_stepControls.Count - 1];
      Controls.Remove(wsc);
      _stepControls.Remove(wsc);
      wsc.Dispose();

      butAddStep.Left -= _stepControlWidth;
      butRemoveStep.Left -= _stepControlWidth;
    }

    public Step[] Steps
    {
      get
      {
        return _stepControls.Any(sc => !sc.IsValid) ? null :  _stepControls.Select(sc => sc.Step).ToArray();
      }
    }

    public string WorkoutName { get { return txtName.Text; } }

    public event EventHandler AddStepButtonClick;
    public event EventHandler RemoveStepButtonClick;
    public event EventHandler SaveButtonClick;
    public event EventHandler<FormClosingEventArgs> EditWorkoutFormClosing;

    private void butSave_Click(object sender, EventArgs e)
    {
      if (SaveButtonClick != null)
      {
        SaveButtonClick(this, e);
      }
    }

    private void butAddStep_Click(object sender, EventArgs e)
    {
      if (AddStepButtonClick != null)
      {
        AddStepButtonClick(this, e);
      }
    }

    private void butRemoveStep_Click(object sender, EventArgs e)
    {
      if (RemoveStepButtonClick != null)
      {
        RemoveStepButtonClick(this, e);
      }
    }

    private void EditWorkoutForm_FormClosing(object sender, FormClosingEventArgs e)
    {
      if (EditWorkoutFormClosing != null)
      {
        EditWorkoutFormClosing(this, e);
      }
    }
  }
}
