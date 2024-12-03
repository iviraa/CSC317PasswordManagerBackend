/*

Program Author: Chetanchal Saud

USM wID: w10171032

Assignment: Password Manager, PT2, Backend

Description: This class implements the Password Model.

*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSC317PassManagerP2Starter.Modules.Models
{
    //Implement the Password Model here.
    public class PasswordModel
    {
       public int Id { get; set; }
       public int UserID{ get; set; }
       public string PlatformName { get; set; }
       public string PlatformUserName { get; set; }
       public byte[] PasswordText { get; set; }

       public PasswordModel(int id, int userId, string platformName, string platformUserName, string password, Tuple<byte[], byte[]> keyIV)
       {
            Id = id;
            UserID = userId;
            PlatformName = platformName;
            PlatformUserName = platformUserName;
            PasswordText = PasswordCrypto.Encrypt(password, keyIV);
       }

    }
}
