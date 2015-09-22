using System.IO;
using TrainingPlanner.Model;
using TrainingPlanner.View;

namespace TrainingPlanner.Presenter
{
  public class MainFormPresenter : IMainFormPresenter
  {
    private readonly IMainForm _view;

    private readonly WeeklyPlan[] _weeklyPlans;

    public MainFormPresenter(IMainForm view)
    {
      this._view = view;

      this._weeklyPlans = new WeeklyPlan[this._view.TrainingWeeks];

      // load training plan before subscribing to the WeeklyPlansChanged event
      LoadTrainingPlan();

      this._view.MainFormClosing += (s, e) => SaveTrainingPlan();
      this._view.AddWorkoutButtonClick += (s, e) => _view.ShowEditWorkoutForm();
      this._view.WeeklyPlansChanged += (s, e) =>
      {
        for (var i = 0; i < _weeklyPlans.Length; i++)
        {
          _weeklyPlans[i] = e.Value[i];
        }
      };
    }

    private void SaveTrainingPlan()
    {
      var data = new string[this._view.TrainingWeeks];
      for (var i = 0; i < this._view.TrainingWeeks; i++)
      {
        data[i] = this._weeklyPlans[i].Json;
      }
      File.WriteAllLines(Program.TrainingPlanFile, data);
    }

    private void LoadTrainingPlan()
    {
      if (!File.Exists(Program.TrainingPlanFile))
      {
        return;
      }

      var data = File.ReadAllLines(Program.TrainingPlanFile);
      for (var i = 0; i < this._view.TrainingWeeks; i++)
      {
        this._weeklyPlans[i] = WeeklyPlan.FromJson(data[i]);
      }

      this._view.UpdateWeeklyPlan(_weeklyPlans);
    }
  }
}