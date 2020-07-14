using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace SAM.DTO
{
    [XmlType(TypeName = "account")]
    public class Account
    {
        public string id { get; set; }
        public string name { get; set; }
        public string status { get; set; }
        public TagList tags { get; set; }
    }

    [XmlType(TypeName = "tags")]
    public class TagList : SamList<Tag>
    {

    }

    [XmlType(TypeName = "tag")]
    public class Tag
    {
        public string name { get; set; }
        public string color { get; set; }
    }
}
