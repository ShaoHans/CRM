using Volo.Abp.Modularity;

namespace CRM;

[DependsOn(
    typeof(CRMDomainModule),
    typeof(CRMTestBaseModule)
)]
public class CRMDomainTestModule : AbpModule
{

}
