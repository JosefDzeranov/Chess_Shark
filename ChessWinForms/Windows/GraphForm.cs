using Sachy_Obrazky.Models;
using Sachy_Obrazky.Repository;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using ZedGraph;

namespace Sachy_Obrazky.Windows
{
    public partial class GraphForm : Form
    {
        private Player user;
        private List<ViewStatistic> attepts = new List<ViewStatistic>();
        public GraphForm()
        {
            InitializeComponent();

            var users = new PlayerRepository().GetAll();
            user = users.FirstOrDefault(ea => ea.Id == MainForm.authenticationPlayer.Id);
            namelabel.Text = $"Имя: {user.Name}";
            familyLabel.Text = $"Фамилия: {user.Family}";
            overallAverageAccuracy_Label.Text = $"Средняя точность (общая): {GetOverallAverageAccuracy(users)}";
            averageAccuracy_Label.Text = $"Средняя точность: {GetAverageAccuracy(user)}";
            countAttempts_Label.Text = $"Количество попыток: {GetCountAttempts(user)}";
            bestAccuracy_Label.Text = $"Лучшая точность: {GetBestAccuracyAttempts(user)}";
            worstAccuracy_Label.Text = $"Худшая точность: {GetWorstAccuracy(user)}";
            foreach (var attept in user.AtteptsRestoreChessParty)
            {
                attepts.Add(new ViewStatistic(attept));
            }

            DrawGraph();

        }

        private double GetWorstAccuracy(Player player)
        {
            double min = 0;
            foreach (var attept in player.AtteptsRestoreChessParty)
            {
                var at = new ViewStatistic(attept);
                if (min > at.Accuracy && at.Accuracy > 0)
                    min = at.Accuracy;
            }
            return min;
        }

        private double GetBestAccuracyAttempts(Player player)
        {
            double max = 0;
            foreach (var attept in player.AtteptsRestoreChessParty)
            {
                var at = new ViewStatistic(attept);
                if (max < at.Accuracy)
                    max = at.Accuracy;

            }
            return max;
        }

        private int GetCountAttempts(Player player)
        {
            int count = 0;
            foreach (var at in player.AtteptsRestoreChessParty)
            {
                count++;
            }
            return count;
        }

        private double GetOverallAverageAccuracy(List<Player> users)
        {
            double summAverageAccuracy = 0;
            int count = 0;
            foreach (var usr in users)
            {
                if (GetAverageAccuracy(usr) != 0)
                {
                    count++;
                    summAverageAccuracy += GetAverageAccuracy(usr);
                }
            }
            return Math.Round(summAverageAccuracy / count, 3);
        }

        private double GetAverageAccuracy(Player player)
        {
            var atteptsPlayer = new List<ViewStatistic>();
            double summAverageAccuracy = 0;
            int count = 0;
            foreach (var attept in player.AtteptsRestoreChessParty)
            {
                var at = new ViewStatistic(attept);
                if (at.Accuracy > 0)
                {
                    count++;
                    summAverageAccuracy += at.Accuracy;
                }
            }

            if (count == 0)
            {
                return 0;
            }
            else
            {
                return Math.Round(summAverageAccuracy / count, 3);
            }
        }

        private void zedGraphControl1_Load(object sender, EventArgs e)
        {

        }

        private void DrawGraph()
        {
            // Получим панель для рисования
            GraphPane pane = zedGraphStatisticControl.GraphPane;

            // !!!
            // Изменим тест надписи по оси X
            pane.XAxis.Title.Text = "Количество попыток";

            // Изменим параметры шрифта для оси X
            pane.XAxis.Title.FontSpec.IsUnderline = true;
            pane.XAxis.Title.FontSpec.IsBold = false;
            pane.XAxis.Title.FontSpec.FontColor = Color.Blue;

            // Изменим текст по оси Y
            pane.YAxis.Title.Text = "Точность";

            // Изменим текст заголовка графика
            pane.Title.Text = "Точность, в каждой попытке";

            // В параметрах шрифта сделаем заливку красным цветом
            //pane.Title.FontSpec.Fill.Brush = new SolidBrush(Color.Red);
            pane.Title.FontSpec.Fill.IsVisible = true;

            // Сделаем шрифт не полужирным
            pane.Title.FontSpec.IsBold = false;


            // Очистим список кривых на тот случай, если до этого сигналы уже были нарисованы
            pane.CurveList.Clear();

            //получаем список значений
            var atteptsList = OverallAccuracy();
            // Создадим список точек
            PointPairList list = new PointPairList();

            double xmin = 0;
            double xmax = atteptsList.Count;

            // Заполняем список точек
            for (int i = 0; i < atteptsList.Count; i++)
            {
                list.Add(i + 1, atteptsList[i].Accuracy);
            }

            // Создадим кривую
            pane.AddCurve("", list, Color.Blue, SymbolType.None);

            // Вызываем метод AxisChange (), чтобы обновить данные об осях.
            zedGraphStatisticControl.AxisChange();

            // Обновляем график
            zedGraphStatisticControl.Invalidate();
        }

        private List<PointOverallAccuracy> OverallAccuracy()
        {
            int count = 0;
            var overallsAccuracy = new List<PointOverallAccuracy>();
            foreach (var att in attepts)
            {
                overallsAccuracy.Add(new PointOverallAccuracy()
                {
                    Date = att.DatePassage,
                    Accuracy = att.Accuracy,
                });
            }

            var overallsAccuracySort = overallsAccuracy.OrderBy(p => p.Date).ToList();
            return overallsAccuracySort;
        }


    }

    class PointOverallAccuracy
    {
        public DateTime Date { get; set; }
        public double Accuracy { get; set; }
    }
}
