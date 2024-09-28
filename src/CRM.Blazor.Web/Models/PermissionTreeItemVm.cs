using Volo.Abp.PermissionManagement;

namespace CRM.Blazor.Web.Models;

public class PermissionTreeItemVm
{
    public int Depth { get; set; }

    public PermissionGrantInfoDto Permission { get; set; } = default!;

    public List<PermissionTreeItemVm> Children { get; set; } = [];

    public PermissionTreeItemVm(int depth, PermissionGrantInfoDto permission, List<PermissionTreeItemVm> children)
    {
        Depth = depth;
        Permission = permission;
        Children = children;
    }
}
