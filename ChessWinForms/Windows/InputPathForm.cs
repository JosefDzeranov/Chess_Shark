using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sachy_Obrazky.Windows
{
    public partial class InputPathForm : Form
    {
        public InputPathForm()
        {
            InitializeComponent();
            textBox1.Text = StatisticUserForm.PathSaveExcel;
            if (textBox1.Text == "")
                textBox1.Text = StatisticUsersForm.PathSaveExcel;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            StatisticUserForm.PathSaveExcel = textBox1.Text;
            StatisticUsersForm.PathSaveExcel = textBox1.Text;
            DialogResult = DialogResult.Yes;
        }
    }
}
