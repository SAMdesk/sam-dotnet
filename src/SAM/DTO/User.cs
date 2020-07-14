using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace SAM.DTO
{
    [XmlType(TypeName = "user")]
    public class User
    {
        public string id { get; set; }

        public string name { get; set; }

        public string avatar_url { get; set; }
    }

    [XmlType(TypeName = "users")]
    public class UserList : SamList<User>
    {

    }
}
