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
            return claims.FirstOrDefault(c => c.Type == TwitterClaimTypes.ScreenName)?.Value;
        }

        public static long UserId(this IEnumerable<Claim> claims)
        {
            return long.Parse(claims.FirstOrDefault(c => c.Type == TwitterClaimTypes.UserId)?.Value);
        }

        public static string AccessToken(this IEnumerable<Claim> claims)
        {
            return claims.FirstOrDefault(c => c.Type == TwitterClaimTypes.AccessToken)?.Value;
        }

        public static string AccessTokenSecret(this IEnumerable<Claim> claims)
        {
            return claims.FirstOrDefault(c => c.Type == TwitterClaimTypes.AccessTokenSecret)?.Value;
        }
    }
}