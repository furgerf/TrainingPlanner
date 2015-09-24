using System.IO;
using TrainingPlanner.Model;
using TrainingPlanner.View;

namespace TrainingPlanner.Presenter
{
  public class MainFormPresenter : IMainFormPresenter
  {
    private readonly Data _data;

    public MainFormPresenter(IMainForm view, Data data)
    {
      this._data = data;

      view.MainFormClosing += (s, e) => SaveTrainingPlan();
      view.AddWorkoutButtonClick += (s, e) =>
      {
        var form = new EditWorkoutForm(this._data);
        var presenter = new EditWorkoutFormPresenter(form, this._data);
        form.Show();
      };
      view.ConfigurePacesButtonClick += (s, e) =>
      {
        var form = new PaceForm();
        var presenter = new PaceFormPresenter(form, this._data);
        form.Show();
      };
      view.EditWorkoutButtonClick += (s, e) =>
      {
        var form = new EditWorkoutForm(this._data, this._data.WorkoutFromName(e));
        var presenter = new EditWorkoutFormPresenter(form, this._data);
        // TODO: maybe notify the user that if he modifies the workout but leaves the name,
        // the workout will be overwritten but if it gets a new name, a new workout is created?
        form.Show();
      };
      view.WeeklyPlansChanged += (s, e) =>
      {
        for (var i = 0; i < this._data.TrainingPlan.Length; i++)
        {
          this._data.TrainingPlan[i] = e.Value[i];
        }
      };

      view.UpdateWeeklyPlan(this._data.TrainingPlan);
    }

    private void SaveTrainingPlan()
    {
      var data = new string[Data.TrainingWeeks];
      for (var i = 0; i < Data.TrainingWeeks; i++)
      {
        data[i] = this._data.TrainingPlan[i].Json;
      }
      File.WriteAllLines(Program.TrainingPlanFile, data);
    }
  }
}