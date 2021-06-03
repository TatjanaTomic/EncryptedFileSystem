using EncryptedFileSystem.Controllers;
using EncryptedFileSystem.Exceptions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EncryptedFileSystem.Forms
{
    public partial class DecryptFileForm : Form
    {
        private readonly string path;
        public DecryptFileForm(string path, string fileName)
        {
            InitializeComponent();

            this.path = path;
            tbFileName.Text = fileName;
            tbPath.Text = path.Replace(Application.StartupPath, "");
            cbAlgorythms.SelectedIndex = 0;
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BtnDecrypt_Click(object sender, EventArgs e)
        {
            string key = tbPassword.Text.Trim();

            if (string.IsNullOrEmpty(key))
                MessageBox.Show("Unesite lozinku za dekripciju!", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            else
                try
                {
                    FileSystemController.DecryptSharedFile(path, cbAlgorythms.SelectedItem.ToString(), key);
                }
                catch (EfsException ex)
                {
                    MessageBox.Show(ex.Message, string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Došlo je do greške prilikom dekripcije!", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Console.WriteLine(ex.StackTrace + " : " + ex.Message);
                }
        }
    }
}
