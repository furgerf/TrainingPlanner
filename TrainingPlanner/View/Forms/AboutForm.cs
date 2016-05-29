using System;
using System.Windows.Forms;

namespace TrainingPlanner.View.Forms
{
  public partial class AboutForm : Form
  {
    public AboutForm()
    {
      InitializeComponent();

      txtAbout.Text =
        string.Format(
          "TrainingPlanner version {1}.{0}Developed by Fabian Furger, 2015-2016.{0}Free for non-commercial use under APL 2.0.{0}For comments and questions, please visit:{0}https://github.com/furgerf/TrainingPlanner",
          Environment.NewLine, Application.ProductVersion);
    }
  }
}
