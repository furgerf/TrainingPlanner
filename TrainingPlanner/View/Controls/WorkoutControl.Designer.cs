namespace TrainingPlanner.View.Controls
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
      this.butRemove = new System.Windows.Forms.Button();
      this.labWorkoutName = new System.Windows.Forms.Label();
      this.labRemove = new System.Windows.Forms.Label();
      this.txtDescription = new System.Windows.Forms.TextBox();
      this.labSelectWorkout = new System.Windows.Forms.Label();
      this.SuspendLayout();
      // 
      // butRemove
      // 
      this.butRemove.Location = new System.Drawing.Point(0, 0);
      this.butRemove.Name = "butRemove";
      this.butRemove.Size = new System.Drawing.Size(143, 87);
      this.butRemove.TabIndex = 6;
      this.butRemove.UseVisualStyleBackColor = true;
      this.butRemove.Click += new System.EventHandler(this.RemoveWorkout);
      // 
      // labWorkoutName
      // 
      this.labWorkoutName.AutoSize = true;
      this.labWorkoutName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.labWorkoutName.Location = new System.Drawing.Point(2, 2);
      this.labWorkoutName.Name = "labWorkoutName";
      this.labWorkoutName.Size = new System.Drawing.Size(41, 13);
      this.labWorkoutName.TabIndex = 8;
      this.labWorkoutName.Text = "label1";
      this.labWorkoutName.Enter += new System.EventHandler(this.SetActiveControl);
      this.labWorkoutName.MouseClick += new System.Windows.Forms.MouseEventHandler(this.RemoveWorkout);
      // 
      // labRemove
      // 
      this.labRemove.AutoSize = true;
      this.labRemove.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.labRemove.ForeColor = System.Drawing.Color.Red;
      this.labRemove.Location = new System.Drawing.Point(125, 2);
      this.labRemove.Name = "labRemove";
      this.labRemove.Size = new System.Drawing.Size(15, 13);
      this.labRemove.TabIndex = 9;
      this.labRemove.Text = "X";
      this.labRemove.TextAlign = System.Drawing.ContentAlignment.TopRight;
      this.labRemove.MouseClick += new System.Windows.Forms.MouseEventHandler(this.RemoveWorkout);
      // 
      // txtDescription
      // 
      this.txtDescription.BorderStyle = System.Windows.Forms.BorderStyle.None;
      this.txtDescription.Cursor = System.Windows.Forms.Cursors.Default;
      this.txtDescription.Location = new System.Drawing.Point(2, 17);
      this.txtDescription.Multiline = true;
      this.txtDescription.Name = "txtDescription";
      this.txtDescription.Size = new System.Drawing.Size(138, 67);
      this.txtDescription.TabIndex = 11;
      this.txtDescription.MouseClick += new System.Windows.Forms.MouseEventHandler(this.RemoveWorkout);
      this.txtDescription.Enter += new System.EventHandler(this.SetActiveControl);
      // 
      // labSelectWorkout
      // 
      this.labSelectWorkout.AutoSize = true;
      this.labSelectWorkout.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
      this.labSelectWorkout.Location = new System.Drawing.Point(12, 37);
      this.labSelectWorkout.Name = "labSelectWorkout";
      this.labSelectWorkout.Size = new System.Drawing.Size(117, 13);
      this.labSelectWorkout.TabIndex = 12;
      this.labSelectWorkout.Text = "Click to select workout!";
      this.labSelectWorkout.MouseClick += new System.Windows.Forms.MouseEventHandler(this.ShowContextMenu);
      // 
      // WorkoutControl
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this.labSelectWorkout);
      this.Controls.Add(this.txtDescription);
      this.Controls.Add(this.labRemove);
      this.Controls.Add(this.labWorkoutName);
      this.Controls.Add(this.butRemove);
      this.Name = "WorkoutControl";
      this.Size = new System.Drawing.Size(144, 88);
      this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.ShowContextMenu);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Button butRemove;
    private System.Windows.Forms.Label labWorkoutName;
    private System.Windows.Forms.Label labRemove;
    private System.Windows.Forms.TextBox txtDescription;
    private System.Windows.Forms.Label labSelectWorkout;
  }
}
