using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Common;
using TaskManager.EntityFramework;

namespace TaskManager.Node
{
    public partial class TaskNode : Form
    {
        public TaskNode()
        {
            InitializeComponent();
            this.ShowInTaskbar = false;
            this.WindowState = FormWindowState.Minimized;
        }

        private void TaskNode_Load(object sender, EventArgs e)
        {
            try
            {
                //if (GlobalConfig.NodeId <= 0)
                //{
                    //string url = GlobalConfig.TaskManagerWebUrl.TrimEnd('/') + "/OpenApi/" + "GetNodeConfigInfo/";
                    //ClientResult r = ApiHelper.Get(url, new
                    //{

                    //});
                    //if (r.success == false)
                    //{
                    //    throw new Exception("请求" + url + "失败,请检查配置中“任务调度平台站点url”配置项");
                    //}

                    //var appconfiginfo = ApiHelper.Data(r);
                    //string connectstring = appconfiginfo.TaskDataBaseConnectString;
                    //appconfiginfo.TaskDataBaseConnectString = StringDESHelper.DecryptDES(connectstring, "dyd88888888");

                    ////var appconfiginfo = new NodeAppConfigInfo();
                    ////appconfiginfo.TaskDataBaseConnectString = "server=192.168.17.201;Initial Catalog=dyd_bs_task;User ID=sa;Password=Xx~!@#;";
                    ////appconfiginfo.NodeID = 1;

                    //if (string.IsNullOrWhiteSpace(GlobalConfig.TaskDataBaseConnectString))
                    //    GlobalConfig.TaskDataBaseConnectString = appconfiginfo.TaskDataBaseConnectString;
                    //if (GlobalConfig.NodeId <= 0)
                    //    GlobalConfig.NodeId = appconfiginfo.NodeId;
                //}

                var path = GlobalConfig.TaskSharedDllsDir + @"\";
                IoHelper.CreateDirectory(path);

                CommandQueueProcessor.Run();

                ////注册后台监控
                //GlobalConfig.Monitors.Add(new SystemMonitor.TaskRecoverMonitor());
                //GlobalConfig.Monitors.Add(new SystemMonitor.TaskPerformanceMonitor());
                //GlobalConfig.Monitors.Add(new SystemMonitor.NodeHeartBeatMonitor());
                //GlobalConfig.Monitors.Add(new SystemMonitor.TaskStopMonitor());
                //this.Text = this.Text + GlobalConfig.NodeId;
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message + @",进程即将退出!");
                Application.Exit();
            }

        }
    }
}
