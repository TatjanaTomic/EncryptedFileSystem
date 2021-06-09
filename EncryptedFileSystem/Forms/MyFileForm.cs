using EncryptedFileSystem.Controllers;
using EncryptedFileSystem.Exceptions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EncryptedFileSystem.Forms
{
    public partial class MyFileForm : Form
    {
        private readonly string username;
        private readonly string currentPath;
        private readonly bool createNew = true;

        public MyFileForm(string username, string path, string name, string content, bool isMessage = false)
        {
            InitializeComponent();
            this.username = username;
            currentPath = path;

            tbText.Text = content;
            
            //prilagodjen izgled za prikaz poruke o share-ovanom fajlu
            if (isMessage)
            {
                tbName.Text = name;
                tbName.ReadOnly = true;
                tbName.TabStop = false;
                tbPath.Text = path;
                tbText.ReadOnly = true;
                tbText.TabStop = false;
                btnCancel.Visible = false;
                btnSave.Visible = false;
            }

            //prilagodjen izgled za kreiranje novog fajla
            else
            {
                tbPath.Text = "\\" + path + "\\" + name;

                //prilagođen izgled za modifikovanje fajla
                if (!string.IsNullOrEmpty(name))
                {
                    tbName.Text = name;
                    tbName.ReadOnly = true;
                    tbName.TabStop = false;
                    createNew = false;
                }
            }
        }

        private void TbName_TextChanged(object sender, EventArgs e)
        {
            tbPath.Text = "\\" + currentPath + "\\" + tbName.Text; 
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            string fileName = tbName.Text.Trim();
            string text = tbText.Text;

            if (string.IsNullOrEmpty(fileName))
                MessageBox.Show("Unesite naziv datoteke!", string.Empty, MessageBoxButtons.OK);
            else if(createNew)
            {
                try
                {
                    FileSystemController.CreateNewFile(username, currentPath, fileName, text);
                    var result = MessageBox.Show("Fajl " + fileName + " je uspješno kreiran!", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    if (result.Equals(DialogResult.OK))
                        this.Close();
                }
                catch (EfsException ex)
                {
                    MessageBox.Show(ex.Message, string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                catch(Exception ex)
                {
                    MessageBox.Show("Došlo je do greške prilikom kreiranja novog fajla!", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Console.WriteLine(ex.StackTrace + " : " + ex.Message);
                }
            }
            else
            {
                try
                {
                    FileSystemController.EditFile(username, currentPath, fileName, text);
                    var result = MessageBox.Show("Fajl " + fileName + " je uspješno ažuriran!", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    if (result.Equals(DialogResult.OK))
                        this.Close();
                }
                catch (EfsException ex)
                {
                    MessageBox.Show(ex.Message, string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Došlo je do greške prilikom izmjene fajla!", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Console.WriteLine(ex.StackTrace + " : " + ex.Message);
                }
            }
        }
    }
}
