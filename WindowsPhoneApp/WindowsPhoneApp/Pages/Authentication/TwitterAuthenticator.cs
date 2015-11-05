using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Security;
using Raksha.Crypto.Macs;
using Raksha.Crypto.Digests;
using Raksha.Crypto.Parameters;
using System.Net.Http;

namespace WindowsPhoneApp.Authentication
{
    public static class TwitterAuthenticator
    {
		private const string oauth_consumer_key = "rWW4aMXiadnwvyd5ThivTpxpx";
		private const string consumerSecret = "PfzjakODL7jR8x8TGxDavLMvxIrEz5BVPCCQ0docNmCMnOSJLg";

		private static Random random = new Random();
		private static HttpClient client = new HttpClient();

		public static async Task<string> GetUserId(string oauth_token, string oauth_verifier)
		{
			var request = new HttpRequestMessage(HttpMethod.Post, "https://api.twitter.com/oauth/access_token");
			request.Content = new StringContent("oauth_verifier=" + oauth_verifier, Encoding.UTF8, "application/x-www-form-urlencoded");

			var parameters = new SortedDictionary<string, string>();
			parameters.Add("oauth_verifier", oauth_verifier);
			request.Headers.Add("Authorization", TwitterAuthenticator.GenerateAuthString("POST", "https://api.twitter.com/oauth/access_token",
				parameters, null, oauth_token));

			var response = await client.SendAsync(request);
			if (!response.IsSuccessStatusCode)
			{
				string content = await response.Content.ReadAsStringAsync();
				throw new NotImplementedException();
			}
			return (await response.Content.ReadAsStringAsync())
				.Split('&')
				.Select(s => s.Split('='))
				.Where(ss => ss[0] == "user_id")
				.Single()[1];
		}

        public static string GenerateAuthString(string httpmethod, string url, SortedDictionary<string, string> parameters, string callback, string oauthToken = "")
        {
            byte[] nonceBuffer = new byte[32];
            random.NextBytes(nonceBuffer);

            string oauth_nonce = Convert.ToBase64String(nonceBuffer);
            string oauth_signature_method = "HMAC-SHA1";
            string oauth_timestamp = UnixTime().ToString();
            string oauth_callback = callback;
            string oauth_version = "1.0";

			if(callback != null)
			{
				parameters.Add("oauth_callback", oauth_callback);
			}
            parameters.Add("oauth_consumer_key", oauth_consumer_key);
            parameters.Add("oauth_nonce", oauth_nonce);
            parameters.Add("oauth_signature_method", oauth_signature_method);
            parameters.Add("oauth_timestamp", oauth_timestamp);
			if(oauthToken != "")
			{
				parameters.Add("oauth_token", oauthToken);
			}
            parameters.Add("oauth_version", oauth_version);

            string parameterString = ParameterString(parameters);

            parameters.Add("oauth_signature", Signature(httpmethod, url, parameterString, oauthToken));
            return "OAuth " + AuthenticationString(parameters);
        }

        private static string AuthenticationString(SortedDictionary<string, string> parameters)
        {
            StringBuilder builder = new StringBuilder();
            foreach (var parameter in parameters)
            {
                builder.Append(parameter.Key);
                builder.Append("=\"");
                builder.Append(PercentEncode(parameter.Value));
                builder.Append("\", ");
            }
            builder.Remove(builder.Length - 2, 2);
            return builder.ToString();
        }

        public static string ParameterString(SortedDictionary<string, string> parameters)
        {
			if (parameters.Empty()) return "";
            StringBuilder builder = new StringBuilder();
            foreach (var parameter in parameters)
            {
                builder.Append(parameter.Key);
                builder.Append("=");
                builder.Append(PercentEncode(parameter.Value));
                builder.Append("&");
            }
            builder.Remove(builder.Length - 1, 1);
            return builder.ToString();
        }

        public static string Signature(string httpmethod, string url, string parameterString, string oauthToken)
        {
            string baseString = httpmethod.ToUpper() + "&" + PercentEncode(url) + "&" + PercentEncode(parameterString);
            string signingKey = PercentEncode(consumerSecret) + "&" + PercentEncode(oauthToken);
            var sha = new HMac(new Sha1Digest());
            sha.Init(new KeyParameter(Encoding.UTF8.GetBytes(signingKey)));
            var data = Encoding.UTF8.GetBytes(baseString);
            sha.BlockUpdate(data, 0, data.Length);
            byte[] result = new byte[20];
            sha.DoFinal(result, 0);
            return Convert.ToBase64String(result);
        }

        private static long UnixTime()
        {
            DateTime unixEpoch = new DateTime(1969, 12, 31);
            return Convert.ToInt64((DateTime.Now - unixEpoch).TotalSeconds);
        }

        private static string PercentEncode(string s)
        {
            return Uri.EscapeDataString(s);
        }
    }
}
