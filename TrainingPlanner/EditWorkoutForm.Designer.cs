namespace TrainingPlanner
{
  partial class EditWorkoutForm
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
      this.label1 = new System.Windows.Forms.Label();
      this.txtName = new System.Windows.Forms.TextBox();
      this.butAddStep = new System.Windows.Forms.Button();
      this.butRemoveStep = new System.Windows.Forms.Button();
      this.butSave = new System.Windows.Forms.Button();
      this.SuspendLayout();
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(12, 15);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(35, 13);
      this.label1.TabIndex = 0;
      this.label1.Text = "Name";
      // 
      // txtName
      // 
      this.txtName.Location = new System.Drawing.Point(56, 12);
      this.txtName.Name = "txtName";
      this.txtName.Size = new System.Drawing.Size(211, 20);
      this.txtName.TabIndex = 3;
      // 
      // butAddStep
      // 
      this.butAddStep.Location = new System.Drawing.Point(15, 38);
      this.butAddStep.Name = "butAddStep";
      this.butAddStep.Size = new System.Drawing.Size(82, 23);
      this.butAddStep.TabIndex = 97;
      this.butAddStep.Text = "Add Step";
      this.butAddStep.UseVisualStyleBackColor = true;
      this.butAddStep.Click += new System.EventHandler(this.butAddStep_Click);
      // 
      // butRemoveStep
      // 
      this.butRemoveStep.Location = new System.Drawing.Point(15, 67);
      this.butRemoveStep.Name = "butRemoveStep";
      this.butRemoveStep.Size = new System.Drawing.Size(82, 23);
      this.butRemoveStep.TabIndex = 98;
      this.butRemoveStep.Text = "Remove Step";
      this.butRemoveStep.UseVisualStyleBackColor = true;
      this.butRemoveStep.Click += new System.EventHandler(this.butRemoveStep_Click);
      // 
      // butSave
      // 
      this.butSave.Location = new System.Drawing.Point(273, 9);
      this.butSave.Name = "butSave";
      this.butSave.Size = new System.Drawing.Size(97, 23);
      this.butSave.TabIndex = 96;
      this.butSave.Text = "Save Workout";
      this.butSave.UseVisualStyleBackColor = true;
      this.butSave.Click += new System.EventHandler(this.butSave_Click);
      // 
      // EditWorkoutForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(1573, 368);
      this.Controls.Add(this.butSave);
      this.Controls.Add(this.butRemoveStep);
      this.Controls.Add(this.butAddStep);
      this.Controls.Add(this.txtName);
      this.Controls.Add(this.label1);
      this.Name = "EditWorkoutForm";
      this.Text = "EditWorkoutForm";
      this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.EditWorkoutForm_FormClosing);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.TextBox txtName;
    private System.Windows.Forms.Button butAddStep;
    private System.Windows.Forms.Button butRemoveStep;
    private System.Windows.Forms.Button butSave;
  }
}