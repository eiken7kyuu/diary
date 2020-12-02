namespace ProtectedDiary.Models
{
    public class TwitterUser
    {
        public long Id { get; }
        public string ScreenName { get; }
        public string IconUrl { get; }

        public TwitterUser(long id, string screenName, string iconUrl) =>
            (Id, ScreenName, IconUrl) = (id, screenName, iconUrl);
    }
}