﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using exl = Microsoft.Office.Interop.Excel;
using Sachy_Obrazky.Models;
using Sachy_Obrazky.Repository;

namespace Sachy_Obrazky.Windows
{
    public partial class StatisticUsersForm : Form
    {
        private exl.Application application;
        private exl.Workbook workBook;
        private exl.Worksheet worksheet;
        private List<ViewStatistic> attepts;
        private List<Player> users;
        public static string PathSaveExcel;
        public StatisticUsersForm()
        {
            InitializeComponent();
        }

        private void exportExcelButton_Click(object sender, EventArgs e)
        {

            PathSaveExcel = Environment.CurrentDirectory;
            var inputForm = new InputPathForm();
            Hide();
            inputForm.ShowDialog();
            Show();
            // Открываем приложение
            application = new exl.Application
            {
                DisplayAlerts = false
            };
            // Файл шаблона
            const string template = "template2.xlsm";

            // Открываем книгу
            workBook = application.Workbooks.Open(Path.Combine(Environment.CurrentDirectory, template));

            // Получаем активную таблицу
            worksheet = workBook.ActiveSheet as exl.Worksheet;

            //Записываем данные
            worksheet.Range["A1"].Value = "id пользователя";
            worksheet.Range["B1"].Value = "Имя и Фамилия";
            worksheet.Range["C1"].Value = "id попытки";
            worksheet.Range["D1"].Value = "id партии";
            worksheet.Range["E1"].Value = "Статус партии";
            worksheet.Range["F1"].Value = "Правильные ответы";
            worksheet.Range["G1"].Value = "Неправильные ответы";
            worksheet.Range["H1"].Value = "Точность (%)";
            worksheet.Range["I1"].Value = "Дата прохождения";
            worksheet.Range["J1"].Value = "Время прохождения (сек)";

            var users = new PlayerRepository().GetAll();
            var i = 0;
            foreach (var user in users)
            {
                foreach (var attept in user.AtteptsRestoreChessParty)
                {
                    var at = new ViewStatistic(attept);

                    worksheet.Cells[i + 2, 1].Value = user.Id.ToString();
                    worksheet.Cells[i + 2, 2].Value = (user.Name + " " + user.Family);
                    worksheet.Cells[i + 2, 3].Value = at.IdAttept;
                    worksheet.Cells[i + 2, 4].Value = at.IdParty;
                    worksheet.Cells[i + 2, 5].Value = TranslateStatusParty(at.StatusParty);
                    worksheet.Cells[i + 2, 6].Value = at.CorrectAnsvers;
                    worksheet.Cells[i + 2, 7].Value = at.IncorrectAnsvers;
                    worksheet.Cells[i + 2, 8].Value = at.Accuracy;
                    worksheet.Cells[i + 2, 9].Value = at.DatePassage;
                    worksheet.Cells[i + 2, 10].Value = at.PassageTime;

                    i++;
                }
            }

            // Показываем приложение
            //application.Visible = true;
            TopMost = true;

            // Сохраняем и закрываем 
            var date = DateTime.Now;
            string savedFileName = $"OverallStatistics_{date.Year}{date.Month}{date.Day}{date.Hour}{date.Minute}{date.Second}.xlsm";


            workBook.SaveAs(Path.Combine(PathSaveExcel, savedFileName));
            CloseExcel();

            MessageBox.Show($"Данные сохранены по адресу: {PathSaveExcel}", "Сообщение", MessageBoxButtons.OK);
        }

        private void StatisticUsersForm_Load(object sender, EventArgs e)
        {
            var users = new PlayerRepository().GetAll();
            foreach (var user in users)
            {
                foreach (var attept in user.AtteptsRestoreChessParty)
                {
                    var at = new ViewStatistic(attept);
                    dataGridView1.Rows.Add(
                        user.Id,
                        user.Name + " " + user.Family,
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
