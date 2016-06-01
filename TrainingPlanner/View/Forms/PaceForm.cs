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
    private readonly Dictionary<Pace.Names, Tuple<Label, MaskedTextBox, TimeSpan, bool>> _paceControls;
    private readonly bool _validatePaces;

    public PaceForm()
    {
      InitializeComponent();

      _paceControls = new Dictionary<Pace.Names, Tuple<Label, MaskedTextBox, TimeSpan, bool>>
      {
        {Pace.Names.Easy, new Tuple<Label, MaskedTextBox, TimeSpan, bool>(label1, txtEasy, Data.Instance.GetDurationFromPace(Pace.Names.Easy), true)},
        {Pace.Names.Base, new Tuple<Label, MaskedTextBox, TimeSpan, bool>(label2, txtBase, Data.Instance.GetDurationFromPace(Pace.Names.Base), true)},
        {Pace.Names.Steady, new Tuple<Label, MaskedTextBox, TimeSpan, bool>(label8, txtSteady, Data.Instance.GetDurationFromPace(Pace.Names.Steady), true)},
        {Pace.Names.Marathon, new Tuple<Label, MaskedTextBox, TimeSpan, bool>(label3, txtMarathon, Data.Instance.GetDurationFromPace(Pace.Names.Marathon), true)},
        {Pace.Names.Halfmarathon, new Tuple<Label, MaskedTextBox, TimeSpan, bool>(label4, txtHalfmarathon, Data.Instance.GetDurationFromPace(Pace.Names.Halfmarathon), true)},
        {Pace.Names.Threshold, new Tuple<Label, MaskedTextBox, TimeSpan, bool>(label5, txtThreshold, Data.Instance.GetDurationFromPace(Pace.Names.Threshold), true)},
        {Pace.Names.TenK, new Tuple<Label, MaskedTextBox, TimeSpan, bool>(label6, txtTenk, Data.Instance.GetDurationFromPace(Pace.Names.TenK), true)},
        {Pace.Names.FiveK, new Tuple<Label, MaskedTextBox, TimeSpan, bool>(label7, txtFivek, Data.Instance.GetDurationFromPace(Pace.Names.FiveK), true)},
      };

      foreach (var c in _paceControls.Values)
      {
        c.Item2.Text = c.Item3.ToString(Pace.PaceFormat);
      }

      _validatePaces = true;
    }

    public IEnumerable<Tuple<Pace.Names, TimeSpan>> ChangedPaces
    {
      get
      {
        return (from c in _paceControls where c.Value.Item2.Text != c.Value.Item3.ToString(Pace.PaceFormat) select new Tuple<Pace.Names, TimeSpan>(c.Key, TimeSpan.Parse("00:" + c.Value.Item2.Text))).ToArray();
      }
    }

    public event EventHandler SaveChangesButtonClick = (s, e) => { };

    public event EventHandler DiscardChangesButtonClick = (s, e) => { }; 

    private void butSaveChanges_Click(object sender, EventArgs e)
    {
        Logger.Debug("Triggering SaveChangesButtonClick event");
        SaveChangesButtonClick(this, e);
    }

    private void butDiscardChanges_Click(object sender, EventArgs e)
    {
      Logger.Debug("Triggering DiscardChangesButtonClick event");
      DiscardChangesButtonClick(this, e);
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
