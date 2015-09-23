namespace TrainingPlanner.View
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
      this.foregroundPanel = new System.Windows.Forms.Panel();
      this.butPaces = new System.Windows.Forms.Button();
      this.SuspendLayout();
      // 
      // butAddWorkout
      // 
      this.butAddWorkout.Location = new System.Drawing.Point(1568, 12);
      this.butAddWorkout.Name = "butAddWorkout";
      this.butAddWorkout.Size = new System.Drawing.Size(96, 23);
      this.butAddWorkout.TabIndex = 1;
      this.butAddWorkout.Text = "Add Workout";
      this.butAddWorkout.UseVisualStyleBackColor = true;
      // 
      // foregroundPanel
      // 
      this.foregroundPanel.Location = new System.Drawing.Point(12, 12);
      this.foregroundPanel.Name = "foregroundPanel";
      this.foregroundPanel.Size = new System.Drawing.Size(1538, 100);
      this.foregroundPanel.TabIndex = 6;
      // 
      // butPaces
      // 
      this.butPaces.Location = new System.Drawing.Point(1568, 41);
      this.butPaces.Name = "butPaces";
      this.butPaces.Size = new System.Drawing.Size(96, 23);
      this.butPaces.TabIndex = 7;
      this.butPaces.Text = "Configure Paces";
      this.butPaces.UseVisualStyleBackColor = true;
      this.butPaces.Click += new System.EventHandler(this.butPaces_Click);
      // 
      // MainForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(1604, 812);
      this.Controls.Add(this.butPaces);
      this.Controls.Add(this.foregroundPanel);
      this.Controls.Add(this.butAddWorkout);
      this.Name = "MainForm";
      this.Text = "Training Planner";
      this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button butAddWorkout;
        private System.Windows.Forms.Panel foregroundPanel;
        private System.Windows.Forms.Button butPaces;


    }
}

