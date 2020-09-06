using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace ProtectedDiary.TwitterAuth
{
    public static class ClaimsExtension
    {
        public static string ScreenName(this IEnumerable<Claim> claims)
        {
            return claims.FirstOrDefault(c => c.Type == "urn:twitter:screenname")?.Value;
        }
    }
}