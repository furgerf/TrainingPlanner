namespace TrainingPlanner.View.Forms
{
  partial class PaceForm
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
      this.txtEasy = new System.Windows.Forms.MaskedTextBox();
      this.txtBase = new System.Windows.Forms.MaskedTextBox();
      this.txtMarathon = new System.Windows.Forms.MaskedTextBox();
      this.txtHalfmarathon = new System.Windows.Forms.MaskedTextBox();
      this.txtThreshold = new System.Windows.Forms.MaskedTextBox();
      this.txtTenk = new System.Windows.Forms.MaskedTextBox();
      this.txtFivek = new System.Windows.Forms.MaskedTextBox();
      this.label2 = new System.Windows.Forms.Label();
      this.label3 = new System.Windows.Forms.Label();
      this.label4 = new System.Windows.Forms.Label();
      this.label5 = new System.Windows.Forms.Label();
      this.label6 = new System.Windows.Forms.Label();
      this.label7 = new System.Windows.Forms.Label();
      this.butSaveChanges = new System.Windows.Forms.Button();
      this.butDiscardChanges = new System.Windows.Forms.Button();
      this.label8 = new System.Windows.Forms.Label();
      this.txtSteady = new System.Windows.Forms.MaskedTextBox();
      this.SuspendLayout();
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(12, 15);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(30, 13);
      this.label1.TabIndex = 0;
      this.label1.Text = "Easy";
      // 
      // txtEasy
      // 
      this.txtEasy.Location = new System.Drawing.Point(86, 12);
      this.txtEasy.Mask = "##:##";
      this.txtEasy.Name = "txtEasy";
      this.txtEasy.Size = new System.Drawing.Size(37, 20);
      this.txtEasy.TabIndex = 7;
      this.txtEasy.TextChanged += new System.EventHandler(this.PaceValueChanged);
      // 
      // txtBase
      // 
      this.txtBase.Location = new System.Drawing.Point(86, 38);
      this.txtBase.Mask = "##:##";
      this.txtBase.Name = "txtBase";
      this.txtBase.Size = new System.Drawing.Size(37, 20);
      this.txtBase.TabIndex = 8;
      this.txtBase.TextChanged += new System.EventHandler(this.PaceValueChanged);
      // 
      // txtMarathon
      // 
      this.txtMarathon.Location = new System.Drawing.Point(86, 90);
      this.txtMarathon.Mask = "##:##";
      this.txtMarathon.Name = "txtMarathon";
      this.txtMarathon.Size = new System.Drawing.Size(37, 20);
      this.txtMarathon.TabIndex = 9;
      this.txtMarathon.TextChanged += new System.EventHandler(this.PaceValueChanged);
      // 
      // txtHalfmarathon
      // 
      this.txtHalfmarathon.Location = new System.Drawing.Point(86, 142);
      this.txtHalfmarathon.Mask = "##:##";
      this.txtHalfmarathon.Name = "txtHalfmarathon";
      this.txtHalfmarathon.Size = new System.Drawing.Size(37, 20);
      this.txtHalfmarathon.TabIndex = 10;
      this.txtHalfmarathon.TextChanged += new System.EventHandler(this.PaceValueChanged);
      // 
      // txtThreshold
      // 
      this.txtThreshold.Location = new System.Drawing.Point(86, 116);
      this.txtThreshold.Mask = "##:##";
      this.txtThreshold.Name = "txtThreshold";
      this.txtThreshold.Size = new System.Drawing.Size(37, 20);
      this.txtThreshold.TabIndex = 11;
      this.txtThreshold.TextChanged += new System.EventHandler(this.PaceValueChanged);
      // 
      // txtTenk
      // 
      this.txtTenk.Location = new System.Drawing.Point(86, 168);
      this.txtTenk.Mask = "##:##";
      this.txtTenk.Name = "txtTenk";
      this.txtTenk.Size = new System.Drawing.Size(37, 20);
      this.txtTenk.TabIndex = 12;
      this.txtTenk.TextChanged += new System.EventHandler(this.PaceValueChanged);
      // 
      // txtFivek
      // 
      this.txtFivek.Location = new System.Drawing.Point(86, 194);
      this.txtFivek.Mask = "##:##";
      this.txtFivek.Name = "txtFivek";
      this.txtFivek.Size = new System.Drawing.Size(37, 20);
      this.txtFivek.TabIndex = 13;
      this.txtFivek.TextChanged += new System.EventHandler(this.PaceValueChanged);
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(12, 41);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(31, 13);
      this.label2.TabIndex = 14;
      this.label2.Text = "Base";
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.Location = new System.Drawing.Point(12, 93);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(52, 13);
      this.label3.TabIndex = 15;
      this.label3.Text = "Marathon";
      // 
      // label4
      // 
      this.label4.AutoSize = true;
      this.label4.Location = new System.Drawing.Point(12, 145);
      this.label4.Name = "label4";
      this.label4.Size = new System.Drawing.Size(70, 13);
      this.label4.TabIndex = 16;
      this.label4.Text = "Halfmarathon";
      // 
      // label5
      // 
      this.label5.AutoSize = true;
      this.label5.Location = new System.Drawing.Point(12, 119);
      this.label5.Name = "label5";
      this.label5.Size = new System.Drawing.Size(54, 13);
      this.label5.TabIndex = 17;
      this.label5.Text = "Threshold";
      // 
      // label6
      // 
      this.label6.AutoSize = true;
      this.label6.Location = new System.Drawing.Point(12, 171);
      this.label6.Name = "label6";
      this.label6.Size = new System.Drawing.Size(25, 13);
      this.label6.TabIndex = 18;
      this.label6.Text = "10k";
      // 
      // label7
      // 
      this.label7.AutoSize = true;
      this.label7.Location = new System.Drawing.Point(12, 197);
      this.label7.Name = "label7";
      this.label7.Size = new System.Drawing.Size(19, 13);
      this.label7.TabIndex = 19;
      this.label7.Text = "5k";
      // 
      // butSaveChanges
      // 
      this.butSaveChanges.Location = new System.Drawing.Point(12, 220);
      this.butSaveChanges.Name = "butSaveChanges";
      this.butSaveChanges.Size = new System.Drawing.Size(111, 23);
      this.butSaveChanges.TabIndex = 20;
      this.butSaveChanges.Text = "Save Changes";
      this.butSaveChanges.UseVisualStyleBackColor = true;
      this.butSaveChanges.Click += new System.EventHandler(this.butSaveChanges_Click);
      // 
      // butDiscardChanges
      // 
      this.butDiscardChanges.Location = new System.Drawing.Point(12, 249);
      this.butDiscardChanges.Name = "butDiscardChanges";
      this.butDiscardChanges.Size = new System.Drawing.Size(111, 23);
      this.butDiscardChanges.TabIndex = 21;
      this.butDiscardChanges.Text = "Discard Changes";
      this.butDiscardChanges.UseVisualStyleBackColor = true;
      this.butDiscardChanges.Click += new System.EventHandler(this.butDiscardChanges_Click);
      // 
      // label8
      // 
      this.label8.AutoSize = true;
      this.label8.Location = new System.Drawing.Point(12, 67);
      this.label8.Name = "label8";
      this.label8.Size = new System.Drawing.Size(40, 13);
      this.label8.TabIndex = 23;
      this.label8.Text = "Steady";
      // 
      // txtSteady
      // 
      this.txtSteady.Location = new System.Drawing.Point(86, 64);
      this.txtSteady.Mask = "##:##";
      this.txtSteady.Name = "txtSteady";
      this.txtSteady.Size = new System.Drawing.Size(37, 20);
      this.txtSteady.TabIndex = 22;
      // 
      // PaceForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(135, 282);
      this.Controls.Add(this.label8);
      this.Controls.Add(this.txtSteady);
      this.Controls.Add(this.butDiscardChanges);
      this.Controls.Add(this.butSaveChanges);
      this.Controls.Add(this.label7);
      this.Controls.Add(this.label6);
      this.Controls.Add(this.label5);
      this.Controls.Add(this.label4);
      this.Controls.Add(this.label3);
      this.Controls.Add(this.label2);
      this.Controls.Add(this.txtFivek);
      this.Controls.Add(this.txtTenk);
      this.Controls.Add(this.txtThreshold);
      this.Controls.Add(this.txtHalfmarathon);
      this.Controls.Add(this.txtMarathon);
      this.Controls.Add(this.txtBase);
      this.Controls.Add(this.txtEasy);
      this.Controls.Add(this.label1);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
      this.Name = "PaceForm";
      this.Text = "Paces";
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.MaskedTextBox txtEasy;
    private System.Windows.Forms.MaskedTextBox txtBase;
    private System.Windows.Forms.MaskedTextBox txtMarathon;
    private System.Windows.Forms.MaskedTextBox txtHalfmarathon;
    private System.Windows.Forms.MaskedTextBox txtThreshold;
    private System.Windows.Forms.MaskedTextBox txtTenk;
    private System.Windows.Forms.MaskedTextBox txtFivek;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.Label label5;
    private System.Windows.Forms.Label label6;
    private System.Windows.Forms.Label label7;
    private System.Windows.Forms.Button butSaveChanges;
    private System.Windows.Forms.Button butDiscardChanges;
    private System.Windows.Forms.Label label8;
    private System.Windows.Forms.MaskedTextBox txtSteady;
  }
}