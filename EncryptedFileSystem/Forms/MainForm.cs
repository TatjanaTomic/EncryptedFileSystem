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
        private readonly string home;

        private readonly string FS_PATH = Application.StartupPath + "\\FileSystem";
        private readonly string SHARED_PATH = Application.StartupPath + "\\Shared";
        
        public MainForm(string currentUser)
        {
            InitializeComponent();
            Text = home = currentUser;
            LoadFiles();
        }

        private void LoadFiles()
        {
            FileSystemView.Nodes.Clear();

            TreeNode root = new TreeNode
            {
                Name = FS_PATH + "\\" + home,
                Text = home,
                Tag = new DirectoryInfo(FS_PATH + "\\" + home)
            };
            FileSystemView.Nodes.Add(root);
            AddChildren(root);

            TreeNode shared = new TreeNode
            {
                Name = SHARED_PATH,
                Text = "Shared",
                Tag = new FileInfo(SHARED_PATH)
            };
            FileSystemView.Nodes.Add(shared);
            AddChildren(shared);

            FileSystemView.ExpandAll();
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

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            var selectedNode = FileSystemView.SelectedNode;

            if(selectedNode == null)
            {
                MessageBox.Show("Izaberite datoteku ili direktorijum koji želite da obrišete", String.Empty, MessageBoxButtons.OK);
            }
            else
            {
                if(selectedNode.Text.Equals(home))
                {
                    MessageBox.Show("Ne možete da obrišete 'home' direktorijum!", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if(selectedNode.Text.Equals("Shared"))
                {
                    MessageBox.Show("Ne možete da obrišete 'shared' direktorijum!", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                //TODO: Realizovati brisanje fajlova iz shared foldera
                else
                {
                    var result = MessageBox.Show("Da li ste sigurni da želite obrisati fajl/folder '" + selectedNode.Text + "' ?", String.Empty, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result.Equals(DialogResult.Yes))
                    {
                        try
                        {
                            FileSystemController.DeleteFromFileSystem(selectedNode.Name, selectedNode.Tag);
                            LoadFiles();
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
        
        private void BtnUpload_Click(object sender, EventArgs e)
        {
            var selectedNode = FileSystemView.SelectedNode;
            string resultPath;
            if (selectedNode == null)
                resultPath = FS_PATH + "\\" + home;
            else
            {
                if (selectedNode.Tag.GetType() == typeof(FileInfo))
                {
                    resultPath = selectedNode.Name.Substring(0, selectedNode.Name.LastIndexOf("\\"));
                }
                else
                    resultPath = selectedNode.Name;
            }

            OpenFileDialog openFileDialog = new OpenFileDialog();

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    FileSystemController.Upload(home, openFileDialog.FileName, resultPath);
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

            //TODO: Dodati provjere za otvaranje datoteka iz shared foldera!!!

            if(FileSystemView.SelectedNode.Tag.GetType() == typeof(FileInfo))
            {
                try
                {
                    FileSystemController.OpenFile((FileInfo)FileSystemView.SelectedNode.Tag, home);
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
}
