using System.Threading.Tasks;
using ERP.Configuration.Dto;

namespace ERP.Configuration
{
    public interface IConfigurationAppService
    {
        Task ChangeUiTheme(ChangeUiThemeInput input);
    }
}
