using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using GrabTheMoment.Properties;

namespace GrabTheMoment.API
{
    class Dropbox
    {
        static Log log = new Log();
        public static string consumerKey = "r9vddy92mkc1sqd";
        public static string consumerSecret = "bqsip2znlcwbxwg";

        public static void requestToken(ref string reqtoken, ref string reqsecret)
        {
            var uri = new Uri("https://api.dropbox.com/1/oauth/request_token");

            // Generate a signature
            OAuthBase oAuth = new OAuthBase();
            string nonce = oAuth.GenerateNonce();
            string timeStamp = oAuth.GenerateTimeStamp();
            string parameters;
            string normalizedUrl;
            string signature = oAuth.GenerateSignature(uri, consumerKey, consumerSecret,
                String.Empty, String.Empty, "GET", timeStamp, nonce, OAuthBase.SignatureTypes.HMACSHA1,
                out normalizedUrl, out parameters);

            signature = HttpUtility.UrlEncode(signature);

            StringBuilder requestUri = new StringBuilder(uri.ToString());
            requestUri.AppendFormat("?oauth_consumer_key={0}&", consumerKey);
            requestUri.AppendFormat("oauth_nonce={0}&", nonce);
            requestUri.AppendFormat("oauth_timestamp={0}&", timeStamp);
            requestUri.AppendFormat("oauth_signature_method={0}&", "HMAC-SHA1");
            requestUri.AppendFormat("oauth_version={0}&", "1.0");
            requestUri.AppendFormat("oauth_signature={0}", signature);

            var request = (HttpWebRequest)WebRequest.Create(new Uri(requestUri.ToString()));
            request.Method = WebRequestMethods.Http.Get;

            var response = request.GetResponse();

            var queryString = new StreamReader(response.GetResponseStream()).ReadToEnd();
            log.WriteEvent("Dropbox/requestToken: requestToken: " + queryString);
            Console.WriteLine(queryString);

            var parts = queryString.Split('&');
            reqtoken = parts[1].Substring(parts[1].IndexOf('=') + 1);
            reqsecret = parts[0].Substring(parts[0].IndexOf('=') + 1);

            log.WriteEvent("Dropbox/requestToken: reqtoken:" + reqtoken + " reqsecret:" + reqsecret);
        }

        public static void Authorize(string token)
        {
            var queryString = String.Format("oauth_token={0}", token);
            var authorizeUrl = "https://www.dropbox.com/1/oauth/authorize?" + queryString;
            Process.Start(authorizeUrl);
        }

        public static void AccessToken(string reqtoken, string reqsecret, ref string eventecske)
        {
            try
            {
                var uri = "https://api.dropbox.com/1/oauth/access_token";

                OAuthBase oAuth = new OAuthBase();

                var nonce = oAuth.GenerateNonce();
                var timeStamp = oAuth.GenerateTimeStamp();
                string parameters;
                string normalizedUrl;
                var signature = oAuth.GenerateSignature(new Uri(uri), consumerKey, consumerSecret,
                    reqtoken, reqsecret, "GET", timeStamp, nonce,
                    OAuthBase.SignatureTypes.HMACSHA1, out normalizedUrl, out parameters);

                signature = HttpUtility.UrlEncode(signature);

                var requestUri = new StringBuilder(uri);
                requestUri.AppendFormat("?oauth_consumer_key={0}&", consumerKey);
                requestUri.AppendFormat("oauth_token={0}&", reqtoken);
                requestUri.AppendFormat("oauth_nonce={0}&", nonce);
                requestUri.AppendFormat("oauth_timestamp={0}&", timeStamp);
                requestUri.AppendFormat("oauth_signature_method={0}&", "HMAC-SHA1");
                requestUri.AppendFormat("oauth_version={0}&", "1.0");
                requestUri.AppendFormat("oauth_signature={0}", signature);

                var request = (HttpWebRequest)WebRequest.Create(requestUri.ToString());
                request.Method = WebRequestMethods.Http.Get;

                var response = request.GetResponse();
                var reader = new StreamReader(response.GetResponseStream());
                var accessToken = reader.ReadToEnd();

                log.WriteEvent("Dropbox/AccessToken: accessToken: " + accessToken);

                var parts = accessToken.Split('&');
                var actoken = parts[1].Substring(parts[1].IndexOf('=') + 1);
                var acsecret = parts[0].Substring(parts[0].IndexOf('=') + 1);
                log.WriteEvent("Dropbox/AccessToken: actoken: " + actoken + " acsecret: " + acsecret);
                eventecske = "OK";
                Settings.Default.MDropbox_accesstoken = actoken;
                Settings.Default.MDropbox_accesssecret = acsecret;
                Settings.Default.Save();
            }
            catch (WebException ex)
            {
                if (ex.Status == WebExceptionStatus.ProtocolError && ex.Response != null)
                {
                    var resp = (HttpWebResponse)ex.Response;
                    if (resp.StatusCode == HttpStatusCode.Unauthorized)
                    {
                        log.WriteEvent("Dropbox/AccessToken: 401 Unauthorized");
                        //requestToken();
                        //Authorize();
                    }
                }
                else
                    log.WriteExceptionEvent(ex, "Dropbox/AccessToken: ");
                eventecske = "Hiba!";
            }
        }

        private static string UpperCaseUrlEncode(string s)
        {
            char[] temp = HttpUtility.UrlEncode(s).ToCharArray();
            for (int i = 0; i < temp.Length - 2; i++)
            {
                if (temp[i] == '%')
                {
                    temp[i + 1] = char.ToUpper(temp[i + 1]);
                    temp[i + 2] = char.ToUpper(temp[i + 2]);
                }
            }

            var values = new Dictionary<string, string>()
            {
                { "+", "%20" },
                { "(", "%28" },
                { ")", "%29" }
            };

            var data = new StringBuilder(new string(temp));
            foreach (string character in values.Keys)
            {
                data.Replace(character, values[character]);
            }
            return data.ToString();
        }

        public static void Upload(byte[] filedata, string fajlneve)
        {
            log.WriteEvent("Dropbox/Upload: Elindultam");
            log.WriteEvent("Dropbox/Upload: accesstoken: " + Settings.Default.MDropbox_accesstoken + " accesssecret: " + Settings.Default.MDropbox_accesssecret);
            fajlneve = UpperCaseUrlEncode(fajlneve);
            var uri = new Uri(new Uri("https://api-content.dropbox.com/1/"),
                String.Format("files_put/{0}/{1}",
                "sandbox", fajlneve));

            OAuthBase oAuth = new OAuthBase();
            var nonce = oAuth.GenerateNonce();
            var timestamp = oAuth.GenerateTimeStamp();
            string parameters;
            string normalizedUrl;
            
            var signature = oAuth.GenerateSignature(
                uri, consumerKey, consumerSecret,
                Settings.Default.MDropbox_accesstoken, Settings.Default.MDropbox_accesssecret, "PUT", timestamp,
                nonce, OAuthBase.SignatureTypes.HMACSHA1,
                out normalizedUrl, out parameters);

            var requestUri = String.Format("{0}?{1}&oauth_signature={2}",
                normalizedUrl, parameters, HttpUtility.UrlEncode(signature));

            var request = (HttpWebRequest)WebRequest.Create(requestUri);
            request.Method = WebRequestMethods.Http.Put;
            request.KeepAlive = true;

            request.ContentLength = filedata.Length;
            using (var requestStream = request.GetRequestStream())
            {
                requestStream.Write(filedata, 0, filedata.Length);
            }

            var response = request.GetResponse();
            var reader = new StreamReader(response.GetResponseStream());
            var json = reader.ReadToEnd();

            log.WriteEvent("Dropbox/Upload: json: " + json.ToString());
        }
    }
}
