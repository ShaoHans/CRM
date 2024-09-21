using Volo.Abp.Modularity;

namespace CRM;

[DependsOn(
    typeof(CRMApplicationModule),
    typeof(CRMDomainTestModule)
)]
public class CRMApplicationTestModule : AbpModule
{

}
