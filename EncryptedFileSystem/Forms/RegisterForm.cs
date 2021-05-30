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

        private void TbUsername_Click(object sender, EventArgs e)
        {
            lbUsername.ForeColor = SystemColors.ControlText;
        }

        private void TbPassword_Click(object sender, EventArgs e)
        {
            lbPassword.ForeColor = SystemColors.ControlText;
        }

        private void TbRepeatedPassword_Click(object sender, EventArgs e)
        {
            lbRepeatedPassword.ForeColor = SystemColors.ControlText;
        }

        private void BtnRegister_Click(object sender, EventArgs e)
        {
            string username = tbUsername.Text = tbUsername.Text.Trim();
            string password = tbPassword.Text = tbPassword.Text.Trim();
            string repeatedPassword = tbRepeatedPassword.Text = tbRepeatedPassword.Text.Trim();

            bool completed = true;
            if (string.IsNullOrEmpty(username))
            {
                lbUsername.ForeColor = Color.Red;
                completed = false;
            }
            if (string.IsNullOrEmpty(password))
            {
                lbPassword.ForeColor = Color.Red;
                completed = false;
            }
            if (string.IsNullOrEmpty(repeatedPassword))
            {
                lbRepeatedPassword.ForeColor = Color.Red;
                completed = false;
            }
            if(!password.Equals(repeatedPassword))
            {
                lbPassword.ForeColor = lbRepeatedPassword.ForeColor = Color.Red;
                MessageBox.Show("Lozinke se ne poklapaju!", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tbPassword.Text = tbRepeatedPassword.Text = "";
                completed = false;
            }

            if(completed)
            {
                string hashAlgorythm = String.Empty;
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
                    UserController.RegisterUser(username, password, hashAlgorythm, cbEncrypt.SelectedItem.ToString());
                    MessageBox.Show("Uspješno ste se registrovali! Možete da se prijavite na sistem pomoću korisničkog imena: " + tbUsername.Text, String.Empty, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                catch (EfsException ex)
                {
                    MessageBox.Show(ex.Message, "Greška", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Došlo je do greške prilikom registracije na sistem!", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Console.WriteLine(ex.StackTrace + " : " + ex.Message);
                    this.Close();
                }
                
            }
        }
    }
}
