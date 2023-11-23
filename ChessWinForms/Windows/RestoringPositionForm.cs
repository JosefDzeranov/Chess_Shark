using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Sachy_Obrazky.Windows
{
    public partial class RestoringPositionForm : Form
    {

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
        };

        static readonly Color light = Color.LightGray;
        static readonly Color dark = Color.DarkGray;
        /// <summary>
        /// Массив с изображением шахматных фигур
        /// </summary>
        static readonly Bitmap[] ImagesPieces = initImagesPieces();
        /// <summary>
        /// Хранилище шахматных позиций
        /// </summary>
        private IEnumerator<ChessRestoreArrangement> _storage;
        /// <summary>
        /// Величина таймера на старте
        /// </summary>
        private const int StartRecoveryTimer = 30;
        /// <summary>
        /// Время таймера
        /// </summary>
        private int recoveryTimerSecondsLeft = StartRecoveryTimer;
        /// <summary>
        /// Таймер
        /// </summary>
        private readonly System.Windows.Forms.Timer recoveryTimer = new System.Windows.Forms.Timer()
        {
            Interval = 1000,
            Enabled = false
        };
        public static bool IsChecked = false;

        private int chessCellSize = 67;
        /// <summary>
        /// Точка начала поля
        /// </summary>
        private Point startCell;

        public RestoringPositionForm()
        {
            _storage = InitializeStorage("storage.txt");
            recoveryTimer.Tick += (sender, e) => OnRecoveryTimerTick();
            DoubleBuffered = true;
            InitializeComponent();
            ShowRestorePositionTest();
            StartGame();
            nameLabel.Text = $"{StartRecoveryTimer} Секунд";
            this.nameLabel.Text = $"Имя: {Program.Name}";
        }

        /// <summary>
        /// Функция, вмещающая в себя действия, необходимые для запуска теста
        /// </summary>
        private void StartGame()
        {
            startCell = new Point((Width - (chessCellSize * 8)) / 2, 50);
            for (var y = 0; y < 8; y++)
            {
                for (var x = 0; x < 8; x++)
                {
                    var x1 = x;
                    var y1 = y;
                    var cell = new Button();
                    recoveryCells[y, x] = cell;
                    cell.Height = chessCellSize;
                    cell.Width = chessCellSize;
                    cell.Anchor = System.Windows.Forms.AnchorStyles.None;
                    cell.BackColor = GetCellColor(y, x);
                    cell.Location = new Point(startCell.X + (x * chessCellSize), startCell.Y + chessCellSize / 2 + (y * chessCellSize));
                    cell.Click += (sender, e) => HandleRecoveryBoardClick(x1, y1);
                    Controls.Add(cell);
                }
            } // создаём клетки поля
            for (var y = 0; y < 8; y++)
            {
                var textBox = new TextBox();
                textBox.Font = new Font(textBox.Font.FontFamily, 39); //for some reason, /16 works pretty well 
                textBox.Width = 35;
                textBox.Text = (8 - y).ToString();
                textBox.Location = new Point(startCell.X - 33, startCell.Y + 32 + (chessCellSize * y));
                textBox.Enabled = false;
                textBox.Anchor = System.Windows.Forms.AnchorStyles.None;
                textBox.BringToFront();
                Controls.Add(textBox);
            } // создаём вертикальные обозначения
            for (var x = 0; x < 8; x++)
            {
                var textBox = new TextBox();
                textBox.Font = new Font(textBox.Font.FontFamily, 39);
                textBox.Width = chessCellSize; //in order to fill the whole button-space
                textBox.Height = chessCellSize;
                textBox.Text = ((char)('a' + x)).ToString();
                textBox.TextAlign = HorizontalAlignment.Center;
                textBox.Location = new Point(startCell.X + (chessCellSize * x), startCell.Y + 32 + (chessCellSize * 8));
                textBox.Enabled = false;
                textBox.Anchor = System.Windows.Forms.AnchorStyles.None;
                textBox.BringToFront();
                Controls.Add(textBox);
            } // создаём горизонтальные обозначения
            groupBox.Location = new Point((Width + chessCellSize * 8) / 2 + 20, (Height - groupBox.Height) / 2 - 30);
            whiteTools = GetWhiteTools(startCell.X, startCell.Y + 15 + (10 * chessCellSize), chessCellSize);
            Controls.AddRange(whiteTools);
            blackTools = GetBlackTools(startCell.X, startCell.Y, chessCellSize);
            Controls.AddRange(blackTools);
            timerLabel.Text = "";
            difficultLabel.Text = $"Сложность: {difficult}";

            for (var i = 1; i <= 10; i++)
            {
                difficults.Items.Add(i);
            }
            difficults.SelectedIndex = 0;
        }

        /// <summary>
        /// Задаём значение цвета ячеек на всём поле
        /// </summary>
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
        }

        /// <summary>
        /// Получаем черный или белый цвет клетки в зависимости от координаты клетки
        /// </summary>
        /// <param name="y"></param>
        /// <param name="x"></param>
        /// <returns></returns>
        private Color GetCellColor(int y, int x)
        {
            var isDark = ((x + y) % 2 != 0);
            var color = isDark ? dark : light;
            return color;
        }

        /// <summary>
        /// Действия при нажатии на клетку по указанным координатам
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
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
            lastRecoveryMove = new Point(x, y); // координаты первого щелчка мыши
            if (selectedTool == ChessPiece.Pointer) // если выбрано перемещение
            {
                if (selectionQueue.Count == 0) // если нет выбранных элементов
                {
                    selectionQueue.Enqueue(new Point(x, y)); // добавляем в очередь координату первого элементв
                }
                else if (selectionQueue.Count == 1)
                {
                    var to = new Point(x, y);
                    var from = selectionQueue.Dequeue(); // удаляем начальную координату
                    currentArrangement.MovePiece(from.X, from.Y, to.X, to.Y); 
                }
            }
            else if (selectedTool == ChessPiece.Remover) // если выбрано удаление
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

            if (!referenceArrangement.EqualsArrangementInPosition(currentArrangement.Arrangement, x, y))
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

        }

        /// <summary>
        /// Счётчик таймера
        /// </summary>
        private void OnRecoveryTimerTick()
        {
            if (recoveryTimerSecondsLeft == 0)
            {
                OnRecoveryTimerLeft();
            }
            else
            {
                timerLabel.Text = $"{recoveryTimerSecondsLeft} Секунд";
                recoveryTimerSecondsLeft--;
            }
        }

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
        }

        /// <summary>
        /// Закрыть тест на "восстановление позиции"
        /// </summary>
        private void CloseRestorePositionTest()
        {
            foreach (var control in Controls)
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
            DialogResult = DialogResult.Yes;
        }


        private void OnRecoveryTimerLeft()
        {
            foreach (var control in Controls)
            {
                var panel = control as Panel;
                if (panel == null)
                {
                    continue;
                }
                if ((string)panel.Tag == "tool")
                    ((Control)control).Visible = true;
            }
            groupBox.Controls.Remove(end);
            groupBox.Controls.Remove(end);
            recoveryTimer.Enabled = false;
            recoveringIsRunning = true;
            timerLabel.Text = "";
            currentArrangement = new ChessArrangement(ChessArrangement.Empty);
            RedrawArrangement(currentArrangement.Arrangement);
        }

        /// <summary>
        /// Перерисовка изображений на всех клетках в соответствии с новой ситуацией
        /// </summary>
        /// <param name="arrangement"></param>
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
        }

        /// <summary>
        /// Прорисовка изображения на клетке
        /// </summary>
        /// <param name="b"></param>
        /// <param name="piece"></param>
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
        }


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
        }

        /// <summary>
        /// Достаём изображения из файла и получаем в массиве
        /// </summary>
        /// <returns></returns>
        static Bitmap[] initImagesPieces()
        {
            Bitmap[] ip = new Bitmap[14];
            for (int i = 0; i < 14; ++i)
            {
                ip[i] = new Bitmap(Application.StartupPath + "\\Image\\" + PieceImages[i]);
            }
            return ip;
        }

        public static Image resizeImage(Image imgToResize, Size size)
        {
            return (Image)(new Bitmap(imgToResize, size));
        }

        private void RecoveryChessPositionForm_Load(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Maximized;
        }

        private void backToMenuButton_Click(object sender, EventArgs e)
        {
            CloseRestorePositionTest();
        }

        /// <summary>
        /// Показать тест на "восстановление позиции"
        /// </summary>
        private void ShowRestorePositionTest()
        {
            difficult = 1;
            difficultLabel.Text = $"Сложность: {difficult}";
            groupBox.Controls.Remove(end);
            groupBox.Controls.Add(start);
        }

        /// <summary>
        /// Старт этапа повторения позиции после запоминания
        /// </summary>
        private void StartRecovery()
        {
            if (!_storage.MoveNext())
            {
                groupBox.Controls.Remove(end);
                var storageAreEmptyForm = new StorageAreEmptyForm();
                storageAreEmptyForm.ShowDialog();
                return;
            }
            referenceArrangement = _storage.Current.Arrangement;
            RedrawArrangement(referenceArrangement.Arrangement);
            recoveryTimer.Enabled = true;
        }

        /// <summary>
        /// Старт нового этапа запоминания
        /// </summary>
        private void StopRecovery()
        {
            timerLabel.Text = $"{StartRecoveryTimer} Секунд";
            RemoveHelpHighlight();
            recoveringIsRunning = false;
            recoveryTimerSecondsLeft = StartRecoveryTimer;
            referenceArrangement = new ChessArrangement(ChessArrangement.Empty);
            currentArrangement = new ChessArrangement(ChessArrangement.Empty);
            RedrawArrangement(referenceArrangement.Arrangement);
            recoveryTimer.Enabled = false;
        }

        /// <summary>
        /// Действия при следующей попытке
        /// </summary>
        private void NextStep()
        {
            // вести историю
            history.Add(difficult);
            groupBox.Controls.Add(end);
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
        }

        /// <summary>
        /// Действия при победе
        /// </summary>
        private void Win()
        {
            var congratulationForm = new CongratulationForm();
            congratulationForm.ShowDialog();
        }

        /// <summary>
        /// Действия при проигрыше
        /// </summary>
        private void Lose()
        {
            var losingForm = new LosingForm();
            losingForm.ShowDialog();
        }

        private bool HasPositiveGrowth()
        {
            var candidates = history.Reverse<int>().Take(3);
            if (candidates.Count() < 3)
                return true;
            var min = candidates.Min();
            var max = candidates.Max();
            return max - min >= 1;
        }


        private int GetNextDifficult(int errors)
        {
            var next = 10 - errors;
            return next <= 0 ? 1 : next;
        }

        private bool IsVictory()
        {
            return referenceArrangement.EqualsArrangement(currentArrangement.Arrangement);
        }

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
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="lastRecoveryMove"></param>
        private void HighlightIfCorrectMove(Point lastRecoveryMove)
        {
            if (referenceArrangement.EqualsArrangementInPosition(currentArrangement.Arrangement, lastRecoveryMove.X, lastRecoveryMove.Y))
            {
                recoveryCells[lastRecoveryMove.Y, lastRecoveryMove.X].BackColor = Color.Green;
            }
            else
            {
                recoveryCells[lastRecoveryMove.Y, lastRecoveryMove.X].BackColor = Color.Red;
            }
        }

        /// <summary>
        /// Присвоить в текущий активный инструмент переданный инструмент "tool"
        /// </summary>
        /// <param name="tool"></param>
        private void SelectTool(ChessPiece tool)
        {
            selectionQueue.Clear();
            selectedTool = tool;
        }

        /// <summary>
        /// Создаём белые фигуры и инструменты, используемые для расстановки
        /// </summary>
        /// <param name="startX"></param>
        /// <param name="startY"></param>
        /// <param name="cellSize"></param>
        /// <returns></returns>
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
        }

        /// <summary>
        /// Создаём черные фигуры и инструменты, используемые для расстановки
        /// </summary>
        /// <param name="startX"></param>
        /// <param name="startY"></param>
        /// <param name="cellSize"></param>
        /// <returns></returns>
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
        }

        /// <summary>
        /// Создание фигуры/инструмента в указанных точках
        /// </summary>
        /// <param name="controls"></param>
        /// <param name="index"></param>
        /// <param name="startX"></param>
        /// <param name="startY"></param>
        /// <param name="cellSize"></param>
        /// <param name="piece"></param>
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
            tool.Anchor = AnchorStyles.None;
            controls.Add(tool);
            ImagePrint(tool, (int)piece);
        }

        private void start_Click(object sender, EventArgs e)
        {
            Start();
        }

        private void end_Click(object sender, EventArgs e)
        {
            OnRecoveryTimerLeft();
        }

        /// <summary>
        /// Дейтвия, при нажатии на кнопку start="Запомнил"
        /// </summary>
        private void Start()
        {
            foreach (var control in Controls)
            {
                var panel = control as Panel;
                if (panel == null)
                {
                    continue;
                }
                if ((string)panel.Tag == "tool")
                    ((Control)control).Visible = false;
            }
            history.Clear();
            currentArrangementId = 1;
            recoveryErrors = 0;
            move = 0;
            history.Clear();
            difficult = 1;
            difficultLabel.Text = $"Сложность: {difficult}";

            groupBox.Controls.Remove(start);
            groupBox.Controls.Add(end);
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
        }
    }
}
