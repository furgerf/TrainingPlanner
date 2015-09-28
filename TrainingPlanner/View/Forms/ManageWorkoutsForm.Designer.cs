namespace TrainingPlanner.View.Forms
{
  partial class ManageWorkoutsForm
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
      this.lisWorkouts = new System.Windows.Forms.ListView();
      this.colName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.colCategory = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.colDistance = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.colDuration = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.colUsage = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.butExit = new System.Windows.Forms.Button();
      this.butDelete = new System.Windows.Forms.Button();
      this.butEdit = new System.Windows.Forms.Button();
      this.butAdd = new System.Windows.Forms.Button();
      this.SuspendLayout();
      // 
      // lisWorkouts
      // 
      this.lisWorkouts.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colName,
            this.colCategory,
            this.colDistance,
            this.colDuration,
            this.colUsage});
      this.lisWorkouts.FullRowSelect = true;
      this.lisWorkouts.GridLines = true;
      this.lisWorkouts.HideSelection = false;
      this.lisWorkouts.Location = new System.Drawing.Point(12, 12);
      this.lisWorkouts.MultiSelect = false;
      this.lisWorkouts.Name = "lisWorkouts";
      this.lisWorkouts.Size = new System.Drawing.Size(422, 292);
      this.lisWorkouts.Sorting = System.Windows.Forms.SortOrder.Ascending;
      this.lisWorkouts.TabIndex = 5;
      this.lisWorkouts.TabStop = false;
      this.lisWorkouts.UseCompatibleStateImageBehavior = false;
      this.lisWorkouts.View = System.Windows.Forms.View.Details;
      this.lisWorkouts.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.lisWorkouts_ColumnClick);
      // 
      // colName
      // 
      this.colName.Text = "Name";
      this.colName.Width = 160;
      // 
      // colCategory
      // 
      this.colCategory.Text = "Category";
      this.colCategory.Width = 80;
      // 
      // colDistance
      // 
      this.colDistance.Text = "Distance";
      this.colDistance.Width = 55;
      // 
      // colDuration
      // 
      this.colDuration.Text = "Duration";
      this.colDuration.Width = 55;
      // 
      // colUsage
      // 
      this.colUsage.Text = "Usages";
      this.colUsage.Width = 50;
      // 
      // butExit
      // 
      this.butExit.Location = new System.Drawing.Point(440, 281);
      this.butExit.Name = "butExit";
      this.butExit.Size = new System.Drawing.Size(75, 23);
      this.butExit.TabIndex = 9;
      this.butExit.Text = "Exit";
      this.butExit.UseVisualStyleBackColor = true;
      this.butExit.Click += new System.EventHandler(this.butExit_Click);
      // 
      // butDelete
      // 
      this.butDelete.Location = new System.Drawing.Point(440, 180);
      this.butDelete.Name = "butDelete";
      this.butDelete.Size = new System.Drawing.Size(75, 23);
      this.butDelete.TabIndex = 8;
      this.butDelete.Text = "Delete";
      this.butDelete.UseVisualStyleBackColor = true;
      this.butDelete.Click += new System.EventHandler(this.butDelete_Click);
      // 
      // butEdit
      // 
      this.butEdit.Location = new System.Drawing.Point(440, 90);
      this.butEdit.Name = "butEdit";
      this.butEdit.Size = new System.Drawing.Size(75, 23);
      this.butEdit.TabIndex = 7;
      this.butEdit.Text = "Edit";
      this.butEdit.UseVisualStyleBackColor = true;
      this.butEdit.Click += new System.EventHandler(this.butEdit_Click);
      // 
      // butAdd
      // 
      this.butAdd.Location = new System.Drawing.Point(440, 12);
      this.butAdd.Name = "butAdd";
      this.butAdd.Size = new System.Drawing.Size(75, 23);
      this.butAdd.TabIndex = 6;
      this.butAdd.Text = "Add";
      this.butAdd.UseVisualStyleBackColor = true;
      this.butAdd.Click += new System.EventHandler(this.butAdd_Click);
      // 
      // ManageWorkoutsForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(528, 316);
      this.Controls.Add(this.lisWorkouts);
      this.Controls.Add(this.butExit);
      this.Controls.Add(this.butDelete);
      this.Controls.Add(this.butEdit);
      this.Controls.Add(this.butAdd);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
      this.Name = "ManageWorkoutsForm";
      this.Text = "Manage Workouts";
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.ListView lisWorkouts;
    private System.Windows.Forms.ColumnHeader colName;
    private System.Windows.Forms.ColumnHeader colCategory;
    private System.Windows.Forms.Button butExit;
    private System.Windows.Forms.Button butDelete;
    private System.Windows.Forms.Button butEdit;
    private System.Windows.Forms.Button butAdd;
    private System.Windows.Forms.ColumnHeader colDuration;
    private System.Windows.Forms.ColumnHeader colUsage;
    private System.Windows.Forms.ColumnHeader colDistance;
  }
}