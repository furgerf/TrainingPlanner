using System;
using TrainingPlanner.Model;
using TrainingPlanner.Model.Serializable;

namespace TrainingPlanner.View.Interfaces
{
  public interface IPaceForm
  {
    /// <summary>
    /// Triggered when the user presses the "save changes"-button.
    /// </summary>
    event EventHandler SaveChangesButtonClick;

    /// <summary>
    /// Triggered when the user presses the "discard changes"-button.
    /// </summary>
    event EventHandler DiscardChangesButtonClick;

    /// <summary>
    /// Retrieve the paces that were modified by the user.
    /// </summary>
    Tuple<Pace.Names, TimeSpan>[] ChangedPaces { get; }

    /// <summary>
    /// Tells the form to close.
    /// </summary>
    void Close();
  }
}