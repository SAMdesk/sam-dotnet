using System;
using System.Xml.Serialization;

namespace SAM
{
    [XmlType(TypeName = "error")]
    public class SamException : Exception
    {
        public string Type { get; set; }
        public string Param { get; set; }

        public SamException()
            : base()
        {
        }

        public SamException(string message)
            : base(message)
        {
        }
    }

    public class SamInvalidRequestException : SamException
    {
        public SamInvalidRequestException(string message)
            : base(message)
        {
        }

        public static SamInvalidRequestException Generate(string xml)
        {
            SamError error = Utils.FromXml<SamError>(xml);
            return new SamInvalidRequestException(error.message) { Type = error.type, Param = error.param };
        }
    }

    public class SamApiException : SamException
    {
        public SamApiException(string message)
            : base(message)
        {
        }

        public static SamApiException Generate(string xml)
        {
            SamError error = Utils.FromXml<SamError>(xml);
            return new SamApiException(error.message) { Type = error.type, Param = error.param };
        }
    }

    public class SamAuthenticationException : SamException
    {
        public SamAuthenticationException(string message)
            : base(message)
        {
        }

        public static SamAuthenticationException Generate(string xml)
        {
            SamError error = Utils.FromXml<SamError>(xml);
            return new SamAuthenticationException(error.message) { Type = error.type, Param = error.param };
        }
    }

    public class SamConnectionException : SamException
    {
        public SamConnectionException(string message)
            : base(message)
        {
        }

        public static SamConnectionException Generate(string xml)
        {
            SamError error = Utils.FromXml<SamError>(xml);
            return new SamConnectionException(error.message) { Type = error.type, Param = error.param };
        }
    }

    [XmlType(TypeName = "error")]
    public class SamError
    {
        public string message { get; set; }
        public string type { get; set; }
        public string param { get; set; }
    }
}
