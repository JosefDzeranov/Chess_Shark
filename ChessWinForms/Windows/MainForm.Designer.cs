using System.Windows.Forms;

namespace Sachy_Obrazky.Windows
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.CoordinateTestButton = new MaterialSkin.Controls.MaterialRaisedButton();
            this.RestoringPositionButton = new MaterialSkin.Controls.MaterialRaisedButton();
            this.RestoreChessPartyButton = new MaterialSkin.Controls.MaterialRaisedButton();
            this.nameLabel = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.addPartyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.OverallStatisticsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statisticButton = new MaterialSkin.Controls.MaterialRaisedButton();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // CoordinateTestButton
            // 
            this.CoordinateTestButton.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.CoordinateTestButton.Depth = 0;
            this.CoordinateTestButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(250)));
            this.CoordinateTestButton.Location = new System.Drawing.Point(25, 242);
            this.CoordinateTestButton.Margin = new System.Windows.Forms.Padding(2);
            this.CoordinateTestButton.MouseState = MaterialSkin.MouseState.HOVER;
            this.CoordinateTestButton.Name = "CoordinateTestButton";
            this.CoordinateTestButton.Primary = true;
            this.CoordinateTestButton.Size = new System.Drawing.Size(250, 150);
            this.CoordinateTestButton.TabIndex = 0;
            this.CoordinateTestButton.Text = "ТРЕНИРОВКА КООРДИНАТ";
            this.CoordinateTestButton.UseVisualStyleBackColor = true;
            this.CoordinateTestButton.Click += new System.EventHandler(this.CoordinateTestButton_Click);
            // 
            // RestoringPositionButton
            // 
            this.RestoringPositionButton.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.RestoringPositionButton.Depth = 0;
            this.RestoringPositionButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(138)));
            this.RestoringPositionButton.Location = new System.Drawing.Point(279, 242);
            this.RestoringPositionButton.Margin = new System.Windows.Forms.Padding(2);
            this.RestoringPositionButton.MouseState = MaterialSkin.MouseState.HOVER;
            this.RestoringPositionButton.Name = "RestoringPositionButton";
            this.RestoringPositionButton.Primary = true;
            this.RestoringPositionButton.Size = new System.Drawing.Size(250, 150);
            this.RestoringPositionButton.TabIndex = 0;
            this.RestoringPositionButton.Text = "ВОССТАНОВЛЕНИЕ ПОЗИЦИИ";
            this.RestoringPositionButton.UseVisualStyleBackColor = true;
            this.RestoringPositionButton.Click += new System.EventHandler(this.RestoringPositionButton_Click);
            // 
            // RestoreChessPartyButton
            // 
            this.RestoreChessPartyButton.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.RestoreChessPartyButton.Depth = 0;
            this.RestoreChessPartyButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.RestoreChessPartyButton.Location = new System.Drawing.Point(533, 242);
            this.RestoreChessPartyButton.Margin = new System.Windows.Forms.Padding(2);
            this.RestoreChessPartyButton.MouseState = MaterialSkin.MouseState.HOVER;
            this.RestoreChessPartyButton.Name = "RestoreChessPartyButton";
            this.RestoreChessPartyButton.Primary = true;
            this.RestoreChessPartyButton.Size = new System.Drawing.Size(250, 150);
            this.RestoreChessPartyButton.TabIndex = 0;
            this.RestoreChessPartyButton.Text = "ВОССТАНОВИТЬ ПАРТИЮ (HARD)";
            this.RestoreChessPartyButton.UseVisualStyleBackColor = true;
            this.RestoreChessPartyButton.Click += new System.EventHandler(this.RestoreChessPartyButton_Click);
            // 
            // nameLabel
            // 
            this.nameLabel.AutoSize = true;
            this.nameLabel.Font = new System.Drawing.Font("Times New Roman", 15F, System.Drawing.FontStyle.Bold);
            this.nameLabel.Location = new System.Drawing.Point(9, 7);
            this.nameLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.nameLabel.Name = "nameLabel";
            this.nameLabel.Size = new System.Drawing.Size(103, 23);
            this.nameLabel.TabIndex = 1;
            this.nameLabel.Text = "nameLabel";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Dock = System.Windows.Forms.DockStyle.Right;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1});
            this.menuStrip1.Location = new System.Drawing.Point(780, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(34, 771);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addPartyToolStripMenuItem,
            this.OverallStatisticsToolStripMenuItem});
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(21, 19);
            this.toolStripMenuItem1.Text = "...";
            // 
            // addPartyToolStripMenuItem
            // 
            this.addPartyToolStripMenuItem.Name = "addPartyToolStripMenuItem";
            this.addPartyToolStripMenuItem.Size = new System.Drawing.Size(175, 22);
            this.addPartyToolStripMenuItem.Text = "Добавить партию";
            this.addPartyToolStripMenuItem.Click += new System.EventHandler(this.AddPartyToolStripMenuItem_Click);
            // 
            // OverallStatisticsToolStripMenuItem
            // 
            this.OverallStatisticsToolStripMenuItem.Name = "OverallStatisticsToolStripMenuItem";
            this.OverallStatisticsToolStripMenuItem.Size = new System.Drawing.Size(175, 22);
            this.OverallStatisticsToolStripMenuItem.Text = "Общая статистика";
            this.OverallStatisticsToolStripMenuItem.Click += new System.EventHandler(this.OverallStatisticsToolStripMenuItem_Click);
            // 
            // statisticButton
            // 
            this.statisticButton.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.statisticButton.Depth = 0;
            this.statisticButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(138)));
            this.statisticButton.Location = new System.Drawing.Point(279, 396);
            this.statisticButton.Margin = new System.Windows.Forms.Padding(2);
            this.statisticButton.MouseState = MaterialSkin.MouseState.HOVER;
            this.statisticButton.Name = "statisticButton";
            this.statisticButton.Primary = true;
            this.statisticButton.Size = new System.Drawing.Size(250, 150);
            this.statisticButton.TabIndex = 0;
            this.statisticButton.Text = "СТАТИСТИКА";
            this.statisticButton.UseVisualStyleBackColor = true;
            this.statisticButton.Click += new System.EventHandler(this.statisticButton_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(814, 771);
            this.Controls.Add(this.nameLabel);
            this.Controls.Add(this.RestoreChessPartyButton);
            this.Controls.Add(this.statisticButton);
            this.Controls.Add(this.RestoringPositionButton);
            this.Controls.Add(this.CoordinateTestButton);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "MainForm";
            this.Text = "Меню";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MaterialSkin.Controls.MaterialRaisedButton CoordinateTestButton;
        private MaterialSkin.Controls.MaterialRaisedButton RestoringPositionButton;
        private MaterialSkin.Controls.MaterialRaisedButton RestoreChessPartyButton;
        private Label nameLabel;
        private int successCount;
        private int errors;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem toolStripMenuItem1;
        private ToolStripMenuItem addPartyToolStripMenuItem;
        private MaterialSkin.Controls.MaterialRaisedButton statisticButton;
        private ToolStripMenuItem OverallStatisticsToolStripMenuItem;
    }
}