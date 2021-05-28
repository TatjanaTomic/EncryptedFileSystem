using EncryptedFileSystem.Controllers;
using EncryptedFileSystem.Exceptions;
using EncryptedFileSystem.Forms;
using EncryptedFileSystem.Model;
using EncryptedFileSystem.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EncryptedFileSystem
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }
        private void TbUsername_Click(object sender, EventArgs e)
        {
            lbUsername.ForeColor = SystemColors.ControlText;
        }

        private void TbPassword_Click(object sender, EventArgs e)
        {
            lbPassword.ForeColor = SystemColors.ControlText;
        }

        private void LbRegister_Click(object sender, EventArgs e)
        {
            new RegisterForm().ShowDialog();
        }

        private void BtnLogin_Click(object sender, EventArgs e)
        {
            string username = tbUsername.Text = tbUsername.Text.Trim();
            string password = tbPassword.Text = tbPassword.Text.Trim();

            if (string.IsNullOrEmpty(username))
            {
                lbUsername.ForeColor = Color.Red;
            }
            if (string.IsNullOrEmpty(password))
            {
                lbPassword.ForeColor = Color.Red;
            }

            if (!(string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password)))
            {
                try
                {
                    User currentUser = UserController.LoginUser(username, password);
                    new MainForm(currentUser).Show();
                }
                catch (EfsException ex)
                {
                    MessageBox.Show(ex.Message, "Greška", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Došlo je do greške prilikom prijave na sistem!", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Console.WriteLine(ex.StackTrace + " : " + ex.Message);
                }
            }
        }

    }
}
