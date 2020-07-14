using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace SAM.DTO
{
    [XmlType(TypeName = "stories")]
    public class StoryList : SamList<Story>
    {

    }

    [XmlType(TypeName = "story")]
    public class Story
    {
        /// <summary>
        /// Id of the story
        /// </summary>
        public string id { get; set; }

        /// <summary>
        /// Name of the story
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// The story status, this should be 'active'. Could also be 'archived'.
        /// </summary>
        public string status { get; set; }

        /// <summary>
        /// Date the story was created
        /// 
        /// Milliseconds since epoch (Jan 1, 1970) in UTC
        /// </summary>
        public long created { get; set; }

        /// <summary>
        /// Date the story was last modified
        /// 
        /// Milliseconds since epoch (Jan 1, 1970) in UTC
        /// </summary>
        public long updated { get; set; }

        public SocialAssetList social_assets { get; set; }
    }

    [XmlType(TypeName = "social_assets")]
    public class SocialAssetList : SamList<SocialAsset>
    {

    }

    [XmlType(TypeName = "social_asset")]
    public class SocialAsset
    {
        public string id { get; set; }
        public string story { get; set; }
        public SocialAssetAuthor author { get; set; }
        public string text { get; set; }
        public string description { get; set; }
        public long posted_date { get; set; }
        public string social_type { get; set; }
        public string public_id { get; set; }

        public TagList tags { get; set; }
        
        public SocialAssetMediaList media { get; set; }

        public GeoItem geo { get; set; } 

        /// <summary>
        /// Date the social was added to the story
        /// 
        /// Milliseconds since epoch (Jan 1, 1970) in UTC
        /// </summary>
        public long created { get; set; }
        public long updated { get; set; }

        [Obsolete]
        public string long_text { get; set; }
        [Obsolete]
        public string short_text { get; set; }
    }

    [XmlType(TypeName = "author")]
    public class SocialAssetAuthor
    {
        public string location { get; set; }
        public string description { get; set; }
        public string avatar_url { get; set; }
        public string display_name { get; set; }
        public string name { get; set; }
        public string id { get; set; }
    }

    public class SocialAssetMediaList : SamList<SocialAssetMedia>
    {

    }

    [XmlType(TypeName = "media")]
    public class SocialAssetMedia
    {
        /// <summary>
        /// facebook, twitter, instagram, youtube ect
        /// </summary>
        public string social_type { get; set; }

        /// <summary>
        /// image or video
        /// </summary>
        public string media_type { get; set; }

        public string media_url { get; set; }

        public string thumbnail_url { get; set; }
    }

    public class GeoItem
    {
        public double lng { get; set; }
        public double lat { get; set; }
    }
}
