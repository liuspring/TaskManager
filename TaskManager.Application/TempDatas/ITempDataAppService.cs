using System.Security.Cryptography.X509Certificates;
using Abp.Application.Services;
using TaskManager.TempDatas.Dto;

namespace TaskManager.TempDatas
{
    public interface ITempDataAppService : IApplicationService
    {
        int Create(TempDataInput input);

        TempData GetTempDataByTaskId(int taskId);
    }
}
