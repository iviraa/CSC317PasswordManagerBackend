/*
    Author: Chetanchal Saud

    USM wID: w10171032

    Assignment: Password Manager, PT2, Backend

    Description: Populates the password view, generates test passwords and handles CRUD operation for the Password Manager app.
*/


using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSC317PassManagerP2Starter.Modules.Controllers;
using CSC317PassManagerP2Starter.Modules.Models;
using CSC317PassManagerP2Starter.Modules.Views;

namespace CSC317PassManagerP2Starter.Modules.Controllers
{
    public class PasswordController
    {
        //Stores a list of sample passwords for the test user.
        public List<PasswordModel> _passwords = new List<PasswordModel>();
        private int counter = 1;

        public PasswordController() { }


        /*
         * The following functions need to be completed.
         */
        //Used to copy the passwords over to the Row Binders.
        public void PopulatePasswordView(ObservableCollection<PasswordRow> source, string search_criteria = "")
        {
            //Complete definition of PopulatePasswordView here.

            source.Clear();

            foreach (var password in _passwords)

            {
                if ( string.IsNullOrEmpty(search_criteria) ||  password.PlatformName.Contains(search_criteria, StringComparison.OrdinalIgnoreCase))

                {
                    source.Add(new PasswordRow(password));
                }
                
            }
               

        }

        //CRUD operations for the password list.
        public void AddPassword(string platform, string username, string password)
        {
            //Complete definition of AddPassword here.

            var currentUser = App.LoginController.GetCurrentUser();

            if (currentUser != null) 
            {
                _passwords.Add(new PasswordModel(counter++, currentUser.Id ,username, platform, password, Tuple.Create(currentUser.Key, currentUser.IV)));
            }
        }

        public PasswordModel? GetPassword(int ID)
        {
            //Complete definition of GetPassword here.

            foreach (var password in _passwords) 
            {
                if (password.Id == ID)
                {
                    return password;
                }
            }
            return null;
        }

        public bool UpdatePassword(PasswordModel changes)
        {
           //Complete definition of Update Password here.
           var existingPassword = GetPassword(changes.Id);

            if (existingPassword != null) 
            {
                existingPassword.PlatformName = changes.PlatformName;
                existingPassword.PasswordText = changes.PasswordText;
                return true;
            }

            return false;
        }

        public bool RemovePassword(int ID)
        {
           //Complete definition of Remove Password here.
           var passwordToRemove = GetPassword(ID);
            if (passwordToRemove != null)
            {
                _passwords.Remove(passwordToRemove);
                return true;
            }

            return false;
        }

        public void GenTestPasswords()
        {
            //Generate a set of random passwords for the test user.
            //Called in Password List Page.

            var currentUser = App.LoginController.GetCurrentUser();

            //Generates test passwords if there are no passwwords stored currently.

            if ( (currentUser != null) & (_passwords.Count <= 0) )
            {
                _passwords.Add(new PasswordModel(counter++, currentUser.Id, "Facebook", "john", "123", Tuple.Create(currentUser.Key, currentUser.IV)));
                _passwords.Add(new PasswordModel(counter++, currentUser.Id, "Instagram", "ivira", "password123", Tuple.Create(currentUser.Key, currentUser.IV)));
                _passwords.Add(new PasswordModel(counter++, currentUser.Id, "Twitter", "tommy", "mypassword321", Tuple.Create(currentUser.Key, currentUser.IV)));
                _passwords.Add(new PasswordModel(counter++, currentUser.Id,  "LinkedIn", "hiko", "linkedInPass2024", Tuple.Create(currentUser.Key, currentUser.IV)));
                _passwords.Add(new PasswordModel(counter++, currentUser.Id, "Spotify", "tenz", "mySpotify@2024", Tuple.Create(currentUser.Key, currentUser.IV)));
                _passwords.Add(new PasswordModel(counter++, currentUser.Id, "Snapchat", "alex123", "snapPass2024", Tuple.Create(currentUser.Key, currentUser.IV)));
                _passwords.Add(new PasswordModel(counter++, currentUser.Id, "Netflix", "jane_doe", "netflixPass@2024", Tuple.Create(currentUser.Key, currentUser.IV)));
            }
            else
            {
                return;
            }
        }
    }
}
