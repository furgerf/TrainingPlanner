using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using TrainingPlanner.Model;

namespace TrainingPlanner.View
{
  public partial class PaceForm : Form, IPaceForm
  {
    private const string PaceFormat = "mm':'ss";
    private static readonly Color ValidPaceColor = SystemColors.Window;
    private static readonly Color InvalidPaceColor = Color.LightSalmon;

    private readonly Dictionary<Pace, Tuple<Label, MaskedTextBox, TimeSpan>> _paceControls;
    public PaceForm()
    {
      InitializeComponent();

      _paceControls = new Dictionary<Pace, Tuple<Label, MaskedTextBox, TimeSpan>>
      {
        {Pace.Easy, new Tuple<Label, MaskedTextBox, TimeSpan>(label1, txtEasy, Data.Paces[Pace.Easy])},
        {Pace.Long, new Tuple<Label, MaskedTextBox, TimeSpan>(label2, txtLong, Data.Paces[Pace.Long])},
        {Pace.Marathon, new Tuple<Label, MaskedTextBox, TimeSpan>(label3, txtMarathon, Data.Paces[Pace.Marathon])},
        {Pace.Halfmarathon, new Tuple<Label, MaskedTextBox, TimeSpan>(label4, txtHalfmarathon, Data.Paces[Pace.Halfmarathon])},
        {Pace.Threshold, new Tuple<Label, MaskedTextBox, TimeSpan>(label5, txtThreshold, Data.Paces[Pace.Threshold])},
        {Pace.Tenk, new Tuple<Label, MaskedTextBox, TimeSpan>(label6, txtTenk, Data.Paces[Pace.Tenk])},
        {Pace.Fivek, new Tuple<Label, MaskedTextBox, TimeSpan>(label7, txtFivek, Data.Paces[Pace.Fivek])},
      };

      foreach (var c in _paceControls.Values)
      {
        c.Item2.Text = c.Item3.ToString(PaceFormat);
      }
    }

    private void butSaveChanges_Click(object sender, EventArgs e)
    {
      if (SaveChangesButtonClick != null)
      {
        SaveChangesButtonClick(this, e);
      }
    }

    private void butDiscardChanges_Click(object sender, EventArgs e)
    {
      if (DiscardChangesButtonClick != null)
      {
        DiscardChangesButtonClick(this, e);
      }
    }

    private void PaceValueChanged(object sender, EventArgs e)
    {
      var data = _paceControls.Values.First(p => p.Item2 == sender);
      TimeSpan ts;
      var valid = TimeSpan.TryParse("00:" + data.Item2.Text, out ts) && ts.TotalMinutes > 2 && ts.TotalMinutes < 10;

      data.Item2.BackColor = valid ? ValidPaceColor : InvalidPaceColor;

      if (data.Item2.Text != data.Item3.ToString(PaceFormat) && !data.Item1.Text.EndsWith("*"))
      {
        data.Item1.Text = data.Item1.Text + "*";
      }
      else if (data.Item2.Text == data.Item3.ToString(PaceFormat) && data.Item1.Text.EndsWith("*"))
      {
        data.Item1.Text = data.Item1.Text.Substring(0, data.Item1.Text.Length - 1);
      }
    }

    public event EventHandler SaveChangesButtonClick;

    public event EventHandler DiscardChangesButtonClick;

    public Tuple<Pace, TimeSpan>[] ChangedPaces
    {
      get
      {
        var result = new List<Tuple<Pace, TimeSpan>>();

        foreach (var c in _paceControls)
        {
          if (c.Value.Item2.Text != c.Value.Item3.ToString(PaceFormat))
          {
            result.Add(new Tuple<Pace, TimeSpan>(c.Key, TimeSpan.Parse("00:" + c.Value.Item2.Text)));
          }
        }

        return result.ToArray();
      }
    }
  }
}
