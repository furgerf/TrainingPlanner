using System;
using System.Collections.Generic;
using TrainingPlanner.Model.Serializable;

namespace TrainingPlanner.View.Interfaces
{
  /// <summary>
  /// Defines the functionality required to interact with the PaceForm.
  /// </summary>
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
    IEnumerable<Tuple<Pace.Names, TimeSpan>> ChangedPaces { get; }

    /// <summary>
    /// Tells the form to close.
    /// </summary>
    void Close();
  }
}