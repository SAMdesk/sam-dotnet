using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Xml;

namespace SAM
{
    public partial class SamClient
    {
        public string ApiProtocol { get; set; }
        public string ApiHost { get; set; }
        public string ApiVersion { get; set; }
        public string UploadHost { get; set; }
        public string UploadVersion { get; set; }
        public SamAuth ApiAuth { get; set; }

        public SamClient()
        {
            ApiProtocol = "https";
            ApiHost = "api.samdesk.io";
            UploadHost = "upload.samdesk.io";
            ApiVersion = "1";
            UploadVersion = "1";
        }

        public SamClient(SamAuth auth)
            : this()
        {
            ApiAuth = auth;
        }

        public SamClient(AuthType authType, string authToken)
            : this(new SamAuth(authType, authToken))
        {

        }

        public void SetAuth(AuthType authType, string authToken)
        {
            this.ApiAuth = new SamAuth(authType, authToken);
        }

        private string ApiBaseUrl
        {
            get
            {
                return string.Format("{0}://{1}/{2}", ApiProtocol, ApiHost, ApiVersion);
            }
        }

        private string UploadBaseUrl
        {
            get
            {
                return string.Format("{0}://{1}/{2}", ApiProtocol, UploadHost, UploadVersion);
            }
        }

        private string request(string url, byte[] body, string method, string contentType, IDictionary<string, string> parameters = null, SamAuth auth = null)
        {
            if (auth == null)
            {
                auth = ApiAuth;
            }

            if (auth == null)
            {
                throw new SamException("No authentication provided. (HINT: you set your SAM client's API authentication using \"client.ApiAuth = new SamAuth(AuthType.API_KEY, {API_KEY})\".");
            }

            if (parameters == null) parameters = new Dictionary<string, string>();
            parameters[auth.Type.ToString().ToLower()] = auth.Token;

            url += generateQueryString(parameters);

            WebRequest request = WebRequest.Create(url);
            request.Method = method;

            if (request.Method == "POST")
            {
                request.ContentType = contentType;

                if (body != null)
                {
                    request.ContentLength = body.Length;
                    var requestStream = request.GetRequestStream();
                    requestStream.Write(body, 0, body.Length);
                }
            }

            // Read the response
            HttpWebResponse response;

            try
            {
                response = (HttpWebResponse)request.GetResponse();
            }
            catch (WebException e)
            {
                response = (HttpWebResponse)e.Response;
                if (response == null)
                {
                    throw new SamConnectionException(e.Message);
                }
            }

            Stream dataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(dataStream);
            string xml = reader.ReadToEnd();

            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    return xml;
                case HttpStatusCode.Created:
                case HttpStatusCode.NoContent:
                    return null;
                case HttpStatusCode.BadRequest:
                    //case HttpStatusCode.NotFound:
                    throw SamInvalidRequestException.Generate(xml);
                case HttpStatusCode.Unauthorized:
                    throw SamAuthenticationException.Generate(xml);
                case HttpStatusCode.InternalServerError:
                    throw SamApiException.Generate(xml);
                default:
                    throw new SamException("Unexpected Response Code: " + (int)response.StatusCode);
            }
        }

        // Shorthand for a GET request
        private string request(string url, IDictionary<string, string> parameters = null, SamAuth auth = null)
        {
            return request(url, null, "GET", null, parameters, auth);
        }

        // Shorthand for a POST request
        private string request(string url, byte[] body, IDictionary<string, string> parameters = null, SamAuth auth = null)
        {
            return request(url, body, "POST", "application/json", parameters, auth);
        }

        private string generateQueryString(IDictionary<string, string> data)
        {
            if (data.Count == 0)
            {
                return String.Empty;
            }

            List<string> list = new List<string>(data.Count);
            foreach (string key in data.Keys)
            {
                list.Add(string.Format("{0}={1}", key, data[key]));
            }
            return "?" + string.Join("&", list.ToArray());
        }
    }
}