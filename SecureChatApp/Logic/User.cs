using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SecureChatApp.Logic.Encryption;

namespace SecureChatApp.Logic
{
    internal class User
    {
        string username;
        List<Contacts> contacts;

        public User(string username)
        {
            this.username = username;
            contacts = new List<Contacts>();
        }

        //TODO: Update the CreateUser method
        /// <summary>
        /// Creates the user and the encrypted file that stores user data
        /// </summary>
        /// <returns></returns>
        public static void CreateUser(string username, string password)
        {
            EncryptFile(username, password);
        }

        /// <summary>
        /// Verifies that the correct user data is used to sign in
        /// </summary>
        /// <returns></returns>
        public static bool CheckLogin(string UName, string PWord)
        {
            if (string.IsNullOrEmpty(UName) || string.IsNullOrEmpty(PWord)) { return false; }
            //TODO: change this check to if encrypted file was successfully decrypted with the right password, or not (also with the correct username),
            if (!string.IsNullOrEmpty(UName) || !string.IsNullOrEmpty(PWord))
            {
                return true;
            }
            return false;
        }

        //TODO: Create a method that turns the user data into json data to store in the encrypted file
        private string UserSummary()
        {
            return null;
        }
    }
}
