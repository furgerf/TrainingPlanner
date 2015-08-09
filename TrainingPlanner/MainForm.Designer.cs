namespace TrainingPlanner
{
    partial class MainForm
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
      this.weekControl1 = new TrainingPlanner.WeekControl();
      this.SuspendLayout();
      // 
      // weekControl1
      // 
      this.weekControl1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
      this.weekControl1.Location = new System.Drawing.Point(12, 12);
      this.weekControl1.Name = "weekControl1";
      this.weekControl1.Size = new System.Drawing.Size(1307, 338);
      this.weekControl1.TabIndex = 0;
      this.weekControl1.Workouts = null;
      // 
      // MainForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(1387, 487);
      this.Controls.Add(this.weekControl1);
      this.Name = "MainForm";
      this.Text = "Training Planner";
      this.ResumeLayout(false);

        }

        #endregion

        private WeekControl weekControl1;

    }
}

