using System;
using System.Windows.Forms;
using TrainingPlanner.View.Interfaces;

namespace TrainingPlanner.View.Forms
{
  public partial class SelectWorkoutCategoryForm : Form, ISelectWorkoutCategoryForm
  {
    public SelectWorkoutCategoryForm()
    {
      InitializeComponent();
    }

    public event EventHandler<string> WorkoutCategorySelected;

    public void SetCategories(string[] categories)
    {
      var selected = this.lisCategories.SelectedItem;
      this.lisCategories.Items.Clear();
      this.lisCategories.Items.AddRange(categories);
      this.lisCategories.SelectedItem = selected;
    }

    private void lisCategories_MouseDoubleClick(object sender, MouseEventArgs e)
    {
      if (WorkoutCategorySelected != null)
      {
        Logger.Debug("Triggering WorkoutCategorySelected event");
        WorkoutCategorySelected(this, this.lisCategories.SelectedItem.ToString());
      }

    }

    private void lisCategories_KeyDown(object sender, KeyEventArgs e)
    {
      if (e.KeyCode == Keys.Enter)
      {
        lisCategories_MouseDoubleClick(this, null);
      }
    }
  }
}
