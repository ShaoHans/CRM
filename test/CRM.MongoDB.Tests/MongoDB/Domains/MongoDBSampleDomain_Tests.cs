using CRM.Samples;
using Xunit;

namespace CRM.MongoDB.Domains;

[Collection(MongoTestCollection.Name)]
public class MongoDBSampleDomain_Tests : SampleManager_Tests<CRMMongoDbTestModule>
{

}
