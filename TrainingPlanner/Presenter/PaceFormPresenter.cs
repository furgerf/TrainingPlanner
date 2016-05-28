using TrainingPlanner.Model;
using TrainingPlanner.Presenter.Interfaces;
using TrainingPlanner.View.Interfaces;

namespace TrainingPlanner.Presenter
{
  public class PaceFormPresenter : IPaceFormPresenter
  {
    private readonly Data _data;

    private readonly IPaceForm _view;

    public PaceFormPresenter(IPaceForm view, Data data)
    {
      _data = data;
      _view = view;

      _view.DiscardChangesButtonClick += (s, e) => _view.Close();
      _view.SaveChangesButtonClick += (s, e) => OnSaveButtonClick();
    }

    private void OnSaveButtonClick()
    {
      foreach (var change in _view.ChangedPaces)
      {
        _data.ChangePace(change.Item1, change.Item2);
      }
      _view.Close();
    }
  }
}