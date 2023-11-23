using System;
using System.Drawing;
using System.Windows.Forms;

namespace Sachy_Obrazky.Windows
{
    public partial class CoordinateTestForm : Form
    {
        private int _coordinateTimerSecondsLeft = 60;
        private static bool _isWhiteCoordinateTest = true;
        private readonly Timer _coordinateTimer = new Timer()
        {
            Interval = 1000,
            Enabled = false
        };
        int chessCellSize = 67;
        private int startCellX => playButtonForWhite.Width + playButtonForWhite.Location.X + 20;
        private  int startCellY => 100 ;

        static readonly Color light = Color.LightGray;
        static readonly Color dark = Color.DarkGray;



        public CoordinateTestForm()
        {
            _coordinateTimer.Tick += (sender, e) => OnCoordinateTimerTick();
            DoubleBuffered = true;
            InitializeComponent();
            CreateChessBoard();
            CreateAccuracyBar();
            this.nameLabel.Text = $"Имя: {Program.Name}";
            cellToRestoreLabel.Text = "";
        }

        /// <summary>
        /// Счётчик, с выводом убывающего таймена на форму
        /// </summary>
        private void OnCoordinateTimerTick()
        {
            if (_coordinateTimerSecondsLeft == 0)
            {
                EndGameAndShowResults(true);
            }
            _coordinateTimerSecondsLeft--;
            timerLabel.Text = $"{_coordinateTimerSecondsLeft} Секунд";
        }

        private void EndGameAndShowResults(bool defaultEndGame)
        {
            Program.LastScore = successCount;
            Program.LastErrors = errors;
            StopCoordinateTest();
            if (defaultEndGame)
                ShowGameResults("Игра окончена по истечению таймера");
            else
                ShowGameResults("Игра окончена досрочно");
        }

        private void StopCoordinateTest()
        {
            if (_coordinateTimer.Enabled)
            {
                _coordinateTimer.Enabled = false;
            }
            BackToDefaultCoordinateTest();
        }

        /// <summary>
        /// Запустить тест "тренировки координат" за белых
        /// </summary>
        private void StartCoordinateTestForWhite()
        {
            _isWhiteCoordinateTest = true;
            _coordinateTimer.Enabled = true;
            BackToDefaultCoordinateTest();
            RandomCellToRestore();
        }

        /// <summary>
        /// Запустить тест "тренировки координат" за черных
        /// </summary>
        private void StartCoordinateTestForBlack()
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
        }

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
        }

        /// <summary>
        /// Возвращает адрес случайной клетки шахматного поля
        /// </summary>
        /// <returns></returns>
        private string GetRandomCell()
        {
            var file = random.Next(1, 9);
            var rank = (char)(97 + random.Next(0, 8));
            return $"{rank}{file}";
        }

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
        }

        private void playButtonForWhite_Click(object sender, EventArgs e)
        {
            if (_coordinateTimer.Enabled)
                EndGameAndShowResults(false);
            else
                StopCoordinateTest();
            StartCoordinateTestForWhite();
        }

        /// <summary>
        /// Закрыть тест по тренировке координат
        /// </summary>
        private void backToMenuButton_Click(object sender, EventArgs e)
        {
            StopCoordinateTest();
            DialogResult = DialogResult.Yes;
        }

        /// <summary>
        /// Cоздаём шахматное поле
        /// </summary>
        private void CreateChessBoard()
        {
            for (var y = 0; y < 8; y++)
            {
                for (var x = 0; x < 8; x++)
                {
                    var x1 = x;
                    var y1 = y;
                    var cell = CreateCell(x, y);
                    cell.Click += (sender, e) => HandleClick(x1, y1);
                    Controls.Add(cell);
                }
            }
        }

        /// <summary>
        /// Создать клетку
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        private Button CreateCell(int x, int y)
        {
            var cell = new Button();
            cell.Height = chessCellSize;
            cell.Width = chessCellSize;
            cell.BackColor = GetCellColor(y, x);
            cell.Location = new Point(startCellX + (x * chessCellSize), startCellY + (y * chessCellSize));
            cell.Anchor = System.Windows.Forms.AnchorStyles.None;
            return cell;
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
                //successLabel.ForeColor = foreColor;
                successLabel.Text = message;
                resultLabel.Text = $"Правильно:\n{successCount}";
                errorsLabel.Text = $"Ошибок:\n{errors}";
                RandomCellToRestore();
            }
        }

        /// <summary>
        /// Изменение шкалы соотношения правильных/неправильных ответов
        /// </summary>
        /// <param name="coefficient"></param>
        private void SetAccuracyBarLevel(double coefficient)
        {
            var level = accuracyBarCells.Length * coefficient;
            for (var i = 0; i < accuracyBarCells.Length; i++)
            {
                var panel = accuracyBarCells[accuracyBarCells.Length - 1 - i];
                if (i <= (level - 1))
                {
                    panel.BackColor = Color.Green;
                }
                else
                {
                    panel.BackColor = Color.Red;
                }
            }
        }

        /// <summary>
        /// Создать шкалу визуального соотношения правильных/неправильных ответов
        /// </summary>
        private void CreateAccuracyBar()
        {
            accuracyBarCells = new Control[20];

            for (var i = 0; i < accuracyBarCells.Length; i++)
            {
                var panel = new Label();
                panel.Size = new Size(25, accuracyBarPanel.Height / accuracyBarCells.Length);
                panel.Location = new Point(0, panel.Height * i);
                panel.BackColor = Color.FromArgb(0, 255, 255, 255);
                accuracyBarCells[i] = panel;
                accuracyBarPanel.Controls.Add(accuracyBarCells[i]);
            }
        }

        /// <summary>
        /// Играть за черные фигуры
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void playButtonForBlack_Click(object sender, EventArgs e)
        {
            if (_coordinateTimer.Enabled)
                EndGameAndShowResults(false);
            else
                StopCoordinateTest();
            StartCoordinateTestForBlack();
        }

        private void CoordinateTestForm_Load(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Maximized;
        }
    }
}
