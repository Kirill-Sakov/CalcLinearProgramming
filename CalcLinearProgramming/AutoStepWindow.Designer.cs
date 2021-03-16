namespace CalcLinearProgramming
{
    partial class AutoStepWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AutoStepWindow));
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.groupBoxCornerDot = new System.Windows.Forms.GroupBox();
            this._dataGridViewCornerDot = new System.Windows.Forms.DataGridView();
            this.labelAnswer = new System.Windows.Forms.Label();
            this.groupBoxProblem = new System.Windows.Forms.GroupBox();
            this._dataGridViewProblem = new System.Windows.Forms.DataGridView();
            this.helpProvider = new System.Windows.Forms.HelpProvider();
            this.tabControl.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBoxCornerDot.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._dataGridViewCornerDot)).BeginInit();
            this.groupBoxProblem.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._dataGridViewProblem)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabPage1);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(709, 510);
            this.tabControl.TabIndex = 2;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.groupBoxCornerDot);
            this.tabPage1.Controls.Add(this.labelAnswer);
            this.tabPage1.Controls.Add(this.groupBoxProblem);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(701, 484);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Выберите действие";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // groupBoxCornerDot
            // 
            this.groupBoxCornerDot.Controls.Add(this._dataGridViewCornerDot);
            this.groupBoxCornerDot.Location = new System.Drawing.Point(35, 383);
            this.groupBoxCornerDot.Name = "groupBoxCornerDot";
            this.groupBoxCornerDot.Size = new System.Drawing.Size(450, 80);
            this.groupBoxCornerDot.TabIndex = 5;
            this.groupBoxCornerDot.TabStop = false;
            this.groupBoxCornerDot.Text = "Угловая точка";
            // 
            // _dataGridViewCornerDot
            // 
            this._dataGridViewCornerDot.AllowUserToAddRows = false;
            this._dataGridViewCornerDot.AllowUserToDeleteRows = false;
            this._dataGridViewCornerDot.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this._dataGridViewCornerDot.Dock = System.Windows.Forms.DockStyle.Fill;
            this._dataGridViewCornerDot.Location = new System.Drawing.Point(3, 16);
            this._dataGridViewCornerDot.Name = "_dataGridViewCornerDot";
            this._dataGridViewCornerDot.ReadOnly = true;
            this._dataGridViewCornerDot.Size = new System.Drawing.Size(444, 61);
            this._dataGridViewCornerDot.TabIndex = 4;
            // 
            // labelAnswer
            // 
            this.labelAnswer.AutoSize = true;
            this.labelAnswer.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelAnswer.Location = new System.Drawing.Point(491, 399);
            this.labelAnswer.Name = "labelAnswer";
            this.labelAnswer.Size = new System.Drawing.Size(65, 20);
            this.labelAnswer.TabIndex = 3;
            this.labelAnswer.Text = "Ответ: ";
            // 
            // groupBoxProblem
            // 
            this.groupBoxProblem.Controls.Add(this._dataGridViewProblem);
            this.groupBoxProblem.Location = new System.Drawing.Point(32, 28);
            this.groupBoxProblem.Name = "groupBoxProblem";
            this.groupBoxProblem.Size = new System.Drawing.Size(631, 349);
            this.groupBoxProblem.TabIndex = 0;
            this.groupBoxProblem.TabStop = false;
            this.groupBoxProblem.Text = "Текущая задача";
            // 
            // _dataGridViewProblem
            // 
            this._dataGridViewProblem.AllowUserToAddRows = false;
            this._dataGridViewProblem.AllowUserToDeleteRows = false;
            this._dataGridViewProblem.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this._dataGridViewProblem.Dock = System.Windows.Forms.DockStyle.Fill;
            this._dataGridViewProblem.Location = new System.Drawing.Point(3, 16);
            this._dataGridViewProblem.MultiSelect = false;
            this._dataGridViewProblem.Name = "_dataGridViewProblem";
            this._dataGridViewProblem.ReadOnly = true;
            this._dataGridViewProblem.RowHeadersWidth = 60;
            this._dataGridViewProblem.Size = new System.Drawing.Size(625, 330);
            this._dataGridViewProblem.TabIndex = 0;
            // 
            // AutoStepWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(709, 510);
            this.Controls.Add(this.tabControl);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "AutoStepWindow";
            this.helpProvider.SetShowHelp(this, true);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "AutoStepWindow";
            this.Load += new System.EventHandler(this.AutoStepWindow_Load);
            this.tabControl.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.groupBoxCornerDot.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._dataGridViewCornerDot)).EndInit();
            this.groupBoxProblem.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._dataGridViewProblem)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.GroupBox groupBoxCornerDot;
        private System.Windows.Forms.DataGridView _dataGridViewCornerDot;
        private System.Windows.Forms.Label labelAnswer;
        private System.Windows.Forms.GroupBox groupBoxProblem;
        private System.Windows.Forms.DataGridView _dataGridViewProblem;
        private System.Windows.Forms.HelpProvider helpProvider;
    }
}