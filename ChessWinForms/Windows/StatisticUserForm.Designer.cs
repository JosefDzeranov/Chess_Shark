namespace Sachy_Obrazky.Windows
{
    partial class StatisticUserForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StatisticUserForm));
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.idAttempt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.idParty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.statusParty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.correctAnswers = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IncorrectAnswers = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Accuracy = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DatePassage = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PassageTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IdAndName_label = new System.Windows.Forms.Label();
            this.GetGraffic = new MaterialSkin.Controls.MaterialRaisedButton();
            this.exportExcelButton = new MaterialSkin.Controls.MaterialRaisedButton();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.idAttempt,
            this.idParty,
            this.statusParty,
            this.correctAnswers,
            this.IncorrectAnswers,
            this.Accuracy,
            this.DatePassage,
            this.PassageTime});
            this.dataGridView1.Location = new System.Drawing.Point(12, 71);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(996, 271);
            this.dataGridView1.TabIndex = 0;
            // 
            // idAttempt
            // 
            this.idAttempt.HeaderText = "id попытки";
            this.idAttempt.Name = "idAttempt";
            // 
            // idParty
            // 
            this.idParty.HeaderText = "id партии";
            this.idParty.Name = "idParty";
            // 
            // statusParty
            // 
            this.statusParty.HeaderText = "Статус партии";
            this.statusParty.Name = "statusParty";
            this.statusParty.Width = 150;
            // 
            // correctAnswers
            // 
            this.correctAnswers.HeaderText = "Правильные ответы";
            this.correctAnswers.Name = "correctAnswers";
            // 
            // IncorrectAnswers
            // 
            this.IncorrectAnswers.HeaderText = "Неправильные ответы";
            this.IncorrectAnswers.Name = "IncorrectAnswers";
            // 
            // Accuracy
            // 
            this.Accuracy.HeaderText = "Точность (%)";
            this.Accuracy.Name = "Accuracy";
            // 
            // DatePassage
            // 
            this.DatePassage.HeaderText = "Дата прохождения";
            this.DatePassage.Name = "DatePassage";
            this.DatePassage.Width = 200;
            // 
            // PassageTime
            // 
            this.PassageTime.HeaderText = "Время прохождения (сек)";
            this.PassageTime.Name = "PassageTime";
            // 
            // IdAndName_label
            // 
            this.IdAndName_label.Location = new System.Drawing.Point(13, 13);
            this.IdAndName_label.Name = "IdAndName_label";
            this.IdAndName_label.Size = new System.Drawing.Size(649, 55);
            this.IdAndName_label.TabIndex = 1;
            this.IdAndName_label.Text = "IdAndName_label";
            // 
            // GetGraffic
            // 
            this.GetGraffic.Depth = 0;
            this.GetGraffic.Location = new System.Drawing.Point(12, 359);
            this.GetGraffic.MouseState = MaterialSkin.MouseState.HOVER;
            this.GetGraffic.Name = "GetGraffic";
            this.GetGraffic.Primary = true;
            this.GetGraffic.Size = new System.Drawing.Size(264, 40);
            this.GetGraffic.TabIndex = 2;
            this.GetGraffic.Text = "Вывести график";
            this.GetGraffic.UseVisualStyleBackColor = true;
            this.GetGraffic.Click += new System.EventHandler(this.GetGraffic_Click);
            // 
            // exportExcelButton
            // 
            this.exportExcelButton.Depth = 0;
            this.exportExcelButton.Location = new System.Drawing.Point(298, 359);
            this.exportExcelButton.MouseState = MaterialSkin.MouseState.HOVER;
            this.exportExcelButton.Name = "exportExcelButton";
            this.exportExcelButton.Primary = true;
            this.exportExcelButton.Size = new System.Drawing.Size(264, 40);
            this.exportExcelButton.TabIndex = 2;
            this.exportExcelButton.Text = "Экспорт в Excel";
            this.exportExcelButton.UseVisualStyleBackColor = true;
            this.exportExcelButton.Click += new System.EventHandler(this.exportExcelButton_Click);
            // 
            // StatisticUserForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1013, 450);
            this.Controls.Add(this.exportExcelButton);
            this.Controls.Add(this.GetGraffic);
            this.Controls.Add(this.IdAndName_label);
            this.Controls.Add(this.dataGridView1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "StatisticUserForm";
            this.Text = "StatisticUserForm";
            this.Load += new System.EventHandler(this.StatisticUserForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label IdAndName_label;
        private MaterialSkin.Controls.MaterialRaisedButton GetGraffic;
        private System.Windows.Forms.DataGridViewTextBoxColumn idAttempt;
        private System.Windows.Forms.DataGridViewTextBoxColumn idParty;
        private System.Windows.Forms.DataGridViewTextBoxColumn statusParty;
        private System.Windows.Forms.DataGridViewTextBoxColumn correctAnswers;
        private System.Windows.Forms.DataGridViewTextBoxColumn IncorrectAnswers;
        private System.Windows.Forms.DataGridViewTextBoxColumn Accuracy;
        private System.Windows.Forms.DataGridViewTextBoxColumn DatePassage;
        private System.Windows.Forms.DataGridViewTextBoxColumn PassageTime;
        private MaterialSkin.Controls.MaterialRaisedButton exportExcelButton;
    }
}