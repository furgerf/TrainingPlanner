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
    private const int Padding = 10;

    private readonly List<WorkoutStepControl> _stepControls = new List<WorkoutStepControl>();

    public EditWorkoutForm(Data data)
    {
      InitializeComponent();

      txtName.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
      txtName.AutoCompleteSource = AutoCompleteSource.CustomSource;
      txtName.AutoCompleteCustomSource.AddRange(data.Workouts.Select(w => w.Name).ToArray());

      UpdateWidth();
    }

    private void UpdateWidth()
    {
      Width = Math.Max(butSave.Right, butAddStep.Right) + 2*Padding;
    }

    public void AddStep()
    {
      var wsc = new WorkoutStepControl
      {
        Location = new Point(_stepControls.Count == 0 ? labName.Left : _stepControls.Last().Right + Padding, butAddStep.Top)
      };

      Controls.Add(wsc);
      _stepControls.Add(wsc);

      butAddStep.Left = _stepControls.Last().Right + Padding;
      butRemoveStep.Left = _stepControls.Last().Right + Padding;

      UpdateWidth();

      wsc.Focus();
    }

    public void RemoveStep()
    {
      var wsc = _stepControls[_stepControls.Count - 1];
      Controls.Remove(wsc);
      _stepControls.Remove(wsc);
      wsc.Dispose();

      butAddStep.Left = _stepControls.Count == 0 ? labName.Left : _stepControls.Last().Right + Padding;
      butRemoveStep.Left = _stepControls.Count == 0 ? labName.Left : _stepControls.Last().Right + Padding;

      UpdateWidth();
    }

    public Step[] Steps
    {
      get
      {
        return _stepControls.Any(sc => !sc.IsValid) ? null :  _stepControls.Select(sc => sc.Step).ToArray();
      }
    }

    public string WorkoutName { get { return txtName.Text; } }
    public string CategoryName { get { return comCategory.Text; } }

    public event EventHandler AddStepButtonClick;
    public event EventHandler RemoveStepButtonClick;
    public event EventHandler SaveButtonClick;
    public event EventHandler<FormClosingEventArgs> EditWorkoutFormClosing;

    public void SetCategories(string[] categories)
    {
      this.comCategory.Items.Clear();
      this.comCategory.Items.AddRange(categories);
    }

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
