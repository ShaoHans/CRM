using Volo.Abp.Modularity;

namespace CRM;

public abstract class CRMApplicationTestBase<TStartupModule> : CRMTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
