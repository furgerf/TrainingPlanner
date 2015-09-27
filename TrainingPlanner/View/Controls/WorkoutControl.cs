using System;
using System.Windows.Forms;
using TrainingPlanner.Model;
using TrainingPlanner.Model.Serializable;

namespace TrainingPlanner.View.Controls
{
  public partial class WorkoutControl : UserControl
  {
    private readonly Control[] _emptyWorkoutControls;

    private readonly Control[] _nonemptyWorkoutControls;

    private Workout _workout;

    private Data _data;

    public WorkoutControl()
    {
      InitializeComponent();

      _emptyWorkoutControls = new Control[] { labSelectWorkout };
      _nonemptyWorkoutControls = new Control[] { labWorkoutName, txtDescription, butRemove, labRemove };

      WorkoutChanged += (s, e) => DisplayCurrentWorkout();

      DisplayCurrentWorkout();
    }

    private bool HasWorkout { get { return _workout != null; } }

    public Workout Workout
    {
      get { return _workout; }
      set
      {
        _workout = value;

        if (WorkoutChanged != null)
        {
          Console.WriteLine("Triggering WorkoutChanged event");
          WorkoutChanged(this, _workout);
        }
      }
    }

    public event EventHandler<Workout> WorkoutChanged;

    private void DisplayCurrentWorkout()
    {
      if (HasWorkout)
      {
        labWorkoutName.Text = Workout.ShortName ?? Workout.Name;
        txtDescription.Text = Workout.Description;
        BackColor = this.Workout.CategoryName == null ? Colors.Default.DefaultWorkoutControlBackground : this._data.WorkoutCategoryFromName(this.Workout.CategoryName).CategoryColor;
      }
      else
      {
        labWorkoutName.Text = "";
        txtDescription.Text = "";
        BackColor = Colors.Default.DefaultWorkoutControlBackground;
      }

      foreach (var c in _emptyWorkoutControls)
      {
        c.Visible = !HasWorkout;
      }
      foreach (var c in _nonemptyWorkoutControls)
      {
        c.Visible = HasWorkout;
      }
    }

    private void CreateContextMenu()
    {
      if (this._data == null)
      {
        return;
      }

      // retrieve menu
      this.ContextMenu = this._data.WorkoutContextMenu;

      // add event listeners
      foreach (MenuItem category in this.ContextMenu.MenuItems)
      {
        foreach (MenuItem workout in category.MenuItems)
        {
          var workout1 = workout;
          workout.Click += (s, e) => WorkoutSelected(workout1.Text);
        }
      }
    }

    private void WorkoutSelected(string workoutName)
    {
      this.Workout = this._data.WorkoutFromName(workoutName);
    }

    private void RemoveWorkout(object sender, EventArgs e)
    {
      Workout = null;
    }

    public void SetData(Data data)
    {
      this._data = data;

      this._data.CategoryChanged += (s, e) => UpdateControl();
      this._data.WorkoutChanged += (s, e) => UpdateControl();

      CreateContextMenu();
    }

    private void UpdateControl()
    {
      // update the context menu
      CreateContextMenu();

      // update the own workout:
      // if this control has a workout...
      if (this.Workout != null)
      {
        // ... it may have been modified so we load the up-to-date information from Data
        this.Workout = this._data.WorkoutFromName(this.Workout.Name);
      }
    }

    private void SetActiveControl(object sender, EventArgs e)
    {
      ActiveControl = butRemove;
    }

    private void ShowContextMenu(object sender, MouseEventArgs e)
    {
      this.ContextMenu.Show((Control)sender, e.Location);
    }
  }
}
