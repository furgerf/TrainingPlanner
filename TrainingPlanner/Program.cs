using System;
using System.Windows.Forms;
using TrainingPlanner.Model;
using TrainingPlanner.Presenter;
using TrainingPlanner.View.Forms;

namespace TrainingPlanner
{
  internal static class Program
  {
    /// <summary>
    /// The main entry point for the application.
    /// </summary>
    [STAThread]
    private static void Main()
    {
      Logger.LogSeverity = Logger.Severity.Info;
      Logger.LoggingPaths.Add(DataPersistence.LogFile);

      Logger.Info("Initializing application...");
      Application.EnableVisualStyles();
      Application.SetCompatibleTextRenderingDefault(false);

      // create data model - load "default"
      var data = new Data("Sempacherseelauf 2016");

      // create main form and its presenter
      var view = new MainForm(data);
      var presenter = new MainFormPresenter(view, data);

      Logger.Info("... initialization complete");
      Application.Run(view);
    }
  }
}
