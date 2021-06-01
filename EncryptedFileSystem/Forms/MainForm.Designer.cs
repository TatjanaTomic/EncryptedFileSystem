
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
            this.FileSystemView = new System.Windows.Forms.TreeView();
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
            this.SuspendLayout();
            // 
            // FileSystemView
            // 
            this.FileSystemView.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.FileSystemView.Location = new System.Drawing.Point(283, 32);
            this.FileSystemView.Name = "FileSystemView";
            this.FileSystemView.Size = new System.Drawing.Size(367, 215);
            this.FileSystemView.TabIndex = 0;
            this.FileSystemView.DoubleClick += new System.EventHandler(this.FileSystemView_DoubleClick);
            this.FileSystemView.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FileSystemView_KeyDown);
            // 
            // btnUpload
            // 
            this.btnUpload.Location = new System.Drawing.Point(12, 159);
            this.btnUpload.Name = "btnUpload";
            this.btnUpload.Size = new System.Drawing.Size(146, 41);
            this.btnUpload.TabIndex = 3;
            this.btnUpload.Text = "Upload";
            this.btnUpload.UseVisualStyleBackColor = true;
            this.btnUpload.Click += new System.EventHandler(this.BtnUpload_Click);
            // 
            // btnDownload
            // 
            this.btnDownload.Location = new System.Drawing.Point(12, 206);
            this.btnDownload.Name = "btnDownload";
            this.btnDownload.Size = new System.Drawing.Size(146, 41);
            this.btnDownload.TabIndex = 4;
            this.btnDownload.Text = "Download";
            this.btnDownload.UseVisualStyleBackColor = true;
            this.btnDownload.Click += new System.EventHandler(this.BtnDownload_Click);
            // 
            // btnNewFile
            // 
            this.btnNewFile.Location = new System.Drawing.Point(12, 32);
            this.btnNewFile.Name = "btnNewFile";
            this.btnNewFile.Size = new System.Drawing.Size(146, 41);
            this.btnNewFile.TabIndex = 5;
            this.btnNewFile.Text = "Nova datoteka";
            this.btnNewFile.UseVisualStyleBackColor = true;
            this.btnNewFile.Click += new System.EventHandler(this.BtnNewFile_Click);
            // 
            // btnEditFile
            // 
            this.btnEditFile.Location = new System.Drawing.Point(12, 79);
            this.btnEditFile.Name = "btnEditFile";
            this.btnEditFile.Size = new System.Drawing.Size(146, 41);
            this.btnEditFile.TabIndex = 6;
            this.btnEditFile.Text = "Uredi datoteku";
            this.btnEditFile.UseVisualStyleBackColor = true;
            this.btnEditFile.Click += new System.EventHandler(this.BtnEditFile_Click);
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(279, 9);
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
            this.label2.Location = new System.Drawing.Point(279, 268);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(110, 20);
            this.label2.TabIndex = 8;
            this.label2.Text = "Dijeljeni fajlovi:";
            // 
            // SharedView
            // 
            this.SharedView.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.SharedView.Location = new System.Drawing.Point(283, 291);
            this.SharedView.Name = "SharedView";
            this.SharedView.Size = new System.Drawing.Size(367, 215);
            this.SharedView.TabIndex = 9;
            this.SharedView.KeyDown += new System.Windows.Forms.KeyEventHandler(this.SharedView_KeyDown);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(533, 532);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(320, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "*Pritisnite \'delete\' dugme na tastaturi da obrišete izabrani fajl/folder.";
            // 
            // btnMakeDirectory
            // 
            this.btnMakeDirectory.Location = new System.Drawing.Point(703, 100);
            this.btnMakeDirectory.Name = "btnMakeDirectory";
            this.btnMakeDirectory.Size = new System.Drawing.Size(150, 41);
            this.btnMakeDirectory.TabIndex = 11;
            this.btnMakeDirectory.Text = "Kreiraj direktorijum";
            this.btnMakeDirectory.UseVisualStyleBackColor = true;
            this.btnMakeDirectory.Click += new System.EventHandler(this.BtnMakeDirectory_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(704, 32);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(125, 16);
            this.label4.TabIndex = 12;
            this.label4.Text = "Naziv direktorijuma:";
            // 
            // tbMakeDirectory
            // 
            this.tbMakeDirectory.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbMakeDirectory.Location = new System.Drawing.Point(703, 51);
            this.tbMakeDirectory.Name = "tbMakeDirectory";
            this.tbMakeDirectory.Size = new System.Drawing.Size(150, 22);
            this.tbMakeDirectory.TabIndex = 13;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::EncryptedFileSystem.Properties.Resources.cryptoBg;
            this.ClientSize = new System.Drawing.Size(865, 554);
            this.Controls.Add(this.tbMakeDirectory);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btnMakeDirectory);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.SharedView);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnEditFile);
            this.Controls.Add(this.btnNewFile);
            this.Controls.Add(this.btnDownload);
            this.Controls.Add(this.btnUpload);
            this.Controls.Add(this.FileSystemView);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
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
    }
}