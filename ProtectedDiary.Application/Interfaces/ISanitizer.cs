namespace ProtectedDiary.Application.Interfaces
{
    public interface ISanitizer
    {
        string Sanitize(string htmlString);
    }
}