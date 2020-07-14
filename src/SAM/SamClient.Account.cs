using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAM.DTO;

namespace SAM
{
    public partial class SamClient
    {
        public Account RetrieveAccount(SamAuth auth = null)
        {
            var url = string.Format("{0}/account.xml", ApiBaseUrl);
            var response = request(url, new Dictionary<string, string>(), auth);
            return Utils.FromXml<Account>(response);
        }

        public UserList ListAccountUsers(SamAuth auth = null)
        {
            var url = string.Format("{0}/account/users.xml", ApiBaseUrl);
            var response = request(url, null, auth);
            return Utils.FromXml<UserList>(response);
        }
    }
}
