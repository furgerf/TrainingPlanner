using System;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using TrainingPlanner.Model;
using TrainingPlanner.Model.Serializable;
using TrainingPlanner.View.Interfaces;

namespace TrainingPlanner.View.Forms
{
  public partial class ManageWorkoutCategoriesForm : Form, IManageWorkoutCategoriesForm
  {
    private readonly Data _data;

    public ManageWorkoutCategoriesForm(Data data)
    {
      this._data = data;

      InitializeComponent();

      this.lisCategories.ListViewItemSorter = new ListViewItemComparer(0, false);
    }

    private void butAdd_Click(object sender, EventArgs e)
    {
      if (AddCategoryButtonClick != null)
      {
        Logger.Debug("Triggering AddCategoryButtonClick event");
        AddCategoryButtonClick(this, e);
      }
    }

    private void butEdit_Click(object sender, EventArgs e)
    {
      if (lisCategories.SelectedItems.Count != 1)
      {
        return;
      }

      if (EditCategoryButtonClick != null)
      {
        Logger.Debug("Triggering EditCategoryButtonClick event");
        EditCategoryButtonClick(this, lisCategories.SelectedItems[0].Text);
      }
    }

    private void butDelete_Click(object sender, EventArgs e)
    {
      if (lisCategories.SelectedItems.Count != 1)
      {
        return;
      }

      if (DeleteCategoryButtonClick != null)
      {
        Logger.Debug("Triggering DeleteCategoryButtonClick event");
        DeleteCategoryButtonClick(this, lisCategories.SelectedItems[0].Text);
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

    public event EventHandler AddCategoryButtonClick;
    public event EventHandler<string> EditCategoryButtonClick;
    public event EventHandler<string> DeleteCategoryButtonClick;
    public event EventHandler ExitButtonClick;

    public void DisplayCategories(WorkoutCategory[] categories)
    {
      this.lisCategories.Items.Clear();
      this.lisCategories.Items.AddRange(categories.Select(c => new ListViewItem(new []{ c.Name, c.CategoryColor.ToKnownColor().ToString(), this._data.Workouts.Count(w => w.CategoryName == c.Name).ToString(CultureInfo.InvariantCulture)}) { BackColor = c.CategoryColor }).ToArray());
      this.lisCategories.Sort();
    }

    private void lisCategories_ColumnClick(object sender, ColumnClickEventArgs e)
    {
      var lvic = (ListViewItemComparer) this.lisCategories.ListViewItemSorter;
      var reverse = e.Column == lvic.Column && !lvic.Reverse;

      this.lisCategories.ListViewItemSorter = new ListViewItemComparer(e.Column, reverse);
      this.lisCategories.Sort();
    }
  }
}
