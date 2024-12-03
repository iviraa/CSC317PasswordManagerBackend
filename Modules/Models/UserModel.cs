/*

Program Author: Chetanchal Saud

USM wID: w10171032

Assignment: Password Manager, PT2, Backend

Description: This class implements the User Model.

*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSC317PassManagerP2Starter.Modules.Models
{
    public class User
    {
        //Implement the User Model here.
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] Key { get; set; }
        public byte[] IV { get; set; }
    }
}
