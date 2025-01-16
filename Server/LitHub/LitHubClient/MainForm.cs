using LitHubClient.Auth;
using LitHubClient.Events;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using Unity;

namespace LitHubClient
{
    public interface IMainForm
    {

    }

    public partial class MainForm : Form, IMainForm
    {
        private LoginForm loginForm;
        private LocalPasswordForm localPasswordForm;
        private DataTable listDataTable;
        private DataTable listFullDataTable;
        private readonly ISession _session;
        private readonly IUnityContainer container;

        public MainForm(IUnityContainer container)
        {
            InitializeComponent();
            this.container = container;
            _session = container.Resolve<ISession>();

            ConnectToRepos();

            dataGridView1.DataSource = bindingSource1;
        }

        private void ConnectToRepos()
        {
            if (_session.UseLocalDataBase)
            {
                Hide();
                localPasswordForm = new LocalPasswordForm();
                localPasswordForm.ShowDialog();
            }
        }

        private void OnGetList(ListEventArgs e)
        {
            BeginInvoke(new Action(() =>
            {
                listDataTable = new DataTable();
                bool first = true;
                foreach (var jt in JArray.Parse(((JObject)e.Data)["data"].ToString()))
                {
                    if (first)
                    {
                        foreach (var j in JsonConvert.DeserializeObject<Dictionary<string, object>>(jt.ToString()))
                        {
                            DataColumn column = new DataColumn(j.Key);
                            listDataTable.Columns.Add(column);
                        }
                        first = false;
                    }
                    DataRow row = listDataTable.NewRow();
                    foreach (var j in JsonConvert.DeserializeObject<Dictionary<string, object>>(jt.ToString()))
                    {
                        row[j.Key] = j.Value;
                    }
                    listDataTable.Rows.Add(row);
                }
                listFullDataTable = listDataTable.Copy();

                bindingSource1.DataSource = listFullDataTable;
            }));
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            WebSocketConnector.Send("None", WebSocketConnector.Types.list);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            loginForm = container.Resolve<LoginForm>();
            loginForm.ShowDialog();
            if (WebSocketConnector.Inited)
            {
                WebSocketConnector.OnGetList += OnGetList;
            }
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        private void AboutToolStripMenuItemClick(object sender, EventArgs e)
        {
            AboutForm form = new AboutForm();
            form.ShowDialog();
        }
    }
}