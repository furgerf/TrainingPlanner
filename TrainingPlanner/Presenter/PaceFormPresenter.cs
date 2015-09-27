using TrainingPlanner.Model;
using TrainingPlanner.Presenter.Interfaces;
using TrainingPlanner.View.Interfaces;

namespace TrainingPlanner.Presenter
{
  public class PaceFormPresenter : IPaceFormPresenter
  {
    public PaceFormPresenter(IPaceForm view, Data data)
    {
      view.DiscardChangesButtonClick += (s, e) => view.Close();
      view.SaveChangesButtonClick += (s, e) =>
      {
        foreach (var change in view.ChangedPaces)
        {
          data.ChangePace(change.Item1, change.Item2);
        }
        view.Close();
      };
    }
  }
}