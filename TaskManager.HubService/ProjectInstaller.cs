using System.ComponentModel;
using System.Configuration.Install;

namespace TaskManager.HubService
{
    [RunInstaller(true)]
    public partial class ProjectInstaller : Installer
    {
        public ProjectInstaller()
        {
            InitializeComponent();
        }
    }
}
