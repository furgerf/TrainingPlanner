using System.Linq;
using TrainingPlanner.Model;
using TrainingPlanner.Presenter.Interfaces;
using TrainingPlanner.View.Interfaces;

namespace TrainingPlanner.Presenter
{
  public class PaceFormPresenter : IPaceFormPresenter
  {
    private readonly IData _data;

    private readonly IPaceForm _view;

    public PaceFormPresenter(IPaceForm view, IData data)
    {
      _data = data;
      _view = view;

      _view.SaveChangesButtonClick += (s, e) => OnSaveButtonClick();
      _view.DiscardChangesButtonClick += (s, e) => _view.Close();
    }

    private void OnSaveButtonClick()
    {
      var changes = _view.ChangedPaces.ToArray();
      _data.ChangePaces(changes.Select(c => c.Item1).ToArray(),
        changes.Select(c => c.Item2).ToArray());
      _view.Close();
    }
  }
}