using System;
using System.Collections.Generic;
using System.Text;

namespace Identity.Service.EventHandler.Responses
{
    public class IdentityAccess
    {
        public bool Success { get; set; }
        public string AccessToken { get; set; }
    }
}
