using Sachy_Obrazky.Repository;
using System;
using System.Windows.Forms;

namespace Sachy_Obrazky.Windows
{
    public partial class CreatorPartyForm : Form
    {
        /// <summary>
        /// Хранилище шахматных позиций (2 версия)
        /// </summary>
        private PartyRepository _storageParty = new PartyRepository();
        public CreatorPartyForm()
        {
            InitializeComponent();
            errorListLabel.Text = "";
            namePartyLabel.Text = "Название партии (Противники, место и год поединка)";
            stepPartyLabel.Text = "Ходы партии, записанные в стандартной шахматной нотации " +
                                  "(английскими символями, без примечаний) Ходы разделять пробелами";
        }

        private void savePartyButton_Click(object sender, EventArgs e)
        {
            try
            {
                _storageParty.Create(namePartyTextBox.Text, stepPartyTextBox.Text);
                DialogResult = DialogResult.Yes;
            }
            catch (Exception exception)
            {
                errorListLabel.Text = exception.Message;
                //throw;
            }

        }
    }
}
