using System;
using System.Windows.Forms;
using Common;

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
            try
            {
                var path = GlobalConfig.TaskSharedDllsDir + @"\";
                IoHelper.CreateDirectory(path);
                CommandQueueProcessor.Run();
                //注册后台监控
                GlobalConfig.Monitors.Add(new SystemMonitor.TaskRecoverMonitor());
                GlobalConfig.Monitors.Add(new SystemMonitor.TaskPerformanceMonitor());
                GlobalConfig.Monitors.Add(new SystemMonitor.NodeHeartBeatMonitor());
                GlobalConfig.Monitors.Add(new SystemMonitor.TaskStopMonitor());
                this.Text = this.Text + GlobalConfig.NodeId;
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message + @",进程即将退出!");
                Application.Exit();
            }
        }
    }
}
