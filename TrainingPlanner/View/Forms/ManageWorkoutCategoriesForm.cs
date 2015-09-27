﻿using System;
using System.Linq;
using System.Windows.Forms;
using TrainingPlanner.Model.Serializable;
using TrainingPlanner.View.Interfaces;

namespace TrainingPlanner.View.Forms
{
  public partial class ManageWorkoutCategoriesForm : Form, IManageWorkoutCategoriesForm
  {
    public ManageWorkoutCategoriesForm()
    {
      InitializeComponent();
    }

    private void butAdd_Click(object sender, EventArgs e)
    {
      if (AddCategoryButtonClick != null)
      {
        Console.WriteLine("Triggering AddCategoryButtonClick event");
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
        Console.WriteLine("Triggering EditCategoryButtonClick event");
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
        Console.WriteLine("Triggering DeleteCategoryButtonClick event");
        DeleteCategoryButtonClick(this, lisCategories.SelectedItems[0].Text);
      }
    }

    private void butExit_Click(object sender, EventArgs e)
    {
      if (ExitButtonClick != null)
      {
        Console.WriteLine("Triggering ExitCategoryButtonClick event");
        ExitButtonClick(this, e);
      }
    }

    public event EventHandler AddCategoryButtonClick;
    public event EventHandler<string> EditCategoryButtonClick;
    public event EventHandler<string> DeleteCategoryButtonClick;
    public event EventHandler ExitButtonClick;

    public void DisplayCategories(WorkoutCategory[] categories)
    {
      lisCategories.Items.Clear();
      lisCategories.Items.AddRange(categories.Select(c => new ListViewItem(new []{ c.Name, c.CategoryColor.ToKnownColor().ToString()}) { BackColor = c.CategoryColor }).ToArray());
    }
  }
}
