using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using TrainingPlanner.Model;
using TrainingPlanner.Model.Serializable;
using TrainingPlanner.View.Controls;
using TrainingPlanner.View.Interfaces;

namespace TrainingPlanner.View.Forms
{
  public partial class EditWorkoutForm : Form, IEditWorkoutForm
  {
    private const int ControlPadding = 10;

    private readonly List<WorkoutStepControl> _stepControls = new List<WorkoutStepControl>();

    public EditWorkoutForm(Data data)
    {
      InitializeComponent();

      txtName.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
      txtName.AutoCompleteSource = AutoCompleteSource.CustomSource;
      txtName.AutoCompleteCustomSource.AddRange(data.Workouts.Select(w => w.Name).ToArray());

      UpdateWidth();
    }

    public EditWorkoutForm(Data data, Workout workout)
      : this(data)
    {
      foreach (var wsc in workout.Steps.Select(t => new WorkoutStepControl
      {
        Location = new Point(_stepControls.Count == 0 ? labName.Left : _stepControls.Last().Right + ControlPadding, butAddStep.Top),
        Parent = this,
        Step = t
      }))
      {
        _stepControls.Add(wsc);
      }

      txtName.Text = workout.Name;

      // assign the category name but also change the dropdownstyle
      // since the combobox doesn't have the category names yet
      comCategory.DropDownStyle = ComboBoxStyle.Simple;
      comCategory.Text = workout.CategoryName;

      butAddStep.Left = _stepControls.Last().Right + ControlPadding;
      butRemoveStep.Left = _stepControls.Last().Right + ControlPadding;
    }

    private void UpdateWidth()
    {
      Width = Math.Max(butDelete.Right, butAddStep.Right) + 2*ControlPadding;
    }

    public void AddStep()
    {
      var wsc = new WorkoutStepControl
      {
        Location = new Point(_stepControls.Count == 0 ? labName.Left : _stepControls.Last().Right + ControlPadding, butAddStep.Top), Parent = this
      };

      _stepControls.Add(wsc);

      butAddStep.Left = _stepControls.Last().Right + ControlPadding;
      butRemoveStep.Left = _stepControls.Last().Right + ControlPadding;

      UpdateWidth();

      wsc.Focus();
    }

    public void RemoveStep()
    {
      var wsc = _stepControls[_stepControls.Count - 1];
      Controls.Remove(wsc);
      _stepControls.Remove(wsc);
      wsc.Dispose();

      butAddStep.Left = _stepControls.Count == 0 ? labName.Left : _stepControls.Last().Right + ControlPadding;
      butRemoveStep.Left = _stepControls.Count == 0 ? labName.Left : _stepControls.Last().Right + ControlPadding;

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

    public string WorkoutShortName { get { return txtShortName.Text; } }

    public string CategoryName { get { return comCategory.Text; } }

    public event EventHandler AddStepButtonClick;
    public event EventHandler RemoveStepButtonClick;
    public event EventHandler SaveButtonClick;
    public event EventHandler DeleteButtonClick;
    public event EventHandler<FormClosingEventArgs> EditWorkoutFormClosing;

    public void SetCategories(string[] categories)
    {
      // remember text that may have already been entered (when editing an existing workout)
      // to be re-assigned later
      var text = this.comCategory.Text;
      this.comCategory.Items.Clear();
      this.comCategory.Items.AddRange(categories);
      this.comCategory.DropDownStyle = ComboBoxStyle.DropDownList;
      this.comCategory.Text = text;
    }

    private void butSave_Click(object sender, EventArgs e)
    {
      if (SaveButtonClick != null)
      {
        Console.WriteLine("Triggering SaveButtonClick event");
        SaveButtonClick(this, e);
      }
    }

    private void butAddStep_Click(object sender, EventArgs e)
    {
      if (AddStepButtonClick != null)
      {
        Console.WriteLine("Triggering AddStepButtonClick event");
        AddStepButtonClick(this, e);
      }
    }

    private void butRemoveStep_Click(object sender, EventArgs e)
    {
      if (RemoveStepButtonClick != null)
      {
        Console.WriteLine("Triggering RemoveStepButtonClick event");
        RemoveStepButtonClick(this, e);
      }
    }

    private void EditWorkoutForm_FormClosing(object sender, FormClosingEventArgs e)
    {
      if (EditWorkoutFormClosing != null)
      {
        Console.WriteLine("Triggering EditWorkoutFormClosing event");
        EditWorkoutFormClosing(this, e);
      }
    }

    private void butDelete_Click(object sender, EventArgs e)
    {
      if (DeleteButtonClick != null)
      {
        Console.WriteLine("Triggering DeleteButtonClick event");
        DeleteButtonClick(this, e);
      }
    }
  }
}
