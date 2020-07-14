using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SAM
{
    public class SamAuth
    {
        public AuthType Type { get; private set; }
        public string Token { get; private set; }

        public SamAuth(AuthType type, string token)
        {
            this.Type = type;
            this.Token = token;
        }
    }
}
