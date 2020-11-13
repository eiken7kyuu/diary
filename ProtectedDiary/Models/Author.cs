namespace ProtectedDiary.Models
{
    public class Author
    {
        public long Id { get; }
        public string ScreenName { get; }
        public string IconUrl { get; }

        public AuthorRelationship Relationship { get; set; }

        public Author(long id, string screenName, string iconUrl) =>
            (Id, ScreenName, IconUrl) = (id, screenName, iconUrl);
    }
}