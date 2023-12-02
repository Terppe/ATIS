using ATIS.WinUi.ViewModels.Authentication;

namespace ATIS.WinUi.Helpers;

public static class Permission
{
    public static bool RolePermission()
    {
        var returnBool = false;
        if (Thread.CurrentPrincipal is not CustomPrincipal customPrincipal)
        {
            return false;
        }
        //  var name = customPrincipal.Identity.Email;
        var role = customPrincipal.Identity.Role;

        if (role == "Administrator" || role == "Developer" || role == "Zoologist" || role == "Biologist")
        {
            returnBool = true;
        }
        return returnBool;
    }
}
