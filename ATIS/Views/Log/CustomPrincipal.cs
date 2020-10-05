using System.Security.Principal;

namespace ATIS.Ui.Views.Log
{
    public class CustomPrincipal : IPrincipal
    {
        private CustomIdentity _identity;

        public CustomIdentity Identity
        {
            get => _identity ?? new AnonymousIdentity();
            set => _identity = value;
        }

        #region IPrincipal Members
        IIdentity IPrincipal.Identity => Identity;

        public bool IsInRole(string role)
        {
            return _identity != null && _identity.Role.Contains(role);
            //  return _identity != null ;
        }
        #endregion
    }
}
