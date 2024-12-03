/*

Program Author: Chetanchal Saud

USM wID: w10171032

Assignment: Password Manager, PT2, Backend

Description: This class implements the Login Controller and handles user authentication.

*/

using CSC317PassManagerP2Starter.Modules.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSC317PassManagerP2Starter.Modules.Controllers
{
    public enum AuthenticationError { NONE, INVALIDUSERNAME, INVALIDPASSWORD }
    public class LoginController
    {

        /*
         * This class is incomplete.  Fill in the method definitions below.
         */
        private User _user = new User();
        private bool _loggedIn = false;

        //Initiliaze test user
        public LoginController()
        {
            var password = "ab1234";
            var passwordHash = PasswordCrypto.GetHash(password);
            var keyIV = PasswordCrypto.GenKey();

            _user = new User
            {
                Id = 1,
                FirstName = "John",
                LastName = "Doe",
                UserName = "test",
                PasswordHash = passwordHash,
                Key = keyIV.Item1,
                IV = keyIV.Item2

            };
            
        }

        public User? GetCurrentUser()
        {
            // Retrieves the current user's information if the user is logged in, otherwise returns a null value.
            if (_loggedIn)
            {
                return new User
                {
                    Id = _user.Id,
                    UserName = _user.UserName,
                    FirstName = _user.FirstName,
                    LastName = _user.LastName,
                    Key = _user.Key,
                    IV = _user.IV
                };
            }
            else
            {
                return null;
            }

            
        }

        public AuthenticationError Authenticate(string username, string password)
        {
            //determines whether the inputted username/password matches the stored
            //username/password.  currently returns a NONE error status.

            // Check if the username matches
            if (username != _user.UserName)
            {
                return AuthenticationError.INVALIDUSERNAME;
            }

            
            byte[] hashedInputPassword = PasswordCrypto.GetHash(password);

            // Compare hashed input password with  the stored password
            if (!PasswordCrypto.CompareBytes(_user.PasswordHash, hashedInputPassword))
            {
                return AuthenticationError.INVALIDPASSWORD;
            }

            _loggedIn = true;
            return AuthenticationError.NONE;
        }
    }

}
