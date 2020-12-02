namespace ProtectedDiary.Models
{
    public class Author
    {
        public long Id { get; }
        public string ScreenName { get; }
        public string IconUrl { get; }
        private bool _followedBy { get; }
        private bool _following { get; }

        public bool IsMutualFollowers => _followedBy && _following;

        public Author(TwitterUser user, bool followedBy, bool following)
        {
            Id = user.Id;
            ScreenName = user.ScreenName;
            IconUrl = user.IconUrl;
            _followedBy = followedBy;
            _following = following;
        }
    }
}