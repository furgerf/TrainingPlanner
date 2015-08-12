namespace TrainingPlanner
{
  partial class WorkoutStepControl
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
      this.label1 = new System.Windows.Forms.Label();
      this.label2 = new System.Windows.Forms.Label();
      this.label3 = new System.Windows.Forms.Label();
      this.txtDuration = new System.Windows.Forms.MaskedTextBox();
      this.txtRest = new System.Windows.Forms.MaskedTextBox();
      this.label4 = new System.Windows.Forms.Label();
      this.numRepetitions = new System.Windows.Forms.NumericUpDown();
      this.label5 = new System.Windows.Forms.Label();
      this.comName = new System.Windows.Forms.ComboBox();
      this.label6 = new System.Windows.Forms.Label();
      this.txtDistance = new System.Windows.Forms.TextBox();
      this.comPace = new System.Windows.Forms.ComboBox();
      ((System.ComponentModel.ISupportInitialize)(this.numRepetitions)).BeginInit();
      this.SuspendLayout();
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(3, 6);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(35, 13);
      this.label1.TabIndex = 0;
      this.label1.Text = "Name";
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(3, 32);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(47, 13);
      this.label2.TabIndex = 1;
      this.label2.Text = "Duration";
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.Location = new System.Drawing.Point(120, 32);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(29, 13);
      this.label3.TabIndex = 2;
      this.label3.Text = "Rest";
      // 
      // txtDuration
      // 
      this.txtDuration.Location = new System.Drawing.Point(62, 29);
      this.txtDuration.Mask = "##:##:##";
      this.txtDuration.Name = "txtDuration";
      this.txtDuration.Size = new System.Drawing.Size(52, 20);
      this.txtDuration.TabIndex = 6;
      this.txtDuration.TextChanged += new System.EventHandler(this.txtDuration_TextChanged);
      // 
      // txtRest
      // 
      this.txtRest.Location = new System.Drawing.Point(155, 29);
      this.txtRest.Mask = "##:##";
      this.txtRest.Name = "txtRest";
      this.txtRest.Size = new System.Drawing.Size(37, 20);
      this.txtRest.TabIndex = 7;
      // 
      // label4
      // 
      this.label4.AutoSize = true;
      this.label4.Location = new System.Drawing.Point(3, 58);
      this.label4.Name = "label4";
      this.label4.Size = new System.Drawing.Size(32, 13);
      this.label4.TabIndex = 8;
      this.label4.Text = "Pace";
      // 
      // numRepetitions
      // 
      this.numRepetitions.Location = new System.Drawing.Point(143, 81);
      this.numRepetitions.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
      this.numRepetitions.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.numRepetitions.Name = "numRepetitions";
      this.numRepetitions.Size = new System.Drawing.Size(49, 20);
      this.numRepetitions.TabIndex = 10;
      this.numRepetitions.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
      // 
      // label5
      // 
      this.label5.AutoSize = true;
      this.label5.Location = new System.Drawing.Point(105, 84);
      this.label5.Name = "label5";
      this.label5.Size = new System.Drawing.Size(32, 13);
      this.label5.TabIndex = 11;
      this.label5.Text = "Reps";
      // 
      // comName
      // 
      this.comName.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
      this.comName.FormattingEnabled = true;
      this.comName.Location = new System.Drawing.Point(62, 2);
      this.comName.Name = "comName";
      this.comName.Size = new System.Drawing.Size(130, 21);
      this.comName.TabIndex = 12;
      // 
      // label6
      // 
      this.label6.AutoSize = true;
      this.label6.Location = new System.Drawing.Point(3, 83);
      this.label6.Name = "label6";
      this.label6.Size = new System.Drawing.Size(49, 13);
      this.label6.TabIndex = 13;
      this.label6.Text = "Distance";
      // 
      // txtDistance
      // 
      this.txtDistance.Location = new System.Drawing.Point(62, 81);
      this.txtDistance.Name = "txtDistance";
      this.txtDistance.Size = new System.Drawing.Size(37, 20);
      this.txtDistance.TabIndex = 14;
      this.txtDistance.TextChanged += new System.EventHandler(this.txtDistance_TextChanged);
      // 
      // comPace
      // 
      this.comPace.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.comPace.FormattingEnabled = true;
      this.comPace.Location = new System.Drawing.Point(62, 55);
      this.comPace.Name = "comPace";
      this.comPace.Size = new System.Drawing.Size(130, 21);
      this.comPace.TabIndex = 15;
      this.comPace.SelectedIndexChanged += new System.EventHandler(this.comPace_SelectedIndexChanged);
      // 
      // WorkoutStepControl
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.Controls.Add(this.comPace);
      this.Controls.Add(this.txtDistance);
      this.Controls.Add(this.label6);
      this.Controls.Add(this.comName);
      this.Controls.Add(this.label5);
      this.Controls.Add(this.numRepetitions);
      this.Controls.Add(this.label4);
      this.Controls.Add(this.txtRest);
      this.Controls.Add(this.txtDuration);
      this.Controls.Add(this.label3);
      this.Controls.Add(this.label2);
      this.Controls.Add(this.label1);
      this.Name = "WorkoutStepControl";
      this.Size = new System.Drawing.Size(200, 110);
      ((System.ComponentModel.ISupportInitialize)(this.numRepetitions)).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.MaskedTextBox txtDuration;
    private System.Windows.Forms.MaskedTextBox txtRest;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.NumericUpDown numRepetitions;
    private System.Windows.Forms.Label label5;
    private System.Windows.Forms.ComboBox comName;
    private System.Windows.Forms.Label label6;
    private System.Windows.Forms.TextBox txtDistance;
    private System.Windows.Forms.ComboBox comPace;
  }
}
