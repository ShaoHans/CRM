using Xunit;

namespace CRM.EntityFrameworkCore;

[CollectionDefinition(CRMTestConsts.CollectionDefinitionName)]
public class CRMEntityFrameworkCoreCollection : ICollectionFixture<CRMEntityFrameworkCoreFixture>
{

}
