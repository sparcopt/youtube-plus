using Google.Apis.Auth.OAuth2;
using Google.Apis.YouTube.v3;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace YoutubePlus.Common
{
    public class OAuthManager
    {
        /// <summary>
        /// Autonomously prompts the user to login with Google OAuth and returns his credential
        /// </summary>
        public static async Task<UserCredential> GetUserCredentialAsync()
        {
            UserCredential credential;
            using (var stream = new FileStream("client_secrets.json", FileMode.Open, FileAccess.Read))
            {
                credential = await GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    new[] { YouTubeService.Scope.Youtube },
                    "user",
                    CancellationToken.None
                );
            }
            return credential; 
        }
    }
}
