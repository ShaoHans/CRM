using CRM.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace CRM;

public abstract class CRMController : AbpControllerBase
{
    protected CRMController()
    {
        LocalizationResource = typeof(CRMResource);
    }
}
