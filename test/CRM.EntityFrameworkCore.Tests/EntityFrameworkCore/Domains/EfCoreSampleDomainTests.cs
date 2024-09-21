using CRM.Samples;
using Xunit;

namespace CRM.EntityFrameworkCore.Domains;

[Collection(CRMTestConsts.CollectionDefinitionName)]
public class EfCoreSampleDomainTests : SampleDomainTests<CRMEntityFrameworkCoreTestModule>
{

}
