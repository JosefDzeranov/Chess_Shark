using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Sachy_Obrazky.Windows;

namespace Sachy_Obrazky
{
    static class Program
    {
        public static string Name;
        public static int Record;
        public static int LastScore;
        internal static int LastErrors;

        /// <summary>
        /// Hlavní vstupní bod aplikace.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
            //Application.Run(new Form1());
        }
    }
}
