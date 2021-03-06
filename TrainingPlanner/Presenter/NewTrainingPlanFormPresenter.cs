﻿using System;
using System.IO;
using System.Windows.Forms;
using TrainingPlanner.Model;
using TrainingPlanner.Model.EventArgs;
using TrainingPlanner.Presenter.Interfaces;
using TrainingPlanner.View.Interfaces;

namespace TrainingPlanner.Presenter
{
  public class NewTrainingPlanFormPresenter : INewTrainingPlanFormPresenter
  {
    private readonly INewTrainingPlanForm _view;

    public NewTrainingPlanFormPresenter(INewTrainingPlanForm view)
    {
      _view = view;
      
      _view.OkButtonClick += OnOkButtonClick;
      _view.CancelButtonClick += OnCancelButtonClick;
      _view.SelectPlanToImportWorkoutsClick += OnSelectPlanToImportWorkoutsButtonClick;
    }

    private void OnOkButtonClick(object sender, EventArgs e)
    {
      if (string.IsNullOrEmpty(_view.NewTrainingPlanName))
      {
        MessageBox.Show("Please enter a valid plan name");
        return;
      }

      if (string.IsNullOrEmpty(_view.PathToTrainingPlanToImportDataFrom) ||
          _view.PathToTrainingPlanToImportDataFrom.IndexOfAny(Path.GetInvalidFileNameChars()) >= 0)
      {
        if (
          MessageBox.Show("Using sample training data for workouts, categories, and paces, is that ok?",
            "Sample training data", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.Cancel)
        {
          return;
        }
      }

      NewTrainingPlanDataEntered(this, new NewTrainingPlanEventArgs(_view.NewTrainingPlanName, _view.NumberOfTrainingWeeks, _view.PathToTrainingPlanToImportDataFrom, _view.StartOfTrainingPlan));

      _view.Close();
    }

    private void OnCancelButtonClick(object sender, EventArgs e)
    {
      _view.Close();
    }

    private void OnSelectPlanToImportWorkoutsButtonClick(object sender, EventArgs e)
    {
      var dlg = new OpenFileDialog
      {
        AddExtension = true,
        DefaultExt = "json",
        Filter = "JSON plan files|plan.json",
        InitialDirectory = DataPersistence.ApplicationDataDirectory,
        Title = "Select Training Plan file to open"
      };

      // abort if no plan was selected
      if (dlg.ShowDialog() != DialogResult.OK)
      {
        return;
      }

      var planName = new DirectoryInfo(dlg.FileName).Parent.Name;
      _view.PathToTrainingPlanToImportDataFrom = planName;
    }

    public event EventHandler<NewTrainingPlanEventArgs> NewTrainingPlanDataEntered = (s, e) => { };
  }
}