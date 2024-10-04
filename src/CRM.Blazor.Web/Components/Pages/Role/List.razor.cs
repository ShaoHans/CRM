using Microsoft.AspNetCore.Authorization;

using Radzen;
using Radzen.Blazor;
using Volo.Abp.Identity;
using Volo.Abp.Identity.Localization;

namespace CRM.Blazor.Web.Components.Pages.Role;

public partial class List
{
    RadzenDataGrid<IdentityRoleDto> _grid = default!;
    IEnumerable<int> pageSizeOptions = [10, 20, 30];
    IEnumerable<IdentityRoleDto> roles = [];
    readonly bool showPagerSummary = true;
    int totalCount = 0;
    bool isLoading = true;
    string? _keyword = null;
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

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();
        await LoadRolesAsync(new LoadDataArgs());
    }

    protected override async Task SetPermissionsAsync()
    {
        await base.SetPermissionsAsync();

        HasManagePermissionsPermission =
            await AuthorizationService.IsGrantedAsync(IdentityPermissions.Roles.ManagePermissions);
    }

    private async Task LoadRolesAsync(LoadDataArgs args)
    {
        isLoading = true;
        var result = await AppService.GetListAsync(
            new GetIdentityRolesInput
            {
                SkipCount = args.Skip ?? 0,
                MaxResultCount = args.Top ?? 10,
                Filter = _keyword,
                Sorting = args.OrderBy
            }
        );

        roles = result.Items;
        totalCount = (int)result.TotalCount;
        isLoading = false;
        StateHasChanged();
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
