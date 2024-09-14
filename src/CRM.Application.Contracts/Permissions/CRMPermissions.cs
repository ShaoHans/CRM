using Volo.Abp.Reflection;

namespace CRM.Permissions;

public class CRMPermissions
{
    public const string GroupName = "CRM";

    public static string[] GetAll()
    {
        return ReflectionHelper.GetPublicConstantsRecursively(typeof(CRMPermissions));
    }
}
