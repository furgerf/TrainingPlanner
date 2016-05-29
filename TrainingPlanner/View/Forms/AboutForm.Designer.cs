namespace TrainingPlanner.View.Forms
{
  partial class AboutForm
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
      this.txtAbout = new System.Windows.Forms.RichTextBox();
      this.SuspendLayout();
      // 
      // txtAbout
      // 
      this.txtAbout.BorderStyle = System.Windows.Forms.BorderStyle.None;
      this.txtAbout.Location = new System.Drawing.Point(12, 12);
      this.txtAbout.Name = "txtAbout";
      this.txtAbout.ReadOnly = true;
      this.txtAbout.Size = new System.Drawing.Size(248, 167);
      this.txtAbout.TabIndex = 2;
      this.txtAbout.TabStop = false;
      this.txtAbout.Text = "";
      // 
      // AboutForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(272, 191);
      this.Controls.Add(this.txtAbout);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
      this.Name = "AboutForm";
      this.Text = "About";
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.RichTextBox txtAbout;

  }
}