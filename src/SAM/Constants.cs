namespace SAM
{
    class Constants
    {
        public class Configuration
        {
            public const string BASE_URL = "BASE_URL";
            public const string API_KEY = "API_KEY";
            public const string API_SECRET = "API_SECRET";
        }

        public class Routes
        {
            public const string GET_ACCOUNT = "/account";
            public const string GET_STORIES = "/stories";
            public const string GET_STORY = "/stories";
        }

        public const int PART_SIZE = 1024 * 1024 * 5; // 5MB
    }

    public enum ResponseFormat
    {
        JSON,
        XML
    }

    public enum AuthType
    {
        API_KEY,
        ACCESS_TOKEN
    }

    public enum StoryVisibility
    {
        open,
        closed
    }

    public enum AssetAuthorType
    {
        me,
        account,
        custom
    }
}
