namespace ProtectedDiary.Core.Entities
{
    public class User
    {
        public long Id { get; }
        public string ScreenName { get; }
        public string IconUrl { get; }

        public User(long id, string screenName, string iconUrl) =>
            (Id, ScreenName, IconUrl) = (id, screenName, iconUrl);
    }
}