using Abp.Application.Services;
using TaskManager.Errors.Dto;

namespace TaskManager.Errors
{
    public interface IErrorAppService : IApplicationService
    {
        int Create(ErrorInput input);
    }
}
