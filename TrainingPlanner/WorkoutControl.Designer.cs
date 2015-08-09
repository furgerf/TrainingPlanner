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
      this.labSelectWorkout = new System.Windows.Forms.Label();
      this.comWorkouts = new System.Windows.Forms.ComboBox();
      this.butRemove = new System.Windows.Forms.Button();
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
      this.txtDescription.Size = new System.Drawing.Size(132, 73);
      this.txtDescription.TabIndex = 2;
      // 
      // txtDistance
      // 
      this.txtDistance.Location = new System.Drawing.Point(72, 16);
      this.txtDistance.Name = "txtDistance";
      this.txtDistance.Size = new System.Drawing.Size(63, 20);
      this.txtDistance.TabIndex = 3;
      // 
      // labSelectWorkout
      // 
      this.labSelectWorkout.AutoSize = true;
      this.labSelectWorkout.Location = new System.Drawing.Point(3, 0);
      this.labSelectWorkout.Name = "labSelectWorkout";
      this.labSelectWorkout.Size = new System.Drawing.Size(78, 13);
      this.labSelectWorkout.TabIndex = 4;
      this.labSelectWorkout.Text = "Select workout";
      // 
      // comWorkouts
      // 
      this.comWorkouts.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.comWorkouts.Location = new System.Drawing.Point(3, 16);
      this.comWorkouts.Name = "comWorkouts";
      this.comWorkouts.Size = new System.Drawing.Size(132, 21);
      this.comWorkouts.TabIndex = 5;
      // 
      // butRemove
      // 
      this.butRemove.Location = new System.Drawing.Point(3, 121);
      this.butRemove.Name = "butRemove";
      this.butRemove.Size = new System.Drawing.Size(132, 24);
      this.butRemove.TabIndex = 6;
      this.butRemove.Text = "Remove";
      this.butRemove.UseVisualStyleBackColor = true;
      this.butRemove.Click += new System.EventHandler(this.butRemove_Click);
      // 
      // WorkoutControl
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.Controls.Add(this.butRemove);
      this.Controls.Add(this.comWorkouts);
      this.Controls.Add(this.labSelectWorkout);
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
    private System.Windows.Forms.Label labSelectWorkout;
    private System.Windows.Forms.ComboBox comWorkouts;
    private System.Windows.Forms.Button butRemove;
  }
}
