using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using TrainingPlanner.Model;
using TrainingPlanner.Model.Serializable;
using TrainingPlanner.View.Interfaces;

namespace TrainingPlanner.View.Forms
{
  public partial class PaceForm : Form, IPaceForm
  {
    private readonly Dictionary<PaceNames, Tuple<Label, MaskedTextBox, TimeSpan, bool>> _paceControls;
    private readonly bool _validatePaces;

    public PaceForm()
    {
      InitializeComponent();

      _paceControls = new Dictionary<PaceNames, Tuple<Label, MaskedTextBox, TimeSpan, bool>>
      {
        {PaceNames.Easy, new Tuple<Label, MaskedTextBox, TimeSpan, bool>(label1, txtEasy, Data.Instance.GetDurationFromPace(PaceNames.Easy), true)},
        {PaceNames.Base, new Tuple<Label, MaskedTextBox, TimeSpan, bool>(label2, txtBase, Data.Instance.GetDurationFromPace(PaceNames.Base), true)},
        {PaceNames.Steady, new Tuple<Label, MaskedTextBox, TimeSpan, bool>(label8, txtSteady, Data.Instance.GetDurationFromPace(PaceNames.Steady), true)},
        {PaceNames.Marathon, new Tuple<Label, MaskedTextBox, TimeSpan, bool>(label3, txtMarathon, Data.Instance.GetDurationFromPace(PaceNames.Marathon), true)},
        {PaceNames.Halfmarathon, new Tuple<Label, MaskedTextBox, TimeSpan, bool>(label4, txtHalfmarathon, Data.Instance.GetDurationFromPace(PaceNames.Halfmarathon), true)},
        {PaceNames.Threshold, new Tuple<Label, MaskedTextBox, TimeSpan, bool>(label5, txtThreshold, Data.Instance.GetDurationFromPace(PaceNames.Threshold), true)},
        {PaceNames.TenK, new Tuple<Label, MaskedTextBox, TimeSpan, bool>(label6, txtTenk, Data.Instance.GetDurationFromPace(PaceNames.TenK), true)},
        {PaceNames.FiveK, new Tuple<Label, MaskedTextBox, TimeSpan, bool>(label7, txtFivek, Data.Instance.GetDurationFromPace(PaceNames.FiveK), true)},
      };

      foreach (var c in _paceControls.Values)
      {
        c.Item2.Text = c.Item3.ToString(Pace.PaceFormat);
      }

      _validatePaces = true;
    }

    public Tuple<PaceNames, TimeSpan>[] ChangedPaces
    {
      get
      {
        var result = new List<Tuple<PaceNames, TimeSpan>>();

        foreach (var c in _paceControls)
        {
          if (c.Value.Item2.Text != c.Value.Item3.ToString(Pace.PaceFormat))
          {
            result.Add(new Tuple<PaceNames, TimeSpan>(c.Key, TimeSpan.Parse("00:" + c.Value.Item2.Text)));
          }
        }

        return result.ToArray();
      }
    }

    public event EventHandler SaveChangesButtonClick;

    public event EventHandler DiscardChangesButtonClick;

    private void butSaveChanges_Click(object sender, EventArgs e)
    {
      if (SaveChangesButtonClick != null)
      {
        Logger.Debug("Triggering SaveChangesButtonClick event");
        SaveChangesButtonClick(this, e);
      }
    }

    private void butDiscardChanges_Click(object sender, EventArgs e)
    {
      if (DiscardChangesButtonClick != null)
      {
        Logger.Debug("Triggering DiscardChangesButtonClick event");
        DiscardChangesButtonClick(this, e);
      }
    }

    private void PaceValueChanged(object sender, EventArgs e)
    {
      if (!_validatePaces)
      {
        return;
      }

      var key = _paceControls.First(pc => pc.Value.Item2 == sender).Key;
      var data = _paceControls[key];
      TimeSpan ts;
      var valid = TimeSpan.TryParse("00:" + data.Item2.Text, out ts) && ts.TotalMinutes > 2 && ts.TotalMinutes < 10;

      _paceControls[key] = new Tuple<Label, MaskedTextBox, TimeSpan, bool>(data.Item1, data.Item2, data.Item3, valid);

      data.Item2.BackColor = valid ? Colors.Default.ValidPaceColor : Colors.Default.InvalidPaceColor;

      if (data.Item2.Text != data.Item3.ToString(Pace.PaceFormat) && !data.Item1.Text.EndsWith("*"))
      {
        data.Item1.Text = data.Item1.Text + "*";
      }
      else if (data.Item2.Text == data.Item3.ToString(Pace.PaceFormat) && data.Item1.Text.EndsWith("*"))
      {
        data.Item1.Text = data.Item1.Text.Substring(0, data.Item1.Text.Length - 1);
      }

      butSaveChanges.Enabled = _paceControls.Values.All(pc => pc.Item4);
    }
  }
}
