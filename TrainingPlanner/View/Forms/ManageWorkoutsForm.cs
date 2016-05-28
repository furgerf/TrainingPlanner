using System;
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
    private readonly Data _data;

    public ManageWorkoutsForm(Data data)
    {
      _data = data;

      InitializeComponent();

      lisWorkouts.ListViewItemSorter = new ListViewItemComparer(1, false);
    }

    private void butAdd_Click(object sender, EventArgs e)
    {
      if (AddWorkoutButtonClick != null)
      {
        Logger.Debug("Triggering AddWorkoutButtonClick event");
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
        Logger.Debug("Triggering EditWorkoutButtonClick event");
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
        Logger.Debug("Triggering DeleteWorkoutButtonClick event");
        DeleteWorkoutButtonClick(this, lisWorkouts.SelectedItems[0].Text);
      }
    }

    private void butExit_Click(object sender, EventArgs e)
    {
      if (ExitButtonClick != null)
      {
        Logger.Debug("Triggering ExitButtonClick event");
        ExitButtonClick(this, e);
      }
    }

    public event EventHandler AddWorkoutButtonClick;
    public event EventHandler<string> EditWorkoutButtonClick;
    public event EventHandler<string> DeleteWorkoutButtonClick;
    public event EventHandler ExitButtonClick;

    public void DisplayWorkouts(Workout[] workouts)
    {
      lisWorkouts.Items.Clear();
      lisWorkouts.Items.AddRange(
        workouts.Select(
          c =>
            new ListViewItem(new[]
            {
              c.Name, c.CategoryName, Math.Round(c.Distance, 2).ToString(CultureInfo.InvariantCulture), c.Duration.ToString("hh':'mm':'ss"),
              _data.TrainingPlan.AllWorkouts.Count(w => w.Name == c.Name).ToString(CultureInfo.InvariantCulture)
            })
            {
              BackColor = _data.WorkoutCategoryFromName(c.CategoryName).CategoryColor
            }).ToArray());
      lisWorkouts.Sort();
    }

    private void lisWorkouts_ColumnClick(object sender, ColumnClickEventArgs e)
    {
      var lvic = (ListViewItemComparer) lisWorkouts.ListViewItemSorter;
      var reverse = e.Column == lvic.Column && !lvic.Reverse;

      lisWorkouts.ListViewItemSorter = new ListViewItemComparer(e.Column, reverse);
      lisWorkouts.Sort();
    }
  }
}
