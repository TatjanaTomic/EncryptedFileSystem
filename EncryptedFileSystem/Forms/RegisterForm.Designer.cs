
namespace EncryptedFileSystem.Forms
{
    partial class RegisterForm
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
            this.tbUsername = new System.Windows.Forms.TextBox();
            this.tbPassword = new System.Windows.Forms.TextBox();
            this.lbUsername = new System.Windows.Forms.Label();
            this.lbPassword = new System.Windows.Forms.Label();
            this.btnRegister = new System.Windows.Forms.Button();
            this.lbRepeatedPassword = new System.Windows.Forms.Label();
            this.tbRepeatedPassword = new System.Windows.Forms.TextBox();
            this.cbHash = new System.Windows.Forms.ComboBox();
            this.lbHash = new System.Windows.Forms.Label();
            this.lbEncrypt = new System.Windows.Forms.Label();
            this.cbEncrypt = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // tbUsername
            // 
            this.tbUsername.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbUsername.Location = new System.Drawing.Point(339, 41);
            this.tbUsername.Name = "tbUsername";
            this.tbUsername.Size = new System.Drawing.Size(220, 26);
            this.tbUsername.TabIndex = 0;
            this.tbUsername.WordWrap = false;
            this.tbUsername.Click += new System.EventHandler(this.tbUsername_Click);
            // 
            // tbPassword
            // 
            this.tbPassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbPassword.Location = new System.Drawing.Point(339, 107);
            this.tbPassword.Name = "tbPassword";
            this.tbPassword.Size = new System.Drawing.Size(220, 26);
            this.tbPassword.TabIndex = 1;
            this.tbPassword.UseSystemPasswordChar = true;
            this.tbPassword.WordWrap = false;
            this.tbPassword.Click += new System.EventHandler(this.tbPassword_Click);
            // 
            // lbUsername
            // 
            this.lbUsername.AutoSize = true;
            this.lbUsername.BackColor = System.Drawing.Color.Transparent;
            this.lbUsername.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbUsername.Location = new System.Drawing.Point(334, 17);
            this.lbUsername.Name = "lbUsername";
            this.lbUsername.Size = new System.Drawing.Size(114, 20);
            this.lbUsername.TabIndex = 2;
            this.lbUsername.Text = "Korisničko ime:";
            // 
            // lbPassword
            // 
            this.lbPassword.AutoSize = true;
            this.lbPassword.BackColor = System.Drawing.Color.Transparent;
            this.lbPassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbPassword.Location = new System.Drawing.Point(334, 83);
            this.lbPassword.Name = "lbPassword";
            this.lbPassword.Size = new System.Drawing.Size(68, 20);
            this.lbPassword.TabIndex = 3;
            this.lbPassword.Text = "Lozinka:";
            // 
            // btnRegister
            // 
            this.btnRegister.BackColor = System.Drawing.Color.DarkGray;
            this.btnRegister.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRegister.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRegister.Location = new System.Drawing.Point(371, 373);
            this.btnRegister.Name = "btnRegister";
            this.btnRegister.Size = new System.Drawing.Size(166, 41);
            this.btnRegister.TabIndex = 4;
            this.btnRegister.Text = "Registrujte se";
            this.btnRegister.UseVisualStyleBackColor = false;
            this.btnRegister.Click += new System.EventHandler(this.btnRegister_Click);
            // 
            // lbRepeatedPassword
            // 
            this.lbRepeatedPassword.AutoSize = true;
            this.lbRepeatedPassword.BackColor = System.Drawing.Color.Transparent;
            this.lbRepeatedPassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbRepeatedPassword.Location = new System.Drawing.Point(333, 150);
            this.lbRepeatedPassword.Name = "lbRepeatedPassword";
            this.lbRepeatedPassword.Size = new System.Drawing.Size(143, 20);
            this.lbRepeatedPassword.TabIndex = 6;
            this.lbRepeatedPassword.Text = "Ponovljena lozinka:";
            // 
            // tbRepeatedPassword
            // 
            this.tbRepeatedPassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbRepeatedPassword.Location = new System.Drawing.Point(338, 174);
            this.tbRepeatedPassword.Name = "tbRepeatedPassword";
            this.tbRepeatedPassword.Size = new System.Drawing.Size(220, 26);
            this.tbRepeatedPassword.TabIndex = 5;
            this.tbRepeatedPassword.UseSystemPasswordChar = true;
            this.tbRepeatedPassword.WordWrap = false;
            this.tbRepeatedPassword.Click += new System.EventHandler(this.tbRepeatedPassword_Click);
            // 
            // cbHash
            // 
            this.cbHash.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbHash.FormattingEnabled = true;
            this.cbHash.Items.AddRange(new object[] {
            "md5",
            "SHA-256",
            "SHA-512"});
            this.cbHash.Location = new System.Drawing.Point(338, 238);
            this.cbHash.Name = "cbHash";
            this.cbHash.Size = new System.Drawing.Size(219, 24);
            this.cbHash.TabIndex = 7;
            // 
            // lbHash
            // 
            this.lbHash.AutoSize = true;
            this.lbHash.BackColor = System.Drawing.Color.Transparent;
            this.lbHash.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbHash.Location = new System.Drawing.Point(334, 215);
            this.lbHash.Name = "lbHash";
            this.lbHash.Size = new System.Drawing.Size(169, 20);
            this.lbHash.TabIndex = 8;
            this.lbHash.Text = "Algoritam za heširanje:";
            // 
            // lbEncrypt
            // 
            this.lbEncrypt.AutoSize = true;
            this.lbEncrypt.BackColor = System.Drawing.Color.Transparent;
            this.lbEncrypt.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbEncrypt.Location = new System.Drawing.Point(334, 278);
            this.lbEncrypt.Name = "lbEncrypt";
            this.lbEncrypt.Size = new System.Drawing.Size(181, 20);
            this.lbEncrypt.TabIndex = 10;
            this.lbEncrypt.Text = "Algoritam za kriptovanje:";
            // 
            // cbEncrypt
            // 
            this.cbEncrypt.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbEncrypt.FormattingEnabled = true;
            this.cbEncrypt.Items.AddRange(new object[] {
            "aes",
            "des",
            "rc4"});
            this.cbEncrypt.Location = new System.Drawing.Point(338, 301);
            this.cbEncrypt.Name = "cbEncrypt";
            this.cbEncrypt.Size = new System.Drawing.Size(219, 24);
            this.cbEncrypt.TabIndex = 9;
            // 
            // RegisterForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::EncryptedFileSystem.Properties.Resources.cryptoBg;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(582, 426);
            this.Controls.Add(this.lbEncrypt);
            this.Controls.Add(this.cbEncrypt);
            this.Controls.Add(this.lbHash);
            this.Controls.Add(this.cbHash);
            this.Controls.Add(this.lbRepeatedPassword);
            this.Controls.Add(this.tbRepeatedPassword);
            this.Controls.Add(this.btnRegister);
            this.Controls.Add(this.lbPassword);
            this.Controls.Add(this.lbUsername);
            this.Controls.Add(this.tbPassword);
            this.Controls.Add(this.tbUsername);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "RegisterForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "RegisterForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbUsername;
        private System.Windows.Forms.TextBox tbPassword;
        private System.Windows.Forms.Label lbUsername;
        private System.Windows.Forms.Label lbPassword;
        private System.Windows.Forms.Button btnRegister;
        private System.Windows.Forms.Label lbRepeatedPassword;
        private System.Windows.Forms.TextBox tbRepeatedPassword;
        private System.Windows.Forms.ComboBox cbHash;
        private System.Windows.Forms.Label lbHash;
        private System.Windows.Forms.Label lbEncrypt;
        private System.Windows.Forms.ComboBox cbEncrypt;
    }
}