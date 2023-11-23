using System;
using System.Drawing;
using System.Windows.Forms;

namespace Sachy_Obrazky.Windows
{
    partial class CoordinateTestForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CoordinateTestForm));
            this.backToMenuButton = new MaterialSkin.Controls.MaterialRaisedButton();
            this.cellToRestoreLabel = new System.Windows.Forms.Label();
            this.playButtonForWhite = new MaterialSkin.Controls.MaterialRaisedButton();
            this.playButtonForBlack = new MaterialSkin.Controls.MaterialRaisedButton();
            this.resultLabel = new System.Windows.Forms.Label();
            this.nameLabel = new System.Windows.Forms.Label();
            this.errorsLabel = new System.Windows.Forms.Label();
            this.timerLabel = new System.Windows.Forms.Label();
            this.successLabel = new System.Windows.Forms.Label();
            this.groupBox = new System.Windows.Forms.GroupBox();
            this.accuracyBarPanel = new System.Windows.Forms.Panel();
            this.groupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // backToMenuButton
            // 
            this.backToMenuButton.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.backToMenuButton.Depth = 0;
            this.backToMenuButton.Location = new System.Drawing.Point(12, 570);
            this.backToMenuButton.MouseState = MaterialSkin.MouseState.HOVER;
            this.backToMenuButton.Name = "backToMenuButton";
            this.backToMenuButton.Primary = true;
            this.backToMenuButton.Size = new System.Drawing.Size(93, 68);
            this.backToMenuButton.TabIndex = 0;
            this.backToMenuButton.Text = "Вернуться в меню";
            this.backToMenuButton.Click += new System.EventHandler(this.backToMenuButton_Click);
            // 
            // cellToRestoreLabel
            // 
            this.cellToRestoreLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cellToRestoreLabel.BackColor = System.Drawing.Color.Transparent;
            this.cellToRestoreLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.cellToRestoreLabel.Location = new System.Drawing.Point(290, 0);
            this.cellToRestoreLabel.Name = "cellToRestoreLabel";
            this.cellToRestoreLabel.Size = new System.Drawing.Size(200, 100);
            this.cellToRestoreLabel.TabIndex = 7;
            this.cellToRestoreLabel.Text = "cellToRestoreLabel";
            this.cellToRestoreLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // playButtonForWhite
            // 
            this.playButtonForWhite.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.playButtonForWhite.Depth = 0;
            this.playButtonForWhite.Location = new System.Drawing.Point(12, 100);
            this.playButtonForWhite.MouseState = MaterialSkin.MouseState.HOVER;
            this.playButtonForWhite.Name = "playButtonForWhite";
            this.playButtonForWhite.Primary = true;
            this.playButtonForWhite.Size = new System.Drawing.Size(93, 44);
            this.playButtonForWhite.TabIndex = 5;
            this.playButtonForWhite.Text = "Начать за белых";
            this.playButtonForWhite.Click += new System.EventHandler(this.playButtonForWhite_Click);
            // 
            // playButtonForBlack
            // 
            this.playButtonForBlack.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.playButtonForBlack.Depth = 0;
            this.playButtonForBlack.Location = new System.Drawing.Point(12, 150);
            this.playButtonForBlack.MouseState = MaterialSkin.MouseState.HOVER;
            this.playButtonForBlack.Name = "playButtonForBlack";
            this.playButtonForBlack.Primary = true;
            this.playButtonForBlack.Size = new System.Drawing.Size(93, 47);
            this.playButtonForBlack.TabIndex = 6;
            this.playButtonForBlack.Text = "Начать за черных";
            this.playButtonForBlack.Click += new System.EventHandler(this.playButtonForBlack_Click);
            // 
            // resultLabel
            // 
            this.resultLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.resultLabel.Location = new System.Drawing.Point(31, 0);
            this.resultLabel.Name = "resultLabel";
            this.resultLabel.Size = new System.Drawing.Size(269, 50);
            this.resultLabel.TabIndex = 1;
            this.resultLabel.Text = "Правильно:\n0";
            // 
            // nameLabel
            // 
            this.nameLabel.Location = new System.Drawing.Point(0, 0);
            this.nameLabel.Name = "nameLabel";
            this.nameLabel.Size = new System.Drawing.Size(100, 23);
            this.nameLabel.TabIndex = 0;
            // 
            // errorsLabel
            // 
            this.errorsLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.errorsLabel.Location = new System.Drawing.Point(31, 50);
            this.errorsLabel.Name = "errorsLabel";
            this.errorsLabel.Size = new System.Drawing.Size(269, 50);
            this.errorsLabel.TabIndex = 2;
            this.errorsLabel.Text = "Ошибок:\n0";
            // 
            // timerLabel
            // 
            this.timerLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.timerLabel.Location = new System.Drawing.Point(31, 100);
            this.timerLabel.Name = "timerLabel";
            this.timerLabel.Size = new System.Drawing.Size(269, 50);
            this.timerLabel.TabIndex = 3;
            this.timerLabel.Text = "60 Секунд";
            // 
            // successLabel
            // 
            this.successLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.successLabel.ForeColor = System.Drawing.Color.Green;
            this.successLabel.Location = new System.Drawing.Point(31, 150);
            this.successLabel.Name = "successLabel";
            this.successLabel.Size = new System.Drawing.Size(300, 50);
            this.successLabel.TabIndex = 0;
            // 
            // groupBox
            // 
            this.groupBox.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.groupBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.groupBox.Controls.Add(this.resultLabel);
            this.groupBox.Controls.Add(this.accuracyBarPanel);
            this.groupBox.Controls.Add(this.timerLabel);
            this.groupBox.Controls.Add(this.successLabel);
            this.groupBox.Controls.Add(this.errorsLabel);
            this.groupBox.Location = new System.Drawing.Point(679, 275);
            this.groupBox.Margin = new System.Windows.Forms.Padding(0);
            this.groupBox.Name = "groupBox";
            this.groupBox.Size = new System.Drawing.Size(155, 253);
            this.groupBox.TabIndex = 8;
            this.groupBox.TabStop = false;
            // 
            // accuracyBarPanel
            // 
            this.accuracyBarPanel.Location = new System.Drawing.Point(0, 0);
            this.accuracyBarPanel.Name = "accuracyBarPanel";
            this.accuracyBarPanel.Size = new System.Drawing.Size(25, 250);
            this.accuracyBarPanel.TabIndex = 0;
            // 
            // CoordinateTestForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(827, 771);
            this.Controls.Add(this.groupBox);
            this.Controls.Add(this.nameLabel);
            this.Controls.Add(this.backToMenuButton);
            this.Controls.Add(this.playButtonForWhite);
            this.Controls.Add(this.playButtonForBlack);
            this.Controls.Add(this.cellToRestoreLabel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "CoordinateTestForm";
            this.Text = "Тренировка координат";
            this.Load += new System.EventHandler(this.CoordinateTestForm_Load);
            this.groupBox.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        /// <summary>
        /// Информация о клетке, которую нужно угадать
        /// </summary>
        private string cellToRestore = "";
        /// <summary>
        /// Текст, в котором написан номер клетки, которую нужно угадать
        /// </summary>
        private Label cellToRestoreLabel;
        /// <summary>
        /// Коэффециент побед относительно поражений
        /// </summary>
        private double coefficient;
        private Label resultLabel;
        private int errors;
        private Label errorsLabel;
        private Label timerLabel;
        private Label successLabel;
        private Label nameLabel;
        private int successCount;
        /// <summary>
        /// Массив элементов, которые визуально определяют соотношение побед и поражений
        /// </summary>
        private Control[] accuracyBarCells;
        private Random random = new Random();
        /// <summary>
        /// Кнопка выхода в меню
        /// </summary>
        private MaterialSkin.Controls.MaterialRaisedButton backToMenuButton;
        /// <summary>
        /// Кнопка играть за белых
        /// </summary>
        private MaterialSkin.Controls.MaterialRaisedButton playButtonForWhite;
        /// <summary>
        /// Кнопка играть за чёрных
        /// </summary>
        private MaterialSkin.Controls.MaterialRaisedButton playButtonForBlack;
        /// <summary>
        /// Область, в которой содержится информация о текущих результатах игры и прочая информация
        /// </summary>
        private GroupBox groupBox;
        /// <summary>
        /// Панель, в которой находится шкала правильных/неправильных ответов
        /// </summary>
        private Panel accuracyBarPanel;
    }
}