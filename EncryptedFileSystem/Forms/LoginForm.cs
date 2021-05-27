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

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = tbUsername.Text;
            string password = tbPassword.Text;

            if (username.Equals(""))
            {
                lbUsername.ForeColor = Color.Red;
            }
            if (password.Equals(""))
            {
                lbPassword.ForeColor = Color.Red;
            }

            if (!username.Equals("") && !password.Equals(""))
            {
                try
                {
                    User currentUser = UserController.LoginUser(username, password);
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

        private void tbUsername_Click(object sender, EventArgs e)
        {
            lbUsername.ForeColor = SystemColors.ControlText;
        }

        private void tbPassword_Click(object sender, EventArgs e)
        {
            lbPassword.ForeColor = SystemColors.ControlText;
        }

        private void lbRegister_Click(object sender, EventArgs e)
        {
            new RegisterForm().ShowDialog();
        }
    }
}
