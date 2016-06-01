namespace TrainingPlanner.View.Forms
{
  partial class EditWorkoutCategoryForm
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
      this.txtName = new System.Windows.Forms.TextBox();
      this.colPicker = new System.Windows.Forms.ColorDialog();
      this.comColorNames = new System.Windows.Forms.ComboBox();
      this.butCancel = new System.Windows.Forms.Button();
      this.labColor = new System.Windows.Forms.Label();
      this.label4 = new System.Windows.Forms.Label();
      this.butOk = new System.Windows.Forms.Button();
      this.label1 = new System.Windows.Forms.Label();
      this.SuspendLayout();
      // 
      // txtName
      // 
      this.txtName.Location = new System.Drawing.Point(77, 12);
      this.txtName.Name = "txtName";
      this.txtName.Size = new System.Drawing.Size(131, 20);
      this.txtName.TabIndex = 9;
      // 
      // colPicker
      // 
      this.colPicker.SolidColorOnly = true;
      // 
      // comColorNames
      // 
      this.comColorNames.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.comColorNames.FormattingEnabled = true;
      this.comColorNames.Location = new System.Drawing.Point(77, 35);
      this.comColorNames.Name = "comColorNames";
      this.comColorNames.Size = new System.Drawing.Size(83, 21);
      this.comColorNames.TabIndex = 14;
      this.comColorNames.SelectedValueChanged += new System.EventHandler(this.comColorNames_SelectedValueChanged);
      // 
      // butCancel
      // 
      this.butCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.butCancel.Location = new System.Drawing.Point(133, 66);
      this.butCancel.Name = "butCancel";
      this.butCancel.Size = new System.Drawing.Size(75, 23);
      this.butCancel.TabIndex = 17;
      this.butCancel.Text = "Cancel";
      this.butCancel.UseVisualStyleBackColor = true;
      this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
      // 
      // labColor
      // 
      this.labColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.labColor.Location = new System.Drawing.Point(166, 35);
      this.labColor.Name = "labColor";
      this.labColor.Size = new System.Drawing.Size(42, 20);
      this.labColor.TabIndex = 19;
      this.labColor.Click += new System.EventHandler(this.labColor_Click);
      // 
      // label4
      // 
      this.label4.AutoSize = true;
      this.label4.Location = new System.Drawing.Point(13, 37);
      this.label4.Name = "label4";
      this.label4.Size = new System.Drawing.Size(34, 13);
      this.label4.TabIndex = 18;
      this.label4.Text = "Color:";
      // 
      // butOk
      // 
      this.butOk.Location = new System.Drawing.Point(16, 66);
      this.butOk.Name = "butOk";
      this.butOk.Size = new System.Drawing.Size(75, 23);
      this.butOk.TabIndex = 16;
      this.butOk.Text = "OK";
      this.butOk.UseVisualStyleBackColor = true;
      this.butOk.Click += new System.EventHandler(this.butOk_Click);
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(13, 15);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(38, 13);
      this.label1.TabIndex = 11;
      this.label1.Text = "Name:";
      // 
      // EditWorkoutCategoryForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.CancelButton = this.butCancel;
      this.ClientSize = new System.Drawing.Size(225, 103);
      this.Controls.Add(this.txtName);
      this.Controls.Add(this.comColorNames);
      this.Controls.Add(this.butCancel);
      this.Controls.Add(this.labColor);
      this.Controls.Add(this.label4);
      this.Controls.Add(this.butOk);
      this.Controls.Add(this.label1);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
      this.Name = "EditWorkoutCategoryForm";
      this.Text = "Add Workout Category";
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.TextBox txtName;
    private System.Windows.Forms.ColorDialog colPicker;
    private System.Windows.Forms.ComboBox comColorNames;
    private System.Windows.Forms.Button butCancel;
    private System.Windows.Forms.Label labColor;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.Button butOk;
    private System.Windows.Forms.Label label1;
  }
}