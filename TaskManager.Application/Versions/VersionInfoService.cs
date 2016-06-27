using System.Linq;
using Abp.Domain.Repositories;

namespace TaskManager.Versions
{
    public class VersionInfoService : TaskManagerAppServiceBase, IVersionInfoService
    {

        private readonly IRepository<VersionInfo, int> _versionInfoRepository;


        public VersionInfoService(IRepository<VersionInfo, int> versionInfoRepository)
        {
            _versionInfoRepository = versionInfoRepository;
        }
        public VersionInfo GetVersionInfo(int taskId, int version)
        {
            return _versionInfoRepository.GetAll().FirstOrDefault(a => a.TaskId == taskId && a.VersionType == version);
        }
    }
}
