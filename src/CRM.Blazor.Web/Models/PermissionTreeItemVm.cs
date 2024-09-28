using Volo.Abp.PermissionManagement;

namespace CRM.Blazor.Web.Models;

public class PermissionTreeItemVm
{
    public int Depth { get; set; }

    public string GroupName { get; set; } = default!;

    public PermissionGrantInfoDto Permission { get; set; } = default!;

    public List<PermissionTreeItemVm> Children { get; set; } = [];

    public string DisplayName
    {
        get { return Permission.DisplayName; }
    }

    public PermissionTreeItemVm(
        int depth,
        string groupName,
        PermissionGrantInfoDto permission,
        List<PermissionTreeItemVm> children
    )
    {
        Depth = depth;
        GroupName = groupName;
        Permission = permission;
        Children = children;
    }
}
