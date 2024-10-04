using Microsoft.AspNetCore.Authorization;
using Radzen;
using Volo.Abp.Identity;
using Volo.Abp.Identity.Localization;

namespace CRM.Blazor.Web.Components.Pages.Role;

public partial class List
{
    protected bool HasManagePermissionsPermission { get; set; }
    protected string ManagePermissionsPolicyName;

    public List()
    {
        ObjectMapperContext = typeof(CRMBlazorWebModule);
        LocalizationResource = typeof(IdentityResource);

        CreatePolicyName = IdentityPermissions.Roles.Create;
        UpdatePolicyName = IdentityPermissions.Roles.Update;
        DeletePolicyName = IdentityPermissions.Roles.Delete;
        ManagePermissionsPolicyName = IdentityPermissions.Roles.ManagePermissions;
    }

    protected override async Task SetPermissionsAsync()
    {
        await base.SetPermissionsAsync();

        HasManagePermissionsPermission = await AuthorizationService.IsGrantedAsync(
            IdentityPermissions.Roles.ManagePermissions
        );
    }

    protected override async Task UpdateGetListInputAsync(LoadDataArgs args)
    {
        GetListInput.Filter = _keyword;
        await base.UpdateGetListInputAsync(args);
    }

    private async Task OpenCreateRoleDialog()
    {
        bool result = await DialogService.OpenAsync<Create>(
            "添加角色",
            options: new DialogOptions()
            {
                Draggable = true,
                Width = "600px",
                Height = "450px"
            }
        );

        if (result)
        {
            await _grid.Reload();
        }
    }

    private async Task OpenEditRoleDialog(IdentityRoleDto role)
    {
        bool result = await DialogService.OpenAsync<Edit>(
            "编辑角色",
            new Dictionary<string, object>() { { "Role", role } },
            options: new DialogOptions()
            {
                Draggable = true,
                Width = "600px",
                Height = "450px"
            }
        );

        if (result)
        {
            await _grid.Reload();
        }
    }

    private async Task OpenAssignPermissionDialog(IdentityRoleDto role)
    {
        await DialogService.OpenAsync<Permission>(
            $"分配权限 - {role.Name}",
            parameters: new Dictionary<string, object>()
            {
                { "ProviderName", "R" },
                { "ProviderKey", role.Name }
            },
            options: new DialogOptions()
            {
                Draggable = true,
                Width = "800px",
                Height = "700px"
            }
        );
    }

    private async Task ShowDeleteConfirmDialogAsync(Guid roleId)
    {
        var result = await DialogService.Confirm(
            "角色一旦删除将无法恢复，确认删除吗?",
            "删除角色",
            new ConfirmOptions() { OkButtonText = "确定", CancelButtonText = "取消" }
        );

        if (result == true)
        {
            await AppService.DeleteAsync(roleId);
            await _grid.Reload();
            NotificationService.Success("删除成功");
        }
    }
}
