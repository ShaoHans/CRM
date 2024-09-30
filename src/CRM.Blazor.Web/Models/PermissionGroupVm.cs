using Volo.Abp.PermissionManagement;

namespace CRM.Blazor.Web.Models;

public class PermissionGroupVm : PermissionGroupDto
{
    public bool GrantAll
    {
        get { return Permissions.All(x => x.IsGranted); }
        set { }
    }
}
