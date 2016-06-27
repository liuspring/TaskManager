using Abp.Application.Services;

namespace TaskManager.Versions
{
    public interface IVersionInfoService : IApplicationService
    {
        VersionInfo GetVersionInfo(int taskId, int version);
    }
}
