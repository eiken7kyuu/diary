namespace ProtectedDiary.TwitterAuth
{
    public class TwitterConfiguration
    {
        public TwitterConfiguration(string consumerKey, string consumerSecret)
        {
            ConsumerKey = consumerKey;
            ConsumerSecret = consumerSecret;
        }

        public readonly string ConsumerKey;
        public readonly string ConsumerSecret;
    }
}