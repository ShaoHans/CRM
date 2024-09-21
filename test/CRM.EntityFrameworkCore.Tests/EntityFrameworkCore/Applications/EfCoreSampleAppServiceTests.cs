using CRM.Samples;
using Xunit;

namespace CRM.EntityFrameworkCore.Applications;

[Collection(CRMTestConsts.CollectionDefinitionName)]
public class EfCoreSampleAppServiceTests : SampleAppServiceTests<CRMEntityFrameworkCoreTestModule>
{

}
