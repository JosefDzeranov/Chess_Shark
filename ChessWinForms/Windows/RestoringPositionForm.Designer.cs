using MaterialSkin.Controls;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Sachy_Obrazky.Windows
{
    partial class RestoringPositionForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RestoringPositionForm));
            this.backToMenuButton = new MaterialRaisedButton();
            this.difficultLabel = new System.Windows.Forms.Label();
            this.nameLabel = new System.Windows.Forms.Label();
            this.groupBox = new System.Windows.Forms.GroupBox();
            this.start = new MaterialRaisedButton();
            this.end = new MaterialRaisedButton();
            this.timerLabel = new System.Windows.Forms.Label();
            this.difficults = new System.Windows.Forms.ComboBox();
            this.groupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // backToMenuButton
            // 
            this.backToMenuButton.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.backToMenuButton.Location = new System.Drawing.Point(12, 570);
            this.backToMenuButton.Name = "backToMenuButton";
            this.backToMenuButton.Size = new System.Drawing.Size(93, 68);
            this.backToMenuButton.TabIndex = 0;
            this.backToMenuButton.Text = "Вернуться в меню";
            this.backToMenuButton.Click += new System.EventHandler(this.backToMenuButton_Click);
            // 
            // difficultLabel
            // 
            this.difficultLabel.Font = new System.Drawing.Font("Times New Roman", 15F, System.Drawing.FontStyle.Bold);
            this.difficultLabel.Location = new System.Drawing.Point(6, 16);
            this.difficultLabel.Name = "difficultLabel";
            this.difficultLabel.Size = new System.Drawing.Size(132, 33);
            this.difficultLabel.TabIndex = 0;
            this.difficultLabel.Text = "difficultLabel";
            // 
            // nameLabel
            // 
            this.nameLabel.Font = new System.Drawing.Font("Times New Roman", 15F, System.Drawing.FontStyle.Bold);
            this.nameLabel.Location = new System.Drawing.Point(12, 9);
            this.nameLabel.Name = "nameLabel";
            this.nameLabel.Size = new System.Drawing.Size(121, 54);
            this.nameLabel.TabIndex = 0;
            this.nameLabel.Text = "nameLabel";
            // 
            // groupBox
            // 
            this.groupBox.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.groupBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.groupBox.Controls.Add(this.difficultLabel);
            this.groupBox.Controls.Add(this.start);
            this.groupBox.Controls.Add(this.end);
            this.groupBox.Controls.Add(this.timerLabel);
            this.groupBox.Location = new System.Drawing.Point(735, 244);
            this.groupBox.Margin = new System.Windows.Forms.Padding(0);
            this.groupBox.Name = "groupBox";
            this.groupBox.Size = new System.Drawing.Size(156, 250);
            this.groupBox.TabIndex = 0;
            this.groupBox.TabStop = false;
            // 
            // start
            // 
            this.start.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.start.Location = new System.Drawing.Point(33, 202);
            this.start.Name = "start";
            this.start.Size = new System.Drawing.Size(87, 23);
            this.start.TabIndex = 1;
            this.start.Text = "Начать";
            this.start.Click += new System.EventHandler(this.start_Click);
            // 
            // end
            // 
            this.end.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.end.Location = new System.Drawing.Point(33, 202);
            this.end.Name = "end";
            this.end.Size = new System.Drawing.Size(87, 23);
            this.end.TabIndex = 2;
            this.end.Text = "Запомнил";
            this.end.Click += new System.EventHandler(this.end_Click);
            // 
            // timerLabel
            // 
            this.timerLabel.Location = new System.Drawing.Point(10, 85);
            this.timerLabel.Name = "timerLabel";
            this.timerLabel.Size = new System.Drawing.Size(100, 23);
            this.timerLabel.TabIndex = 0;
            this.timerLabel.Text = "timerLabel";
            // 
            // difficults
            // 
            this.difficults.Location = new System.Drawing.Point(10, 52);
            this.difficults.Name = "difficults";
            this.difficults.Size = new System.Drawing.Size(100, 21);
            this.difficults.TabIndex = 3;
            // 
            // RestoringPositionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(850, 771);
            this.Controls.Add(this.nameLabel);
            this.Controls.Add(this.groupBox);
            this.Controls.Add(this.backToMenuButton);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "RestoringPositionForm";
            this.Text = "Восстановление позиции";
            this.Load += new System.EventHandler(this.RecoveryChessPositionForm_Load);
            this.groupBox.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private int difficult = 1;
        private Label difficultLabel;
        private int recoveryErrors;
        private int move;
        private MaterialRaisedButton backToMenuButton;
        private Label nameLabel;
        private GroupBox groupBox; 
        /// <summary>
        /// Кнопка начала запоминания позиции
        /// </summary>
        private MaterialRaisedButton start; 
        /// <summary>
        /// Кнопка завершения запоминания и начала выставления фигур
        /// </summary>
        private MaterialRaisedButton end;
        private Label timerLabel;
        private bool recoveringIsRunning;
        private ComboBox difficults;
        /// <summary>
        /// Шахматное поле
        /// </summary>
        private ChessArrangement currentArrangement;
        private int currentArrangementId = 1;
        /// <summary>
        /// Шахматное поле с набором клеток-кнопок
        /// </summary>
        private Button[,] recoveryCells = new Button[8, 8];
        private List<int> history = new List<int>();
        private Point lastRecoveryMove;
        /// <summary>
        /// текущий активный инструмент
        /// </summary>
        private ChessPiece selectedTool; 
        private Queue<Point> selectionQueue = new Queue<Point>(); 
        private ChessArrangement referenceArrangement;
        private Random random = new Random();
        private Control[] whiteTools;
        private Control[] blackTools;
    }
}