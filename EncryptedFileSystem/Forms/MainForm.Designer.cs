
namespace EncryptedFileSystem.Forms
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.FileSystemView = new System.Windows.Forms.TreeView();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.btnUpload = new System.Windows.Forms.Button();
            this.btnDownload = new System.Windows.Forms.Button();
            this.btnNewFile = new System.Windows.Forms.Button();
            this.btnEditFile = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SharedView = new System.Windows.Forms.TreeView();
            this.label3 = new System.Windows.Forms.Label();
            this.btnMakeDirectory = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.tbMakeDirectory = new System.Windows.Forms.TextBox();
            this.btnShareWith = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.listBoxUsers = new System.Windows.Forms.ListBox();
            this.label6 = new System.Windows.Forms.Label();
            this.tbPassword = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.cbAlgorythm = new System.Windows.Forms.ComboBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label7 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // FileSystemView
            // 
            this.FileSystemView.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.FileSystemView.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FileSystemView.ImageIndex = 0;
            this.FileSystemView.ImageList = this.imageList1;
            this.FileSystemView.Location = new System.Drawing.Point(25, 47);
            this.FileSystemView.Name = "FileSystemView";
            this.FileSystemView.SelectedImageIndex = 6;
            this.FileSystemView.Size = new System.Drawing.Size(405, 221);
            this.FileSystemView.TabIndex = 0;
            this.FileSystemView.DoubleClick += new System.EventHandler(this.FileSystemView_DoubleClick);
            this.FileSystemView.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FileSystemView_KeyDown);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "smilefolderblank_99355.png");
            this.imageList1.Images.SetKeyName(1, "pdf (2).png");
            this.imageList1.Images.SetKeyName(2, "jpeg (3).png");
            this.imageList1.Images.SetKeyName(3, "png (3).png");
            this.imageList1.Images.SetKeyName(4, "txt (3).png");
            this.imageList1.Images.SetKeyName(5, "docx (2).png");
            this.imageList1.Images.SetKeyName(6, "selected.png");
            // 
            // btnUpload
            // 
            this.btnUpload.Location = new System.Drawing.Point(22, 123);
            this.btnUpload.Name = "btnUpload";
            this.btnUpload.Size = new System.Drawing.Size(146, 41);
            this.btnUpload.TabIndex = 2;
            this.btnUpload.Text = "Upload";
            this.btnUpload.UseVisualStyleBackColor = true;
            this.btnUpload.Click += new System.EventHandler(this.BtnUpload_Click);
            // 
            // btnDownload
            // 
            this.btnDownload.Location = new System.Drawing.Point(22, 170);
            this.btnDownload.Name = "btnDownload";
            this.btnDownload.Size = new System.Drawing.Size(146, 41);
            this.btnDownload.TabIndex = 3;
            this.btnDownload.Text = "Download";
            this.btnDownload.UseVisualStyleBackColor = true;
            this.btnDownload.Click += new System.EventHandler(this.BtnDownload_Click);
            // 
            // btnNewFile
            // 
            this.btnNewFile.Location = new System.Drawing.Point(22, 12);
            this.btnNewFile.Name = "btnNewFile";
            this.btnNewFile.Size = new System.Drawing.Size(146, 41);
            this.btnNewFile.TabIndex = 0;
            this.btnNewFile.Text = "Nova datoteka";
            this.btnNewFile.UseVisualStyleBackColor = true;
            this.btnNewFile.Click += new System.EventHandler(this.BtnNewFile_Click);
            // 
            // btnEditFile
            // 
            this.btnEditFile.Location = new System.Drawing.Point(22, 59);
            this.btnEditFile.Name = "btnEditFile";
            this.btnEditFile.Size = new System.Drawing.Size(146, 41);
            this.btnEditFile.TabIndex = 1;
            this.btnEditFile.Text = "Uredi datoteku";
            this.btnEditFile.UseVisualStyleBackColor = true;
            this.btnEditFile.Click += new System.EventHandler(this.BtnEditFile_Click);
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(25, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(84, 20);
            this.label1.TabIndex = 7;
            this.label1.Text = "Moji fajlovi:";
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(25, 286);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(110, 20);
            this.label2.TabIndex = 8;
            this.label2.Text = "Dijeljeni fajlovi:";
            // 
            // SharedView
            // 
            this.SharedView.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.SharedView.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SharedView.ImageIndex = 0;
            this.SharedView.ImageList = this.imageList1;
            this.SharedView.Location = new System.Drawing.Point(25, 309);
            this.SharedView.Name = "SharedView";
            this.SharedView.SelectedImageIndex = 6;
            this.SharedView.Size = new System.Drawing.Size(405, 215);
            this.SharedView.TabIndex = 1;
            this.SharedView.DoubleClick += new System.EventHandler(this.SharedView_DoubleClick);
            this.SharedView.KeyDown += new System.Windows.Forms.KeyEventHandler(this.SharedView_KeyDown);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(26, 557);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(320, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "*Pritisnite \'delete\' dugme na tastaturi da obrišete izabrani fajl/folder.";
            // 
            // btnMakeDirectory
            // 
            this.btnMakeDirectory.Location = new System.Drawing.Point(213, 59);
            this.btnMakeDirectory.Name = "btnMakeDirectory";
            this.btnMakeDirectory.Size = new System.Drawing.Size(146, 41);
            this.btnMakeDirectory.TabIndex = 5;
            this.btnMakeDirectory.Text = "Kreiraj direktorijum";
            this.btnMakeDirectory.UseVisualStyleBackColor = true;
            this.btnMakeDirectory.Click += new System.EventHandler(this.BtnMakeDirectory_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(210, 12);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(125, 16);
            this.label4.TabIndex = 12;
            this.label4.Text = "Naziv direktorijuma:";
            // 
            // tbMakeDirectory
            // 
            this.tbMakeDirectory.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbMakeDirectory.Location = new System.Drawing.Point(213, 31);
            this.tbMakeDirectory.Name = "tbMakeDirectory";
            this.tbMakeDirectory.Size = new System.Drawing.Size(146, 22);
            this.tbMakeDirectory.TabIndex = 4;
            // 
            // btnShareWith
            // 
            this.btnShareWith.Location = new System.Drawing.Point(209, 162);
            this.btnShareWith.Name = "btnShareWith";
            this.btnShareWith.Size = new System.Drawing.Size(146, 41);
            this.btnShareWith.TabIndex = 3;
            this.btnShareWith.Text = "Podijeli sa korisnikom";
            this.btnShareWith.UseVisualStyleBackColor = true;
            this.btnShareWith.Click += new System.EventHandler(this.BtnShareWith_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(15, 8);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(61, 16);
            this.label5.TabIndex = 15;
            this.label5.Text = "Korisnici:";
            // 
            // listBoxUsers
            // 
            this.listBoxUsers.FormattingEnabled = true;
            this.listBoxUsers.Location = new System.Drawing.Point(18, 30);
            this.listBoxUsers.Name = "listBoxUsers";
            this.listBoxUsers.Size = new System.Drawing.Size(146, 173);
            this.listBoxUsers.TabIndex = 0;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(206, 64);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(143, 16);
            this.label6.TabIndex = 17;
            this.label6.Text = "Lozinka za kriptovanje:";
            // 
            // tbPassword
            // 
            this.tbPassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbPassword.Location = new System.Drawing.Point(209, 83);
            this.tbPassword.Name = "tbPassword";
            this.tbPassword.Size = new System.Drawing.Size(146, 22);
            this.tbPassword.TabIndex = 2;
            this.tbPassword.UseSystemPasswordChar = true;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(206, 8);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(154, 16);
            this.label8.TabIndex = 21;
            this.label8.Text = "Algoritam za kriptovanje:";
            // 
            // cbAlgorythm
            // 
            this.cbAlgorythm.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbAlgorythm.FormattingEnabled = true;
            this.cbAlgorythm.Items.AddRange(new object[] {
            "rc4",
            "des",
            "des3",
            "desx",
            "aes256",
            "aes-256-ecb",
            "aes-256-cbc",
            "aria256",
            "camellia256",
            "cast5-cbc",
            "idea",
            "idea-ofb",
            "sm4-cbc",
            "sm4-ecb"});
            this.cbAlgorythm.Location = new System.Drawing.Point(209, 30);
            this.cbAlgorythm.Name = "cbAlgorythm";
            this.cbAlgorythm.Size = new System.Drawing.Size(146, 21);
            this.cbAlgorythm.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.cbAlgorythm);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.tbPassword);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.listBoxUsers);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.btnShareWith);
            this.panel1.Location = new System.Drawing.Point(478, 309);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(374, 215);
            this.panel1.TabIndex = 23;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.tbMakeDirectory);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.btnMakeDirectory);
            this.panel2.Controls.Add(this.btnEditFile);
            this.panel2.Controls.Add(this.btnNewFile);
            this.panel2.Controls.Add(this.btnDownload);
            this.panel2.Controls.Add(this.btnUpload);
            this.panel2.Location = new System.Drawing.Point(478, 47);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(377, 221);
            this.panel2.TabIndex = 1;
            // 
            // label7
            // 
            this.label7.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(478, 286);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(143, 20);
            this.label7.TabIndex = 25;
            this.label7.Text = "Dijeljenje datoteke:";
            // 
            // label9
            // 
            this.label9.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(478, 24);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(146, 20);
            this.label9.TabIndex = 26;
            this.label9.Text = "Osnovne komande:";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::EncryptedFileSystem.Properties.Resources.cryptoBg;
            this.ClientSize = new System.Drawing.Size(879, 584);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.SharedView);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.FileSystemView);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView FileSystemView;
        private System.Windows.Forms.Button btnUpload;
        private System.Windows.Forms.Button btnDownload;
        private System.Windows.Forms.Button btnNewFile;
        private System.Windows.Forms.Button btnEditFile;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TreeView SharedView;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnMakeDirectory;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbMakeDirectory;
        private System.Windows.Forms.Button btnShareWith;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ListBox listBoxUsers;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox tbPassword;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox cbAlgorythm;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label9;
    }
}