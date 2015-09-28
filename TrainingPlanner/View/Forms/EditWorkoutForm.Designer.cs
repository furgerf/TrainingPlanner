namespace TrainingPlanner.View.Forms
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
      this.labName = new System.Windows.Forms.Label();
      this.txtName = new System.Windows.Forms.TextBox();
      this.butAddStep = new System.Windows.Forms.Button();
      this.butRemoveStep = new System.Windows.Forms.Button();
      this.butSave = new System.Windows.Forms.Button();
      this.labCategory = new System.Windows.Forms.Label();
      this.comCategory = new System.Windows.Forms.ComboBox();
      this.butDelete = new System.Windows.Forms.Button();
      this.txtShortName = new System.Windows.Forms.TextBox();
      this.labShortName = new System.Windows.Forms.Label();
      this.SuspendLayout();
      // 
      // labName
      // 
      this.labName.AutoSize = true;
      this.labName.Location = new System.Drawing.Point(12, 15);
      this.labName.Name = "labName";
      this.labName.Size = new System.Drawing.Size(35, 13);
      this.labName.TabIndex = 0;
      this.labName.Text = "Name";
      // 
      // txtName
      // 
      this.txtName.Location = new System.Drawing.Point(53, 12);
      this.txtName.Name = "txtName";
      this.txtName.Size = new System.Drawing.Size(157, 20);
      this.txtName.TabIndex = 3;
      // 
      // butAddStep
      // 
      this.butAddStep.Location = new System.Drawing.Point(12, 38);
      this.butAddStep.Name = "butAddStep";
      this.butAddStep.Size = new System.Drawing.Size(82, 23);
      this.butAddStep.TabIndex = 97;
      this.butAddStep.Text = "Add Step";
      this.butAddStep.UseVisualStyleBackColor = true;
      this.butAddStep.Click += new System.EventHandler(this.butAddStep_Click);
      // 
      // butRemoveStep
      // 
      this.butRemoveStep.Location = new System.Drawing.Point(12, 67);
      this.butRemoveStep.Name = "butRemoveStep";
      this.butRemoveStep.Size = new System.Drawing.Size(82, 23);
      this.butRemoveStep.TabIndex = 98;
      this.butRemoveStep.Text = "Remove Step";
      this.butRemoveStep.UseVisualStyleBackColor = true;
      this.butRemoveStep.Click += new System.EventHandler(this.butRemoveStep_Click);
      // 
      // butSave
      // 
      this.butSave.Location = new System.Drawing.Point(588, 12);
      this.butSave.Name = "butSave";
      this.butSave.Size = new System.Drawing.Size(97, 23);
      this.butSave.TabIndex = 96;
      this.butSave.Text = "Save Workout";
      this.butSave.UseVisualStyleBackColor = true;
      this.butSave.Click += new System.EventHandler(this.butSave_Click);
      // 
      // labCategory
      // 
      this.labCategory.AutoSize = true;
      this.labCategory.Location = new System.Drawing.Point(406, 15);
      this.labCategory.Name = "labCategory";
      this.labCategory.Size = new System.Drawing.Size(49, 13);
      this.labCategory.TabIndex = 99;
      this.labCategory.Text = "Category";
      // 
      // comCategory
      // 
      this.comCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.comCategory.FormattingEnabled = true;
      this.comCategory.Location = new System.Drawing.Point(461, 12);
      this.comCategory.Name = "comCategory";
      this.comCategory.Size = new System.Drawing.Size(121, 21);
      this.comCategory.TabIndex = 100;
      // 
      // butDelete
      // 
      this.butDelete.Location = new System.Drawing.Point(691, 12);
      this.butDelete.Name = "butDelete";
      this.butDelete.Size = new System.Drawing.Size(97, 23);
      this.butDelete.TabIndex = 101;
      this.butDelete.Text = "Delete Workout";
      this.butDelete.UseVisualStyleBackColor = true;
      this.butDelete.Click += new System.EventHandler(this.butDelete_Click);
      // 
      // txtShortName
      // 
      this.txtShortName.Location = new System.Drawing.Point(285, 12);
      this.txtShortName.Name = "txtShortName";
      this.txtShortName.Size = new System.Drawing.Size(115, 20);
      this.txtShortName.TabIndex = 103;
      // 
      // labShortName
      // 
      this.labShortName.AutoSize = true;
      this.labShortName.Location = new System.Drawing.Point(216, 15);
      this.labShortName.Name = "labShortName";
      this.labShortName.Size = new System.Drawing.Size(63, 13);
      this.labShortName.TabIndex = 102;
      this.labShortName.Text = "Short Name";
      // 
      // EditWorkoutForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(800, 163);
      this.Controls.Add(this.txtShortName);
      this.Controls.Add(this.labShortName);
      this.Controls.Add(this.butDelete);
      this.Controls.Add(this.comCategory);
      this.Controls.Add(this.labCategory);
      this.Controls.Add(this.butSave);
      this.Controls.Add(this.butRemoveStep);
      this.Controls.Add(this.butAddStep);
      this.Controls.Add(this.txtName);
      this.Controls.Add(this.labName);
      this.Name = "EditWorkoutForm";
      this.Text = "EditWorkoutForm";
      this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.EditWorkoutForm_FormClosing);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Label labName;
    private System.Windows.Forms.TextBox txtName;
    private System.Windows.Forms.Button butAddStep;
    private System.Windows.Forms.Button butRemoveStep;
    private System.Windows.Forms.Button butSave;
    private System.Windows.Forms.Label labCategory;
    private System.Windows.Forms.ComboBox comCategory;
    private System.Windows.Forms.Button butDelete;
    private System.Windows.Forms.TextBox txtShortName;
    private System.Windows.Forms.Label labShortName;
  }
}