using System;
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
      this._data = data;
      this._view = view;

      this._view.DiscardChangesButtonClick += (s, e) => this._view.Close();
      this._view.SaveChangesButtonClick += (s, e) => OnSaveButtonClick();
    }

    private void OnSaveButtonClick()
    {
      foreach (var change in this._view.ChangedPaces)
      {
        this._data.ChangePace(change.Item1, change.Item2);
      }
      this._view.Close();
    }
  }
}