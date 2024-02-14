using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecureChatApp.Logic
{
    public static class CheckLogin
    {
        public static bool CLogin(string UName, string PWord)
        {
            if (string.IsNullOrEmpty(UName) || string.IsNullOrEmpty(PWord)) { return false; }
            //change this check to if encrypted file was successfully decrypted with the right password, or not (also with the correct username), preferrably by saving the username in an encrypted format inside the encrypted file for the verification,
            //if file.Decrypt == true && line 1 == {encrypted username} -> log user in, I also want to use some sort of hardware identifier to make the encrypted file even more secure (use the HID as the KEY for the encryption, or part of the KEY)
            if (!string.IsNullOrEmpty(UName) || !string.IsNullOrEmpty(PWord))
            {
                return true;
            }
            return false;
        }
    }
}
