namespace TrainingPlanner.View
{
  partial class ManageWorkoutCategoriesForm
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
      this.lisCategories = new System.Windows.Forms.ListView();
      this.colName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.colColor = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.butExit = new System.Windows.Forms.Button();
      this.butDelete = new System.Windows.Forms.Button();
      this.butEdit = new System.Windows.Forms.Button();
      this.butAdd = new System.Windows.Forms.Button();
      this.SuspendLayout();
      // 
      // lisCategories
      // 
      this.lisCategories.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colName,
            this.colColor});
      this.lisCategories.FullRowSelect = true;
      this.lisCategories.GridLines = true;
      this.lisCategories.HideSelection = false;
      this.lisCategories.Location = new System.Drawing.Point(12, 12);
      this.lisCategories.MultiSelect = false;
      this.lisCategories.Name = "lisCategories";
      this.lisCategories.Size = new System.Drawing.Size(306, 292);
      this.lisCategories.TabIndex = 5;
      this.lisCategories.TabStop = false;
      this.lisCategories.UseCompatibleStateImageBehavior = false;
      this.lisCategories.View = System.Windows.Forms.View.Details;
      // 
      // colName
      // 
      this.colName.Text = "Name";
      this.colName.Width = 202;
      // 
      // colColor
      // 
      this.colColor.Text = "Color";
      this.colColor.Width = 100;
      // 
      // butExit
      // 
      this.butExit.Location = new System.Drawing.Point(324, 281);
      this.butExit.Name = "butExit";
      this.butExit.Size = new System.Drawing.Size(75, 23);
      this.butExit.TabIndex = 9;
      this.butExit.Text = "Exit";
      this.butExit.UseVisualStyleBackColor = true;
      this.butExit.Click += new System.EventHandler(this.butExit_Click);
      // 
      // butDelete
      // 
      this.butDelete.Location = new System.Drawing.Point(324, 180);
      this.butDelete.Name = "butDelete";
      this.butDelete.Size = new System.Drawing.Size(75, 23);
      this.butDelete.TabIndex = 8;
      this.butDelete.Text = "Delete";
      this.butDelete.UseVisualStyleBackColor = true;
      this.butDelete.Click += new System.EventHandler(this.butDelete_Click);
      // 
      // butEdit
      // 
      this.butEdit.Location = new System.Drawing.Point(324, 90);
      this.butEdit.Name = "butEdit";
      this.butEdit.Size = new System.Drawing.Size(75, 23);
      this.butEdit.TabIndex = 7;
      this.butEdit.Text = "Edit";
      this.butEdit.UseVisualStyleBackColor = true;
      this.butEdit.Click += new System.EventHandler(this.butEdit_Click);
      // 
      // butAdd
      // 
      this.butAdd.Location = new System.Drawing.Point(324, 12);
      this.butAdd.Name = "butAdd";
      this.butAdd.Size = new System.Drawing.Size(75, 23);
      this.butAdd.TabIndex = 6;
      this.butAdd.Text = "Add";
      this.butAdd.UseVisualStyleBackColor = true;
      this.butAdd.Click += new System.EventHandler(this.butAdd_Click);
      // 
      // ManageWorkoutCategoriesForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(411, 316);
      this.Controls.Add(this.lisCategories);
      this.Controls.Add(this.butExit);
      this.Controls.Add(this.butDelete);
      this.Controls.Add(this.butEdit);
      this.Controls.Add(this.butAdd);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
      this.Name = "ManageWorkoutCategoriesForm";
      this.Text = "Manage Workout Categories";
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.ListView lisCategories;
    private System.Windows.Forms.ColumnHeader colName;
    private System.Windows.Forms.ColumnHeader colColor;
    private System.Windows.Forms.Button butExit;
    private System.Windows.Forms.Button butDelete;
    private System.Windows.Forms.Button butEdit;
    private System.Windows.Forms.Button butAdd;
  }
}