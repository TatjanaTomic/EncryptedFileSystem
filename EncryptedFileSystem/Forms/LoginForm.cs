using EncryptedFileSystem.Forms;
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
                //TODO: Dodati prijavu na sistem sa provjerama
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
            new RegisterForm().Show();
        }
    }
}
