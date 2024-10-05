using CRM.Blazor.Web.Models;

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
        var dialogFromOption = new DialogFromOption<IdentityRoleCreateDto>
        {
            OkSubmitText = "����",
            CancelButtonText = "ȡ��",
            OnSubmit = CreateRoleAsync,
            OnCancel = CloseDialog
        };

        bool result = await DialogService.OpenAsync<Create>(
            title: "��ӽ�ɫ",
            parameters:new Dictionary<string, object>
            {
                { "DialogFromOption", dialogFromOption},
            },
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

    void CloseDialog()
    {
        DialogService.Close(false);
    }

    async Task CreateRoleAsync(IdentityRoleCreateDto model)
    {
        try
        {
            await AppService.CreateAsync(model);
            NotificationService.Success("����ɹ�");
            DialogService.Close(true);
        }
        catch (Exception ex)
        {
            NotificationService.Error(ex.Message);
        }
    }

    private async Task OpenEditRoleDialog(IdentityRoleDto role)
    {
        var dialogFromOption = new DialogFromOption<IdentityRoleUpdateDto>
        {
            OkSubmitText = "����",
            CancelButtonText = "ȡ��",
            OnSubmit = UpdateRoleAsync,
            OnCancel = CloseDialog
        };

        EditingEntityId = role.Id;
        bool result = await DialogService.OpenAsync<Edit>(
            title: "�༭��ɫ",
            parameters: new Dictionary<string, object>() 
            {
                { "DialogFromOption", dialogFromOption},
                { "Role", role } 
            },
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

    async Task UpdateRoleAsync(IdentityRoleUpdateDto model)
    {

        try
        {
            await AppService.UpdateAsync(EditingEntityId, model);
            NotificationService.Success("����ɹ�");
            DialogService.Close(true);
        }
        catch (Exception ex)
        {
            NotificationService.Error(ex.Message);
        }
    }

    private async Task OpenAssignPermissionDialog(IdentityRoleDto role)
    {
        await DialogService.OpenAsync<Permission>(
            $"����Ȩ�� - {role.Name}",
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
            "��ɫһ��ɾ�����޷��ָ���ȷ��ɾ����?",
            "ɾ����ɫ",
            new ConfirmOptions() { OkButtonText = "ȷ��", CancelButtonText = "ȡ��" }
        );

        if (result == true)
        {
            await AppService.DeleteAsync(roleId);
            await _grid.Reload();
            NotificationService.Success("ɾ���ɹ�");
        }
    }
}
