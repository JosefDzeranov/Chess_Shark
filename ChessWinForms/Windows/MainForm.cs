using Sachy_Obrazky.Repository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Sachy_Obrazky.Models;

namespace Sachy_Obrazky.Windows
{
    public partial class MainForm : Form
    {
        /// <summary>
        /// Хранилище шахматных позиций
        /// </summary>
        private IEnumerator<ChessRestoreArrangement> _storage;

        /// <summary>
        /// Хранилище игроков (для 2 режима)
        /// </summary>
        private const string UserSaveFilePath = "userdata.txt";
        /// <summary>
        /// Хранилище игроков (для 3 режима)
        /// </summary>
        private PlayerRepository players = new PlayerRepository();

        /// <summary>
        /// Данные авторизованного пользователя
        /// </summary>
        public static Player authenticationPlayer { get; set; } = new Player();

        public MainForm()
        {
            _storage = InitializeStorage("storage.txt");
            FormClosing += (sender, e) => HandleOnClosing();
            var authenticationData = Authenticate();
            Program.Name = $"{authenticationData.FirstName}";
            Program.Record = GetRecord($"{Program.Name}");

            DoubleBuffered = true;
            InitializeComponent();
            this.nameLabel.Text = $"Имя: {Program.Name}"; // => ct
        }

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

            authenticationPlayer.Name = authenticationForm.FirstName;
            authenticationPlayer.Family = authenticationForm.LastName;
            var playerTemp = new Player()
            {
                Name = authenticationPlayer.Name,
                Family = authenticationPlayer.Family
            };
            if (players.Find(playerTemp) == null)
            {
                players.Create(new Player(playerTemp.Name, playerTemp.Family));
                authenticationPlayer = players.Find(playerTemp);
            }
            else
            {
                authenticationPlayer = players.Find(playerTemp);
            }
            return new AuthenticationData(authenticationForm.FirstName, authenticationForm.LastName);
        }

        /// <summary>
        /// Показать тест по тренировке координат
        /// </summary>
        private void CoordinateTestButton_Click(object sender, EventArgs e)
        {
            var CoordinateTest = new CoordinateTestForm();
            Hide();
            CoordinateTest.ShowDialog();
            Show();
        }

        private void RestoringPositionButton_Click(object sender, EventArgs e)
        {
            var restoringPositionForm = new RestoringPositionForm();
            Hide();
            restoringPositionForm.ShowDialog();
            Show();
        }

        private void RestoreChessPartyButton_Click(object sender, EventArgs e)
        {
            var restoreChessPartyForm = new RestoreChessPartyForm();
            Hide();
            restoreChessPartyForm.ShowDialog();
            Show();
        }

        /// <summary>
        /// Инициализация хранилища шахматных позиций
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        private IEnumerator<ChessRestoreArrangement> InitializeStorage(string path) //
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


                if (players.Find(authenticationPlayer) != null)
                {
                    var user = players.Find(authenticationPlayer);
                    user.RecordRestoringPosition = Program.Record;
                    players.Update(user);
                }
                File.WriteAllText(UserSaveFilePath, builder.ToString());
            }
            else
            {
                File.Create(UserSaveFilePath);
                File.WriteAllText(UserSaveFilePath, $"{Program.Name}-{Program.Record}\n");
            }
        }

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
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Maximized;
        }

        private void AddPartyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var createParty = new CreatorPartyForm();
            Hide();
            createParty.ShowDialog();
            Show();
        }

        private void statisticButton_Click(object sender, EventArgs e)
        {
            var statisticUserForm = new StatisticUserForm();
            Hide();
            statisticUserForm.ShowDialog();
            Show();
        }

        private void OverallStatisticsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var statisticUsersForm = new StatisticUsersForm();
            Hide();
            statisticUsersForm.ShowDialog();
            Show();
        }
    }
}
