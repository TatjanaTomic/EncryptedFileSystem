using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EncryptedFileSystem.Controllers;

namespace EncryptedFileSystem.Forms
{
    public partial class RegisterForm : Form
    {
        public RegisterForm()
        {
            InitializeComponent();
            cbHash.SelectedIndex = 0;
            cbEncrypt.SelectedIndex = 0;
        }

        private void tbUsername_Click(object sender, EventArgs e)
        {
            lbUsername.ForeColor = SystemColors.ControlText;
        }

        private void tbPassword_Click(object sender, EventArgs e)
        {
            lbPassword.ForeColor = SystemColors.ControlText;
        }

        private void tbRepeatedPassword_Click(object sender, EventArgs e)
        {
            lbRepeatedPassword.ForeColor = SystemColors.ControlText;
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            bool completed = true;
            if (tbUsername.Text.Equals(""))
            {
                lbUsername.ForeColor = Color.Red;
                completed = false;
            }
            if (tbPassword.Text.Equals(""))
            {
                lbPassword.ForeColor = Color.Red;
                completed = false;
            }
            if (tbRepeatedPassword.Text.Equals(""))
            {
                lbRepeatedPassword.ForeColor = Color.Red;
                completed = false;
            }

            if(completed)
            {
                int hashType = 1;
                switch(cbHash.SelectedIndex)
                {
                    case 0:
                        hashType = 1;
                        break;
                    case 1:
                        hashType = 5;
                        break;
                    case 2:
                        hashType = 6;
                        break;
                }

                if (!UserController.CheckNameExists(tbUsername.Text.ToLower()))
                {
                    if(UserController.RegisterUser(new Model.User(tbUsername.Text, tbPassword.Text, hashType, cbEncrypt.SelectedItem.ToString())))
                    {
                        MessageBox.Show("Uspješno ste se registrovali na sistem!", "", MessageBoxButtons.OK);
                    }
                    else
                    {
                        MessageBox.Show("Došlo je do greške prilikom registracije na sistem.", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Uneseno korisničko ime već postoji! Molimo Vas da unesete novo korisničko ime.", "Greška", MessageBoxButtons.OK);
                }
            }
        }
    }
}
