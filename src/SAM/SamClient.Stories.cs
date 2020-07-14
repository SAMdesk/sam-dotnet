using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAM.DTO;
using Newtonsoft.Json;

namespace SAM
{
    public partial class SamClient
    {
        public StoryList ListStories(IDictionary<string, string> parameters = null, SamAuth auth = null)
        {
            var url = string.Format("{0}/stories.xml", ApiBaseUrl);
            var response = request(url, parameters, auth);
            return Utils.FromXml<StoryList>(response);
        }

        public Story RetrieveStory(string storyId, IDictionary<string, string> parameters = null, SamAuth auth = null)
        {
            var url = string.Format("{0}/stories/{1}.xml", ApiBaseUrl, storyId);
            var response = request(url, parameters, auth);
            return Utils.FromXml<Story>(response);
        }

        /// <summary>
        /// Creates a new story in your account.
        /// </summary>
        /// <param name="storyParams">Describes the story to be created.</param>
        /// <param name="auth">Your SAM auth object, if not already set up.</param>
        /// <returns>A story object containing the name and ID of the newly created story.</returns>
        public Story CreateStory(SamCreateStoryParams storyParams, SamAuth auth = null)
        {
            var body = Encoding.ASCII.GetBytes(JsonConvert.SerializeObject(storyParams));
            var url = string.Format("{0}/stories.xml", ApiBaseUrl);
            var response = request(url, body, null, auth);
            return Utils.FromXml<Story>(response);
        }

        /// <summary>
        /// Deletes the specified story from your accout.
        /// </summary>
        /// <param name="storyId">The ID of the story to be deleted.</param>
        /// <param name="auth">Your SAM auth object, if not already set up.</param>
        public void DeleteStory(string storyId, SamAuth auth = null)
        {
            var url = string.Format("{0}/stories/{1}.xml", ApiBaseUrl, storyId);
            request(url, null, "DELETE", null, null, auth);
        }
    }
}
