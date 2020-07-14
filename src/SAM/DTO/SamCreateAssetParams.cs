using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace SAM.DTO
{
    [JsonConverter(typeof(Converters.CreateAssetConverter))]
    public class SamCreateAssetParams
    {
        /// <summary>
        /// Specifies whether the asset should be credited to the uploader or
        /// to the organization's account.
        /// </summary>
        public AssetAuthorType author_type { get; set; }

        /// <summary>
        /// Used to specify a custom author credit for someone who is not part
        /// of your organization.
        /// </summary>
        public SamAssetAuthor author { get; set; }

        /// <summary>
        /// The text portion of the asset.
        /// </summary>
        public string text { get; set; }

        /// <summary>
        /// The ID of any accompanying media
        /// </summary>
        public string media_id { get; set; }
    }

    public class SamAssetAuthor
    {
        public string name { get; set; }
    }
}
