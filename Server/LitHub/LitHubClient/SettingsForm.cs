using LitHubClient.Auth;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LitHubClient
{
    public partial class SettingsForm : Form
    {
        private Account account;
        private ISession _session;

        public SettingsForm(ISession session)
        {
            InitializeComponent();
           
            SQLiteConnect();
        }

        private void SQLiteConnect()
        {
            if (_session.UseLocalDataBase)
            {
                if (!SQLiteConnector.Inited)
                {
                    SQLiteConnector.Init(_session.LocalDataBaseFile, _session.LocalDataBasePassword);
                    if (!SQLiteConnector.Inited)
                    {
                        Close();
                    }
                    else
                    {
                        account = SQLiteConnector.GetAccount();
                        serverTextBox.Text = account.Server; 
                        portTextBox.Text = account.Port;
                        nameTextBox.Text = account.Name; 
                        loginTextBox.Text = account.Login; 
                        passwordTextBox.Text = account.Password; 
                        localDirTextBox.Text = account.LocalDir;
                    }
                }
            }
            else
            {
                MessageBox.Show("Локальные настройки не используются");
                Close();
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void selectDirButton_Click(object sender, EventArgs e)
        {
            var result = folderBrowserDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                account.LocalDir = folderBrowserDialog1.SelectedPath;
            }
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            SQLiteConnector.UpdateAccount(serverTextBox.Text, portTextBox.Text, 
                nameTextBox.Text, loginTextBox.Text, passwordTextBox.Text, localDirTextBox.Text);
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
