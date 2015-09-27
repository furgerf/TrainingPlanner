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
      Console.WriteLine("Initializing application...");
      Application.EnableVisualStyles();
      Application.SetCompatibleTextRenderingDefault(false);

      // create data model
      var data = new Data();

      // create main form and its presenter
      var view = new MainForm(data);
      var presenter = new MainFormPresenter(view, data);

      Console.WriteLine("... initialization complete");
      Application.Run(view);
    }
  }
}
