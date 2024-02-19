using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecureChatApp.Logic
{
    internal class Contacts
    {
        string ID;
        string IP;
        //TODO: Update the Contacts class:
        //msgs should contain datetime of sending/receiving, ID of the contact and a "me" for the user, msg ID based on the datetime to get correct sorting
        List<string> msgs;
    }
}
