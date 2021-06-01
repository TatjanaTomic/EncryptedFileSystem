using EncryptedFileSystem.Controllers;
using EncryptedFileSystem.Exceptions;
using EncryptedFileSystem.Model;
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
    public partial class MainForm : Form
    {
        private readonly string username;

        private readonly string FS_PATH = Application.StartupPath + "\\FileSystem";
        private readonly string SHARED_PATH = Application.StartupPath + "\\Shared";
        
        public MainForm(string currentUser)
        {
            InitializeComponent();
            Text = username = currentUser;
            LoadFiles();
        }

        private void LoadFiles()
        {
            FileSystemView.Nodes.Clear();
            SharedView.Nodes.Clear(); 
            
            TreeNode root = new TreeNode
            {
                Name = FS_PATH + "\\" + username,
                Text = username,
                Tag = new DirectoryInfo(FS_PATH + "\\" + username)
            };
            FileSystemView.Nodes.Add(root);
            AddChildren(root);

            TreeNode shared = new TreeNode
            {
                Name = SHARED_PATH,
                Text = "Shared",
                Tag = new DirectoryInfo(SHARED_PATH)
            };
            SharedView.Nodes.Add(shared);
            AddChildren(shared);

            FileSystemView.ExpandAll();
            SharedView.ExpandAll();
        }

        private void AddChildren(TreeNode parent)
        {
            DirectoryInfo home = new DirectoryInfo(parent.Name);
            
            var files = home.GetFiles();
            foreach (var file in files)
            {
                if (!file.Name.Contains('#'))
                {
                    TreeNode child = new TreeNode
                    {
                        Name = file.FullName,
                        Text = file.Name,
                        Tag = file
                    };
                    parent.Nodes.Add(child);
                }
            }

            var directories = home.GetDirectories();
            foreach(var directory in directories)
            {
                TreeNode child = new TreeNode
                {
                    Name = directory.FullName,
                    Text = directory.Name,
                    Tag = directory
                };
                parent.Nodes.Add(child);

                AddChildren(child);
            }
        }
        
        private void BtnUpload_Click(object sender, EventArgs e)
        {
            //TODO: Upload datoteka u shared folder!

            var resultPath = FS_PATH + "\\" + GetRelativePath();

            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    FileSystemController.Upload(username, openFileDialog.FileName, resultPath);
                    LoadFiles();
                }
                catch (EfsException ex)
                {
                    MessageBox.Show(ex.Message, string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Došlo je do greške prilikom upload-a", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Console.WriteLine(ex.StackTrace + " : " + ex.Message);
                }
            }
        }

        private void FileSystemView_DoubleClick(object sender, EventArgs e)
        {

            if(FileSystemView.SelectedNode.Tag.GetType() == typeof(FileInfo))
            {
                try
                {
                    FileSystemController.OpenFile((FileInfo)FileSystemView.SelectedNode.Tag, username);
                }
                catch (EfsException ex)
                {
                    MessageBox.Show(ex.Message, string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Došlo je do greške prilikom otvaranja datoteke!", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Console.WriteLine(ex.StackTrace + " : " + ex.Message);
                }
            }
        }

        private void BtnDownload_Click(object sender, EventArgs e)
        {
            //TODO: Shared folder !!!

            if (FileSystemView.SelectedNode == null || FileSystemView.SelectedNode.Tag.GetType() != typeof(FileInfo))
            {
                MessageBox.Show("Izaberite datoteku koju želite da preuzmete!", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            { 
                try
                {
                    string path = FileSystemController.DownloadFile((FileInfo)FileSystemView.SelectedNode.Tag, username);
                    MessageBox.Show("Datoteka je uspješno preuzeta i nalazi se na lokaciji: " + path, "Uspješno preuzimanje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (EfsException ex)
                {
                    MessageBox.Show(ex.Message, string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Došlo je do greške prilikom preuzimanja datoteke!", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Console.WriteLine(ex.StackTrace + " : " + ex.Message);
                }
            }
        }

        private void BtnNewFile_Click(object sender, EventArgs e)
        {
            new MyFile(username, GetRelativePath(), string.Empty, string.Empty).ShowDialog();
            LoadFiles();
        }

        private void BtnEditFile_Click(object sender, EventArgs e)
        {
            if (FileSystemView.SelectedNode == null || FileSystemView.SelectedNode.Tag.GetType() != typeof(FileInfo))
            {
                MessageBox.Show("Izaberite datoteku koju želite da uredite!", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                FileInfo selectedFile = (FileInfo)FileSystemView.SelectedNode.Tag;

                if(!selectedFile.Extension.Equals(".txt"))
                    MessageBox.Show("Možete da uredite samo tekstualne datoteke!", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                else
                {
                    try
                    {
                        var content = FileSystemController.GetFileContent(username, selectedFile);
                        var result = new MyFile(username, GetRelativePath(), selectedFile.Name, content).ShowDialog();
                        if(result.Equals(DialogResult.OK))
                            LoadFiles();
                    }
                    catch (EfsException ex)
                    {
                         MessageBox.Show(ex.Message, string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Došlo je do greške prilikom otvaranja datoteke!", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Console.WriteLine(ex.StackTrace + " : " + ex.Message);
                    }
                }
            }
        }

        private string GetRelativePath()
        {
            var selectedNode = FileSystemView.SelectedNode;
            string resultPath;
            if (selectedNode == null)
                resultPath = username;
            else
            {
                if (selectedNode.Tag.GetType() == typeof(FileInfo))
                {
                    resultPath = selectedNode.FullPath.Substring(0, selectedNode.FullPath.LastIndexOf("\\"));
                }
                else
                    resultPath = selectedNode.FullPath;
            }

            return resultPath;
        }

        private void SharedView_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                Delete(SharedView.SelectedNode);
            }
        }

        private void FileSystemView_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                Delete(FileSystemView.SelectedNode);
            }
        }

        private void Delete(TreeNode selectedNode)
        {
            if (selectedNode == null)
            {
                MessageBox.Show("Izaberite datoteku ili direktorijum koji želite da obrišete", String.Empty, MessageBoxButtons.OK);
            }
            else
            {
                if (selectedNode.Text.Equals(username))
                {
                    MessageBox.Show("Ne možete da obrišete 'home' direktorijum!", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (selectedNode.Text.Equals("Shared"))
                {
                    MessageBox.Show("Ne možete da obrišete 'shared' direktorijum!", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if(selectedNode.Name.StartsWith(SHARED_PATH) && !selectedNode.Name.StartsWith(SHARED_PATH + "\\" + username))
                {
                    MessageBox.Show("Možete da brišete samo svoje fajlove iz 'shared' foldera!", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    var result = MessageBox.Show("Da li ste sigurni da želite obrisati fajl/folder '" + selectedNode.Text + "' ?", String.Empty, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result.Equals(DialogResult.Yes))
                    {
                        try
                        {
                            if (selectedNode.Name.StartsWith(SHARED_PATH))
                            {
                                FileSystemController.DeleteFromShared();
                                LoadFiles();
                            }
                            else
                            {
                                FileSystemController.DeleteFromFileSystem(selectedNode.Name, selectedNode.Tag);
                                LoadFiles();
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Došlo je do greške prilikom brisanja fajla/foldera", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            Console.WriteLine(ex.StackTrace + " : " + ex.Message);
                        }
                    }
                }
            }
        }

        private void BtnMakeDirectory_Click(object sender, EventArgs e)
        {
            string name = tbMakeDirectory.Text.Trim();
            if (string.IsNullOrEmpty(name))
                MessageBox.Show("Unesite naziv novog direktorijuma i izaberite direktorijum na Vašem fajl sistemu u kom će se nalaziti!", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            else
            {
                try
                {
                    FileSystemController.MakeDirectory(username, GetRelativePath(), name.Replace(' ', '_'));
                    LoadFiles();
                }
                catch (EfsException ex)
                {
                    MessageBox.Show(ex.Message, string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Došlo je do greške prilikom kreiranja direktorijuma!", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Console.WriteLine(ex.StackTrace + " : " + ex.Message);
                }
            }
        }
    }
}
