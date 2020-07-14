using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAM.DTO;
using Newtonsoft.Json;
using System.IO;

namespace SAM
{
    public partial class SamClient
    {
        public SocialAssetList ListAssets(string storyId, IDictionary<string, string> parameters = null, SamAuth auth = null)
        {
            var url = string.Format("{0}/stories/{1}/assets.xml", ApiBaseUrl, storyId);
            var response = request(url, parameters, auth);
            return Utils.FromXml<SocialAssetList>(response);
        }

        public SocialAsset RetrieveAsset(string storyId, string assetId, IDictionary<string, string> parameters = null, SamAuth auth = null)
        {
            var url = string.Format("{0}/stories/{1}/assets/{2}.xml", ApiBaseUrl, storyId, assetId);
            var response = request(url, parameters, auth);
            return Utils.FromXml<SocialAsset>(response);
        }

        /// <summary>
        /// Creates a new asset in the specified story.
        /// </summary>
        /// <param name="storyId">The ID of the story to add the asset to.</param>
        /// <param name="assetParams">An object describing the asset's properties.</param>
        /// <param name="auth">Your SAM auth object, if not already set up.</param>
        /// <returns>A SocialAsset object.</returns>
        public SocialAsset CreateAsset(string storyId, SamCreateAssetParams assetParams, SamAuth auth = null)
        {
            byte[] body = Encoding.ASCII.GetBytes(JsonConvert.SerializeObject(assetParams));
            var url = string.Format("{0}/stories/{1}/assets.xml", ApiBaseUrl, storyId);
            var response = request(url, body, null, auth);
            return Utils.FromXml<SocialAsset>(response);
        }
    }
}
