﻿using Volo.Abp.Domain;
using Volo.Abp.Modularity;

namespace CRM;

[DependsOn(
    typeof(AbpDddDomainModule),
    typeof(CRMDomainSharedModule)
)]
public class CRMDomainModule : AbpModule
{

}
