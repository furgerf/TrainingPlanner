namespace TrainingPlanner.View.Forms
{
  partial class NewTrainingPlanForm
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
      this.txtTrainingPlanName = new System.Windows.Forms.TextBox();
      this.label1 = new System.Windows.Forms.Label();
      this.numTrainingWeeks = new System.Windows.Forms.NumericUpDown();
      this.label2 = new System.Windows.Forms.Label();
      this.label3 = new System.Windows.Forms.Label();
      this.txtPlanToImportWorkoutsFrom = new System.Windows.Forms.TextBox();
      this.butSelectWorkouts = new System.Windows.Forms.Button();
      this.butOk = new System.Windows.Forms.Button();
      this.butCancel = new System.Windows.Forms.Button();
      this.label4 = new System.Windows.Forms.Label();
      this.dtpStartOfTrainingPlan = new System.Windows.Forms.MonthCalendar();
      ((System.ComponentModel.ISupportInitialize)(this.numTrainingWeeks)).BeginInit();
      this.SuspendLayout();
      // 
      // txtTrainingPlanName
      // 
      this.txtTrainingPlanName.Location = new System.Drawing.Point(126, 12);
      this.txtTrainingPlanName.Name = "txtTrainingPlanName";
      this.txtTrainingPlanName.Size = new System.Drawing.Size(186, 20);
      this.txtTrainingPlanName.TabIndex = 0;
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(12, 15);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(38, 13);
      this.label1.TabIndex = 1;
      this.label1.Text = "Name:";
      // 
      // numTrainingWeeks
      // 
      this.numTrainingWeeks.Location = new System.Drawing.Point(126, 38);
      this.numTrainingWeeks.Maximum = new decimal(new int[] {
            50,
            0,
            0,
            0});
      this.numTrainingWeeks.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.numTrainingWeeks.Name = "numTrainingWeeks";
      this.numTrainingWeeks.Size = new System.Drawing.Size(186, 20);
      this.numTrainingWeeks.TabIndex = 2;
      this.numTrainingWeeks.Value = new decimal(new int[] {
            16,
            0,
            0,
            0});
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(12, 40);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(44, 13);
      this.label2.TabIndex = 3;
      this.label2.Text = "Weeks:";
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.Location = new System.Drawing.Point(12, 69);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(108, 13);
      this.label3.TabIndex = 4;
      this.label3.Text = "Import workouts from:";
      // 
      // txtPlanToImportWorkoutsFrom
      // 
      this.txtPlanToImportWorkoutsFrom.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.txtPlanToImportWorkoutsFrom.Enabled = false;
      this.txtPlanToImportWorkoutsFrom.Location = new System.Drawing.Point(126, 66);
      this.txtPlanToImportWorkoutsFrom.Name = "txtPlanToImportWorkoutsFrom";
      this.txtPlanToImportWorkoutsFrom.Size = new System.Drawing.Size(130, 20);
      this.txtPlanToImportWorkoutsFrom.TabIndex = 5;
      // 
      // butSelectWorkouts
      // 
      this.butSelectWorkouts.Location = new System.Drawing.Point(262, 64);
      this.butSelectWorkouts.Name = "butSelectWorkouts";
      this.butSelectWorkouts.Size = new System.Drawing.Size(50, 23);
      this.butSelectWorkouts.TabIndex = 6;
      this.butSelectWorkouts.Text = "Select";
      this.butSelectWorkouts.UseVisualStyleBackColor = true;
      this.butSelectWorkouts.Click += new System.EventHandler(this.butSelectWorkouts_Click);
      // 
      // butOk
      // 
      this.butOk.Location = new System.Drawing.Point(15, 275);
      this.butOk.Name = "butOk";
      this.butOk.Size = new System.Drawing.Size(75, 23);
      this.butOk.TabIndex = 7;
      this.butOk.Text = "OK";
      this.butOk.UseVisualStyleBackColor = true;
      this.butOk.Click += new System.EventHandler(this.butOk_Click);
      // 
      // butCancel
      // 
      this.butCancel.Location = new System.Drawing.Point(237, 274);
      this.butCancel.Name = "butCancel";
      this.butCancel.Size = new System.Drawing.Size(75, 23);
      this.butCancel.TabIndex = 8;
      this.butCancel.Text = "Cancel";
      this.butCancel.UseVisualStyleBackColor = true;
      this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
      // 
      // label4
      // 
      this.label4.AutoSize = true;
      this.label4.Location = new System.Drawing.Point(12, 98);
      this.label4.Name = "label4";
      this.label4.Size = new System.Drawing.Size(109, 13);
      this.label4.TabIndex = 9;
      this.label4.Text = "Start of Training Plan:";
      // 
      // dtpStartOfTrainingPlan
      // 
      this.dtpStartOfTrainingPlan.Location = new System.Drawing.Point(148, 99);
      this.dtpStartOfTrainingPlan.Name = "dtpStartOfTrainingPlan";
      this.dtpStartOfTrainingPlan.ShowToday = false;
      this.dtpStartOfTrainingPlan.TabIndex = 11;
      this.dtpStartOfTrainingPlan.DateChanged += new System.Windows.Forms.DateRangeEventHandler(this.dtpStartOfTrainingPlan_DateChanged);
      // 
      // NewTrainingPlanForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(324, 309);
      this.Controls.Add(this.dtpStartOfTrainingPlan);
      this.Controls.Add(this.label4);
      this.Controls.Add(this.butCancel);
      this.Controls.Add(this.butOk);
      this.Controls.Add(this.butSelectWorkouts);
      this.Controls.Add(this.txtPlanToImportWorkoutsFrom);
      this.Controls.Add(this.label3);
      this.Controls.Add(this.label2);
      this.Controls.Add(this.numTrainingWeeks);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.txtTrainingPlanName);
      this.Name = "NewTrainingPlanForm";
      this.Text = "Enter Data for new Training Plan";
      ((System.ComponentModel.ISupportInitialize)(this.numTrainingWeeks)).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.TextBox txtTrainingPlanName;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.NumericUpDown numTrainingWeeks;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.TextBox txtPlanToImportWorkoutsFrom;
    private System.Windows.Forms.Button butSelectWorkouts;
    private System.Windows.Forms.Button butOk;
    private System.Windows.Forms.Button butCancel;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.MonthCalendar dtpStartOfTrainingPlan;
  }
}