using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace SAM.DTO
{
    [XmlType(TypeName = "upload")]
    public class SamUpload
    {
        public string media_id { get; set; }
    }
}
