namespace TrainingPlanner.View.Forms
{
  partial class SelectWorkoutForm
  {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
      if (disposing && (components != null))
      {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      this.lisCategories = new System.Windows.Forms.ListBox();
      this.lisWorkouts = new System.Windows.Forms.ListBox();
      this.SuspendLayout();
      // 
      // lisCategories
      // 
      this.lisCategories.FormattingEnabled = true;
      this.lisCategories.Location = new System.Drawing.Point(12, 12);
      this.lisCategories.Name = "lisCategories";
      this.lisCategories.Size = new System.Drawing.Size(120, 95);
      this.lisCategories.TabIndex = 0;
      this.lisCategories.SelectedValueChanged += new System.EventHandler(this.lisCategories_SelectedValueChanged);
      // 
      // lisWorkouts
      // 
      this.lisWorkouts.FormattingEnabled = true;
      this.lisWorkouts.Location = new System.Drawing.Point(138, 12);
      this.lisWorkouts.Name = "lisWorkouts";
      this.lisWorkouts.Size = new System.Drawing.Size(120, 95);
      this.lisWorkouts.TabIndex = 1;
      this.lisWorkouts.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lisWorkouts_KeyDown);
      this.lisWorkouts.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lisWorkouts_MouseDoubleClick);
      // 
      // SelectWorkoutForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(271, 122);
      this.Controls.Add(this.lisWorkouts);
      this.Controls.Add(this.lisCategories);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
      this.Name = "SelectWorkoutForm";
      this.Text = "Select Workout";
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.ListBox lisCategories;
    private System.Windows.Forms.ListBox lisWorkouts;
  }
}