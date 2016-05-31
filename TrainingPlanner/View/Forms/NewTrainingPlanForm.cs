using System;
using System.Windows.Forms;
using TrainingPlanner.View.Interfaces;

namespace TrainingPlanner.View.Forms
{
  public partial class NewTrainingPlanForm : Form, INewTrainingPlanForm
  {
    private bool _updateDateRange = true;

    public NewTrainingPlanForm()
    {
      InitializeComponent();

      var daysOffset = DayOfWeek.Sunday - DateTime.Today.DayOfWeek + 1;
      if (daysOffset < 0)
      {
        daysOffset += 7;
      }
      var firstWeekStart = DateTime.Today.AddDays(daysOffset);
      dtpStartOfTrainingPlan_DateChanged(this, new DateRangeEventArgs(firstWeekStart, firstWeekStart));
      dtpStartOfTrainingPlan.MinDate = firstWeekStart.AddDays(-1); // subtracting one day to avoid range issues in calendar...
    }

    public string NewTrainingPlanName { get { return txtTrainingPlanName.Text; } }

    public int NumberOfTrainingWeeks { get { return (int) numTrainingWeeks.Value; } }

    public string TrainingPlanToImportWorkoutsFrom
    {
      get { return txtPlanToImportWorkoutsFrom.Text; }
      set { txtPlanToImportWorkoutsFrom.Text = value; }
    }

    public DateTime StartOfTrainingPlan { get { return dtpStartOfTrainingPlan.SelectionStart; } }

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

    private void dtpStartOfTrainingPlan_DateChanged(object sender, DateRangeEventArgs e)
    {
      if (!_updateDateRange)
      {
        return;
      }

      _updateDateRange = false;

      var diff = e.Start.DayOfWeek - DayOfWeek.Sunday;
      if (diff < 0)
      {
        diff += 7;
      }
      dtpStartOfTrainingPlan.SelectionStart = e.Start.AddDays(-1 * diff).Date;
      dtpStartOfTrainingPlan.SelectionEnd = dtpStartOfTrainingPlan.SelectionStart.AddDays(7);

      _updateDateRange = true;
    }
  }
}
