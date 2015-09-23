using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using TrainingPlanner.Model;

namespace TrainingPlanner.View
{
  public partial class PaceForm : Form, IPaceForm
  {
    private const string PaceFormat = "mm':'ss";
    private static readonly Color ValidPaceColor = SystemColors.Window;
    private static readonly Color InvalidPaceColor = Color.LightSalmon;

    private readonly Dictionary<string, Tuple<Label, MaskedTextBox, TimeSpan>> _paceControls;
    public PaceForm()
    {
      InitializeComponent();

      _paceControls = new Dictionary<string, Tuple<Label, MaskedTextBox, TimeSpan>>
      {
        {"Easy", new Tuple<Label, MaskedTextBox, TimeSpan>(label1, txtEasy, Data.Paces["Easy"])},
        {"Long", new Tuple<Label, MaskedTextBox, TimeSpan>(label2, txtLong, Data.Paces["Long"])},
        {"Marathon", new Tuple<Label, MaskedTextBox, TimeSpan>(label3, txtMarathon, Data.Paces["Marathon"])},
        {"Halfmarathon", new Tuple<Label, MaskedTextBox, TimeSpan>(label4, txtHalfmarathon, Data.Paces["Halfmarathon"])},
        {"Threshold", new Tuple<Label, MaskedTextBox, TimeSpan>(label5, txtThreshold, Data.Paces["Threshold"])},
        {"TenK", new Tuple<Label, MaskedTextBox, TimeSpan>(label6, txtTenk, Data.Paces["TenK"])},
        {"FiveK", new Tuple<Label, MaskedTextBox, TimeSpan>(label7, txtFivek, Data.Paces["FiveK"])},
      };

      txtEasy.Text = Data.Paces["Easy"].ToString(PaceFormat);
      txtLong.Text = Data.Paces["Long"].ToString(PaceFormat);
      txtMarathon.Text = Data.Paces["Marathon"].ToString(PaceFormat);
      txtHalfmarathon.Text = Data.Paces["Halfmarathon"].ToString(PaceFormat);
      txtThreshold.Text = Data.Paces["Threshold"].ToString(PaceFormat);
      txtTenk.Text = Data.Paces["TenK"].ToString(PaceFormat);
      txtFivek.Text = Data.Paces["FiveK"].ToString(PaceFormat);
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
      var textBox = (MaskedTextBox) sender;
      TimeSpan ts;
      var valid = TimeSpan.TryParse("00:" + textBox.Text, out ts) && ts.TotalMinutes > 2 && ts.TotalMinutes < 10;

      textBox.BackColor = valid ? ValidPaceColor : InvalidPaceColor;

      // TODO: Mark chaned pace values
    }

    public event EventHandler SaveChangesButtonClick;

    public event EventHandler DiscardChangesButtonClick;

    public Tuple<string, TimeSpan>[] ChangedPaces
    {
      get
      {
        var result = new List<Tuple<string, TimeSpan>>();

        foreach (var c in _paceControls)
        {
          if (c.Value.Item2.Text != c.Value.Item3.ToString(PaceFormat))
          {
            result.Add(new Tuple<string, TimeSpan>(c.Key, TimeSpan.Parse("00:" + c.Value.Item2.Text)));
          }
        }

        return result.ToArray();
      }
    }
  }
}
