using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using TrainingPlanner.Model;

namespace TrainingPlanner.View
{
  public partial class EditWorkoutCategoryForm : Form, IEditWorkoutCategoryForm
  {
    public EditWorkoutCategoryForm()
    {
      InitializeComponent();

      // fill combobox with known color names
      foreach (KnownColor kc in Enum.GetValues(typeof(KnownColor)))
        comColorNames.Items.Add(Color.FromKnownColor(kc).Name);
    }

    public EditWorkoutCategoryForm(WorkoutCategory category)
      : this()
    {
      // assign control values according to the category to be edited
      txtName.Text = category.Name;
      comColorNames.Text = category.CategoryColor.ToKnownColor().ToString();
    }

    public event EventHandler SaveButtonClick;

    public string CategoryName { get { return txtName.Text; } }

    public Color CategoryColor { get { return labColor.BackColor; } }

    /// <summary>
    /// Calculates the difference between two colors.
    /// </summary>
    private static int ColorDifference(Color a, Color b)
    {
      return Math.Abs(a.R - b.R) + Math.Abs(a.G - b.G) + Math.Abs(a.B - b.B);
    }

    private void labColor_Click(object sender, EventArgs e)
    {
      // Show the color dialog
      colPicker.Color = labColor.BackColor;
      var result = colPicker.ShowDialog();

      // See if user pressed ok.
      if (result != DialogResult.OK) return;
      var closest = Color.Empty;
      foreach (var known in from KnownColor kc in Enum.GetValues(typeof(KnownColor)) select Color.FromKnownColor(kc))
      {
        if (colPicker.Color.ToArgb() == known.ToArgb())
        {
          labColor.BackColor = colPicker.Color;
          comColorNames.SelectedIndex = comColorNames.Items.IndexOf(known.ToKnownColor().ToString());
          return;
        }

        if (closest == Color.Empty)
        {
          closest = known;
          continue;
        }

        if (ColorDifference(colPicker.Color, known) < ColorDifference(colPicker.Color, closest))
          closest = known;
      }

      comColorNames.SelectedIndex = comColorNames.Items.IndexOf(closest.Name);
      MessageBox.Show(
          "Color was slightly altered (absolute RGB difference: " + ColorDifference(closest, colPicker.Color) + ").",
          "Color altered", MessageBoxButtons.OK, MessageBoxIcon.Information);
    }

    private void comColorNames_SelectedValueChanged(object sender, EventArgs e)
    {
      labColor.BackColor = Color.FromName(comColorNames.Text);
    }

    private void butOk_Click(object sender, EventArgs e)
    {
      if (SaveButtonClick != null)
      {
        SaveButtonClick(this, e);
      }
    }

    private void butCancel_Click(object sender, EventArgs e)
    {
      Close();
    }
  }
}
