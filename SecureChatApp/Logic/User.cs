using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SecureChatApp.Logic.Encryption;

namespace SecureChatApp.Logic
{
    public class User
    {
        public string _username;
        List<Contacts> _contacts;

        private User(string username)
        {
            _username = username;
            _contacts = new List<Contacts>();
        }

        //TODO: Update the CreateUser method
        /// <summary>
        /// Creates the user and the encrypted file that stores user data
        /// </summary>
        /// <returns></returns>
        public static User CreateUser(string username, string password)
        {
            EncryptFile(username, password);
            return new User(username);
        }

        /// <summary>
        /// Verifies that the correct user data is used to sign in
        /// </summary>
        /// <returns></returns>
        public static bool CheckLogin(string UName, string PWord)
        {
            if (!string.IsNullOrEmpty(UName) || !string.IsNullOrEmpty(PWord))
            {
                return VerifyAccount(PWord);
            }
            return false;
        }

        //TODO: Create a method that turns the user data into json data to store in the encrypted file
        
        //TODO: create a method that outputs the contacts list into the GUI

        //TODO: Create a method that outputs the messages with the selected contact
    }
}
