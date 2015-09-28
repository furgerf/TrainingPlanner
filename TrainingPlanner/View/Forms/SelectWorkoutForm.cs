﻿using System;
using System.Windows.Forms;
using TrainingPlanner.View.Interfaces;

namespace TrainingPlanner.View.Forms
{
  public partial class SelectWorkoutForm : Form, ISelectWorkoutForm
  {
    public SelectWorkoutForm()
    {
      InitializeComponent();
    }

    private void lisWorkouts_MouseDoubleClick(object sender, MouseEventArgs e)
    {
      if (WorkoutSelected != null)
      {
        Logger.Debug("Triggering WorkoutSelected event");
        WorkoutSelected(this, this.lisWorkouts.SelectedItem.ToString());
      }
    }

    private void lisCategories_SelectedValueChanged(object sender, EventArgs e)
    {
      if (WorkoutCategoryChanged != null)
      {
        Logger.Debug("Triggering WorkoutCategoryChanged event");
        WorkoutCategoryChanged(this, this.lisCategories.SelectedItem.ToString());
      }
    }

    public event EventHandler<string> WorkoutSelected;
    public event EventHandler<string> WorkoutCategoryChanged;

    public string CurrentCategory { get { return lisCategories.SelectedItem.ToString(); }}

    public void SetCategories(string[] categories)
    {
      var selected = this.lisCategories.SelectedItem;
      this.lisCategories.Items.Clear();
      this.lisCategories.Items.AddRange(categories);
      this.lisCategories.SelectedItem = selected;
    }

    public void SetWorkouts(string[] workouts)
    {
      var selected = this.lisWorkouts.SelectedItem;
      this.lisWorkouts.Items.Clear();
      this.lisWorkouts.Items.AddRange(workouts);
      this.lisWorkouts.SelectedItem = selected;
    }

    private void lisWorkouts_KeyDown(object sender, KeyEventArgs e)
    {
      if (e.KeyCode == Keys.Enter)
      {
        lisWorkouts_MouseDoubleClick(this, null);
      }
    }
  }
}
