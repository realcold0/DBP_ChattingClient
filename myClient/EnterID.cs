using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace myClient
{
    public partial class EnterID : Form
    {
        string ID;

        public EnterID()
        {
            InitializeComponent();
        }

        private void IDbtn_Click(object sender, EventArgs e)
        {
            if (IDInputField.Text.Equals(String.Empty))
            {

            }
            else
            {
                ID = IDInputField.Text;
                ClientForm clientform = new ClientForm(ID);
                this.Hide();
                clientform.ShowDialog();
            }
        }

        private void IDInputField_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return) IDbtn_Click(sender, e);
        }

        private void EnterID_Load(object sender, EventArgs e)
        {

        }
    }
}
