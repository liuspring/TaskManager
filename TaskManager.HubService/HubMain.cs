using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TaskManager.HubService
{
    public partial class HubMain : Form
    {
        public HubMain()
        {
            InitializeComponent();
            this.ShowInTaskbar = false;
            this.WindowState = FormWindowState.Minimized;
        }

        private void HubMain_Load(object sender, EventArgs e)
        {

        }
    }
}
