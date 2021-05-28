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
        private readonly string FS_PATH = Application.StartupPath + "\\FileSystem";
        private readonly string SHARED_PATH = Application.StartupPath + "\\Shared";
        
        public MainForm(User currentUser)
        {
            InitializeComponent();
            Text = currentUser.Name;
            LoadFiles(currentUser.Name);

            FileSystemView.ExpandAll();
        }

        private void LoadFiles(string home)
        {
            TreeNode root = new TreeNode
            {
                Name = FS_PATH + "\\" + home,
                Text = home,
            };
            FileSystemView.Nodes.Add(root);
            AddChildren(root);

            TreeNode shared = new TreeNode
            {
                Name = SHARED_PATH,
                Text = "Shared"
            };
            FileSystemView.Nodes.Add(shared);
            AddChildren(shared);
        }

        private void AddChildren(TreeNode parent)
        {
            DirectoryInfo home = new DirectoryInfo(parent.Name);
            
            var files = home.GetFiles();
            foreach (var file in files)
            {
                TreeNode child = new TreeNode
                {
                    Name = file.FullName,
                    Text = file.Name
                };
                parent.Nodes.Add(child);
            }

            var directories = home.GetDirectories();
            foreach(var directory in directories)
            {
                TreeNode child = new TreeNode
                {
                    Name = directory.FullName,
                    Text = directory.Name
                };
                parent.Nodes.Add(child);

                AddChildren(child);
            }
        }
    }
}
