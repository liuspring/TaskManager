using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services;

namespace TaskManager.Versions
{
    public interface IVersionInfoService : IApplicationService
    {
        VersionInfo GetVersionInfo(int taskId, int version);
    }
}
