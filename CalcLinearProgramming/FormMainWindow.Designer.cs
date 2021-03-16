namespace CalcLinearProgramming
{
    partial class FormMain
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuButton = new System.Windows.Forms.ToolStripMenuItem();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openFileToolStripMenuButton = new System.Windows.Forms.ToolStripMenuItem();
            this.saveFileToolStripMenuButton = new System.Windows.Forms.ToolStripMenuItem();
            this.f1ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpProvider = new System.Windows.Forms.HelpProvider();
            this._dataGridViewCornerDot = new System.Windows.Forms.DataGridView();
            this.checkBoxCornerDot = new System.Windows.Forms.CheckBox();
            this.groupBoxRestrictions = new System.Windows.Forms.GroupBox();
            this._dataGridViewRestrictions = new System.Windows.Forms.DataGridView();
            this.groupBoxTargetFunction = new System.Windows.Forms.GroupBox();
            this._dataGridViewTargetFunction = new System.Windows.Forms.DataGridView();
            this.groupBoxParameters = new System.Windows.Forms.GroupBox();
            this.buttonSolve = new System.Windows.Forms.Button();
            this.groupBoxMode = new System.Windows.Forms.GroupBox();
            this.radioButtonAutoMode = new System.Windows.Forms.RadioButton();
            this.radioButtonStepByStepMode = new System.Windows.Forms.RadioButton();
            this.groupBoxMinMax = new System.Windows.Forms.GroupBox();
            this.radioButtonMax = new System.Windows.Forms.RadioButton();
            this.radioButtonMin = new System.Windows.Forms.RadioButton();
            this.numericCountRestrictions = new System.Windows.Forms.NumericUpDown();
            this.labelCountRestrictions = new System.Windows.Forms.Label();
            this.numericCountVariables = new System.Windows.Forms.NumericUpDown();
            this.labelCountVariables = new System.Windows.Forms.Label();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.menuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._dataGridViewCornerDot)).BeginInit();
            this.groupBoxRestrictions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._dataGridViewRestrictions)).BeginInit();
            this.groupBoxTargetFunction.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._dataGridViewTargetFunction)).BeginInit();
            this.groupBoxParameters.SuspendLayout();
            this.groupBoxMode.SuspendLayout();
            this.groupBoxMinMax.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericCountRestrictions)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericCountVariables)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItem,
            this.fileToolStripMenuItem,
            this.f1ToolStripMenuItem,
            this.aboutToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(1144, 24);
            this.menuStrip.TabIndex = 0;
            this.menuStrip.Text = "menuStrip";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuButton});
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(63, 20);
            this.exitToolStripMenuItem.Text = "Выход...";
            // 
            // exitToolStripMenuButton
            // 
            this.exitToolStripMenuButton.Name = "exitToolStripMenuButton";
            this.exitToolStripMenuButton.Size = new System.Drawing.Size(109, 22);
            this.exitToolStripMenuButton.Text = "Выход";
            this.exitToolStripMenuButton.Click += new System.EventHandler(this.exitToolStripMenuButton_Click);
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openFileToolStripMenuButton,
            this.saveFileToolStripMenuButton});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(57, 20);
            this.fileToolStripMenuItem.Text = "Файл...";
            // 
            // openFileToolStripMenuButton
            // 
            this.openFileToolStripMenuButton.Name = "openFileToolStripMenuButton";
            this.openFileToolStripMenuButton.Size = new System.Drawing.Size(222, 22);
            this.openFileToolStripMenuButton.Text = "Открыть задачу из файла...";
            this.openFileToolStripMenuButton.Click += new System.EventHandler(this.openFileToolStripMenuButton_Click);
            // 
            // saveFileToolStripMenuButton
            // 
            this.saveFileToolStripMenuButton.Name = "saveFileToolStripMenuButton";
            this.saveFileToolStripMenuButton.Size = new System.Drawing.Size(222, 22);
            this.saveFileToolStripMenuButton.Text = "Сохранить задачу в файл...";
            this.saveFileToolStripMenuButton.Click += new System.EventHandler(this.saveFileToolStripMenuButton_Click);
            // 
            // f1ToolStripMenuItem
            // 
            this.f1ToolStripMenuItem.Name = "f1ToolStripMenuItem";
            this.f1ToolStripMenuItem.Size = new System.Drawing.Size(65, 20);
            this.f1ToolStripMenuItem.Text = "Справка";
            this.f1ToolStripMenuItem.Click += new System.EventHandler(this.f1ToolStripMenuItem_Click);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(94, 20);
            this.aboutToolStripMenuItem.Text = "О программе";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // _dataGridViewCornerDot
            // 
            this._dataGridViewCornerDot.AllowUserToAddRows = false;
            this._dataGridViewCornerDot.AllowUserToDeleteRows = false;
            this._dataGridViewCornerDot.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this._dataGridViewCornerDot.Location = new System.Drawing.Point(179, 479);
            this._dataGridViewCornerDot.Name = "_dataGridViewCornerDot";
            this._dataGridViewCornerDot.Size = new System.Drawing.Size(947, 60);
            this._dataGridViewCornerDot.TabIndex = 12;
            this._dataGridViewCornerDot.Visible = false;
            // 
            // checkBoxCornerDot
            // 
            this.checkBoxCornerDot.AutoSize = true;
            this.checkBoxCornerDot.Location = new System.Drawing.Point(179, 456);
            this.checkBoxCornerDot.Name = "checkBoxCornerDot";
            this.checkBoxCornerDot.Size = new System.Drawing.Size(193, 17);
            this.checkBoxCornerDot.TabIndex = 11;
            this.checkBoxCornerDot.Text = "Задать начальную угловую точку";
            this.checkBoxCornerDot.UseVisualStyleBackColor = true;
            this.checkBoxCornerDot.CheckedChanged += new System.EventHandler(this.checkBoxCornerDot_CheckedChanged);
            // 
            // groupBoxRestrictions
            // 
            this.groupBoxRestrictions.Controls.Add(this._dataGridViewRestrictions);
            this.groupBoxRestrictions.Location = new System.Drawing.Point(176, 128);
            this.groupBoxRestrictions.Name = "groupBoxRestrictions";
            this.groupBoxRestrictions.Size = new System.Drawing.Size(953, 322);
            this.groupBoxRestrictions.TabIndex = 10;
            this.groupBoxRestrictions.TabStop = false;
            this.groupBoxRestrictions.Text = "Ограничения";
            // 
            // _dataGridViewRestrictions
            // 
            this._dataGridViewRestrictions.AllowUserToAddRows = false;
            this._dataGridViewRestrictions.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this._dataGridViewRestrictions.Dock = System.Windows.Forms.DockStyle.Fill;
            this._dataGridViewRestrictions.Location = new System.Drawing.Point(3, 16);
            this._dataGridViewRestrictions.Name = "_dataGridViewRestrictions";
            this._dataGridViewRestrictions.Size = new System.Drawing.Size(947, 303);
            this._dataGridViewRestrictions.TabIndex = 0;
            // 
            // groupBoxTargetFunction
            // 
            this.groupBoxTargetFunction.Controls.Add(this._dataGridViewTargetFunction);
            this.groupBoxTargetFunction.Location = new System.Drawing.Point(176, 39);
            this.groupBoxTargetFunction.Name = "groupBoxTargetFunction";
            this.groupBoxTargetFunction.Size = new System.Drawing.Size(956, 83);
            this.groupBoxTargetFunction.TabIndex = 9;
            this.groupBoxTargetFunction.TabStop = false;
            this.groupBoxTargetFunction.Text = "Целевая функция";
            // 
            // _dataGridViewTargetFunction
            // 
            this._dataGridViewTargetFunction.AllowUserToAddRows = false;
            this._dataGridViewTargetFunction.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this._dataGridViewTargetFunction.Dock = System.Windows.Forms.DockStyle.Fill;
            this._dataGridViewTargetFunction.Location = new System.Drawing.Point(3, 16);
            this._dataGridViewTargetFunction.Name = "_dataGridViewTargetFunction";
            this._dataGridViewTargetFunction.Size = new System.Drawing.Size(950, 64);
            this._dataGridViewTargetFunction.TabIndex = 0;
            // 
            // groupBoxParameters
            // 
            this.groupBoxParameters.Controls.Add(this.buttonSolve);
            this.groupBoxParameters.Controls.Add(this.groupBoxMode);
            this.groupBoxParameters.Controls.Add(this.groupBoxMinMax);
            this.groupBoxParameters.Controls.Add(this.numericCountRestrictions);
            this.groupBoxParameters.Controls.Add(this.labelCountRestrictions);
            this.groupBoxParameters.Controls.Add(this.numericCountVariables);
            this.groupBoxParameters.Controls.Add(this.labelCountVariables);
            this.groupBoxParameters.Location = new System.Drawing.Point(12, 39);
            this.groupBoxParameters.Name = "groupBoxParameters";
            this.groupBoxParameters.Size = new System.Drawing.Size(158, 500);
            this.groupBoxParameters.TabIndex = 8;
            this.groupBoxParameters.TabStop = false;
            this.groupBoxParameters.Text = "Условия";
            // 
            // buttonSolve
            // 
            this.buttonSolve.Location = new System.Drawing.Point(6, 457);
            this.buttonSolve.Name = "buttonSolve";
            this.buttonSolve.Size = new System.Drawing.Size(146, 37);
            this.buttonSolve.TabIndex = 9;
            this.buttonSolve.Text = "Решить";
            this.buttonSolve.UseVisualStyleBackColor = true;
            this.buttonSolve.Click += new System.EventHandler(this.buttonSolve_Click);
            // 
            // groupBoxMode
            // 
            this.groupBoxMode.Controls.Add(this.radioButtonAutoMode);
            this.groupBoxMode.Controls.Add(this.radioButtonStepByStepMode);
            this.groupBoxMode.Location = new System.Drawing.Point(6, 200);
            this.groupBoxMode.Name = "groupBoxMode";
            this.groupBoxMode.Size = new System.Drawing.Size(146, 70);
            this.groupBoxMode.TabIndex = 8;
            this.groupBoxMode.TabStop = false;
            this.groupBoxMode.Text = "Режим";
            // 
            // radioButtonAutoMode
            // 
            this.radioButtonAutoMode.AutoSize = true;
            this.radioButtonAutoMode.Location = new System.Drawing.Point(8, 42);
            this.radioButtonAutoMode.Name = "radioButtonAutoMode";
            this.radioButtonAutoMode.Size = new System.Drawing.Size(86, 17);
            this.radioButtonAutoMode.TabIndex = 5;
            this.radioButtonAutoMode.TabStop = true;
            this.radioButtonAutoMode.Text = "Сразу ответ";
            this.radioButtonAutoMode.UseVisualStyleBackColor = true;
            // 
            // radioButtonStepByStepMode
            // 
            this.radioButtonStepByStepMode.AutoSize = true;
            this.radioButtonStepByStepMode.Location = new System.Drawing.Point(8, 19);
            this.radioButtonStepByStepMode.Name = "radioButtonStepByStepMode";
            this.radioButtonStepByStepMode.Size = new System.Drawing.Size(76, 17);
            this.radioButtonStepByStepMode.TabIndex = 4;
            this.radioButtonStepByStepMode.TabStop = true;
            this.radioButtonStepByStepMode.Text = "Пошагово";
            this.radioButtonStepByStepMode.UseVisualStyleBackColor = true;
            // 
            // groupBoxMinMax
            // 
            this.groupBoxMinMax.Controls.Add(this.radioButtonMax);
            this.groupBoxMinMax.Controls.Add(this.radioButtonMin);
            this.groupBoxMinMax.Location = new System.Drawing.Point(6, 145);
            this.groupBoxMinMax.Name = "groupBoxMinMax";
            this.groupBoxMinMax.Size = new System.Drawing.Size(146, 49);
            this.groupBoxMinMax.TabIndex = 5;
            this.groupBoxMinMax.TabStop = false;
            this.groupBoxMinMax.Text = "Задача на";
            // 
            // radioButtonMax
            // 
            this.radioButtonMax.AutoSize = true;
            this.radioButtonMax.Location = new System.Drawing.Point(67, 22);
            this.radioButtonMax.Name = "radioButtonMax";
            this.radioButtonMax.Size = new System.Drawing.Size(44, 17);
            this.radioButtonMax.TabIndex = 1;
            this.radioButtonMax.TabStop = true;
            this.radioButtonMax.Text = "max";
            this.radioButtonMax.UseVisualStyleBackColor = true;
            // 
            // radioButtonMin
            // 
            this.radioButtonMin.AutoSize = true;
            this.radioButtonMin.Location = new System.Drawing.Point(20, 22);
            this.radioButtonMin.Name = "radioButtonMin";
            this.radioButtonMin.Size = new System.Drawing.Size(41, 17);
            this.radioButtonMin.TabIndex = 0;
            this.radioButtonMin.TabStop = true;
            this.radioButtonMin.Text = "min";
            this.radioButtonMin.UseVisualStyleBackColor = true;
            // 
            // numericCountRestrictions
            // 
            this.numericCountRestrictions.Location = new System.Drawing.Point(6, 105);
            this.numericCountRestrictions.Maximum = new decimal(new int[] {
            16,
            0,
            0,
            0});
            this.numericCountRestrictions.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericCountRestrictions.Name = "numericCountRestrictions";
            this.numericCountRestrictions.Size = new System.Drawing.Size(146, 20);
            this.numericCountRestrictions.TabIndex = 4;
            this.numericCountRestrictions.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.numericCountRestrictions.ValueChanged += new System.EventHandler(this.numericCountRestrictions_ValueChanged);
            // 
            // labelCountRestrictions
            // 
            this.labelCountRestrictions.AutoSize = true;
            this.labelCountRestrictions.Location = new System.Drawing.Point(6, 89);
            this.labelCountRestrictions.Name = "labelCountRestrictions";
            this.labelCountRestrictions.Size = new System.Drawing.Size(106, 13);
            this.labelCountRestrictions.TabIndex = 3;
            this.labelCountRestrictions.Text = "Число ограничений";
            // 
            // numericCountVariables
            // 
            this.numericCountVariables.Location = new System.Drawing.Point(6, 53);
            this.numericCountVariables.Maximum = new decimal(new int[] {
            16,
            0,
            0,
            0});
            this.numericCountVariables.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericCountVariables.Name = "numericCountVariables";
            this.numericCountVariables.Size = new System.Drawing.Size(146, 20);
            this.numericCountVariables.TabIndex = 2;
            this.numericCountVariables.Value = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.numericCountVariables.ValueChanged += new System.EventHandler(this.numericCountVariables_ValueChanged);
            // 
            // labelCountVariables
            // 
            this.labelCountVariables.AutoSize = true;
            this.labelCountVariables.Location = new System.Drawing.Point(6, 37);
            this.labelCountVariables.Name = "labelCountVariables";
            this.labelCountVariables.Size = new System.Drawing.Size(132, 13);
            this.labelCountVariables.TabIndex = 1;
            this.labelCountVariables.Text = "Количество переменных";
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "openFileDialog";
            this.openFileDialog.Filter = "Текстовые документы|*.txt";
            this.openFileDialog.RestoreDirectory = true;
            // 
            // saveFileDialog
            // 
            this.saveFileDialog.Filter = "Текстовые документы|*.txt";
            // 
            // FormMain
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1144, 551);
            this.Controls.Add(this._dataGridViewCornerDot);
            this.Controls.Add(this.checkBoxCornerDot);
            this.Controls.Add(this.groupBoxRestrictions);
            this.Controls.Add(this.groupBoxTargetFunction);
            this.Controls.Add(this.groupBoxParameters);
            this.Controls.Add(this.menuStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip;
            this.Name = "FormMain";
            this.helpProvider.SetShowHelp(this, true);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CalcLinearProgramming v1";
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.FormMain_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.FormMain_DragEnter);
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this._dataGridViewCornerDot)).EndInit();
            this.groupBoxRestrictions.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._dataGridViewRestrictions)).EndInit();
            this.groupBoxTargetFunction.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._dataGridViewTargetFunction)).EndInit();
            this.groupBoxParameters.ResumeLayout(false);
            this.groupBoxParameters.PerformLayout();
            this.groupBoxMode.ResumeLayout(false);
            this.groupBoxMode.PerformLayout();
            this.groupBoxMinMax.ResumeLayout(false);
            this.groupBoxMinMax.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericCountRestrictions)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericCountVariables)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuButton;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openFileToolStripMenuButton;
        private System.Windows.Forms.ToolStripMenuItem saveFileToolStripMenuButton;
        private System.Windows.Forms.ToolStripMenuItem f1ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.HelpProvider helpProvider;
        private System.Windows.Forms.DataGridView _dataGridViewCornerDot;
        private System.Windows.Forms.CheckBox checkBoxCornerDot;
        private System.Windows.Forms.GroupBox groupBoxRestrictions;
        private System.Windows.Forms.DataGridView _dataGridViewRestrictions;
        private System.Windows.Forms.GroupBox groupBoxTargetFunction;
        private System.Windows.Forms.DataGridView _dataGridViewTargetFunction;
        private System.Windows.Forms.GroupBox groupBoxParameters;
        private System.Windows.Forms.Button buttonSolve;
        private System.Windows.Forms.GroupBox groupBoxMode;
        private System.Windows.Forms.RadioButton radioButtonAutoMode;
        private System.Windows.Forms.RadioButton radioButtonStepByStepMode;
        private System.Windows.Forms.GroupBox groupBoxMinMax;
        private System.Windows.Forms.RadioButton radioButtonMax;
        private System.Windows.Forms.RadioButton radioButtonMin;
        private System.Windows.Forms.NumericUpDown numericCountRestrictions;
        private System.Windows.Forms.Label labelCountRestrictions;
        private System.Windows.Forms.NumericUpDown numericCountVariables;
        private System.Windows.Forms.Label labelCountVariables;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
    }
}

