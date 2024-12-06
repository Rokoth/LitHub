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
    public partial class NewLocalPasswordForm : Form
    {
        public string Password { get { return textBox1.Text; } }

        public NewLocalPasswordForm()
        {
            InitializeComponent();
        }
    }
}
