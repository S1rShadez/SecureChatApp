using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecureChatApp.Logic
{
    internal class Messages
    {
        string ID;
        DateTime _dateTime;
        string _message;

        public Messages(string msg, string id, bool user)
        {
            ID = id;
            _message = msg;
            _dateTime = DateTime.Now;
        }
    }
}
