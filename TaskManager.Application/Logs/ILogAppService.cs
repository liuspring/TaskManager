using System.Security.Cryptography.X509Certificates;
using Abp.Application.Services;
using TaskManager.Logs.Dto;

namespace TaskManager.Logs
{
    public interface ILogAppService : IApplicationService
    {
        int Create(LogInput input);
    }
}
