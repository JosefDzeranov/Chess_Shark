using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MaterialSkin;
using MaterialSkin.Controls;

namespace Sachy_Obrazky
{
    public partial class AuthenticationForm : MaterialForm
    {
        public AuthenticationForm()
        {
            InitializeComponent();
            Text = "Авторизация";
        }

        public string FirstName => textBox1.Text;
        public string LastName => textBox2.Text;

        private bool IsValid(string s)
        {
            if (IsEmptyOrNullOrWhiteSpace(s))
                return false;
            var russianLetters = "АаБбВвГгДдЕеЁёЖжЗзИиЙйКкЛлМмНнОоПпРрСсТтУуФфХхЦцЧчШшЩщЪъЫыЬьЭэЮюЯя";
            var englishLetters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
            var isEmpty = IsEmptyOrNullOrWhiteSpace(s);
            for(var i = 0; i < s.Length; i++)
            {
                var symbol = s[i];
                if (!russianLetters.Contains(symbol) && !englishLetters.Contains(symbol))
                    return false;
            }
            return true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!IsValid(FirstName) || !IsValid(LastName))
            {
                if (!IsValid(FirstName))
                {

                    if (IsEmptyOrNullOrWhiteSpace(FirstName))
                    {
                        FirstNameMessage.ForeColor = Color.Red;
                        FirstNameMessage.Text = "Введите имя";
                    }
                    else
                    {
                        FirstNameMessage.ForeColor = Color.Red;
                        FirstNameMessage.Text = "Имя содержит недопустимые символы.\nОно должно состоять из символов [а-яА-Я] или [a-zA-Z]";
                    }
                }
                else
                {
                    FirstNameMessage.Text = "";
                }
                if (!IsValid(LastName))
                {
                    if (IsEmptyOrNullOrWhiteSpace(LastName))
                    {
                        LastNameMessage.ForeColor = Color.Red;
                        LastNameMessage.Text = "Введите фамилию";
                    }
                    else
                    {
                        LastNameMessage.ForeColor = Color.Red;
                        LastNameMessage.Text = "Фамилия содержит недопустимые символы.\nОна должна состоять из символов [а-яА-Я] или [a-zA-Z]";
                    }
                }
                else
                {
                    LastNameMessage.Text = "";
                }
            }
            else
            {
                DialogResult = DialogResult.Yes;
            }
        }
        private bool IsEmptyOrNullOrWhiteSpace(string s)
        {
            return string.IsNullOrWhiteSpace(s) && string.IsNullOrEmpty(s);
        }

        private void AuthenticationForm_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_Click(object sender, EventArgs e)
        {
            if(textBox1.Text == "Имя")
                textBox1.Text = "";
        }

        private void textBox2_Click(object sender, EventArgs e)
        {
            if (textBox2.Text == "Фамилия")
                textBox2.Text = "";
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (!IsValid(FirstName) || !IsValid(LastName))
            {
                if (!IsValid(FirstName))
                {

                    if (IsEmptyOrNullOrWhiteSpace(FirstName))
                    {
                        FirstNameMessage.ForeColor = Color.Red;
                        FirstNameMessage.Text = "Введите имя";
                    }
                    else
                    {
                        FirstNameMessage.ForeColor = Color.Red;
                        FirstNameMessage.Text = "Имя содержит недопустимые символы.\nОно должно состоять из символов [а-яА-Я] или [a-zA-Z]";
                    }
                }
                else
                {
                    FirstNameMessage.Text = "";
                }
                if (!IsValid(LastName))
                {
                    if (IsEmptyOrNullOrWhiteSpace(LastName))
                    {
                        LastNameMessage.ForeColor = Color.Red;
                        LastNameMessage.Text = "Введите фамилию";
                    }
                    else
                    {
                        LastNameMessage.ForeColor = Color.Red;
                        LastNameMessage.Text = "Фамилия содержит недопустимые символы.\nОна должна состоять из символов [а-яА-Я] или [a-zA-Z]";
                    }
                }
                else
                {
                    LastNameMessage.Text = "";
                }
            }
            else
            {
                DialogResult = DialogResult.Yes;
            }
        }
    }
}
