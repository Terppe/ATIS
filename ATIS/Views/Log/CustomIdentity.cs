using System.Security.Principal;

namespace ATIS.Ui.Views.Log
{
    public class CustomIdentity : IIdentity
    {
        public CustomIdentity(string email, string role)
        {
            Email = email;
            Role = role;
        }

        //      public string Name { get; private set; }
        public string Email { get; private set; }
        public string Role { get; private set; }

        #region IIdentity Members
        public string AuthenticationType => "Custom authentication";

        public bool IsAuthenticated => !string.IsNullOrEmpty(Email);

        public string? Name { get; }

        #endregion
    }
}
