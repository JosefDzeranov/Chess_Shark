using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using MaterialSkin.Controls;


namespace Sachy_Obrazky.Windows
{
    sealed partial class RestoreChessPartyForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RestoreChessPartyForm));
            this.backToMenuButton = new MaterialSkin.Controls.MaterialRaisedButton();
            this.nameLabel = new System.Windows.Forms.Label();
            this.groupBox = new System.Windows.Forms.GroupBox();
            this.trueAnswer_label = new System.Windows.Forms.Label();
            this.errors_label = new System.Windows.Forms.Label();
            this.startMemorizing = new MaterialSkin.Controls.MaterialRaisedButton();
            this.endMemorizing = new MaterialSkin.Controls.MaterialRaisedButton();
            this.CountStepLabel = new System.Windows.Forms.Label();
            this.playbackPartyTimerLabel = new System.Windows.Forms.Label();
            this.timerLabel = new System.Windows.Forms.Label();
            this.namePartyLabel = new System.Windows.Forms.Label();
            this.groupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // backToMenuButton
            // 
            this.backToMenuButton.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.backToMenuButton.Depth = 0;
            this.backToMenuButton.Location = new System.Drawing.Point(16, 586);
            this.backToMenuButton.MouseState = MaterialSkin.MouseState.HOVER;
            this.backToMenuButton.Name = "backToMenuButton";
            this.backToMenuButton.Primary = true;
            this.backToMenuButton.Size = new System.Drawing.Size(93, 68);
            this.backToMenuButton.TabIndex = 0;
            this.backToMenuButton.Text = "Вернуться в меню";
            this.backToMenuButton.Click += new System.EventHandler(this.backToMenuButton_Click);
            // 
            // nameLabel
            // 
            this.nameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.nameLabel.Location = new System.Drawing.Point(12, 9);
            this.nameLabel.Name = "nameLabel";
            this.nameLabel.Size = new System.Drawing.Size(121, 54);
            this.nameLabel.TabIndex = 0;
            this.nameLabel.Text = "nameLabel";
            // 
            // groupBox
            // 
            this.groupBox.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.groupBox.BackColor = System.Drawing.SystemColors.Control;
            this.groupBox.Controls.Add(this.trueAnswer_label);
            this.groupBox.Controls.Add(this.errors_label);
            this.groupBox.Controls.Add(this.startMemorizing);
            this.groupBox.Controls.Add(this.endMemorizing);
            this.groupBox.Controls.Add(this.CountStepLabel);
            this.groupBox.Controls.Add(this.playbackPartyTimerLabel);
            this.groupBox.Controls.Add(this.timerLabel);
            this.groupBox.Location = new System.Drawing.Point(695, 189);
            this.groupBox.Margin = new System.Windows.Forms.Padding(0);
            this.groupBox.Name = "groupBox";
            this.groupBox.Size = new System.Drawing.Size(130, 258);
            this.groupBox.TabIndex = 0;
            this.groupBox.TabStop = false;
            // 
            // trueAnswer_label
            // 
            this.trueAnswer_label.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.trueAnswer_label.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.trueAnswer_label.Location = new System.Drawing.Point(9, 168);
            this.trueAnswer_label.Name = "trueAnswer_label";
            this.trueAnswer_label.Size = new System.Drawing.Size(115, 23);
            this.trueAnswer_label.TabIndex = 4;
            this.trueAnswer_label.Text = "trueAnswer_label";
            this.trueAnswer_label.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // errors_label
            // 
            this.errors_label.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.errors_label.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.errors_label.Location = new System.Drawing.Point(6, 146);
            this.errors_label.Name = "errors_label";
            this.errors_label.Size = new System.Drawing.Size(118, 22);
            this.errors_label.TabIndex = 3;
            this.errors_label.Text = "errors_label";
            this.errors_label.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // startMemorizing
            // 
            this.startMemorizing.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.startMemorizing.Depth = 0;
            this.startMemorizing.Location = new System.Drawing.Point(12, 210);
            this.startMemorizing.MouseState = MaterialSkin.MouseState.HOVER;
            this.startMemorizing.Name = "startMemorizing";
            this.startMemorizing.Primary = true;
            this.startMemorizing.Size = new System.Drawing.Size(101, 23);
            this.startMemorizing.TabIndex = 1;
            this.startMemorizing.Text = "Начать";
            this.startMemorizing.Click += new System.EventHandler(this.StartMemorizing_Click);
            // 
            // endMemorizing
            // 
            this.endMemorizing.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.endMemorizing.Depth = 0;
            this.endMemorizing.Location = new System.Drawing.Point(12, 210);
            this.endMemorizing.MouseState = MaterialSkin.MouseState.HOVER;
            this.endMemorizing.Name = "endMemorizing";
            this.endMemorizing.Primary = true;
            this.endMemorizing.Size = new System.Drawing.Size(101, 23);
            this.endMemorizing.TabIndex = 2;
            this.endMemorizing.Text = "Запомнил";
            this.endMemorizing.Click += new System.EventHandler(this.EndMemorizing_Click);
            // 
            // CountStepLabel
            // 
            this.CountStepLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.CountStepLabel.Location = new System.Drawing.Point(3, 123);
            this.CountStepLabel.Name = "CountStepLabel";
            this.CountStepLabel.Size = new System.Drawing.Size(121, 23);
            this.CountStepLabel.TabIndex = 0;
            this.CountStepLabel.Text = "CountStepLabel";
            this.CountStepLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // playbackPartyTimerLabel
            // 
            this.playbackPartyTimerLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.playbackPartyTimerLabel.Location = new System.Drawing.Point(3, 77);
            this.playbackPartyTimerLabel.Name = "playbackPartyTimerLabel";
            this.playbackPartyTimerLabel.Size = new System.Drawing.Size(121, 23);
            this.playbackPartyTimerLabel.TabIndex = 0;
            this.playbackPartyTimerLabel.Text = "playbackPartyTimerLabel";
            this.playbackPartyTimerLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // timerLabel
            // 
            this.timerLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.timerLabel.Location = new System.Drawing.Point(3, 100);
            this.timerLabel.Name = "timerLabel";
            this.timerLabel.Size = new System.Drawing.Size(121, 23);
            this.timerLabel.TabIndex = 0;
            this.timerLabel.Text = "timerLabel";
            this.timerLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // namePartyLabel
            // 
            this.namePartyLabel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.namePartyLabel.BackColor = System.Drawing.SystemColors.Control;
            this.namePartyLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.namePartyLabel.Location = new System.Drawing.Point(12, 189);
            this.namePartyLabel.Name = "namePartyLabel";
            this.namePartyLabel.Size = new System.Drawing.Size(121, 233);
            this.namePartyLabel.TabIndex = 1;
            this.namePartyLabel.Text = "namePartyLabel";
            this.namePartyLabel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // RestoreChessPartyForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(870, 830);
            this.Controls.Add(this.namePartyLabel);
            this.Controls.Add(this.nameLabel);
            this.Controls.Add(this.groupBox);
            this.Controls.Add(this.backToMenuButton);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "RestoreChessPartyForm";
            this.Text = "Восстановление хода шахматной партии";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.RestoreChessPartyForm_FormClosing);
            this.Load += new System.EventHandler(this.RecoveryChessPositionForm_Load);
            this.groupBox.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private int recoveryErrors;

        private int move;
        /// <summary>
        /// Кнопка возврата в меню
        /// </summary>
        private MaterialSkin.Controls.MaterialRaisedButton backToMenuButton;
        /// <summary>
        /// Имя пользователя, введённое при входе
        /// </summary>
        private Label nameLabel;
        /// <summary>
        /// Панель с информацией и элементами управления, которая находится справа
        /// </summary>
        private GroupBox groupBox;
        /// <summary>
        /// Кнопка начала запоминания позиции
        /// </summary>
        private MaterialSkin.Controls.MaterialRaisedButton startMemorizing;
        /// <summary>
        /// Кнопка завершения запоминания и начала выставления фигур
        /// </summary>
        private MaterialSkin.Controls.MaterialRaisedButton endMemorizing;
        /// <summary>
        /// Время, оставшееся до оканчания периода запоминания и начала повторения позиций партии на память
        /// </summary>
        private Label timerLabel;
        /// <summary>
        /// Нужно чтобы при true эффект клика по клеткам не работал
        /// </summary>
        private bool memorizingIsRunning;
        /// <summary>
        /// Текущая позиция на шахматном поле
        /// </summary>
        private ChessArrangement currentArrangement;
        /// <summary>
        /// Шахматное поле с набором клеток-кнопок
        /// </summary>
        private Button[,] recoveryCells = new Button[8, 8];
        /// <summary>
        /// Ряд кнопок для управления 
        /// </summary>
        private Panel[] elementsPlayer = new Panel[6];
        private Point lastRecoveryMove;
        /// <summary>
        /// текущий активный инструмент
        /// </summary>
        private ChessPiece selectedTool = ChessPiece.Pointer;
        private Queue<Point> selectionQueue = new Queue<Point>();
        /// <summary>
        /// Позиция на шахматном поле, которую необходимо получить
        /// </summary>
        private ChessArrangement desiredArrangement;
        private Random random = new Random();
        /// <summary>
        /// Control[] для инструментов управления и шахматных фигур для БЕЛОЙ стороны
        /// </summary>
        private Control[] whiteTools;
        /// <summary>
        /// Control[] для инструментов управления и шахматных фигур для ЧЁРНОЙ стороны
        /// </summary>
        private Control[] blackTools;
        /// <summary>
        /// Надпись, показывающая текущий ход партии
        /// </summary>
        private Label CountStepLabel;
        /// <summary>
        /// Название текущей партии
        /// </summary>
        private Label namePartyLabel;
        /// <summary>
        /// Количество ошибок
        /// </summary>
        private Label errors_label;
        private Label trueAnswer_label;
        private Label playbackPartyTimerLabel;
    }
}