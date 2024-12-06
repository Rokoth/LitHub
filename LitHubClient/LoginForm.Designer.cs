namespace LitHubClient
{
    partial class LoginForm
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
            this.LoginLabel = new System.Windows.Forms.Label();
            this.LoginTextBox = new System.Windows.Forms.TextBox();
            this.ServerPasswordLabel = new System.Windows.Forms.Label();
            this.ServerPasswordTextBox = new System.Windows.Forms.TextBox();
            this.ConnectButton = new System.Windows.Forms.Button();
            this.CancelButton = new System.Windows.Forms.Button();
            this.ServerLabel = new System.Windows.Forms.Label();
            this.ServerTextBox = new System.Windows.Forms.TextBox();
            this.Headerlabel = new System.Windows.Forms.Label();
            this.LocalPasswordTextBox = new System.Windows.Forms.TextBox();
            this.LocalPasswordLabel = new System.Windows.Forms.Label();
            this.UseLocalBaseCheckBox = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // LoginLabel
            // 
            this.LoginLabel.AutoSize = true;
            this.LoginLabel.Location = new System.Drawing.Point(6, 66);
            this.LoginLabel.Name = "LoginLabel";
            this.LoginLabel.Size = new System.Drawing.Size(44, 13);
            this.LoginLabel.TabIndex = 0;
            this.LoginLabel.Text = "Логин: ";
            // 
            // LoginTextBox
            // 
            this.LoginTextBox.Location = new System.Drawing.Point(63, 63);
            this.LoginTextBox.Name = "LoginTextBox";
            this.LoginTextBox.Size = new System.Drawing.Size(208, 20);
            this.LoginTextBox.TabIndex = 1;
            // 
            // ServerPasswordLabel
            // 
            this.ServerPasswordLabel.AutoSize = true;
            this.ServerPasswordLabel.Location = new System.Drawing.Point(6, 93);
            this.ServerPasswordLabel.Name = "ServerPasswordLabel";
            this.ServerPasswordLabel.Size = new System.Drawing.Size(51, 13);
            this.ServerPasswordLabel.TabIndex = 2;
            this.ServerPasswordLabel.Text = "Пароль: ";
            // 
            // ServerPasswordTextBox
            // 
            this.ServerPasswordTextBox.Location = new System.Drawing.Point(63, 89);
            this.ServerPasswordTextBox.Name = "ServerPasswordTextBox";
            this.ServerPasswordTextBox.Size = new System.Drawing.Size(208, 20);
            this.ServerPasswordTextBox.TabIndex = 3;
            this.ServerPasswordTextBox.UseSystemPasswordChar = true;
            // 
            // ConnectButton
            // 
            this.ConnectButton.Location = new System.Drawing.Point(9, 160);
            this.ConnectButton.Name = "ConnectButton";
            this.ConnectButton.Size = new System.Drawing.Size(132, 26);
            this.ConnectButton.TabIndex = 4;
            this.ConnectButton.Text = "Подключиться";
            this.ConnectButton.UseVisualStyleBackColor = true;
            this.ConnectButton.Click += new System.EventHandler(this.ConnectButton_Click);
            // 
            // CancelButton
            // 
            this.CancelButton.Location = new System.Drawing.Point(148, 160);
            this.CancelButton.Name = "CancelButton";
            this.CancelButton.Size = new System.Drawing.Size(123, 26);
            this.CancelButton.TabIndex = 5;
            this.CancelButton.Text = "Отмена";
            this.CancelButton.UseVisualStyleBackColor = true;
            this.CancelButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // ServerLabel
            // 
            this.ServerLabel.AutoSize = true;
            this.ServerLabel.Location = new System.Drawing.Point(6, 38);
            this.ServerLabel.Name = "ServerLabel";
            this.ServerLabel.Size = new System.Drawing.Size(47, 13);
            this.ServerLabel.TabIndex = 6;
            this.ServerLabel.Text = "Сервер:";
            // 
            // ServerTextBox
            // 
            this.ServerTextBox.Location = new System.Drawing.Point(63, 36);
            this.ServerTextBox.Name = "ServerTextBox";
            this.ServerTextBox.Size = new System.Drawing.Size(208, 20);
            this.ServerTextBox.TabIndex = 7;
            // 
            // Headerlabel
            // 
            this.Headerlabel.AutoSize = true;
            this.Headerlabel.Location = new System.Drawing.Point(13, 13);
            this.Headerlabel.Name = "Headerlabel";
            this.Headerlabel.Size = new System.Drawing.Size(76, 13);
            this.Headerlabel.TabIndex = 8;
            this.Headerlabel.Text = "Подключение";
            // 
            // LocalPasswordTextBox
            // 
            this.LocalPasswordTextBox.Location = new System.Drawing.Point(64, 113);
            this.LocalPasswordTextBox.Name = "LocalPasswordTextBox";
            this.LocalPasswordTextBox.Size = new System.Drawing.Size(208, 20);
            this.LocalPasswordTextBox.TabIndex = 10;
            this.LocalPasswordTextBox.UseSystemPasswordChar = true;
            // 
            // LocalPasswordLabel
            // 
            this.LocalPasswordLabel.AutoSize = true;
            this.LocalPasswordLabel.Location = new System.Drawing.Point(7, 117);
            this.LocalPasswordLabel.Name = "LocalPasswordLabel";
            this.LocalPasswordLabel.Size = new System.Drawing.Size(51, 13);
            this.LocalPasswordLabel.TabIndex = 9;
            this.LocalPasswordLabel.Text = "Пароль: ";
            // 
            // UseLocalBaseCheckBox
            // 
            this.UseLocalBaseCheckBox.AutoSize = true;
            this.UseLocalBaseCheckBox.Location = new System.Drawing.Point(10, 139);
            this.UseLocalBaseCheckBox.Name = "UseLocalBaseCheckBox";
            this.UseLocalBaseCheckBox.Size = new System.Drawing.Size(223, 17);
            this.UseLocalBaseCheckBox.TabIndex = 11;
            this.UseLocalBaseCheckBox.Text = "Использовать локальную базу данных";
            this.UseLocalBaseCheckBox.UseVisualStyleBackColor = true;
            this.UseLocalBaseCheckBox.CheckedChanged += new System.EventHandler(this.UseLocalBaseCheckBox_CheckedChanged);
            // 
            // LoginForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 188);
            this.Controls.Add(this.UseLocalBaseCheckBox);
            this.Controls.Add(this.LocalPasswordTextBox);
            this.Controls.Add(this.LocalPasswordLabel);
            this.Controls.Add(this.Headerlabel);
            this.Controls.Add(this.ServerTextBox);
            this.Controls.Add(this.ServerLabel);
            this.Controls.Add(this.CancelButton);
            this.Controls.Add(this.ConnectButton);
            this.Controls.Add(this.ServerPasswordTextBox);
            this.Controls.Add(this.ServerPasswordLabel);
            this.Controls.Add(this.LoginTextBox);
            this.Controls.Add(this.LoginLabel);
            this.Name = "LoginForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Вход в программу";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label LoginLabel;
        private System.Windows.Forms.TextBox LoginTextBox;
        private System.Windows.Forms.Label ServerPasswordLabel;
        private System.Windows.Forms.TextBox ServerPasswordTextBox;
        private System.Windows.Forms.Button ConnectButton;
        private System.Windows.Forms.Button CancelButton;
        private System.Windows.Forms.Label ServerLabel;
        private System.Windows.Forms.TextBox ServerTextBox;
        private System.Windows.Forms.Label Headerlabel;
        private System.Windows.Forms.TextBox LocalPasswordTextBox;
        private System.Windows.Forms.Label LocalPasswordLabel;
        private System.Windows.Forms.CheckBox UseLocalBaseCheckBox;
    }
}