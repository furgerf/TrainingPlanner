using System;
using System.Collections;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using TrainingPlanner.Model;
using TrainingPlanner.Model.Serializable;
using TrainingPlanner.View.Interfaces;

namespace TrainingPlanner.View.Forms
{
  public partial class ManageWorkoutsForm : Form, IManageWorkoutsForm
  {
    private class ListViewItemComparer : IComparer
    {
      private readonly int _col;
      private readonly bool _reverse;

      public ListViewItemComparer(int column, bool reverse)
      {
        this._col = column;
        this._reverse = reverse;
      }

      public int Column { get { return _col; } }
      public bool Reverse { get { return _reverse; } }

      public int Compare(object x, object y)
      {
        var sx = ((ListViewItem) x).SubItems[_col].Text;
        var sy = ((ListViewItem) y).SubItems[_col].Text;

        double dx, dy;
        TimeSpan tx, ty;

        // attempt double comparison
        if (double.TryParse(sx, out dx) && double.TryParse(sy, out dy))
        {
          if (dx == dy)
          {
            return 0;
          }
          return (_reverse ? -1 : 1) * (dx > dy ? 1 : -1);
        }

        // attempt timespan comparison
        if (TimeSpan.TryParse(sx, out tx) && TimeSpan.TryParse(sy, out ty))
        {
          if (tx == ty)
          {
            return 0;
          }
          return (_reverse ? -1 : 1) * (tx > ty ? 1 : -1);
        }

        // fall back to string comparison
        return (_reverse ? -1 : 1) * string.CompareOrdinal(((ListViewItem)x).SubItems[_col].Text, ((ListViewItem)y).SubItems[_col].Text);
      }
    }

    private readonly Data _data;

    public ManageWorkoutsForm(Data data)
    {
      this._data = data;

      InitializeComponent();

      this.lisWorkouts.ListViewItemSorter = new ListViewItemComparer(1, false);
    }

    private void butAdd_Click(object sender, EventArgs e)
    {
      if (AddWorkoutButtonClick != null)
      {
        Logger.Debug("Triggering AddCategoryButtonClick event");
        AddWorkoutButtonClick(this, e);
      }
    }

    private void butEdit_Click(object sender, EventArgs e)
    {
      if (lisWorkouts.SelectedItems.Count != 1)
      {
        return;
      }

      if (EditWorkoutButtonClick != null)
      {
        Logger.Debug("Triggering EditCategoryButtonClick event");
        EditWorkoutButtonClick(this, lisWorkouts.SelectedItems[0].Text);
      }
    }

    private void butDelete_Click(object sender, EventArgs e)
    {
      if (lisWorkouts.SelectedItems.Count != 1)
      {
        return;
      }

      if (DeleteWorkoutButtonClick != null)
      {
        Logger.Debug("Triggering DeleteCategoryButtonClick event");
        DeleteWorkoutButtonClick(this, lisWorkouts.SelectedItems[0].Text);
      }
    }

    private void butExit_Click(object sender, EventArgs e)
    {
      if (ExitButtonClick != null)
      {
        Logger.Debug("Triggering ExitCategoryButtonClick event");
        ExitButtonClick(this, e);
      }
    }

    public event EventHandler AddWorkoutButtonClick;
    public event EventHandler<string> EditWorkoutButtonClick;
    public event EventHandler<string> DeleteWorkoutButtonClick;
    public event EventHandler ExitButtonClick;

    public void DisplayWorkouts(Workout[] workouts)
    {
      this.lisWorkouts.Items.Clear();
      this.lisWorkouts.Items.AddRange(
        workouts.Select(
          c =>
            new ListViewItem(new[]
            {
              c.Name, c.CategoryName, Math.Round(c.Distance, 2).ToString(CultureInfo.InvariantCulture), c.Duration.ToString("hh':'mm':'ss"),
              this._data.TrainingPlan.AllWorkouts.Count(w => w.Name == c.Name).ToString(CultureInfo.InvariantCulture)
            })
            {
              BackColor = this._data.WorkoutCategoryFromName(c.CategoryName).CategoryColor
            }).ToArray());
      this.lisWorkouts.Sort();
    }

    private void lisWorkouts_ColumnClick(object sender, ColumnClickEventArgs e)
    {
      var lvic = (ListViewItemComparer) this.lisWorkouts.ListViewItemSorter;
      var reverse = e.Column == lvic.Column && !lvic.Reverse;

      this.lisWorkouts.ListViewItemSorter = new ListViewItemComparer(e.Column, reverse);
      this.lisWorkouts.Sort();
    }
  }
}
