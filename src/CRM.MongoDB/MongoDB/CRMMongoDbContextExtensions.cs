using Volo.Abp;
using Volo.Abp.MongoDB;

namespace CRM.MongoDB;

public static class CRMMongoDbContextExtensions
{
    public static void ConfigureCRM(
        this IMongoModelBuilder builder)
    {
        Check.NotNull(builder, nameof(builder));
    }
}
