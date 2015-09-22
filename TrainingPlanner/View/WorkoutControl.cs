using System;
using System.Linq;
using System.Windows.Forms;
using TrainingPlanner.Model;

namespace TrainingPlanner.View
{
  public partial class WorkoutControl : UserControl
  {
    public delegate void OnWorkoutChanged(Workout newWorkout);

    public event OnWorkoutChanged WorkoutChanged;

    private Workout _workout;

    private readonly Control[] _emptyWorkoutControls;

    private readonly Control[] _nonemptyWorkoutControls;
    private Data _data;

    private bool HasWorkout { get { return _workout != null; } }

    public Workout Workout
    {
      get { return _workout; }
      set
      {
        _workout = value;

        if (WorkoutChanged != null)
        {
          WorkoutChanged(_workout);
        }
      }
    }

    private void DisplayCurrentWorkout()
    {
      if (HasWorkout)
      {
        labWorkoutName.Text = Workout.ShortName ?? Workout.Name;
        txtDescription.Text = Workout.Description;
        BackColor = this.Workout.CategoryName == null ? Data.DefaultBackgroundColor : this._data.WorkoutCategoryFromName(this.Workout.CategoryName).CategoryColor;
      }
      else
      {
        labWorkoutName.Text = "";
        txtDescription.Text = "";
        BackColor = Data.DefaultBackgroundColor;
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

    public WorkoutControl()
    {
      InitializeComponent();

      _emptyWorkoutControls = new Control[] { labSelectWorkout };
      _nonemptyWorkoutControls = new Control[] { labWorkoutName, txtDescription, butRemove, labRemove };

      WorkoutChanged += workout => DisplayCurrentWorkout();

      DisplayCurrentWorkout();

      this.ContextMenu = new ContextMenu();
    }

    private void CreateContextMenu()
    {
      if (this._data == null)
      {
        return;
      }

      // clear previous entries
      this.ContextMenu.MenuItems.Clear();

      // add categories and their workouts
      this.ContextMenu.MenuItems.AddRange(this._data.Categories.Select(c => new MenuItem(c.Name)).ToArray());
      foreach (MenuItem mi in this.ContextMenu.MenuItems)
      {
        mi.MenuItems.AddRange(this._data.Workouts.Where(w => mi.Text.Equals(w.CategoryName)).Select(w => new MenuItem(w.Name)).ToArray());
      }

      // add uncategorized workouts
      var uncategorizedMenu = new MenuItem("(uncategorized)");
      uncategorizedMenu.MenuItems.AddRange(this._data.Workouts.Where(w => w.CategoryName == null).Select(w => new MenuItem(w.Name)).ToArray());
      if (uncategorizedMenu.MenuItems.Count > 0)
      {
        this.ContextMenu.MenuItems.Add(uncategorizedMenu);
      }

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

    private void butRemove_Click(object sender, EventArgs e)
    {
      Workout = null;
    }

    public void SetData(Data data)
    {
      this._data = data;

      this._data.WorkoutsChanged += (s, e) => CreateContextMenu();

      CreateContextMenu();
    }

    private void txtDescription_MouseClick(object sender, MouseEventArgs e)
    {
      butRemove_Click(sender, e);
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
