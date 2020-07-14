using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAM.DTO;

namespace SAM
{
    public partial class SamClient
    {
        public User RetrieveUser(SamAuth auth = null)
        {
            var url = string.Format("{0}/user.xml", ApiBaseUrl);
            var response = request(url, null, auth);
            return Utils.FromXml<User>(response);
        }
    }
}
