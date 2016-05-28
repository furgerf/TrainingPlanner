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
      var selected = lisCategories.SelectedItem;
      lisCategories.Items.Clear();
      lisCategories.Items.AddRange(categories);
      lisCategories.SelectedItem = selected;
    }

    private void lisCategories_MouseDoubleClick(object sender, MouseEventArgs e)
    {
      if (WorkoutCategorySelected != null)
      {
        Logger.Debug("Triggering WorkoutCategorySelected event");
        WorkoutCategorySelected(this, lisCategories.SelectedItem.ToString());
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
