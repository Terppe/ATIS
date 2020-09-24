using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using ATIS.Dal.Models;
using ATIS.Ui.Core;
using ATIS.Ui.Helper;

namespace ATIS.Ui.Views.Log
{
    public interface IAuthenticationService
    {
        User AuthenticateUser(string username, string password);
    }

    public class AuthenticationService : ViewModelBase, IAuthenticationService
    {
        private readonly UnitOfWork _uow = new UnitOfWork(new AtisDbContext());
        private readonly AtisDbContext _context = new AtisDbContext();

        private class InternalUserData
        {
            public InternalUserData(string email, string hashedPassword, string role)
            {
                Email = email;
                HashedPassword = hashedPassword;
                Role = role;
            }
            //public string Username
            //{
            //    get;
            //    private set;
            //}

            public string Email
            {
                get;
                private set;
            }

            public string HashedPassword
            {
                get;
                private set;
            }

            public string Role
            {
                get;
                private set;
            }
        }

        //private static readonly List<InternalUserData> _users = new List<InternalUserData>()
        //{
        //    new InternalUserData("rudolf@terppe.de",
        //        "erdQ5bRF3u8fYp8WQlKBK5yqfQMovOk8kns4coIjitMHkV9q26OU1332JRFjwhEOKKfqWxGq6AWTBf4i1Id/Dg==", "Administrator" ),
        //    new InternalUserData("marion@terppe.de",
        //        "/Zg8bxF0L2HN2DJGwOlcSEI1swVVVru4PrEMqnvpA+fwzTHZd7Q7lYMjF/5j+/Ii1ZPNFioT1qZ4Un7AS6et7Q==", "User" ),
        //    new InternalUserData("Mark",  
        //        "MB5PYIsbI2YzCUe34Q5ZU2VferIoI4Ttd+ydolWV0OE=", "Administrators" ),
        //    new InternalUserData("John",  
        //    "hMaLizwzOQ5LeOnMuj+C6W75Zl5CXXYbwDSHWW9ZOXc=", "")
        //};

        //private ObservableCollection<TblUserProfile> _user1(string email, string hashedPassword )
        //{
        //    var collection =  
        //        new ObservableCollection<TblUserProfile>(_uow.TblUserProfiles
        //            .Find(e => e.Email == email && e.Password == hashedPassword )
        //        );
        //    return collection;
        //}

        public User AuthenticateUser(string username, string clearTextPassword)
        {

            var hashedPassword = Crypt.CalculateHash(clearTextPassword, username);
            var userData = _context.TblUserProfiles.SingleOrDefault(i => i.Email.Equals(username) && i.Password.Equals(hashedPassword));

            //     userData = _businessLayer.SingleListTblUserProfilesByEmailAndPassword(username, hashedPassword);

            /*          var userData = Users.FirstOrDefault(u => u.Username.Equals(username)
                                                                             && u.HashedPassword.Equals(
                                                                                 Crypt.CalculateHash(clearTextPassword,
                                                                                     username)));
                      */
            if (userData == null)
                throw new UnauthorizedAccessException("Access denied. Please provider some valide credentials.");

            return new User(userData.Email, userData.Role);
        }

        //public User AuthenticateUser2(string email, string clearTextPassword )
        //{
        //    var hashedPassword = Crypt.CalculateHash(clearTextPassword, email);
        //    var userData = _context.TblUserProfiles.SingleOrDefault(i => i.Email.Equals(email) && i.Password.Equals(hashedPassword));

        //    //var user = _uow.TblUserProfiles
        //    //    .Find(u => u.Email == email && u.Password == CalculateHash(clearTextPassword, email)  );

        //    //InternalUserData userData1 = _users.FirstOrDefault(u => u.Email.Equals(email)
        //    //                                                        && u.HashedPassword.Equals(CalculateHash(clearTextPassword, u.Email)));

        //    if (userData == null)
        //        throw new UnauthorizedAccessException("Access denied. Please provide some valid credentials.");

        //    return new User(userData.Email, userData.Role);
        //}


        //public User AuthenticateUser1(string username, string clearTextPassword)
        //{
        //    InternalUserData userData = _users.FirstOrDefault(u => u.Email.Equals(username)
        //        && u.HashedPassword.Equals(CalculateHash(clearTextPassword, u.Email)));
        //    if (userData == null)
        //        throw new UnauthorizedAccessException("Access denied. Please provide some valid credentials.");

        //    return new User(userData.Email, userData.Role);
        //}

        private string CalculateHash(string clearTextPassword, string salt)
        {
            // Convert the salted password to a byte array
            byte[] saltedHashBytes = Encoding.UTF8.GetBytes(clearTextPassword + salt);
            // Use the hash algorithm to calculate the hash
            HashAlgorithm algorithm = new SHA512Managed();  // von 256 auf 512 geändert
            byte[] hash = algorithm.ComputeHash(saltedHashBytes);
            // Return the hash as a base64 encoded string to be compared to the stored password
            return Convert.ToBase64String(hash);
        }
    }

    public class User
    {
        public User(string email, string role)
        {
            Email = email;
            Role = role;
        }
        //public string Username
        //{
        //    get;
        //    set;
        //}

        public string Email
        {
            get;
            set;
        }

        public string Role
        {
            get;
            set;
        }
    }
}
