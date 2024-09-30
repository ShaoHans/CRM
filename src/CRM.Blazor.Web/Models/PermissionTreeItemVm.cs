using Volo.Abp.PermissionManagement;

namespace CRM.Blazor.Web.Models;

public class PermissionTreeItemVm
{
    public string GroupName { get; set; } = default!;

    public int Depth { get; set; }

    public string Name { get; set; } = default!;

    public string DisplayName { get; set; } = default!;

    public string? ParentName { get; set; } = null;

    public bool IsGranted { get; set; }

    public PermissionTreeItemVm? Parent { get; set; }

    public List<PermissionTreeItemVm> Children { get; set; } = [];

    public PermissionTreeItemVm(string groupName, int depth, PermissionGrantInfoDto permission)
    {
        GroupName = groupName;
        Depth = depth;
        Name = permission.Name;
        DisplayName = permission.DisplayName;
        ParentName = permission.ParentName;
        IsGranted = permission.IsGranted;
    }

    public void SetParent(PermissionTreeItemVm? parent)
    {
        Parent = parent;
    }
}
