using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sachy_Obrazky
{
    partial class Form1
    {

        /// <summary>
        /// Vyžaduje se proměnná návrháře.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Uvolněte všechny používané prostředky.
        /// </summary>
        /// <param name="disposing">hodnota true, když by se měl spravovaný prostředek odstranit; jinak false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, сгенерированный конструктором форм Windows

        /// <summary>
        /// Метод, необходимый для поддержки дизайнера - не редактировать
        /// содержимое этого метода в редакторе кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.WindowState = FormWindowState.Maximized;

            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(836, 812);

            this.components = new System.ComponentModel.Container();
            this.panel1 = new System.Windows.Forms.Panel(); // sdc
            this.timer1 = new System.Windows.Forms.Timer(this.components); // sdc
            this.timer2 = new System.Windows.Forms.Timer(this.components); // sdc
            this.KonecHry = new System.Windows.Forms.TextBox(); // sdc
            this.panel2 = new System.Windows.Forms.Panel(); // sdc
            this.panel3 = new System.Windows.Forms.Panel(); // sdc
            this.SaveName = new System.Windows.Forms.TextBox();
            this.defaultChessPanel = new System.Windows.Forms.Panel(); // sdc
            this.panel3.SuspendLayout(); // sdc
            this.defaultChessPanel.SuspendLayout(); // sdc
            this.SuspendLayout();

            this.MainFormButton = new System.Windows.Forms.Button();

            // 
            // MainFormButton
            // 
            this.MainFormButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(240)));
            this.MainFormButton.Location = new System.Drawing.Point(100, 50);
            this.MainFormButton.Name = "CoordinateTestButton";
            this.MainFormButton.Size = new System.Drawing.Size(100, 50);
            this.MainFormButton.TabIndex = 0;
            this.MainFormButton.Text = "MainFormButton";
            this.MainFormButton.UseVisualStyleBackColor = true;
            this.MainFormButton.Click += new System.EventHandler(this.MainFormButton_Click);
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(151, 38);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(604, 654);
            this.panel1.TabIndex = 64;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // timer1
            // 
            this.timer1.Enabled = false;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // timer2
            // 
            this.timer2.Enabled = true;
            this.timer2.Interval = 300;
            this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
            // 
            // KonecHry
            // 
            this.KonecHry.Enabled = false;
            this.KonecHry.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.KonecHry.HideSelection = false;
            this.KonecHry.Location = new System.Drawing.Point(0, 62);
            this.KonecHry.Name = "KonecHry";
            this.KonecHry.ReadOnly = true;
            this.KonecHry.Size = new System.Drawing.Size(604, 38);
            this.KonecHry.TabIndex = 0;
            this.KonecHry.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.KonecHry.Visible = false;
            this.KonecHry.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // panel2
            // 
            this.panel2.Location = new System.Drawing.Point(2, 38);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(143, 654);
            this.panel2.TabIndex = 0;

;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.KonecHry);
            this.panel3.Location = new System.Drawing.Point(151, 38);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(604, 100);
            this.panel3.TabIndex = 65;
            // 
            // SaveName
            // 
            this.SaveName.Location = new System.Drawing.Point(9, 15);
            this.SaveName.Margin = new System.Windows.Forms.Padding(2);
            this.SaveName.Name = "SaveName";
            this.SaveName.Size = new System.Drawing.Size(76, 20);
            this.SaveName.TabIndex = 66;
            this.SaveName.Text = "prepsana";

            #region RecoveryChessPanel
            
            //
            // nameLabel2 
            //
            this.nameLabel2 = new Label();
            this.nameLabel2.Size = new System.Drawing.Size(200, 100);
            this.nameLabel2.Location = new System.Drawing.Point(0, 0);

            //
            // RecoveryChessPanel
            //
            var size = 100 / 2; // 100/2
            var leftMenuPanel2 = new Panel()
            {
                Size = new Size(3 * size / 2 + 50, Height) // new Size(3 * size / 2+50, Height)
            }; // rc-


            recoveryChessPanel = new Panel() { Dock = DockStyle.Fill }; // rc -
            var backToMenuButton2 = new Button(); // => rc
            backToMenuButton2.Height = 3 * 71 / 2;// => rc
            backToMenuButton2.Width = 3 * 71 / 2;// => rc
            backToMenuButton2.Text = "Вернуться в меню";// => rc
            backToMenuButton2.Location = new Point(8, 71 * 5 + 140);// => rc
            backToMenuButton2.Click += (sender, e) =>
            {
                CloseRestorePositionTest();
                ShowGameModesSwitchPanel();
            };// => rc
            leftMenuPanel2.Controls.Add(backToMenuButton2); // rc -

            var chessBoardPanel2 = new Panel()
            {
                Dock = DockStyle.Fill
            }; // rc -

            int chessCellSize2 = 67; // => rc
            var startCellX2 = (Width - (chessCellSize2 * 8)) / 2;// => rc
            var startCellY2 = 50;// => rc
            //currentArrangement = ChessRestoreArrangementsStorage.Storage.FirstOrDefault(a => a.Id == 1).Arrangement;
            for (var y = 0; y < 8; y++)
            {
                for (var x = 0; x < 8; x++)
                {
                    var x1 = x;
                    var y1 = y;
                    var cell = new Button();
                    //cell.FlatAppearance.BorderSize = 0;
                    //cell.FlatStyle = FlatStyle.Flat;

                    recoveryCells[y, x] = cell;
                    cell.Height = chessCellSize2;
                    cell.Width = chessCellSize2;
                    cell.BackColor = GetCellColor(y, x);
                    cell.Location = new Point(startCellX2 + (x * chessCellSize2), startCellY2 + chessCellSize2 / 2 + (y * chessCellSize2));
                    cell.Click += (sender, e) => HandleRecoveryBoardClick(x1, y1);
                    chessBoardPanel2.Controls.Add(cell);
                    //var piece = (int)currentArrangement.Arrangement[y, x];
                    //if (piece >= 0)
                    //    ImagePrint(cell, piece);
                }
            } // => rc
            for (var y = 0; y < 8; y++)
            {
                var textBox = new TextBox();

                textBox.Font = new Font(textBox.Font.FontFamily, 39); //for some reason, /16 works pretty well 
                textBox.Width = 35;
                textBox.Text = (8 - y).ToString();
                textBox.Location = new Point(startCellX2 - 33, startCellY2 + 32 + (chessCellSize2 * y));
                textBox.Enabled = false;
                textBox.BringToFront();
                chessBoardPanel2.Controls.Add(textBox);
            } // => rc
            for (var x = 0; x < 8; x++)
            {
                var textBox = new TextBox();
                panel1.Controls.Add(textBox);

                textBox.Font = new Font(textBox.Font.FontFamily, 39);
                textBox.Width = chessCellSize2; //in order to fill the whole button-space
                textBox.Height = chessCellSize2;
                textBox.Text = ((char)('a' + x)).ToString();
                textBox.TextAlign = HorizontalAlignment.Center;
                textBox.Location = new Point(startCellX2 + (chessCellSize2 * x), startCellY2 + 32 + (chessCellSize2 * 8));
                textBox.Enabled = false;
                textBox.BringToFront();
                chessBoardPanel2.Controls.Add(textBox);
            } // => rc

            whiteTools = GetWhiteTools(startCellX2, startCellY2 + 15 + (10 * chessCellSize2), chessCellSize2); // => rc
            blackTools = GetBlackTools(startCellX2, startCellY2, chessCellSize2); // => rc
            
            var rightMenuPanel2 = new Panel()
            {
                Size = new Size(200, Height),
                Location = new Point(Width - 150, 0)
            }; // rc -
            groupBox2 = new GroupBox(); // => rc
            groupBox2.Size = new Size(300, 250); // => rc
            groupBox2.Location = new Point(0, Height / 2 - 150); // => rc 
            groupBox2.Margin = new Padding() { Right = 200 }; // => rc
            groupBox2.BackColor = Color.FromArgb(230, 230, 230); // => rc
            timerLabel2 = new Label()
            {
                Text = $"{StartRecoveryTimer} Секунд",
                Font = new Font(FontFamily.GenericSerif, 15, FontStyle.Bold),
                Size = new Size(300, 50),
                Location = new Point(00, 0)
            }; // => rc
            difficultLabel = new Label()
            {
                Text = $"Сложность: {difficult}",
                Font = new Font(FontFamily.GenericSerif, 15, FontStyle.Bold),
                Size = new Size(300, 50),
                Location = new Point(0, 50)
            }; // => rc
            var difficults = new ComboBox()
            {
                Size = new Size(100, 50),
                Location = new Point(50, 100)
            }; // => rc
            for (var i = 1; i <= 10; i++)
            {
                difficults.Items.Add(i);
            } // => rc
            difficults.SelectedIndex = 0; // => rc
            start = new Button()
            {
                Text = "Начать",
                Size = new Size(100, 50),
                Location = new Point(25, 125)
            }; // => rc
            end = new Button()
            {
                Text = "Запомнил",
                Size = new Size(100, 50),
                Location = new Point(25, 125)
            }; // => rc
            end.Click += (sender, e) =>
            {
                OnRecoveryTimerLeft();

            }; // => rc
            start.Click += (sender, e) =>
            {
                foreach (var control in recoveryChessPanel.Controls)
                {
                    var panel = control as Panel;
                    if (panel == null)
                    {
                        continue;
                    }
                    if ((string)panel.Tag == "tool")
                        ((Control)control).Visible = false;
                } // => rc
                history.Clear();
                currentArrangementId = 1;
                recoveryErrors = 0;
                move = 0;
                history.Clear();
                difficult = 1;
                difficultLabel.Text = $"Сложность: {difficult}";
                groupBox2.Controls.Remove(start);
                groupBox2.Controls.Add(end);
                RemoveHelpHighlight();
                if (difficults.SelectedIndex == -1)
                {
                    var error = new DifficultNotSelectedForm();
                    error.ShowDialog();
                }
                else
                {
                    difficult = difficults.SelectedIndex + 1;
                    StopRecovery();
                    StartRecovery();
                }
            };// => rc

            groupBox2.Controls.Add(timerLabel2);// => rc
            groupBox2.Controls.Add(difficultLabel);// => rc
            groupBox2.Controls.Add(difficults); // => rc
            groupBox2.Controls.Add(start);// => rc
            groupBox2.Controls.Add(end); // => rc

            rightMenuPanel2.Controls.Add(groupBox2); // => rc
            recoveryChessPanel.Controls.AddRange(whiteTools); // => rc
            recoveryChessPanel.Controls.AddRange(blackTools); // => rc
            recoveryChessPanel.Controls.Add(leftMenuPanel2); // rc-
            recoveryChessPanel.Controls.Add(rightMenuPanel2); // rc-
            recoveryChessPanel.Controls.Add(chessBoardPanel2); // rc-
            #endregion

            #region CoordinateTestPanel
            //
            // nameLabel
            //
            this.nameLabel = new Label();
            this.nameLabel.Size = new System.Drawing.Size(200, 100);
            this.nameLabel.Location = new System.Drawing.Point(0, 0);

            //
            // CoordinateTestPanel
            //
            coordinateTestPanel = new Panel() { Dock = DockStyle.Fill }; // -
            var leftMenuPanel = new Panel()
            {
                Size = new Size(3 * size / 2 + 60, Height)
            }; // -

            var backToMenuButton = new Button(); // => ct
            backToMenuButton.Height = 3 * 71 / 2; // => ct
            backToMenuButton.Width = 3 * 71 / 2; // => ct
            backToMenuButton.Text = "Вернуться в меню"; // => ct
            backToMenuButton.Location = new Point(8, 71 * 5 + 140); // => ct
            backToMenuButton.Click += (sender, e) => // => ct
            {
                CloseCoordinateTest();
                ShowGameModesSwitchPanel();
            };
            leftMenuPanel.Controls.Add(backToMenuButton); // -
            var chessBoardPanel = new Panel()
            {
                Dock = DockStyle.Fill
            }; // -

            int chessCellSize = 67;     // => ct
            var cells = new Button[64]; // => ct
            var startCellX = leftMenuPanel.Right; // => ct 
            var startCellY = 100;       // => ct

            cellToRestore = ""; // => ct
            cellToRestoreLabel = new Label()
            {
                Size = new Size(200, 100),
                Font = new Font(FontFamily.GenericSerif, 45, FontStyle.Bold),
                Text = cellToRestore,
                Location = new Point(startCellX + 3 * chessCellSize, 0) // => 
            }; // => ct
            var playButtonForWhite = new Button() 
            {
                Size = new Size(3 * 71 / 2, 40),
                Text = "Начать за белых",
                Location = new Point(8, startCellY)
                //Location = new Point(startCellX + 2 * chessCellSize, 700)
            }; // => ct
            playButtonForWhite.Click += (sender, e) => // => ct
            {
                if (_coordinateTimer.Enabled)
                    EndGameAndShowResults(false);
                else
                    StopCoordinateTest();
                StartCoordinateTestForWhite();
            };
            var playButtonForBlack = new Button() 
            {
                Size = new Size(3 * 71 / 2, 40),
                Text = "Начать за черных",
                Location = new Point(8, startCellY + 45)
            }; // => ct
            playButtonForBlack.Click += (sender, e) =>
            {
                if (_coordinateTimer.Enabled)
                    EndGameAndShowResults(false);
                else
                    StopCoordinateTest();
                StartCoordinateTestForBlack();
            };  // => ct
            chessBoardPanel.Controls.Add(cellToRestoreLabel); // => ct
            leftMenuPanel.Controls.Add(playButtonForWhite); // => ct
            leftMenuPanel.Controls.Add(playButtonForBlack); // => ct
            //chessBoardPanel.Controls.Add(playButtonForWhite);
            //chessBoardPanel.Controls.Add(playButtonForBlack);

            for (var y = 0; y < 8; y++)
            {
                for (var x = 0; x < 8; x++)
                {
                    var x1 = x;
                    var y1 = y;
                    var cell = new Button();
                    cell.Height = chessCellSize;
                    cell.Width = chessCellSize;
                    cell.BackColor = GetCellColor(y, x);
                    cell.Location = new Point(startCellX + (x * chessCellSize), startCellY + (y * chessCellSize));
                    cell.Click += (sender, e) => HandleClick(x1, y1);
                    chessBoardPanel.Controls.Add(cell);
                }
            }  // => ct

            var accuracyBarPanel = new Panel()
            {
                Size = new Size(25, Height),
                Location = new Point(Width - 176, 0)
            }; // => ct

            accuracyBarCells = new Control[10];    // => ct
            for (var i = 0; i < 10; i++)
            {
                var panel = new Panel();
                panel.Size = new Size(25, 25);
                panel.Location = new Point(0, Height / 2 + 150 - 50 - (25 * (10 - i)));
                panel.BackColor = Color.FromArgb(0, 255, 255, 255);
                accuracyBarCells[i] = panel;
                accuracyBarPanel.Controls.Add(panel); 
            } // => ct
            var rightMenuPanel = new Panel()
            {
                //BackColor = Color.Black,
                Size = new Size(200, Height),
                Location = new Point(accuracyBarPanel.Right, 0),
                //Dock = DockStyle.Right
            }; //-

            var groupBox = new GroupBox();   // => ct
            groupBox.Size = new Size(300, 250);                   // => ct
            groupBox.Location = new Point(0, Height / 2 - 150);          // => ct
            groupBox.Margin = new Padding() { Right = 200 };                 // => ct
            groupBox.BackColor = Color.FromArgb(230, 230, 230); // => ct

            resultLabel = new Label()
            {
                Text = "Правильно:\n0",
                Font = new Font(FontFamily.GenericSerif, 15, FontStyle.Bold),
                Size = new Size(300, 50),
                Location = new Point(0, 0)
            };   //=> ct
            errorsLabel = new Label() 
            {
                Text = "Ошибок:\n0",
                Font = new Font(FontFamily.GenericSerif, 15, FontStyle.Bold),
                Size = new Size(300, 50),
                Location = new Point(0, 50)
            };   //=> ct
            timerLabel = new Label() 
            {
                Text = "60 Секунд",
                Font = new Font(FontFamily.GenericSerif, 15, FontStyle.Bold),
                Size = new Size(300, 50),
                Location = new Point(0, 100)
            };    //=> ct

            successLabel = new Label() 
            {
                ForeColor = Color.Green,
                Font = new Font(FontFamily.GenericSerif, 15, FontStyle.Bold),
                Size = new Size(300, 50),
                Location = new Point(0, 150)
            };      //=>ct
            groupBox.Controls.Add(resultLabel); //=>ct
            groupBox.Controls.Add(timerLabel);  //=>ct
            groupBox.Controls.Add(successLabel);//=>ct
            groupBox.Controls.Add(errorsLabel); //=>ct
            leftMenuPanel.Controls.Add(nameLabel); // -
            leftMenuPanel2.Controls.Add(nameLabel2);  // -
            //rightMenuPanel.Controls.Add(resultLabel);
            //rightMenuPanel.Controls.Add(timerLabel);
            rightMenuPanel.Controls.Add(groupBox); //=>ct
            //rightMenuPanel.Controls.Add(successLabel);

            coordinateTestPanel.Controls.Add(leftMenuPanel);// -
            coordinateTestPanel.Controls.Add(accuracyBarPanel);// -
            coordinateTestPanel.Controls.Add(rightMenuPanel);// -
            coordinateTestPanel.Controls.Add(chessBoardPanel);// -

            #endregion

            #region DefaultChessPanel
            // 
            // DefaultChessPanel
            // 

            this.defaultChessPanel.Controls.Add(this.panel1);
            this.defaultChessPanel.Controls.Add(this.panel2);
            this.defaultChessPanel.Controls.Add(this.panel3);
            this.defaultChessPanel.Controls.Add(this.SaveName);
            this.defaultChessPanel.Location = new System.Drawing.Point(0, 0);
            this.defaultChessPanel.Margin = new System.Windows.Forms.Padding(2);
            this.defaultChessPanel.Name = "DefaultChessPanel";
            this.defaultChessPanel.Size = new System.Drawing.Size(150, 81);
            this.defaultChessPanel.TabIndex = 0;
            defaultChessPanel.Dock = System.Windows.Forms.DockStyle.Fill;

            #endregion

            #region GameModesSwitchPanel

            //
            // GameModesSwitchPanel
            //
            gameModesSwitchPanel = new Panel();
            gameModesSwitchPanel.Dock = DockStyle.Fill;
            gameModesSwitchPanel.AutoSize = true;
            gameModesSwitchPanel.AutoSizeMode = AutoSizeMode.GrowOnly;
            var welcomeLabel = new MaterialSkin.Controls.MaterialLabel()
            {
                Size = new Size(500, 20),
                Text = $"Пользователь: {Program.Name}"
            };
            /*var defaultChessButton = new MaterialSkin.Controls.MaterialRaisedButton();
            defaultChessButton.Text = "Основной режим";
            defaultChessButton.TabStop = false;
            defaultChessButton.Click += (sender, e) =>
            {
                Controls.Remove(gameModesSwitchPanel);
                ShowDefaultChess();
            };

            
            defaultChessButton.Size = new Size(buttonWidth, 200);
            defaultChessButton.Location = new Point(0, Width / 2 - 100);
            */



            // кнопка перехода к тесту "тренировка координат"

            // здесь центр координат находится не в верхнем левом углу, а непонятным образом зависит от размера ClientSize
            // центр распологается, для размера (836 х 812), в точке 321 х 356

            // определим реальный центр экрана
            var pointCentrScreen = new Point(ClientSize.Width / 2 - 321, ClientSize.Height / 2 - 356);

            var quantityButton = 3; // количество кнопок
            var buttonWidth = (ClientSize.Width - 50) / quantityButton;
            int buttonHeight = Convert.ToInt32(buttonWidth / 1.618);

            var coordinateTestButton = new MaterialSkin.Controls.MaterialRaisedButton();
            coordinateTestButton.Text = "Тренировка координат";
            coordinateTestButton.Click += (sender, e) =>
            {
                Controls.Remove(gameModesSwitchPanel);
                ShowCoordinateTest();
            };
            coordinateTestButton.Anchor = System.Windows.Forms.AnchorStyles.None;
            coordinateTestButton.Size = new Size(buttonWidth, buttonHeight);
            coordinateTestButton.Location = new Point(pointCentrScreen.X - buttonWidth / 2 * quantityButton, pointCentrScreen.Y - buttonHeight / 2); // -(buttonHeight / 2) 
            coordinateTestButton.TabStop = false;

            // кнопка перехода к тесту "восстановление позиции"
            var restorePositionTestButton = new MaterialSkin.Controls.MaterialRaisedButton();
            restorePositionTestButton.Text = "Восстановление позиции";
            restorePositionTestButton.TabStop = false;
            restorePositionTestButton.Click += (sender, e) =>
            {
                Controls.Remove(gameModesSwitchPanel);
                ShowRestorePositionTest();
            };

            restorePositionTestButton.Anchor = System.Windows.Forms.AnchorStyles.None;
            restorePositionTestButton.Size = new Size(buttonWidth, buttonHeight);
            restorePositionTestButton.Location = new Point(coordinateTestButton.Right, coordinateTestButton.Top);

            // кнопка перехода к тесту "Восстановить партию (HARD)"
            var restoreChessGameTestButton = new MaterialSkin.Controls.MaterialRaisedButton();
            restoreChessGameTestButton.Text = "Восстановить партию (HARD)";
            restoreChessGameTestButton.Click += (sender, e) =>
            {
                Controls.Remove(gameModesSwitchPanel);
                ShowCoordinateTest();
            };

            restoreChessGameTestButton.Anchor = System.Windows.Forms.AnchorStyles.None;
            restoreChessGameTestButton.Size = new Size(buttonWidth, buttonHeight);
            restoreChessGameTestButton.Location = new Point(restoreChessGameTestButton.Right, coordinateTestButton.Top);
            restoreChessGameTestButton.TabStop = false;

            restoreChessGameTestButton.Location = new Point(restorePositionTestButton.Right, coordinateTestButton.Top);


            //

            gameModesSwitchPanel.Controls.Add(welcomeLabel);
            //gameModesSwitchPanel.Controls.Add(defaultChessButton);
            gameModesSwitchPanel.Controls.Add(coordinateTestButton);
            gameModesSwitchPanel.Controls.Add(restorePositionTestButton);
            gameModesSwitchPanel.Controls.Add(restoreChessGameTestButton);

            #endregion

            #region Form1

            // 
            // Form1
            //

            this.Name = "Form1";
            this.Text = "Шахматы"; 
            this.Load += new System.EventHandler(this.Form1_Load); // -
            this.panel3.ResumeLayout(false);// -
            this.panel3.PerformLayout();// -
            this.defaultChessPanel.ResumeLayout(false);// -
            this.defaultChessPanel.PerformLayout();// -
            this.Controls.Add(this.MainFormButton);
            this.ResumeLayout(false); // => ct

            #endregion
        }
        #endregion

        private System.Windows.Forms.Panel panel1; // sdc
        private System.Windows.Forms.Timer timer1;  // sdc
        private System.Windows.Forms.Timer timer2; // sdc
        private System.Windows.Forms.Panel panel2; // sdc 
        private System.Windows.Forms.Panel panel3; // sdc 
        private System.Windows.Forms.Panel coordinateTestPanel; // ct-
        private System.Windows.Forms.Panel recoveryChessPanel; // rc -
        private System.Windows.Forms.Panel gameModesSwitchPanel; // -

        private Random random = new Random(); // => ct + rc

        private System.Windows.Forms.Button MainFormButton;

        // Это объекты для режима запоминания координат 
        #region CoordinateTest
        private double coefficient; // => ct
        private int errors; // => ct
        private Label resultLabel; // => ct
        private Label errorsLabel; // => ct
        private Label timerLabel; // => ct
        private Label successLabel; // => ct
        private string cellToRestore; // => ct
        private Label cellToRestoreLabel; // => ct
        private int successCount; // => ct
        private Label nameLabel; // => ct
        private Control[] accuracyBarCells; // => ct

        #endregion

        // Это объекты для режима восстановления позиции
        #region RecoveryChessPosition

        private Label nameLabel2; // => rc
        private GroupBox groupBox2; // => rc
        private Button start; // => rc
        private Button end; // => rc
        private Label timerLabel2; // => rc
        private bool recoveringIsRunning; // => rc
        private ChessArrangement currentArrangement; // => rc
        private int currentArrangementId = 1;  // => rc
        private Button[,] recoveryCells = new Button[8, 8]; // => rc
        private Label difficultLabel; // => rc
        private int difficult = 1; // => rc
        private int move; // => rc
        private int recoveryErrors; // => rc
        private List<int> history = new List<int>(); // => rc
        private Point lastRecoveryMove; // => rc
        private Control[] whiteTools; // => rc
        private Control[] blackTools; // => rc
        private ChessPiece selectedTool; // => rc
        private Queue<Point> selectionQueue = new Queue<Point>(); // => rc
        private ChessArrangement finalArrangement; // => rc

        #endregion

        #region ShowDefaultChess

        private System.Windows.Forms.TextBox KonecHry; // sdc
        private System.Windows.Forms.Panel defaultChessPanel; // sdc -
        private System.Windows.Forms.TextBox SaveName; // sdc

        #endregion

    }
}

