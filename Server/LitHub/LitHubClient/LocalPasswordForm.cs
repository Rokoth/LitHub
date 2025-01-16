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
    public partial class LocalPasswordForm : Form
    {        
        private const int MAX_TRIES = 3;
        private static int tries;

        public LocalPasswordForm()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Будет удалена учётная запись со всеми настройками. Продолжить?",
                "Удаление учётной записи",
                MessageBoxButtons.OKCancel) == DialogResult.OK)
                DialogResult = DialogResult.Retry;
        }
    }
}
