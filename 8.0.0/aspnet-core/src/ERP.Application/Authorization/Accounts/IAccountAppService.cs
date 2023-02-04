using System.Threading.Tasks;
using Abp.Application.Services;
using ERP.Authorization.Accounts.Dto;

namespace ERP.Authorization.Accounts
{
    public interface IAccountAppService : IApplicationService
    {
        Task<IsTenantAvailableOutput> IsTenantAvailable(IsTenantAvailableInput input);

        Task<RegisterOutput> Register(RegisterInput input);
    }
}
