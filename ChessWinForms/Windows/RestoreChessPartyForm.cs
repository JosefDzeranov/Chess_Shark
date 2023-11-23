using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Sachy_Obrazky.Models;
using Sachy_Obrazky.Repository;
using Button = System.Windows.Forms.Button;
using TextBox = System.Windows.Forms.TextBox;

namespace Sachy_Obrazky.Windows
{
    public sealed partial class RestoreChessPartyForm : Form
    {
        #region TimerPlaybackParty
        /// <summary>
        /// Таймер воспроизведения партии
        /// </summary>
        private readonly System.Windows.Forms.Timer timerPlaybackParty = new System.Windows.Forms.Timer()
        {
            Interval = 1000,
            Enabled = false
        };
        /// <summary>
        /// Значение таймера воспроизведения партии
        /// </summary>
        private int timerPlaybackPartyTimerSeconds { get; set; }

        #endregion

        #region TimerRecovery
        /// <summary>
        /// Величина таймера на старте
        /// </summary>
        private const int timerRecoveryStart = 60 * 3;
        /// <summary>
        /// Время таймера
        /// </summary>
        private int timerRecoverySecondsLeft = timerRecoveryStart;
        /// <summary>
        /// Таймер, использующийся для отсчёта времени на запоминание текущей партии
        /// </summary>
        private readonly System.Windows.Forms.Timer timerRecovery = new System.Windows.Forms.Timer()
        {
            Interval = 1000,
            Enabled = false
        };
        #endregion

        #region TimerPlayer
        /// <summary>
        /// Величина таймера нужная для показа следующего шага
        /// </summary>
        private const int timerPlayerSize = 3;
        /// <summary>
        /// Переменная виличина таймера нужная для показа следующего шага
        /// </summary>
        private int timerPlayerTime = timerPlayerSize;
        /// <summary>
        /// Таймер плеера
        /// </summary>
        private readonly System.Windows.Forms.Timer timerPlayer = new System.Windows.Forms.Timer()
        {
            Interval = 1000,
            Enabled = false
        };
        #endregion

        public static string[] PieceImages = {
            "Wking_light",
            "Wpawn_light",
            "Wknight_light",
            "Wbishop_light",
            "Wrook_light",
            "Wqueen_light",
            "Bking_light",
            "Bpawn_light",
            "Bknight_light",
            "Bbishop_light",
            "Brook_light",
            "Bqueen_light",
            "Pointer",
            "Remover"
        };

        public static string[] PlayerImages = {
            "start_party", // 0
            "previous_button",  // 1
            "play",  // 2
            "pause",  // 3
            "next_step",  // 4
            "end_party"  // 5
        };

        static readonly Color _light = Color.LightGray;

        static readonly Color _dark = Color.DarkGray;

        private const int WM_SYSCOMMAND = 0x0112;
        private const int SC_MINIMIZE = 0xF020;

        /// <summary>
        /// Массив с изображением шахматных фигур
        /// </summary>
        private readonly Bitmap[] _imagesPieces;

        /// <summary>
        /// Массив с изображением элементов плеера
        /// </summary>
        private readonly Bitmap[] _imagesPlayer;

        /// <summary>
        /// Хранилище шахматных позиций (2 версия)
        /// </summary>
        private PartyRepository _storageParty;

        /// <summary>
        /// Текущая партия
        /// </summary>
        private ChessPartyHistory currentParty;

        /// <summary>
        /// Хранилище пользователей
        /// </summary>
        private PlayerRepository players = new PlayerRepository();

        /// <summary>
        /// Данные текущего игрока
        /// </summary>
        private Player player { get; set; }

        /// <summary>
        /// Текущая попытка по восстановлению партии
        /// </summary>
        private Attept currentAttept { get; set; }

        /// <summary>
        /// Максимальное количество ошибок на один шаг в партии
        /// </summary>
        private int limitErrors = 5;

        //public static bool IsChecked = false;

        //private bool stageMemorizingParty = true;

        private int chessCellSize = 67;

        /// <summary>
        /// Точка начала поля
        /// </summary>
        private Point startCell;



        #region  Constructor
        public RestoreChessPartyForm()
        {
            //Resize += RestoreChessPartyForm_Resize;
            player = players.Find(MainForm.authenticationPlayer);
            DoubleBuffered = true;
            _imagesPieces = initImages(PieceImages);
            _imagesPlayer = initImages(PlayerImages);
            _storageParty = new PartyRepository();
            currentParty = GetRandomParty();
            InitializeComponent();
            СreateCellField();
            CreateVerticalMarking();
            CreateHorizontalMarking();
            //CreateTools();
            CreatePlayerElements();

            timerRecovery.Tick += (sender, e) => OnRecoveryTimerTick();
            timerPlayer.Tick += (sender, e) => OnPlayerTimerTick();
            timerPlaybackParty.Tick += (sender, e) => OnPlaybackPartyTimerTick();
            timerLabel.Text = "";
            playbackPartyTimerLabel.Text = "";
            errors_label.Text = "";
            CountStepLabel.Text = "";
            trueAnswer_label.Text = "";
            nameLabel.Text = $"Имя: {player.Name}";
            namePartyLabel.Text = "";
            groupBox.Location = new Point((Width + chessCellSize * 8) / 2 + 20, (Height - groupBox.Height) / 2 - 45);
            groupBox.Controls.Remove(endMemorizing);
            groupBox.Controls.Add(startMemorizing);
            players.Find(Guid.Empty);
            namePartyLabel.Location = new Point(namePartyLabel.Location.X, startCell.Y*2 + 20 );
        }



        /// <summary>
        /// Достаём изображения элементов из файла и получаем в массиве Bitmap
        /// </summary>
        /// <returns></returns>
        static Bitmap[] initImages(string[] arr)
        {
            Bitmap[] ip = new Bitmap[arr.Length];
            for (int i = 0; i < arr.Length; ++i)
            {
                ip[i] = new Bitmap(Application.StartupPath + "\\Image\\" + arr[i] + ".png");
            }
            return ip;
        }

        /// <summary>
        /// Выбор случайной партии из доступных
        /// </summary>
        /// <returns></returns>
        private ChessPartyHistory GetRandomParty()
        {
            var rnd = new Random().Next(0, _storageParty.GetAll().Count);
            var party = _storageParty.GetAll()[rnd];
            currentAttept = new Attept(party.Id);
            player.AtteptsRestoreChessParty.Add(currentAttept);
            players.Update(player);
            return new ChessPartyHistory(party.Name, party.Steps);
        }

        /// <summary>
        /// Создаём клетки поля
        /// </summary>
        private void СreateCellField()
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
                    cell.Location = new Point(startCell.X + (x * chessCellSize), startCell.Y + chessCellSize + (y * chessCellSize));
                    cell.Click += (sender, e) => HandleRecoveryBoardClick(x1, y1);
                    Controls.Add(cell);


                }
            }

            var t = this.namePartyLabel.Location.Y;
            

        }

        /// <summary>
        /// Cоздаём вертикальную разметку
        /// </summary>
        private void CreateVerticalMarking()
        {
            for (var y = 0; y < 8; y++)
            {
                var textBox = new TextBox();
                textBox.Font = new Font(textBox.Font.FontFamily, 39); //for some reason, /16 works pretty well 
                textBox.Width = 35;
                textBox.Text = (8 - y).ToString();
                textBox.Location = new Point(startCell.X - 33, startCell.Y * 2 + 17 + (chessCellSize * y));
                textBox.Enabled = false;
                textBox.Anchor = System.Windows.Forms.AnchorStyles.None;
                textBox.BringToFront();
                Controls.Add(textBox);
            }
        }

        /// <summary>
        /// Создаём горизонтальную разметку
        /// </summary>
        private void CreateHorizontalMarking()
        {
            for (var x = 0; x < 8; x++)
            {
                var textBox = new TextBox();
                textBox.Font = new Font(textBox.Font.FontFamily, 39);
                textBox.Width = chessCellSize; //in order to fill the whole button-space
                textBox.Height = chessCellSize;
                textBox.Text = ((char)('a' + x)).ToString();
                textBox.TextAlign = HorizontalAlignment.Center;
                textBox.Location = new Point(startCell.X + (chessCellSize * x), startCell.Y + (chessCellSize * 9));
                textBox.Enabled = false;
                textBox.Anchor = System.Windows.Forms.AnchorStyles.None;
                textBox.BringToFront();
                Controls.Add(textBox);
            }
        }

        /// <summary>
        /// Создаём инструменты для управления шахматами
        /// </summary>
        private void CreateTools()
        {
            whiteTools = GetWhiteTools(startCell.X,  15 + (10 * chessCellSize), chessCellSize);
            Controls.AddRange(whiteTools);
            blackTools = GetBlackTools(startCell.X, startCell.Y, chessCellSize);
            Controls.AddRange(blackTools);
        }

        /// <summary>
        /// Создаём элементы для "плейера" (проигрыание ходов партии)
        /// </summary>
        private void CreatePlayerElements()
        {
            elementsPlayer = new Panel[elementsPlayer.Length];
            for (int i = 0; i < elementsPlayer.Length; i++)
            {
                int x0 = (Width + chessCellSize * 8) / 2 + 20;//startCell.X + (8 * chessCellSize) / 2 - chessCellSize * 5 / 2;
                int y0 = (Height - groupBox.Height) / 2 - 45 + 270;//Convert.ToInt32(startCell.Y + (10 * chessCellSize) + 15);
                var element = new Panel();
                elementsPlayer[i] = element;
                Controls.Add(element);
                element.Height = Convert.ToInt32(chessCellSize/2.6);
                element.Width = Convert.ToInt32(chessCellSize /2.6);
                
                element.Anchor = System.Windows.Forms.AnchorStyles.None;
                element.Name = PlayerImages[i] + "_button";
                element.Text = PlayerImages[i] + "_button";
                element.Visible = false;
                element.BackColor = ColorTranslator.FromHtml("#F0F0F0");
                //element.BackColor = Color.Aqua;
                element.Location = new Point(x0 + (i * element.Width), y0);
                ImagePrint(element, (int)i, _imagesPlayer);
                switch (PlayerImages[i])
                {
                    case ("start_party"):
                        {
                            element.Tag = "start_party";
                            element.Click += this.StartParty_Player_Click;
                            element.Location = new Point(x0 + (0 * element.Width), y0);
                            break;
                        }
                    case ("previous_button"):
                        {
                            element.Tag = "previous_button";
                            element.Click += this.PreviousStep_Player_Click;
                            element.Location = new Point(x0 + (1 * element.Width), y0);
                            break;
                        }
                    case ("play"):
                        {
                            element.Tag = "play";
                            element.Click += this.Play_Player_Click;
                            element.Location = new Point(x0 + (2 * element.Width), y0);
                            break;
                        }
                    case ("pause"):
                        {
                            element.Tag = "pause";
                            element.Click += this.Pause_Player_Click;
                            element.Location = new Point(x0 + (2 * element.Width), y0);
                            break;
                        }
                    case ("next_step"):
                        {
                            element.Tag = "next_stepe";
                            element.Click += this.NextStep_Player_Click;
                            element.Location = new Point(x0 + (3 * element.Width), y0);
                            break;
                        }
                    case ("end_party"):
                        {
                            element.Tag = "end_party";
                            element.Click += this.EndPartyStep_Player_Click;
                            element.Location = new Point(x0 + (4 * element.Width), y0);
                            break;
                        }
                    default:
                        break;
                }
            }
        }

        /// <summary>
        /// Действия при тике recovery таймера
        /// </summary>
        private void OnRecoveryTimerTick()
        {
            if (timerRecoverySecondsLeft == 0)
            {
                StartTestingForMemoryParty();
            }
            else
            {
                timerLabel.Text = $"{timerRecoverySecondsLeft} Секунд";
                timerRecoverySecondsLeft--;
            }
        }

        /// <summary>
        /// Действия при тике timerPlaybackParty таймера
        /// </summary>
        private void OnPlaybackPartyTimerTick()
        {
            playbackPartyTimerLabel.Text = $"Прошло: {timerPlaybackPartyTimerSeconds} секунд";
            timerPlaybackPartyTimerSeconds++;
        }

        /// <summary>
        /// Действия при тике player таймера
        /// </summary>
        private void OnPlayerTimerTick()
        {
            if (timerPlayerTime <= 0)
            {
                var x = currentParty.GetNumberCurrentStep();
                var y = currentParty.GetNumberEndStep();

                if (currentParty.GetNumberCurrentStep() == currentParty.GetNumberEndStep())
                {
                    timerPlayer.Stop();
                    foreach (var element in elementsPlayer)
                    {
                        if (element.Tag == "play")
                        {
                            element.Visible = true;
                        }
                        if (element.Tag == "pause")
                        {
                            element.Visible = false;
                        }
                    }
                }
                else
                {
                    var pos = currentParty.GetNextPosition();
                    CountStepLabel.Text = $"Ход {currentParty.GetNumberCurrentStep()}";
                    timerPlayerTime = timerPlayerSize;
                    RedrawArrangement(pos.Arrangement);
                }
            }
            else
            {
                timerPlayerTime--;
            }
        }

        /// <summary>
        /// Действия при загрузке окна
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RecoveryChessPositionForm_Load(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Maximized;  // включаем полноэкранный режим
        }
        #endregion

        /// <summary>
        /// Действие при нажатии на кнопку возврата в меню
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void backToMenuButton_Click(object sender, EventArgs e)
        {
            currentAttept.ReasonCompletion = ReasonCompletionAttempt.ExitMenu;
            players.Update(player);
            CloseRestoreChessParty();
        }

        /// <summary>
        /// Закрыть тест
        /// </summary>
        private void CloseRestoreChessParty()
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
            timerPlaybackParty.Stop();
            DialogResult = DialogResult.Yes;
        }

        #region The stage of memorizing te party

        private void StartMemorizing_Click(object sender, EventArgs e)
        {
            StartMemorizing();
        }

        /// <summary>
        /// Старт этапа запоминания позиций текущей партии
        /// </summary>
        private void StartMemorizing()
        {
            VisibleTools_StageMemorizingParty();
            timerPlayer.Start();
            timerRecovery.Start();
            recoveryErrors = 0;

            CountStepLabel.Text = $"Ход {currentParty.GetNumberCurrentStep()}";
            groupBox.Controls.Remove(startMemorizing);
            groupBox.Controls.Add(endMemorizing);
            SetDefoultColorCells();
            currentArrangement = currentParty.GetStartPosition();
            namePartyLabel.Text = currentParty.GetName();
            RedrawArrangement(currentArrangement.Arrangement);
            desiredArrangement = currentParty.partyHistory[0];
        }

        /// <summary>
        /// Определение видимости элементов на этапе запоминания ходов партии
        /// </summary>
        private void VisibleTools_StageMemorizingParty()
        {
            foreach (var control in Controls)
            {
                var panel = control as Panel;
                if (panel == null)
                {
                    continue;
                }
                foreach (var name in PlayerImages)
                {
                    if (panel.Name.Contains(name) && name != "play")
                    {
                        panel.Visible = true;
                    }
                }
                if ((string)panel.Tag == "tool")
                    ((Control)control).Visible = false;
            }
        }

        private void EndMemorizing_Click(object sender, EventArgs e)
        {
            StartTestingForMemoryParty();
        }

        #endregion

        #region Player
        private void EndPartyStep_Player_Click(object sender, EventArgs e)
        {
            var pos = currentParty.GetEndPosition();
            CountStepLabel.Text = $"Ход {currentParty.GetNumberCurrentStep()}";
            RedrawArrangement(pos.Arrangement);
        }

        private void NextStep_Player_Click(object sender, EventArgs e)
        {
            var pos = currentParty.GetNextPosition();
            CountStepLabel.Text = $"Ход {currentParty.GetNumberCurrentStep()}";
            RedrawArrangement(pos.Arrangement);
        }

        private void Pause_Player_Click(object sender, EventArgs e)
        {
            timerPlayer.Stop();
            foreach (var element in elementsPlayer)
            {
                if (element.Tag == "play")
                {
                    element.Visible = true;
                }
                if (element.Tag == "pause")
                {
                    element.Visible = false;
                }
            }
        }

        private void Play_Player_Click(object sender, EventArgs e)
        {
            timerPlayer.Start();
            foreach (var element in elementsPlayer)
            {
                if (element.Tag == "play")
                {
                    element.Visible = false;
                }
                if (element.Tag == "pause")
                {
                    element.Visible = true;
                }
            }
        }

        private void PreviousStep_Player_Click(object sender, EventArgs e)
        {
            var pos = currentParty.GetPreviousPosition();
            CountStepLabel.Text = $"Ход {currentParty.GetNumberCurrentStep()}";
            RedrawArrangement(pos.Arrangement);
        }

        private void StartParty_Player_Click(object sender, EventArgs e)
        {
            var pos = currentParty.GetStartPosition();
            CountStepLabel.Text = $"Ход {currentParty.GetNumberCurrentStep()}";
            RedrawArrangement(pos.Arrangement);
        }
        #endregion

        #region Testing the correct memorization of the moves of the game

        /// <summary>
        /// Действия при запуске этапа повторения ходов текущей партии
        /// </summary>
        private void StartTestingForMemoryParty()
        {
            timerRecovery.Stop();
            timerPlaybackParty.Start();
            timerPlayer.Stop();
            memorizingIsRunning = true;
            VisibleTools_TestingForMemoryParty();
            SetDefoultColorCells();
            timerLabel.Text = "";
            currentArrangement = new ChessArrangement(currentParty.GetStartPosition().Arrangement);
            RedrawArrangement(currentArrangement.Arrangement);
            desiredArrangement = new ChessArrangement(currentParty.GetNextPosition().Arrangement);
            //
            groupBox.Controls.Remove(endMemorizing);
            CountStepLabel.Text = $"Ход {currentParty.GetNumberCurrentStep()}";
            //player.AtteptsRestoreChessParty.Last().Steps.Add(new Step());
        }

        /// <summary>
        /// Показать инструменты для работе в режиме повторения партии
        /// </summary>
        private void VisibleTools_TestingForMemoryParty()
        {
            foreach (var control in Controls)
            {
                var panel = control as Panel;

                if (panel == null)
                {
                    continue;
                }
                foreach (var name in PlayerImages)
                {
                    if (panel.Name.Contains(name))
                    {
                        panel.Visible = false;
                    }
                }
                if ((string)panel.Tag == "tool")
                    ((Control)control).Visible = true;
            }
        }

        /// <summary>
        /// Действия при нажатии на клетку по указанным координатам
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        private void HandleRecoveryBoardClick(int x, int y)
        {
            if (!memorizingIsRunning)
                return;
            SetDefoultColorCells();

            if (selectedTool == ChessPiece.Pointer) // если выбрано перемещение
            {
                if (selectionQueue.Count == 0) // если нет выбранных элементов
                {
                    selectionQueue.Enqueue(new Point(x, y)); // добавляем в очередь координату первого элементов
                    lastRecoveryMove = new Point(x, y); // координаты первого щелчка мыши
                }
                else if (selectionQueue.Count == 1) // если это уже вторая координата то:
                {
                    var to = new Point(x, y);
                    HighlightIfCorrectMove(to);

                    var from = selectionQueue.Dequeue(); // удаляем начальную координату и выводим ту, что встала после неё
                    HighlightIfCorrectMove(from);
                    if (CheckForCastling(from, to))
                    {
                        Castling(from, to);
                    }
                    else
                    {
                        currentArrangement.MovePiece(from.X, from.Y, to.X, to.Y);
                        var v1 = desiredArrangement.EqualsArrangementInPosition(currentArrangement.Arrangement, x, y);
                        var v2 = selectionQueue.Count == 0;
                        if (desiredArrangement.EqualsArrangementInPosition(currentArrangement.Arrangement, x, y) &
                            selectionQueue.Count == 0 & from != to)
                        {
                            if (currentArrangement.GetPiece(from.X, from.Y) != ChessPiece.Empty)
                            {
                                currentArrangement.RemovePiece(from.X, from.Y);
                            }
                        }
                        else
                        {
                            currentArrangement.MovePiece(x, y, lastRecoveryMove.X, lastRecoveryMove.Y);
                        }
                    }
                }
            }
            else if (selectedTool == ChessPiece.Remover) // если выбрано удаление фигуры
            {
                currentArrangement.RemovePiece(x, y);
                HighlightIfCorrectMove(new Point(x, y));
            }
            else
            {
                currentArrangement.SetPiece(x, y, selectedTool);
            }

            //CheckCorrectnessMoveMade(x, y);

            // возвращаем фигуры обратно если была ошибка
            if (!desiredArrangement.EqualsArrangementInPosition(currentArrangement.Arrangement, x, y) &
                selectionQueue.Count == 0)
            {
                currentArrangement.MovePiece(x, y, lastRecoveryMove.X, lastRecoveryMove.Y);
            }
            else if(selectionQueue.Count == 0)
            {
                players.Update(player);
            }

            RedrawArrangement(currentArrangement.Arrangement);

            CheckCorrectnessPositionStep(x, y);
        }

        /// <summary>
        /// Производим рокировку
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <exception cref="NotImplementedException"></exception>
        private void Castling(Point from, Point to)
        {

            var kw = new Point(4, 0);
            var kb = new Point(4, 7);
            var rw1 = new Point(0, 0);
            var rw2 = new Point(7, 0);
            var rb1 = new Point(0, 7);
            var rb2 = new Point(7, 7);
            if ((from == kw && to == rw1) || (from == rw1 && to == kw))
            {
                CastilingLargeWhite();
            };
            if ((from == kw && to == rw2) || (from == rw2 && to == kw))
            {
                CastilingSmallWhite();
            };

            if ((from == kb && to == rb1) || (from == rb1 && to == kb))
            {
                CastilingLargeBlack();
            };
            if ((from == kb && to == rb2) || (from == rb2 && to == kb))
            {
                CastilingSmallBlack();
            };
        }

        private void CastilingSmallBlack()
        {
            var field = currentArrangement;
            var k = new Point(4, 7);
            var r = new Point(7, 7);
            field.MovePiece(k.X, k.Y, k.X + 2, k.Y);
            field.MovePiece(r.X, r.Y, r.X - 2, r.Y);
        }

        private void CastilingSmallWhite()
        {
            var field = currentArrangement;
            var k = new Point(4, 0);
            var r = new Point(7, 0);
            field.MovePiece(k.X, k.Y, k.X + 2, k.Y);
            field.MovePiece(r.X, r.Y, r.X - 2, r.Y);
        }

        private void CastilingLargeBlack()
        {
            var field = currentArrangement;
            var k = new Point(4, 7);
            var r = new Point(0, 7);
            field.MovePiece(k.X, k.Y, k.X - 2, k.Y);
            field.MovePiece(r.X, r.Y, r.X + 3, r.Y);
        }

        private void CastilingLargeWhite()
        {
            var field = currentArrangement;
            var k = new Point(4, 0);
            var r = new Point(0, 0);
            field.MovePiece(k.X, k.Y, k.X - 2, k.Y);
            field.MovePiece(r.X, r.Y, r.X + 3, r.Y);
        }

        /// <summary>
        /// Определяем, это рокировка или нет
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        private bool CheckForCastling(Point from, Point to)
        {
            var field = currentArrangement;
            var e = ChessPiece.Empty;
            var kw = new Point(4, 0);
            var kb = new Point(4, 7);
            var rw1 = new Point(0, 0);
            var rw2 = new Point(7, 0);
            var rb1 = new Point(0, 7);
            var rb2 = new Point(7, 7);
            if (from == kw && to == rw1)
            {
                if (field.GetPiece(rw1.X + 1, rw1.Y) == e ||
                    field.GetPiece(rw1.X + 2, rw1.Y) == e ||
                    field.GetPiece(rw1.X + 3, rw1.Y) == e)
                {
                    return true;
                }
            }

            if (from == kw && to == rw2)
            {
                if (field.GetPiece(rw2.X - 1, rw2.Y) == e ||
                    field.GetPiece(rw2.X - 2, rw2.Y) == e)
                {
                    return true;
                }
            }
            if (from == rw1 && to == kw)
            {
                if (field.GetPiece(rw1.X + 1, rw1.Y) == e ||
                    field.GetPiece(rw1.X + 2, rw1.Y) == e ||
                    field.GetPiece(rw1.X + 3, rw1.Y) == e)
                {
                    return true;
                }
            }
            if (from == rw2 && to == kw)
            {
                if (field.GetPiece(rw2.X - 1, rw2.Y) == e ||
                    field.GetPiece(rw2.X - 2, rw2.Y) == e)
                {
                    return true;
                }
            }
            if (from == kb && to == rb1)
            {
                if (field.GetPiece(rb1.X + 1, rb1.Y) == e ||
                    field.GetPiece(rb1.X + 2, rb1.Y) == e ||
                    field.GetPiece(rb1.X + 3, rb1.Y) == e)
                {
                    return true;
                }
            }
            if (from == kb && to == rb2)
            {
                if (field.GetPiece(rb2.X - 1, rb2.Y) == e ||
                    field.GetPiece(rb2.X - 2, rb2.Y) == e)
                {
                    return true;
                }
            }
            if (from == rb1 && to == kb)
            {
                if (field.GetPiece(rb1.X + 1, rb1.Y) == e ||
                    field.GetPiece(rb1.X + 2, rb1.Y) == e ||
                    field.GetPiece(rb1.X + 3, rb1.Y) == e)
                {
                    return true;
                }
            }
            if (from == rb2 && to == kb)
            {
                if (field.GetPiece(rb2.X - 1, rb2.Y) == e ||
                    field.GetPiece(rb2.X - 2, rb2.Y) == e)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Определение цвета в зависимости от того, соответствует ли выставленная фигура образцу
        /// </summary>
        /// <param name="lastRecoveryMove"></param>
        private void HighlightIfCorrectMove(Point lastRecoveryMove)
        {
            var a = desiredArrangement.EqualsArrangementInPosition(currentArrangement.Arrangement, lastRecoveryMove.X,
                lastRecoveryMove.Y);
            if (!desiredArrangement.EqualsArrangementInPosition(currentArrangement.Arrangement, lastRecoveryMove.X, lastRecoveryMove.Y))
            {
                recoveryCells[lastRecoveryMove.Y, lastRecoveryMove.X].BackColor = Color.Green;
            }
            else
            {
                recoveryCells[lastRecoveryMove.Y, lastRecoveryMove.X].BackColor = Color.Red;
            }
        }

        /// <summary>
        /// Проверка на максимальное количество ошибок
        /// </summary>
        /// <returns></returns>
        private bool CheckOnLimitErrors()
        {
            if (recoveryErrors > limitErrors) return true;
            return false;
        }

        /// <summary>
        /// Действия при проигрыше
        /// </summary>
        private void Lose()
        {
            player.AtteptsRestoreChessParty.Last().ReasonCompletion = ReasonCompletionAttempt.ExceedingErrorsLimit;
            players.Update(player);
            timerPlaybackParty.Stop();
            var losingForm = new LosingForm();
            losingForm.ShowDialog();
            CloseRestoreChessParty();
        }

        /// <summary>
        /// Проверка и действия в случае, если позиции всех фигур на поле СООТВЕТСТВУЮТ требуемой
        /// </summary>
        private void CheckCorrectnessPositionStep(int x, int y)
        {
            if (desiredArrangement.EqualsArrangement(currentArrangement.Arrangement)) // если позиция фигур на поле СООТВЕТСТВУЕТ требуемой, то: 
            {
                if (currentParty.GetNumberCurrentStep() == currentParty.GetNumberEndStep())
                {
                    player.AtteptsRestoreChessParty.Last().Steps.Last().AddCorrectAnsver(new[] { x, y });
                    player.AtteptsRestoreChessParty.Last().ReasonCompletion = ReasonCompletionAttempt.Win;
                    player.AtteptsRestoreChessParty.Last().Time = timerPlaybackPartyTimerSeconds;
                    players.Update(player);
                    timerPlaybackParty.Stop();
                    DialogResult result = MessageBox.Show(
                        $"Поздравляем! Вы успешно повторили всю партию \"{currentParty.GetName()}\" за {currentAttept.GetTime()} секунд",
                        "Победа",
                        MessageBoxButtons.OK
                    );
                    if (result == DialogResult.OK)
                    {
                        CloseRestoreChessParty();
                    }
                }
                else
                {
                    if (player.AtteptsRestoreChessParty.Last().Steps.Count == 0)
                    {
                        player.AtteptsRestoreChessParty.Last().Steps.Add(new Step());
                    }
                    player.AtteptsRestoreChessParty.Last().Steps.Last().AddCorrectAnsver(new[] { x, y });
                    players.Update(player);

                    TestingNextStep();
                }
            }
            else if(selectionQueue.Count == 0)
            {
                recoveryErrors++;
                errors_label.Text = $"Ошибок: {recoveryErrors}";
                if (player.AtteptsRestoreChessParty.Last().Steps.Count == 0)
                {
                    player.AtteptsRestoreChessParty.Last().Steps.Add(new Step());
                }
                player.AtteptsRestoreChessParty.Last().Steps.Last().AddIncorrectAnsver(new[] { x, y });
                if (CheckOnLimitErrors())
                {
                    Lose();
                }
                players.Update(player);
            }
        }

        /// <summary>
        /// Переход к проверке следующего хода
        /// </summary>
        private void TestingNextStep()
        {
            recoveryErrors = 0;
            errors_label.Text = "";
            trueAnswer_label.Text = $"Выполненно {currentParty.GetNumberCurrentStep()}/{currentParty.GetNumberEndStep()} ходов";
            desiredArrangement = currentParty.GetNextPosition();
            CountStepLabel.Text = $"Ход {currentParty.GetNumberCurrentStep()}";
            player.AtteptsRestoreChessParty.Last().Steps.Add(new Step());
        }
        #endregion

        #region Tools
        /// <summary>
        /// Действие при сворачивании формы
        /// </summary>
        /// <param name="m"></param>
        protected override void WndProc(ref Message m)
        {
            if (m.Msg == WM_SYSCOMMAND)
            {
                if (m.WParam.ToInt32() == SC_MINIMIZE)
                {
                    DialogResult result = MessageBox.Show(
                        $"Если вы свернёте окно - текущая попытка завершится досрочно \n Завершить попытку?",
                        "Внимание!",
                        MessageBoxButtons.YesNo
                    );
                    if (result == DialogResult.Yes)
                    {
                        player.AtteptsRestoreChessParty.Last().ReasonCompletion = ReasonCompletionAttempt.HidingWindow;
                        players.Update(player);
                        CloseRestoreChessParty();
                    }
                    if (result == DialogResult.No)
                    {
                        var w = this.WindowState;
                        this.WindowState = w;
                    }
                    return;
                }
            }
            base.WndProc(ref m);
        }

        /// <summary>
        /// Действие при закрытии формы
        /// </summary>
        /// <param name="m"></param>
        private void RestoreChessPartyForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (player.AtteptsRestoreChessParty.Last().ReasonCompletion == ReasonCompletionAttempt.Nothing)
            {
                player.AtteptsRestoreChessParty.Last().ReasonCompletion = ReasonCompletionAttempt.CloseWindow;
                players.Update(player);
            }
            CloseRestoreChessParty();
        }

        /// <summary>
        /// Задаём значение цвета ячеек на всём поле
        /// </summary>
        private void SetDefoultColorCells()
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
            var color = isDark ? _dark : _light;
            return color;
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
        /// Отрисовка изображения на клетке
        /// </summary>
        /// <param name="b"></param>
        /// <param name="piece"></param>
        public void ImagePrint(Button b, int piece)
        {
            const double ScaleRatio = 0.8;
            int width = (int)(b.Width * ScaleRatio);
            int height = (int)(b.Height * ScaleRatio);

            Bitmap originImage = _imagesPieces[piece];
            originImage.MakeTransparent(Color.FromArgb(200, 200, 200));
            Image image = ResizeImage(originImage, new Size(height, width));

            b.Image = image;
        }

        /// <summary>
        /// Отрисовка изображения на панели
        /// </summary>
        /// <param name="b"></param>
        /// <param name="piece"></param>
        /// <param name="arr"></param>
        public void ImagePrint(Panel b, int piece, Bitmap[] arr)
        {
            const double ScaleRatio = 1;
            int width = (int)(b.Width * ScaleRatio);
            int height = (int)(b.Height * ScaleRatio);

            Bitmap originImage = arr[piece];
            originImage.MakeTransparent(Color.FromArgb(200, 200, 200));
            Image image = ResizeImage(originImage, new Size(height, width));

            b.BackgroundImage = image;
        }

        /// <summary>
        /// Изменение размера изображения
        /// </summary>
        /// <param name="imgToResize"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public static Image ResizeImage(Image imgToResize, Size size)
        {
            return (Image)(new Bitmap(imgToResize, size));
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
            ImagePrint(tool, (int)piece, _imagesPieces);
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
        #endregion


    }
}
