namespace TrainingPlanner
{
  partial class WorkoutControl
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

    #region Component Designer generated code

    /// <summary> 
    /// Required method for Designer support - do not modify 
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      this.labWorkoutName = new System.Windows.Forms.Label();
      this.txtDuration = new System.Windows.Forms.TextBox();
      this.txtDescription = new System.Windows.Forms.TextBox();
      this.txtDistance = new System.Windows.Forms.TextBox();
      this.SuspendLayout();
      // 
      // labWorkoutName
      // 
      this.labWorkoutName.AutoSize = true;
      this.labWorkoutName.Location = new System.Drawing.Point(3, 0);
      this.labWorkoutName.Name = "labWorkoutName";
      this.labWorkoutName.Size = new System.Drawing.Size(35, 13);
      this.labWorkoutName.TabIndex = 0;
      this.labWorkoutName.Text = "label1";
      // 
      // txtDuration
      // 
      this.txtDuration.Location = new System.Drawing.Point(3, 16);
      this.txtDuration.Name = "txtDuration";
      this.txtDuration.Size = new System.Drawing.Size(63, 20);
      this.txtDuration.TabIndex = 1;
      // 
      // txtDescription
      // 
      this.txtDescription.Location = new System.Drawing.Point(3, 42);
      this.txtDescription.Multiline = true;
      this.txtDescription.Name = "txtDescription";
      this.txtDescription.Size = new System.Drawing.Size(132, 103);
      this.txtDescription.TabIndex = 2;
      // 
      // txtDistance
      // 
      this.txtDistance.Location = new System.Drawing.Point(72, 16);
      this.txtDistance.Name = "txtDistance";
      this.txtDistance.Size = new System.Drawing.Size(63, 20);
      this.txtDistance.TabIndex = 3;
      // 
      // WorkoutControl
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.Controls.Add(this.txtDistance);
      this.Controls.Add(this.txtDescription);
      this.Controls.Add(this.txtDuration);
      this.Controls.Add(this.labWorkoutName);
      this.Name = "WorkoutControl";
      this.Size = new System.Drawing.Size(142, 148);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Label labWorkoutName;
    private System.Windows.Forms.TextBox txtDuration;
    private System.Windows.Forms.TextBox txtDescription;
    private System.Windows.Forms.TextBox txtDistance;
  }
}
