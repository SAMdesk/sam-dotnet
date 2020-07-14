using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace SAM.DTO
{
    public class SamCreateStoryParams
    {
        /// <summary>
        /// The name of the story.
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// Sets the visibility of the story. OPEN stories are visible to all
        /// users of the account, while CLOSED stories are only visible to the
        /// story owner and users they invite to collaborate.
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        public StoryVisibility visibility { get; set; }

        /// <summary>
        /// Required if authenticating with an API key. Sets the owner of the
        /// story in your organization.
        /// </summary>
        public string owner { get; set; }
    }
}
