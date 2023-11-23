using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Microsoft.Office.Interop.Excel;
using Sachy_Obrazky.Models;
using Sachy_Obrazky.Repository;
using Application = Microsoft.Office.Interop.Excel.Application;


namespace Sachy_Obrazky.Windows
{
    public partial class StatisticUserForm : Form
    {
        private Application application;
        private Workbook workBook;
        private Worksheet worksheet;
        private List<ViewStatistic> attepts;
        private Player user;
        public static string PathSaveExcel;
        public StatisticUserForm()
        {
            InitializeComponent();
            
        }

        private void GetGraffic_Click(object sender, EventArgs e)
        {
            var GraphForm = new GraphForm();
            Hide();
            GraphForm.ShowDialog();
            Show();
        }

        private void StatisticUserForm_Load(object sender, EventArgs e)
        {
            var users = new PlayerRepository().GetAll();
            user = users.FirstOrDefault(ea => ea.Id == MainForm.authenticationPlayer.Id);
            IdAndName_label.Text = $"Id:{user.Id}\nИмя: {user.Name}\nФамилия: {user.Family}";
            attepts = new List<ViewStatistic>();
            foreach (var attept in user.AtteptsRestoreChessParty)
            {
                attepts.Add(new ViewStatistic(attept));
            }

            foreach (var at in attepts)
            {
                dataGridView1.Rows.Add(
                    at.IdAttept, 
                    at.IdParty,
                    TranslateStatusParty(at.StatusParty), 
                    at.CorrectAnsvers, 
                    at.IncorrectAnsvers,
                    at.Accuracy, 
                    at.DatePassage, 
                    at.PassageTime);
            }
        }

        private string TranslateStatusParty(StatusParty statusParty)
        {
            switch (statusParty)
            {
                case StatusParty.Completed:
                    return "Завершена";
                case StatusParty.NotCompleted:
                    return "Не завершена";
                case StatusParty.CompletedAheadSchedule:
                    return "Завершена досрочно – попытка не считается";
                default: return "-";
            }
        }

        private void exportExcelButton_Click(object sender, EventArgs e)
        {
            PathSaveExcel = Environment.CurrentDirectory;
            var inputForm = new InputPathForm();
            Hide();
            inputForm.ShowDialog();
            Show();
            // Открываем приложение
            application = new Application
            {
                DisplayAlerts = false
            };
            // Файл шаблона
            const string template = "template.xlsm";

            // Открываем книгу
            workBook = application.Workbooks.Open(Path.Combine(Environment.CurrentDirectory, template));

            // Получаем активную таблицу
            worksheet = workBook.ActiveSheet as Worksheet;

            //Записываем данные
            worksheet.Range["A1"].Value = "id попытки";
            worksheet.Range["B1"].Value = "id партии";
            worksheet.Range["C1"].Value = "Статус партии";
            worksheet.Range["D1"].Value = "Правильные ответы";
            worksheet.Range["E1"].Value = "Неправильные ответы";
            worksheet.Range["F1"].Value = "Точность (%)";
            worksheet.Range["G1"].Value = "Дата прохождения";
            worksheet.Range["H1"].Value = "Время прохождения (сек)";
            for (int i = 0; i < attepts.Count; i++)
            {
                worksheet.Cells[i + 2, 1].Value = attepts[i].IdAttept;
                worksheet.Cells[i + 2, 2].Value = attepts[i].IdParty;
                worksheet.Cells[i + 2, 3].Value = TranslateStatusParty(attepts[i].StatusParty);
                worksheet.Cells[i + 2, 4].Value = attepts[i].CorrectAnsvers;
                worksheet.Cells[i + 2, 5].Value = attepts[i].IncorrectAnsvers;
                worksheet.Cells[i + 2, 6].Value = attepts[i].Accuracy;
                worksheet.Cells[i + 2, 7].Value = attepts[i].DatePassage;
                worksheet.Cells[i + 2, 8].Value = attepts[i].PassageTime;
            }

            // Показываем приложение
            //application.Visible = true;
            TopMost = true;

            // Сохраняем и закрываем 
            var date = DateTime.Now;
            string savedFileName = $"{user.Name}_{user.Family}_{date.Year}{date.Month}{date.Day}{date.Hour}{date.Minute}{date.Second}.xlsm";


            workBook.SaveAs(Path.Combine(PathSaveExcel, savedFileName));
            CloseExcel();

            MessageBox.Show($"Данные сохранены по адресу: {PathSaveExcel}", "Сообщение", MessageBoxButtons.OK);
        }

        private void CloseExcel()
        {
            if (application != null)
            {
                int excelProcessId = -1;
                GetWindowThreadProcessId(application.Hwnd, ref excelProcessId);

                Marshal.ReleaseComObject(worksheet);
                workBook.Close();
                Marshal.ReleaseComObject(workBook);
                application.Quit();
                Marshal.ReleaseComObject(application);

                application = null;
                // Прибиваем висящий процесс
                try
                {
                    Process process = Process.GetProcessById(excelProcessId);
                    process.Kill();
                }
                finally { }
            }
        }

        [DllImport("user32.dll", SetLastError = true)]
        static extern uint GetWindowThreadProcessId(int hWnd, ref int lpdwProcessId);

    }
}
