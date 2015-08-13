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
      this.butAddWorkout = new System.Windows.Forms.Button();
      this.panel1 = new System.Windows.Forms.Panel();
      this.vScrollBar1 = new System.Windows.Forms.VScrollBar();
      this.SuspendLayout();
      // 
      // butAddWorkout
      // 
      this.butAddWorkout.Location = new System.Drawing.Point(1400, 12);
      this.butAddWorkout.Name = "butAddWorkout";
      this.butAddWorkout.Size = new System.Drawing.Size(96, 23);
      this.butAddWorkout.TabIndex = 1;
      this.butAddWorkout.Text = "Add Workout";
      this.butAddWorkout.UseVisualStyleBackColor = true;
      this.butAddWorkout.Click += new System.EventHandler(this.butAddWorkout_Click);
      // 
      // panel1
      // 
      this.panel1.BackColor = System.Drawing.SystemColors.ControlDark;
      this.panel1.Location = new System.Drawing.Point(12, 12);
      this.panel1.Name = "panel1";
      this.panel1.Size = new System.Drawing.Size(1366, 584);
      this.panel1.TabIndex = 3;
      // 
      // vScrollBar1
      // 
      this.vScrollBar1.Location = new System.Drawing.Point(1381, 12);
      this.vScrollBar1.Name = "vScrollBar1";
      this.vScrollBar1.Size = new System.Drawing.Size(16, 80);
      this.vScrollBar1.TabIndex = 4;
      // 
      // MainForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(1500, 812);
      this.Controls.Add(this.vScrollBar1);
      this.Controls.Add(this.panel1);
      this.Controls.Add(this.butAddWorkout);
      this.Name = "MainForm";
      this.Text = "Training Planner";
      this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button butAddWorkout;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.VScrollBar vScrollBar1;


    }
}

