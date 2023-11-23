using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using MaterialSkin;
using MaterialSkin.Controls;
using Sachy_Obrazky.Windows;

namespace Sachy_Obrazky
{
    public partial class Form1 : Form
    {
        /// <summary>
        /// Хранилище шахматных позиций
        /// </summary>
        private IEnumerator<ChessRestoreArrangement> _storage; // => rc
        private const string UserSaveFilePath = "userdata.txt"; // 
        private static bool _isWhiteCoordinateTest = true; // => ct
        private readonly Panel _defaultChessPanel;
        private int _coordinateTimerSecondsLeft = 60; // => ct
        /// <summary>
        /// Величина таймера на старте
        /// </summary>
        private const int StartRecoveryTimer = 30; // => rc
        /// <summary>
        /// Таймер
        /// </summary>
        private int recoveryTimerSecondsLeft = StartRecoveryTimer; // => rc
        private readonly System.Windows.Forms.Timer _coordinateTimer = new System.Windows.Forms.Timer() // => ct
        {
            Interval = 1000,
            Enabled = false
        }; // => ct
        private readonly System.Windows.Forms.Timer recoveryTimer = new System.Windows.Forms.Timer() // => rc
        {
            Interval = 1000,
            Enabled = false
        }; // => rc
        public static bool IsChecked = false; //+
        string notation = "";

        public Form1()
        {
            _storage = InitializeStorage("storage.txt"); // => ct + rc
            FormClosing += (sender, e) => HandleOnClosing(); // => m
            _coordinateTimer.Tick += (sender, e) => OnCoordinateTimerTick(); // => ct
            recoveryTimer.Tick += (sender, e) => OnRecoveryTimerTick(); // => rc
            DoubleBuffered = true; //+
            var authenticationData = Authenticate(); // => m
            Program.Name = $"{authenticationData.FirstName}"; // => m
            Program.Record = GetRecord($"{Program.Name}"); // => m
            _defaultChessPanel = defaultChessPanel; // -
            InitializeComponent();
            ShowGameModesSwitchPanel();
            this.nameLabel.Text = $"Имя: {Program.Name}"; // => ct
            this.nameLabel2.Text = $"Имя: {Program.Name}"; // => rc
            //ShowCoordinateTest();
        }


        // В Main 
        #region Main

        /// <summary>
        /// Считывает файл с рекордами и перезаписывает текущий рекорд (если файла нет - создаёт его)
        /// </summary>
        private void HandleOnClosing()
        {
            if (File.Exists(UserSaveFilePath))
            {
                var builder = new StringBuilder();

                foreach (var line in File.ReadLines(UserSaveFilePath))
                {
                    if (line.StartsWith(Program.Name))
                    {
                        continue;
                    }
                    builder.AppendLine(line);
                }
                builder.AppendLine($"{Program.Name}-{Program.Record}");
                File.WriteAllText(UserSaveFilePath, builder.ToString());
            }
            else
            {
                File.Create(UserSaveFilePath);
                File.WriteAllText(UserSaveFilePath, $"{Program.Name}-{Program.Record}\n");
            }
        } // => m

        /// <summary>
        /// Вывод рекорда пользователя, по имени пользователя
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        private int GetRecord(string name)
        {
            if (File.Exists(UserSaveFilePath))
            {
                foreach (var line in File.ReadLines(UserSaveFilePath))
                {
                    var splittedLine = line.Split('-');
                    if (splittedLine[0] == name)
                    {
                        return int.Parse(splittedLine[1]);
                    }
                }
            }
            else
            {
                File.Create(UserSaveFilePath);
            }
            return 0;
        } // => m
        
        /// <summary>
        /// Вывод результатов авторизации
        /// </summary>
        /// <returns></returns>
        private AuthenticationData Authenticate()
        {
            var authenticationForm = new AuthenticationForm();
            var dialogResult = authenticationForm.ShowDialog();
            if (dialogResult != DialogResult.Yes)
            {
                System.Environment.Exit(0);
            }

            return new AuthenticationData(authenticationForm.FirstName, authenticationForm.LastName);
        }   // => m

        #endregion

        private Color GetCellColor(int y, int x)
        {
            var isDark = ((x + y) % 2 != 0);
            var color = isDark ? dark : light;
            return color;
        } // => ct + rc
        static readonly Color light = Color.LightGray; // => ct + rc
        static readonly Color dark = Color.DarkGray; // => ct + rc

        // Это функции для режима теста запоминания координат 
        #region CoordinateTest

        static readonly Color last_move = Color.Yellow; // => ct

        private void ShowGameResults(string title)
        {
            var info = new CoordinateTrainingInfo(title);
            info.result.Text = $"{Program.LastScore}";
            info.record.Text = $"{Program.Record}";
            info.errors.Text = $"{Program.LastErrors}";
            info.ShowDialog();
            if (Program.LastScore > Program.Record)
            {
                Program.Record = Program.LastScore;
            }
        }  // => ct 

        private void OnCoordinateTimerTick()
        {
            if (_coordinateTimerSecondsLeft == 0)
            {
                EndGameAndShowResults(true);
            }
            _coordinateTimerSecondsLeft--;
            timerLabel.Text = $"{_coordinateTimerSecondsLeft} Секунд";
        } // => ct

        private void EndGameAndShowResults(bool defaultEndGame)
        {
            Program.LastScore = successCount;
            Program.LastErrors = errors;
            StopCoordinateTest();
            if (defaultEndGame)
                ShowGameResults("Игра окончена по истечению таймера");
            else
                ShowGameResults("Игра окончена досрочно");
        } // => ct

        /// <summary>
        /// Закрыть тест по тренировке координат
        /// </summary>
        private void CloseCoordinateTest()
        {
            StopCoordinateTest();
            Controls.Remove(coordinateTestPanel);
        } // => ct
        
        /// <summary>
        /// Запустить тест "тренировки координат" за белых
        /// </summary>
        private void StartCoordinateTestForWhite()
        {
            _isWhiteCoordinateTest = true;
            _coordinateTimer.Enabled = true;
            BackToDefaultCoordinateTest();
            RandomCellToRestore();
        } // => ct

        /// <summary>
        /// Запустить тест "тренировки координат" за черных
        /// </summary>
        private void StartCoordinateTestForBlack() // => ct
        {
            _isWhiteCoordinateTest = false;
            _coordinateTimer.Enabled = true;
            BackToDefaultCoordinateTest();
            RandomCellToRestore();  // 
        }

        /// <summary>
        /// Создать тест "тренировки координат" по умолчанию
        /// </summary>
        private void BackToDefaultCoordinateTest()
        {
            coefficient = 0;
            _coordinateTimerSecondsLeft = 60;
            cellToRestoreLabel.Text = "";
            successCount = 0;
            errors = 0;
            foreach (var cell in accuracyBarCells)
            {
                cell.BackColor = Color.Transparent;
            }
            resultLabel.Text = $"Правильно:\n{successCount}";
            errorsLabel.Text = $"Ошибок:\n{errors}";
            successLabel.Text = "";
            timerLabel.Text = $"{_coordinateTimerSecondsLeft} Секунд";
        } // => ct

        private void StopCoordinateTest()
        {
            if (_coordinateTimer.Enabled)
            {
                _coordinateTimer.Enabled = false;
            }
            BackToDefaultCoordinateTest();
        } // => ct

        /// <summary>
        /// Вывести случайную клетку, которую нужно указать на поле
        /// </summary>
        private void RandomCellToRestore()
        {
            string cell = GetRandomCell();
            while (cell == cellToRestore)
            {
                cell = GetRandomCell();
            }
            cellToRestore = cell;
            cellToRestoreLabel.Text = cellToRestore;
        } // => ct

        /// <summary>
        /// Возвращает адрес случайной клетки шахматного поля
        /// </summary>
        /// <returns></returns>
        private string GetRandomCell()
        {
            var file = random.Next(1, 9);
            var rank = (char)(97 + random.Next(0, 8));
            return $"{rank}{file}";
        } // => ct
        
        private void HandleClick(int x, int y)
        {
            var move = _isWhiteCoordinateTest ? $"{(char)(97 + x)}{(7 - y) + 1}" : $"{(char)(104 - x)}{y + 1}";
            if (_coordinateTimer.Enabled)
            {
                var success = move == cellToRestore;

                var foreColor = success ? Color.Green : Color.Red;
                if (success)
                {
                    successCount += 1;
                }
                else
                {
                    errors += 1;
                }
                coefficient = (double)successCount / (successCount + errors);
                SetAccuracyBarLevel(coefficient);
                var message = success ? "Верно" : "Неверно";
                successLabel.ForeColor = foreColor;
                successLabel.Text = message;
                resultLabel.Text = $"Правильно:\n{successCount}";
                errorsLabel.Text = $"Ошибок:\n{errors}";
                RandomCellToRestore();
            }
        } // => ct

        private void SetAccuracyBarLevel(double coefficient)
        {
            var level = (int)10 * coefficient;
            for (var i = 0; i < accuracyBarCells.Length; i++)
            {
                var panel = accuracyBarCells[9 - i];
                if (i <= (level - 1))
                {
                    panel.BackColor = Color.Green;
                }
                else
                {

                    panel.BackColor = Color.Red;
                }
            }
        } // => ct

        #endregion

        // Это функции для режима повторения шахматной позиции 
        #region RecoveryCessPosition

        public static string[] PieceImages = {
            "Wking_light.png",
            "Wpawn_light.png",
            "Wknight_light.png",
            "Wbishop_light.png",
            "Wrook_light.png",
            "Wqueen_light.png",
            "Bking_light.png",
            "Bpawn_light.png",
            "Bknight_light.png",
            "Bbishop_light.png",
            "Brook_light.png",
            "Bqueen_light.png",
            "Pointer.png",
            "Remover.png"
        }; // => rc

        /// <summary>
        /// Массив с изображением шахматных фигур
        /// </summary>
        static readonly Bitmap[] ImagesPieces = initImagesPieces(); // => rc

        /// <summary>
        /// Инициализация хранилища шахматных позиций
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        private IEnumerator<ChessRestoreArrangement> InitializeStorage(string path) 
        {
            if (!File.Exists("storage.txt"))
            {
                var storageFileNotExistsForm = new StorageFileNotExistsForm();
                storageFileNotExistsForm.ShowDialog();
                System.Environment.Exit(0);
                throw new Exception();
            }
            else
            {
                return File.ReadLines(path).Select(line => new ChessRestoreArrangement(1, ChessRestoreArrangementsStorage.CreateArrangementFromFen(line))).GetEnumerator();
            }
        } // => rc

        public void ImagePrint(Panel b, int piece)
        {
            const double ScaleRatio = 1;
            int width = (int)(b.Width * ScaleRatio);
            int height = (int)(b.Height * ScaleRatio);

            Bitmap originImage = ImagesPieces[piece]; //new Bitmap(Application.StartupPath + "\\Image\\" + PieceImages[piece]);
            originImage.MakeTransparent(Color.FromArgb(200, 200, 200));
            Image image = resizeImage(originImage, new Size(height, width));

            b.BackgroundImage = image;


            /*b.ImageAlign = ContentAlignment.MiddleRight;
            b.TextAlign = ContentAlignment.MiddleLeft;
            // Give the button a flat appearance.
            b.FlatStyle = FlatStyle.Flat;*/
        } // => rc

        /// <summary>
        /// Закрыть тест на "восстановление позиции"
        /// </summary>
        private void CloseRestorePositionTest()
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
            }
            StopRecovery();
            Controls.Remove(recoveryChessPanel);
        } // => rc

        /// <summary>
        /// Показать тест на "восстановление позиции"
        /// </summary>
        private void ShowRestorePositionTest()
        {
            difficult = 1;
            difficultLabel.Text = $"Сложность: {difficult}";
            groupBox2.Controls.Remove(end);
            groupBox2.Controls.Add(start);
            Controls.Add(recoveryChessPanel);
        } // => rc
        
        public void ImagePrint(Button b, int piece)
        {
            const double ScaleRatio = 0.8;
            int width = (int)(b.Width * ScaleRatio);
            int height = (int)(b.Height * ScaleRatio);

            Bitmap originImage = ImagesPieces[piece]; //new Bitmap(Application.StartupPath + "\\Image\\" + PieceImages[piece]);
            originImage.MakeTransparent(Color.FromArgb(200, 200, 200));
            Image image = resizeImage(originImage, new Size(height, width));

            b.Image = image;


            /*b.ImageAlign = ContentAlignment.MiddleRight;
            b.TextAlign = ContentAlignment.MiddleLeft;
            // Give the button a flat appearance.
            b.FlatStyle = FlatStyle.Flat;*/
        } // => rc

        private void OnRecoveryTimerTick()
        {
            if (recoveryTimerSecondsLeft == 0)
            {
                OnRecoveryTimerLeft();
            }
            else
            {
                timerLabel2.Text = $"{recoveryTimerSecondsLeft} Секунд";
                recoveryTimerSecondsLeft--;
            }
        } // => rc

        static Bitmap[] initImagesPieces()
        {
            Bitmap[] ip = new Bitmap[14];
            for (int i = 0; i < 14; ++i)
            {
                ip[i] = new Bitmap(Application.StartupPath + "\\Image\\" + PieceImages[i]);
            }
            return ip;
        } // => rc

        private void OnRecoveryTimerLeft()
        {
            foreach (var control in recoveryChessPanel.Controls)
            {
                var panel = control as Panel;
                if (panel == null)
                {
                    continue;
                }
                if ((string)panel.Tag == "tool")
                    ((Control)control).Visible = true;
            }
            groupBox2.Controls.Remove(end);
            groupBox2.Controls.Remove(end);
            recoveryTimer.Enabled = false;
            recoveringIsRunning = true;
            timerLabel2.Text = "";
            currentArrangement = new ChessArrangement(ChessArrangement.Empty);
            RedrawArrangement(currentArrangement.Arrangement);
        } // => rc

        private void StartRecovery()
        {
            if (!_storage.MoveNext())
            {
                groupBox2.Controls.Remove(end);
                var storageAreEmptyForm = new StorageAreEmptyForm();
                storageAreEmptyForm.ShowDialog();
                return;
            }
            finalArrangement = _storage.Current.Arrangement;
            RedrawArrangement(finalArrangement.Arrangement);
            recoveryTimer.Enabled = true;
        } // => rc

        private void StopRecovery()
        {
            timerLabel2.Text = $"{StartRecoveryTimer} Секунд";
            RemoveHelpHighlight();
            recoveringIsRunning = false;
            recoveryTimerSecondsLeft = StartRecoveryTimer;
            finalArrangement = new ChessArrangement(ChessArrangement.Empty);
            currentArrangement = new ChessArrangement(ChessArrangement.Empty);
            RedrawArrangement(finalArrangement.Arrangement);
            recoveryTimer.Enabled = false;
        } // => rc
        
        public void RedrawArrangement(ChessPiece[,] arrangement)
        {
            for (var y = 0; y < 8; y++)
            {
                for (var x = 0; x < 8; x++)
                {
                    var cell = recoveryCells[y, x];
                    var piece = (int)arrangement[y, x];
                    if (piece >= 0)
                        ImagePrint(cell, piece);
                    else
                        ((Button)cell).Image = null;
                }
            }
        } // => rc

        private void HighlightCellIfIsPossibleByDifficultLevel()
        {
            if (selectedTool == ChessPiece.Remover)
            {
                move--;
                return;
            }
            var isHelpMove = move % difficult == 0;
            if (isHelpMove)
            {
                HighlightIfCorrectMove(lastRecoveryMove);
            }
        } //  => rc

        private void HighlightIfCorrectMove(Point lastRecoveryMove)
        {
            if (finalArrangement.EqualsArrangementInPosition(currentArrangement.Arrangement, lastRecoveryMove.X, lastRecoveryMove.Y))
            {
                recoveryCells[lastRecoveryMove.Y, lastRecoveryMove.X].BackColor = Color.Green;
            }
            else
            {
                recoveryCells[lastRecoveryMove.Y, lastRecoveryMove.X].BackColor = Color.Red;
            }
        } // => rc

        private void SelectTool(ChessPiece tool)
        {
            selectionQueue.Clear();
            selectedTool = tool;
        }  // => rc

        Control[] GetWhiteTools(int startX, int startY, int cellSize)
        {
            var tools = new List<Control>();
            CreateTool(tools, 0, startX, startY, cellSize, ChessPiece.Pointer);
            CreateTool(tools, 1, startX, startY, cellSize, ChessPiece.WhiteKing);
            CreateTool(tools, 2, startX, startY, cellSize, ChessPiece.WhiteQueen);
            CreateTool(tools, 3, startX, startY, cellSize, ChessPiece.WhiteRook);
            CreateTool(tools, 4, startX, startY, cellSize, ChessPiece.WhiteBishop);
            CreateTool(tools, 5, startX, startY, cellSize, ChessPiece.WhiteKnight);
            CreateTool(tools, 6, startX, startY, cellSize, ChessPiece.WhitePawn);
            CreateTool(tools, 7, startX, startY, cellSize, ChessPiece.Remover);
            return tools.ToArray();
        } // => rc

        Control[] GetBlackTools(int startX, int startY, int cellSize)
        {
            var tools = new List<Control>();
            CreateTool(tools, 0, startX, startY, cellSize, ChessPiece.Pointer);
            CreateTool(tools, 1, startX, startY, cellSize, ChessPiece.BlackKing);
            CreateTool(tools, 2, startX, startY, cellSize, ChessPiece.BlackQueen);
            CreateTool(tools, 3, startX, startY, cellSize, ChessPiece.BlackRook);
            CreateTool(tools, 4, startX, startY, cellSize, ChessPiece.BlackBishop);
            CreateTool(tools, 5, startX, startY, cellSize, ChessPiece.BlackKnight);
            CreateTool(tools, 6, startX, startY, cellSize, ChessPiece.BlackPawn);
            CreateTool(tools, 7, startX, startY, cellSize, ChessPiece.Remover);
            return tools.ToArray();
        } // => rc
        void CreateTool(List<Control> controls, int index, int startX, int startY, int cellSize, ChessPiece piece)
        {
            var tool = new Panel()
            {
                Location = new Point(startX + (cellSize * index), startY - 50),
                Size = new Size(cellSize, cellSize)
            };
            tool.Visible = false;
            tool.Tag = "tool";
            tool.Click += (sender, e) => SelectTool(piece);
            controls.Add(tool);
            ImagePrint(tool, (int)piece);
        } // => rc
        private void HandleRecoveryBoardClick(int x, int y)
        {
            if (!recoveringIsRunning)
                return;
            RemoveHelpHighlight();
            move++;
            //
            //
            //
            //
            //
            //
            // CHECK FOR GAME IS STARTED
            //
            //
            //
            //
            //
            //
            //
            lastRecoveryMove = new Point(x, y);
            if (selectedTool == ChessPiece.Pointer)
            {
                if (selectionQueue.Count == 0)
                {
                    selectionQueue.Enqueue(new Point(x, y));
                }
                else if (selectionQueue.Count == 1)
                {
                    var to = new Point(x, y);
                    var from = selectionQueue.Dequeue();
                    currentArrangement.MovePiece(from.X, from.Y, to.X, to.Y);
                }
            }
            else if (selectedTool == ChessPiece.Remover)
            {
                currentArrangement.RemovePiece(x, y);
            }
            else
            {
                if (selectedTool == ChessPiece.WhiteKing || selectedTool == ChessPiece.BlackKing)
                {
                    if (selectedTool == ChessPiece.WhiteKing)
                    {
                        var position = currentArrangement.GetPieceFirstPositionOrNull(ChessPiece.WhiteKing);
                        if (position == null)
                        {
                            currentArrangement.SetPiece(x, y, selectedTool);
                        }
                        else
                        {
                            currentArrangement.MovePiece(x, y, position.Value.X, position.Value.Y);
                        }
                    }
                    else
                    {
                        var position = currentArrangement.GetPieceFirstPositionOrNull(ChessPiece.BlackKing);
                        if (position == null)
                        {
                            currentArrangement.SetPiece(x, y, selectedTool);
                        }
                        else
                        {
                            currentArrangement.MovePiece(x, y, position.Value.X, position.Value.Y);
                        }
                    }
                }
                else
                {
                    currentArrangement.SetPiece(x, y, selectedTool);
                }
            }

            RedrawArrangement(currentArrangement.Arrangement);

            if (!finalArrangement.EqualsArrangementInPosition(currentArrangement.Arrangement, x, y))
            {
                recoveryErrors++;
            }

            // Если нужно сделать учитывание только ошибок без подсказок, то ошибки будем снижать во время подсветки красным.
            //
            //
            //
            //
            //
            //
            //
            //
            //
            //
            //
            if (IsVictory())
            {
                //RedrawArrangement(currentArrangement.Arrangement);
                NextStep();
            }
            else
            {
                HighlightCellIfIsPossibleByDifficultLevel();
            }

        } // =>  rc

        private bool IsVictory()
        {
            return finalArrangement.EqualsArrangement(currentArrangement.Arrangement);
        } // => rc
        private void NextStep()
        {
            history.Add(difficult);
            currentArrangementId++;
            var lastDifficult = difficult;
            difficult = GetNextDifficult(recoveryErrors);
            recoveryErrors = 0;
            difficultLabel.Text = $"Сложность: {difficult}";
            if (difficult == 10 && lastDifficult == 10)
            {
                Win();
                return;
            }
            else if (lastDifficult == 1 && difficult == 1)
            {
                //Lose();
                return;
            }
            else if (!HasPositiveGrowth())
            {
                Lose();
                return;
            }
            else
            {
                StopRecovery();
                StartRecovery();
            }
        } // => rc

        private int GetNextDifficult(int errors)
        {
            var next = 10 - errors;
            return next <= 0 ? 1 : next;
        } // => rc

        private bool HasPositiveGrowth()
        {
            var candidates = history.Reverse<int>().Take(3);
            if (candidates.Count() < 3)
                return true;
            var min = candidates.Min();
            var max = candidates.Max();
            return max - min >= 1;
        } // => rc

        private void RemoveHelpHighlight()
        {
            for (var y = 0; y < 8; y++)
            {
                for (var x = 0; x < 8; x++)
                {
                    var cell = recoveryCells[y, x];
                    cell.BackColor = GetCellColor(y, x);
                }
            }
        } // => rc

        private void Win()
        {
            var congratulationForm = new CongratulationForm();
            congratulationForm.ShowDialog();
        } // => rc

        private void Lose()
        {
            var losingForm = new LosingForm();
            losingForm.ShowDialog();
        } // => rc

        public static Image resizeImage(Image imgToResize, Size size)
        {
            return (Image)(new Bitmap(imgToResize, size));
        } // => rc

        #endregion

        // Предположительно - это свойства и функции для игры в шахматы с написанным AI
        #region ShowDefaultChess

        const int MinimalMoveDelay = 1000; // ms 
        Engine enj;  // sdc
        bool moveMade = true;  // sdc
        public Button[] ButtonBoard = new Button[8 * 8];  // sdc
        TextBox Analys = new TextBox();  // sdc
        static readonly Color[] PieceColors = new Color[]
        {
            Color.White, //king
            Color.Yellow,
            Color.LightGreen,
            Color.LightBlue,
            Color.Orange, //rook
            Color.Pink,

            Color.Black,
            Color.Magenta, //pawn
            Color.DarkGreen,
            Color.DarkBlue,
            Color.DarkRed,
            Color.Purple, //queen
        };  // sdc
        int gamelength;  // sdc
        bool white;  // sdc
        ulong[] bitbs = new ulong[12];  // sdc
        static int result = 0;
        bool continuing = true; // sdc
        bool whitePlayer_AI = false; // sdc
        bool blackPlayer_AI = true; // sdc
        int intelWhite = 4; // sdc
        int intelBlack = 4; // sdc
        static readonly string[] results =
        {
            "*",
            "1/2 - 1/2",
            "0 - 1",
            "1 - 0"
        }; // sdc

        private long lastTimeRan = System.DateTime.Now.Ticks; // sdc

        const int AutoSaveFreq = 5;  // sdc

        private void timer1_Tick(object sender, EventArgs e)  //tick time = 10
        {
            //if (!Controls.Contains(defaultChessPanel))
            //{
            //    timer1.Stop();
            //    return;
            //}
            //if the last time ran is less than x seconds ago, skip the tick.
            if (moveMade is false || lastTimeRan > (System.DateTime.Now.Ticks - MinimalMoveDelay * 10000) || !continuing)
            {
                return;
            }
            if (white && whitePlayer_AI || (white is false && blackPlayer_AI))
            {
                if ((gamelength + 1) % (AutoSaveFreq * 2) == 0)
                    Engine.RewritePartia("partie.txt", SaveName.Text);
                result = GoNextMove(enj);
            }
            else
            {
                return;
            }
            lastTimeRan = System.DateTime.Now.Ticks;

            //continuing = PlayGame(enj) == 0;
            if (result != 0)
            {
                Engine.RewritePartia("partie.txt", SaveName.Text);
                Finish();
            }
        } // sdc

        public int GoNextMove(Engine enj) //ход, сделанный компьютером
        {

            moveMade = false;

            int mov = enj.ComputersMove(white, gamelength, white ? intelWhite : intelBlack);
            string textforbox = enj.analysisEvaluation.ToString() + " ";
            for (int i = 0; i < enj.pvmoves.Length; ++i)
            {
                if (enj.pvmoves[i] == "Ka8-a8")
                    break;
                textforbox += enj.pvmoves[i] + " ";
            }
            Analys.Text = textforbox;
            //int x = enj.PlayersMove(white, gamelength, 0);
            notation = enj.Notation;
            gamelength += 1;
            white ^= true;
            bitbs = enj.GetBitBoards();
            PrintPic(bitbs);
            HighlightMove(mov);
            int y = enj.DetermineResult(white);
            if (y == 0)
            {
                if (enj.IsCheck_White() || enj.IsCheck_Black())
                {
                    ShowCheck();
                }
                else
                {
                    UnshowCheck();
                }
            }
            moveMade = true;
            return y;
        } // sdc

        public void PrintPic(ulong[] bitboards)
        { //то же самое, что и функция Printout in engine, но изменяет названия кнопок
            int n = -1;
            foreach (Button btn in ButtonBoard) //надеюсь, что их заказывают одинаково
            {
                ++n;
                char piece = ' ';
                if (Engine.SqColor(n))
                {
                    ButtonBoard[n].BackColor = light;
                }
                else
                {
                    ButtonBoard[n].BackColor = dark;
                }

                //if occupied, write something here...
                for (int k = 0; k < bitboards.Length; ++k)
                {
                    ButtonBoard[n].Text = "";
                    if (Engine.Bit(bitboards[k], n))
                    {
                        piece = Engine.pieces[k];
                        //ButtonBoard[n].Text = piece.ToString();
                        //ButtonBoard[n].BackColor = PieceColors[k];
                        ImagePrint(ButtonBoard[n], k);
                        //ButtonBoard[n].BringToFront();
                        break;
                    }

                    //ButtonBoard[n].BackColor = SystemColors.Control;
                }
                if (piece == ' ')
                {
                    //if there is no piece
                    if (Engine.SqColor(n)) //if light
                    {
                        //ButtonBoard[n].Paint += new System.Windows.Forms.PaintEventHandler(Dark_Paint);
                        ButtonBoard[n].Image = null;
                    }
                    else
                    {
                        //ButtonBoard[n].Paint += new System.Windows.Forms.PaintEventHandler(Light_Paint);
                        ButtonBoard[n].Image = null;
                    }
                }
            }
            /*
            for (int i = 0; i < 8; i++) //i is for rows, j is for columns
            {
                for (int j = 0; j < 8; j++)
                {
                    char piece = ' ';
                    //if occupied, write something here...
                    for (int k = 0; k < bitboards.Length; k++)
                    {
                        if (Engine.Bit(bitboards[k], 8 * i + j))
                        {
                            piece = Engine.pieces[k];

                            break;
                        }
                    }
                }
            }
            */
        } // sdc

        void HighlightMove(int move)
        { //only highlights the "from" and "to" squares
            int from = (move >> 16) & 255;
            int to = (move >> 8) & 255;
            if (from == to)
            { //castling - it is called on previous move, so white is the opposite...
                from = white ? 4 : 60;
                if (Engine.Bit(move, 5)) //qside
                {
                    to = from - 2;
                }
                else if (Engine.Bit(move, 6)) //kside
                {
                    to = from + 2;
                }
            }
            ButtonBoard[from].BackColor = last_move;
            ButtonBoard[to].BackColor = last_move;
        } // sdc

        private void ShowCheck()
        {
            IsChecked = true;
            int checkedKing = enj.IsCheck_Black() ? Engine.Bking : Engine.Wking;
            ButtonBoard[checkedKing].BackColor = Color.DarkRed;

            var t = Engine.Wking;
            timer1.Stop();
            KonecHry.Text = "Шах!";
            KonecHry.Show();
            KonecHry.BringToFront();
            timer1.Start();
        }  // sdc

        private void UnshowCheck()
        {
            IsChecked = false;
            KonecHry.Text = "";
            KonecHry.Hide();
        }  // sdc

        private void Finish()
        {
            timer1.Stop();
            KonecHry.Text = "Результат " + results[result];
            KonecHry.Show();
            KonecHry.BringToFront();
            foreach (var b in ButtonBoard)
            {
                b.Enabled = false;
            }
        } // sdc
        
        const int StartingPos = 3;
        static readonly Color dark2 = Color.Brown;
        static readonly Color clicked_own = Color.LightGreen;
        static readonly Color clicked_opponent = Color.Red;
        static readonly Color allowed = Color.Green;
        ulong[] temporaryBitBoards;

        TextBox[] rankCoords = new TextBox[8];
        TextBox[] fileCoords = new TextBox[8];
        Button[] proms = new Button[4];
        Button aiw = new Button();
        Button aib = new Button();
        Button newg = new Button();
        int selectedSquare;
        int targetSquare;
        int selectedPiece;
        int promot = -1;
        int intelAI = 4;
        bool flipped = false;

        private void ShowDefaultChess()
        {
            if (timer1.Enabled)
            {
                // Если все ок и ничего не ломается можно убрать этот IF и выброс исключения
                throw new Exception();
            }
            Controls.Add(KonecHry);
            timer1.Start();
            Controls.Add(defaultChessPanel);
            enj = new Engine();
            MakeBoard(flipped);
            CreateOptions();
            ulong[] BB = enj.Initialize(StartingPos);
            temporaryBitBoards = new ulong[12];
            PrintPic(new ulong[12]);
            PrintPic(BB);
            white = (Engine.Position & (1 << 4)) != 0;
            //PlayGame(enj);
            moveMade = false;
        }  // sdc

        private void MakeBoard(bool flipped)
        {
            //creates buttons
            int size = panel1.Width / 9;

            //print buttons
            for (int i = 0; i < 64; i++)
            {
                ButtonBoard[i] = new Button();
                ButtonBoard[i].Height = size;
                ButtonBoard[i].Width = size;
                ButtonBoard[i].BackColor = Engine.SqColor(i) ? dark : light;
                panel1.Controls.Add(ButtonBoard[i]);

                //sets location 
                ButtonBoard[i].Location = flipped ?
                    new Point(panel1.Width / 9 + ((63 - i) & 0b111) * size, ((63 - i) >> 3) * size) :
                    new Point(panel1.Width / 9 + (i & 0b111) * size, (i >> 3) * size);


                //tag is according to index
                ButtonBoard[i].Tag = i;

                //text is determined by the normal notation rule
                ButtonBoard[i].Text = ((char)((i & 0b111) + 'a')).ToString() + ((8 - (i >> 3)).ToString());

                //click handler
                ButtonBoard[i].Click += Button_Click;
            }
            //coordinates - rank
            for (int i = 0; i < 8; ++i)
            {
                rankCoords[i] = new TextBox();
                var tx = rankCoords[i];
                panel1.Controls.Add(tx);

                tx.Font = new Font(tx.Font.FontFamily, panel1.Height / 16); //for some reason, /16 works pretty well 
                tx.Width = size / 2;
                tx.Text = (8 - i).ToString();
                tx.Location = flipped ?
                    new Point(panel1.Width / 9 - tx.Width,
                    (7 - i) * size + (size - tx.Height) / 2) : //fixes diff between button height and textbox height
                    new Point(panel1.Width / 9 - tx.Width,
                    i * size + (size - tx.Height) / 2);
                tx.Enabled = false;
                tx.BringToFront();
            }
            //coordinates - file (almost the same)
            for (int i = 0; i < 8; ++i)
            {
                fileCoords[i] = new TextBox();
                var tx = fileCoords[i];
                panel1.Controls.Add(tx);

                tx.Font = new Font(tx.Font.FontFamily, panel1.Height / 16);
                tx.Width = size; //in order to fill the whole button-space
                tx.Text = ((char)('a' + i)).ToString();
                tx.TextAlign = HorizontalAlignment.Center;
                tx.Location = flipped ?
                    new Point((8 - i) * size, panel1.Width - panel1.Width / 9) :
                    new Point((1 + i) * size, panel1.Width - panel1.Width / 9);
                tx.Enabled = false;
                tx.BringToFront();
            }
            panel2.Height = panel1.Height;
            panel2.Location = new Point(panel1.Location.X - panel2.Width, panel1.Location.Y);
            panel3.Width = panel1.Width * 8 / 9;
            panel3.Location = new Point(panel1.Location.X + panel1.Width / 9, panel1.Location.Y + panel1.Height);
            //endgame panel
            //panel3.Controls.Add(KonecHry);
            KonecHry.Location = new Point(panel3.Location.X, panel3.Location.Y + panel3.Height - KonecHry.Height);
            KonecHry.Width = panel3.Width;
            KonecHry.Show();
        } // sdc

        void Button_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            // use Name or Tag of button
            PieceClicked((int)button.Tag);
            return;
        } // sdc

        public void PieceClicked(int idx)
        { //при нажатии на фигуру отображаются зеленые квадраты, в которых фигура может двигаться

            //если та же часть ч.б. уже нажата, верните кнопки в нормальное состояние;


            ulong moveBitboard;
            var g = Engine.Position;
            if (promot != -1)
            {
                return;
            }
            if (ButtonBoard[idx].BackColor == clicked_own)
            { //same piece - cancel the lighting
              //ButtonBoard[idx].BackColor = Engine.SqColor(idx) ? light:dark;

                for (int i = 0; i < 64; ++i)
                {
                    int exceptIndex = 0;
                    if (enj.IsCheck_Black())
                    {
                        exceptIndex = Engine.Bking;
                    }
                    else if (enj.IsCheck_White())
                    {
                        exceptIndex = Engine.Wking;
                    }
                    if (IsChecked && i == exceptIndex)
                        continue;
                    ButtonBoard[i].BackColor = Engine.SqColor(i) ? light : dark;
                }
                selectedPiece = -1;
                selectedSquare = -1;
            }
            else if (ButtonBoard[idx].BackColor == allowed)
            { //target square of current piece
                targetSquare = idx;
                if (selectedPiece == 1 && 0 <= idx && idx < 8) //promotion - make a choice for white
                {
                    ShowPromotion(idx, true);
                    return;
                }
                else if (selectedPiece == 7 && 58 <= idx && idx < 64) //promotion - make a choice for black
                {
                    ShowPromotion(idx, false);
                    return;
                }
                if (IsChecked)
                {
                    UnshowCheck();
                }
                uint nextmove = enj.CompleteMove(selectedPiece, selectedSquare, idx, white);
                PlayNextMove(enj, nextmove);

                for (int i = 0; i < 64; ++i)
                {
                    int exceptIndex = 0;
                    if (enj.IsCheck_Black())
                    {
                        exceptIndex = Engine.Bking;
                    }
                    else if (enj.IsCheck_White())
                    {
                        exceptIndex = Engine.Wking;
                    }
                    if (IsChecked && i == exceptIndex)
                        continue;
                    ButtonBoard[i].BackColor = Engine.SqColor(i) ? light : dark;
                }

                if (!IsChecked)
                    ButtonBoard[selectedSquare].BackColor = last_move;
                ButtonBoard[idx].BackColor = last_move;
                selectedPiece = -1;
                selectedSquare = -1;
                return;
            }
            else
            { //new piece
                for (int i = 0; i < 64; ++i)
                {
                    int exceptIndex = 0;
                    if (enj.IsCheck_Black())
                    {
                        exceptIndex = Engine.Bking;
                    }
                    else if (enj.IsCheck_White())
                    {
                        exceptIndex = Engine.Wking;
                    }
                    if (IsChecked && i == exceptIndex)
                        continue;
                    ButtonBoard[i].BackColor = Engine.SqColor(i) ? light : dark;
                }
                moveBitboard = enj.DisplayLegalMoves(idx, white);
                selectedSquare = idx;
                selectedPiece = enj.GetPiece(idx);
                ulong pointr = 1;
                for (int i = 0; i < 64; ++i)
                {
                    if ((pointr & moveBitboard) != 0) //if legal
                    {
                        ButtonBoard[i].BackColor = Color.Green;
                    }
                    else
                    { //set it as default
                        int exceptIndex = 0;
                        if (enj.IsCheck_Black())
                        {
                            exceptIndex = Engine.Bking;
                        }
                        else if (enj.IsCheck_White())
                        {
                            exceptIndex = Engine.Wking;
                        }
                        if (IsChecked && i != exceptIndex)
                            ButtonBoard[i].BackColor = Engine.SqColor(i) ? light : dark;
                    }
                    pointr <<= 1;
                }
            }
        } // sdc

        void ShowPromotion(int target, bool white)
        {
            ButtonBoard[target].BackColor = clicked_opponent;
            promot = -2;
            for (int i = 0; i < 4; ++i)
            {
                ImagePrint(proms[i], (white ? 5 : 11) - i);
                proms[i].Show();
                proms[i].Enabled = true;
            }
        } // sdc

        private void CreateOptions()
        {
            int size = panel2.Width / 2;
            //board flipping
            var flp = new Button();
            panel2.Controls.Add(flp);
            flp.Height = size;
            flp.Width = size;
            flp.Text = "Переворот \n" + (flipped ? "Черные" : "Белые");
            flp.Location = new Point(size * 0, size * 0);
            flp.Click += Flip_Click;

            //saving notation
            var sav = new Button();
            panel2.Controls.Add(sav);
            sav.Height = size;
            sav.Width = size;
            sav.Text = "Сохранить";
            sav.Location = new Point(size * 0, size * 1);
            sav.Click += Save_Click;

            //new game
            panel2.Controls.Add(newg);
            newg.Height = 3 * size / 2;
            newg.Width = 3 * size / 2;
            newg.Text = "Новая игра:\n" + (whitePlayer_AI ? "AI" + intelWhite.ToString() : "Игрок") + "\n vs \n" + (blackPlayer_AI ? "AI" + intelBlack.ToString() : "Игрок");
            newg.Location = new Point(size * 0, size * 4);
            newg.Click += NewGame_Click;

            // Back to menu
            var backToMenuButton = new Button();
            backToMenuButton.Height = 3 * size / 2;
            backToMenuButton.Width = 3 * size / 2;
            backToMenuButton.Text = "Вернуться в меню";
            backToMenuButton.Location = new Point(size * 0, size * 5 + 100);
            backToMenuButton.Click += (sender, e) =>
            {
                CloseDefaultChess();
                ShowGameModesSwitchPanel();
            };
            panel2.Controls.Add(backToMenuButton);

            //AI diff for white
            panel2.Controls.Add(aiw);
            aiw.Height = size / 2;
            aiw.Width = size / 2;
            aiw.Text = "AI" + intelWhite.ToString();
            aiw.Location = new Point(size * 0, size * 3);
            aiw.Tag = 0;
            aiw.Click += AISet_Click;

            //AI diff for black
            panel2.Controls.Add(aib);
            aib.Height = size / 2;
            aib.Width = size / 2;
            aib.Text = "AI" + intelBlack.ToString();
            aib.Location = new Point(size * 0, (int)(size * 3.5));
            aib.Tag = 1;
            aib.Click += AISet_Click;

            //AI increase
            var aiinc = new Button();
            panel2.Controls.Add(aiinc);
            aiinc.Height = size / 2;
            aiinc.Width = size / 2;
            aiinc.Text = "+";
            aiinc.Location = new Point((int)(size * 0.5), size * 3);
            aiinc.Click += AII_Click;

            var aidec = new Button();
            panel2.Controls.Add(aidec);
            aidec.Height = size / 2;
            aidec.Width = size / 2;
            aidec.Text = "-";
            aidec.Location = new Point((int)(size * 0.5), (int)(size * 3.5));
            aidec.Click += AID_Click;

            var plw = new Button();
            panel2.Controls.Add(plw);
            plw.Height = size / 2;
            plw.Width = 2 * size / 3;
            plw.Text = "Игрок";
            plw.Location = new Point(size * 1, (int)(size * 3));
            plw.Tag = 0;
            plw.Click += AsPlayer_Click;

            var plb = new Button();
            panel2.Controls.Add(plb);
            plb.Height = size / 2;
            plb.Width = 2 * size / 3;
            plb.Text = "Игрок";
            plb.Location = new Point(size * 1, (int)(size * 3.5));
            plb.Tag = 1;
            plb.Click += AsPlayer_Click;

            var hideanalys = new Button();
            panel2.Controls.Add(hideanalys);
            hideanalys.Height = size;
            hideanalys.Width = size;
            hideanalys.Text = "Скрыть или показать ходы";
            hideanalys.Location = new Point(size * 0, (int)(size * 2));
            hideanalys.Tag = 1;
            hideanalys.Click += HideAna_Click;

            //promotions
            for (int i = 0; i < 4; ++i)
            {
                proms[i] = new Button();
                panel3.Controls.Add(proms[i]);
                proms[i].Height = size;
                proms[i].Width = size;
                proms[i].Location = new Point(size * (i), 0);
                proms[i].Hide();
                proms[i].Enabled = false;
                proms[i].Tag = i;
                proms[i].Click += Promot_Click;
            }

            //analysis box
            panel3.Controls.Add(Analys);
            Analys.Location = new Point(panel3.Width / 2, 0);
            Analys.Width = panel3.Width / 2;
            Analys.BringToFront();
            Analys.Multiline = true;
        } // sdc

        void HideAna_Click(object sender, EventArgs e)
        {
            if (Analys.Visible)
            {
                Analys.Hide();
            }
            else
            {
                Analys.Show();
            }
        } // sdc

        private void CloseDefaultChess()
        {
            Controls.Remove(KonecHry);
            panel1.Controls.Clear();
            timer1.Stop();
            Controls.Remove(defaultChessPanel);
        } // sdc

        void NewGame_Click(object sender, EventArgs e)
        {
            //just start the game, everything else is initialized
            timer1.Stop();
            enj = new Engine();
            ulong[] BB = enj.Initialize(StartingPos);
            temporaryBitBoards = new ulong[12];
            PrintPic(new ulong[12]);
            PrintPic(BB);
            white = (Engine.Position & (1 << 4)) != 0;
            //PlayGame(enj);
            result = 0;
            KonecHry.Hide();
            KonecHry.Text = "";
            foreach (var b in ButtonBoard)
            {
                b.Enabled = true;
            }
            timer1.Start();
            if ((white && whitePlayer_AI) || (white is false && blackPlayer_AI))
            {
                moveMade = true;
            }
            else
            {
                moveMade = false;
            }
        } // sdc

        void AsPlayer_Click(object sender, EventArgs e)
        {
            Button b = (Button)sender;
            if ((int)b.Tag == 0)
            {
                whitePlayer_AI = false;
            }
            else
            {
                blackPlayer_AI = false;
            }
            newg.Text = "New game:\n" +
                        (whitePlayer_AI ? "AI" + intelWhite.ToString() : "Player")
                        + "\n vs \n" +
                        (blackPlayer_AI ? "AI" + intelBlack.ToString() : "Player");
        } // sdc

        void Flip_Click(object sender, EventArgs e)
        {
            //flips board
            flipped ^= true;
            Button b = (Button)(sender);
            b.Text = "Flip \n" + (flipped ? "Black" : "White");
            //by reorganizing buttons
            /*MakeBoard(flipped);
            bitbs = enj.GetBitBoards();
            PrintPic(bitbs);*/
            int size = panel1.Width / 9;
            if (flipped)
            {
                for (int i = 0; i < 64; ++i)
                {
                    ButtonBoard[i].Location =
                        new Point(panel1.Width / 9 + ((63 - i) & 0b111) * size, ((63 - i) >> 3) * size);
                }
                for (int i = 0; i < 8; ++i)
                {
                    rankCoords[i].Location =
                        new Point(panel1.Width / 9 - rankCoords[i].Width,
                            (7 - i) * size + (size - rankCoords[i].Height) / 2); //fixes diff between button height and textbox height
                    fileCoords[i].Location =
                        new Point((8 - i) * size, size * 8);
                }
            }

            else
            {
                for (int i = 0; i < 64; ++i)
                {
                    ButtonBoard[i].Location =
                        new Point(panel1.Width / 9 + (i & 0b111) * size, (i >> 3) * size);
                }
                for (int i = 0; i < 8; ++i)
                {
                    rankCoords[i].Location =
                        new Point(panel1.Width / 9 - rankCoords[i].Width,
                            i * size + (size - rankCoords[i].Height) / 2);
                    fileCoords[i].Location =
                        new Point((1 + i) * size, size * 8);
                }
            }
            return;
        } // sdc

        void AISet_Click(object sender, EventArgs e)
        {
            Button b = (Button)sender;
            if ((int)b.Tag == 0)
            {
                whitePlayer_AI = true;
                intelWhite = intelAI;
            }
            else
            {
                blackPlayer_AI = true;
                intelBlack = intelAI;
            }
            newg.Text = "Новая игра:\n" +
                        (whitePlayer_AI ? "AI" + intelWhite.ToString() : "Игрок")
                        + "\n vs \n" +
                        (blackPlayer_AI ? "AI" + intelBlack.ToString() : "Игрок");
        } // sdc

        void AII_Click(object sender, EventArgs e)
        { //increases diff of AI
            intelAI += 1;
            if (intelAI > 9)
                intelAI = 1;
            aiw.Text = "AI" + intelAI;
            aib.Text = "AI" + intelAI;
        } // sdc


        void Save_Click(object sender, EventArgs e)
        {
            Engine.RewritePartia("partie.txt", SaveName.Text);
        } // sdc

        void Promot_Click(object sender, EventArgs e)
        {
            var b = (Button)sender;
            promot = (int)b.Tag;
            uint nextmove = enj.CompleteMove(selectedPiece, selectedSquare, targetSquare, white);
            nextmove |= ((uint)promot << 3);
            PlayNextMove(enj, nextmove);
            for (int i = 0; i < 64; ++i)
            {
                ButtonBoard[i].BackColor = Engine.SqColor(i) ? light : dark;
            }

            ButtonBoard[selectedSquare].BackColor = last_move;
            ButtonBoard[targetSquare].BackColor = last_move;
            selectedPiece = -1;
            selectedSquare = -1;
            targetSquare = -1;
            promot = -1;

            foreach (var p in proms)
            {
                p.Enabled = false;
                p.Hide();
            }
        } // sdc

        void AID_Click(object sender, EventArgs e)
        { //decreases diff of AI
            intelAI -= 1;
            if (intelAI < 1)
                intelAI = 9;
            aiw.Text = "AI" + intelAI;
            aib.Text = "AI" + intelAI;
            return;
        } // sdc

        /*protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000;
                return cp;
            }
        }*/

        public int PlayNextMove(Engine enj, uint move) //move made by player
        {
            enj.PlayersMove(white, gamelength, move);
            notation = enj.Notation;
            gamelength += 1;
            white ^= true;
            bitbs = enj.GetBitBoards();
            PrintPic(bitbs);
            temporaryBitBoards = bitbs;
            result = enj.DetermineResult(white);
            if (result != 0)
            {
                Finish();
            }
            else if (enj.IsCheck_White() || enj.IsCheck_Black())
            {
                ShowCheck();
            }
            else
            {
                //UnshowCheck();
            }
            moveMade = true;
            return result;
        } // sdc

        #endregion

        // функции, которые не нужны
        #region Other
        private void timer2_Tick(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        #endregion

        /// <summary>
        /// Показать панель переключения режимов игры
        /// </summary>
        private void ShowGameModesSwitchPanel()
            => Controls.Add(gameModesSwitchPanel);  // -

        /// <summary>
        /// Закрыть панель переключения режимов игры
        /// </summary>
        private void CloseGameModesSwitchPanel()
            => Controls.Remove(gameModesSwitchPanel); // -

        /// <summary>
        /// Показать тест по тренировке координат
        /// </summary>
        private void ShowCoordinateTest()
        {
            Controls.Add(coordinateTestPanel);
        } // -

        private void MainFormButton_Click(object sender, EventArgs e)
        {
            var MainForm = new MainForm();
            MainForm.Show();
        }
    }
}
