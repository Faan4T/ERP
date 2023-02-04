using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;
using ERP.Authorization;

namespace ERP
{
    [DependsOn(
        typeof(ERPCoreModule), 
        typeof(AbpAutoMapperModule))]
    public class ERPApplicationModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Authorization.Providers.Add<ERPAuthorizationProvider>();
        }

        public override void Initialize()
        {
            var thisAssembly = typeof(ERPApplicationModule).GetAssembly();

            IocManager.RegisterAssemblyByConvention(thisAssembly);

            Configuration.Modules.AbpAutoMapper().Configurators.Add(
                // Scan the assembly for classes which inherit from AutoMapper.Profile
                cfg => cfg.AddMaps(thisAssembly)
            );
        }
    }
}
