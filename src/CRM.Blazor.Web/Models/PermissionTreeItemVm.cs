using Volo.Abp.PermissionManagement;

namespace CRM.Blazor.Web.Models;

public class PermissionTreeItemVm
{
    public int Depth { get; set; }

    public string Name { get; set; } = default!;

    public string DisplayName { get; set; } = default!;

    public string? ParentName { get; set; } = null;

    public bool IsGranted { get; set; }

    public PermissionTreeItemVm? Parent { get; set; }

    public List<PermissionTreeItemVm> Children { get; set; } = [];

    public PermissionTreeItemVm(int depth, PermissionGrantInfoDto permission)
    {
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
