using CRM.MongoDB;
using CRM.Samples;
using Xunit;

namespace CRM.MongoDb.Applications;

[Collection(MongoTestCollection.Name)]
public class MongoDBSampleAppService_Tests : SampleAppService_Tests<CRMMongoDbTestModule>
{

}
