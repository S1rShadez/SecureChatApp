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
            if(!string.IsNullOrEmpty(UName) ||  !string.IsNullOrEmpty(PWord))
            {
                return true;
            }
            return false;
        }
    }
}
