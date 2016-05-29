using System;
using System.Windows.Forms;
using TrainingPlanner.View.Interfaces;

namespace TrainingPlanner.View.Forms
{
  public partial class NewTrainingPlanForm : Form, INewTrainingPlanForm
  {
    public NewTrainingPlanForm()
    {
      InitializeComponent();
    }

    public string NewTrainingPlanName { get { return txtTrainingPlanName.Text; } }

    public int NumberOfTrainingWeeks { get { return (int) numTrainingWeeks.Value; } }

    public string TrainingPlanToImportWorkoutsFrom
    {
      get { return txtPlanToImportWorkoutsFrom.Text; }
      set { txtPlanToImportWorkoutsFrom.Text = value; }
    }

    public event EventHandler SelectPlanToImportWorkoutsClick = (s, e) => { }; 
    public event EventHandler OkButtonClick = (s, e) => { };
    public event EventHandler CancelButtonClick = (s, e) => { };

    private void butSelectWorkouts_Click(object sender, EventArgs e)
    {
      SelectPlanToImportWorkoutsClick(this, null);
    }

    private void butOk_Click(object sender, EventArgs e)
    {
      OkButtonClick(this, null);
    }

    private void butCancel_Click(object sender, EventArgs e)
    {
      CancelButtonClick(this, null);
    }
  }
}
