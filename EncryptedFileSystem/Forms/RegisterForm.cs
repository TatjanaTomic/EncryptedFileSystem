using System;
using System.Drawing;
using System.Windows.Forms;
using EncryptedFileSystem.Controllers;
using EncryptedFileSystem.Exceptions;

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
            if(!tbPassword.Text.Equals(tbRepeatedPassword.Text))
            {
                lbPassword.ForeColor = lbRepeatedPassword.ForeColor = Color.Red;
                MessageBox.Show("Lozinke se ne poklapaju!", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tbPassword.Text = tbRepeatedPassword.Text = "";
                completed = false;
            }

            if(completed)
            {
                string hashAlgorythm = "";
                switch(cbHash.SelectedIndex)
                {
                    case 0:
                        hashAlgorythm = "1";
                        break;
                    case 1:
                        hashAlgorythm = "5";
                        break;
                    case 2:
                        hashAlgorythm = "6";
                        break;
                }

                try
                {
                    UserController.RegisterUser(tbUsername.Text, tbPassword.Text, hashAlgorythm, cbEncrypt.SelectedItem.ToString());
                    MessageBox.Show("Uspješno ste se registrovali na sistem!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (EfsException ex)
                {
                    MessageBox.Show(ex.Message, "Greška", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Došlo je do greške prilikom registracije na sistem!", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Console.WriteLine(ex.StackTrace + " : " + ex.Message);
                }
                
            }
        }
    }
}
