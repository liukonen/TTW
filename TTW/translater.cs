using System;
using System.Text;
using System.IO;
using System.Net;



namespace TTW
{
    public class translater
    {
        #region Constants
        const string cApiCallUrl = "https://api.microsofttranslator.com/v2/http.svc/Translate?appid={0}&text={1}&from={2}&to={3}";
        const string cTokenUrl = "https://api.cognitive.microsoft.com/sts/v1.0/issueToken";

        const string cWebRequestContentType = "Content-Type: application/json";
        const string cTokenRequestSubKeyHeader = "Ocp-Apim-Subscription-Key";
        #endregion

        #region Global Objects
        private string key;
        private string appId;
        #endregion

        #region Public Methods
        /// <summary>
        /// Declare a new translater object
        /// </summary>
        /// <param name="AzureSubscriptionKey">One of the subscription keys that is provided
        /// by the the Azure Text Translation services.</param>
        public translater(string AzureSubscriptionKey)
        {
            key = AzureSubscriptionKey;
        }

        /// <summary>
        /// The default time a token is active is 10 minutes. This method allows
        /// the developer to regenerate a token without having to repass there 
        /// subscription key.
        /// </summary>
        public void regenerateToken()
        {
            const string cWebRequestMethod  = "POST";
            string response;
            WebRequest tokenRequest = WebRequest.Create(cTokenUrl);
            tokenRequest.ContentType = cWebRequestContentType;
            tokenRequest.Headers.Add(cTokenRequestSubKeyHeader, key);
            tokenRequest.Method = cWebRequestMethod;
            Byte[] MessageBody = Encoding.ASCII.GetBytes(cTokenUrl);
            tokenRequest.ContentLength = MessageBody.Length;
            using (Stream InputStream = tokenRequest.GetRequestStream()){InputStream.Write(MessageBody, 0, MessageBody.Length);}
            response = WebResponseToString(tokenRequest.GetResponse());
            appId = string.Concat("Bearer ", response);  
        }

        /// <summary>
        /// Makes the live call out the door to make a translation call
        /// </summary>
        /// <param name="text"> A string representing the text to translate. The size of the text must not exceed ten thousand characters.</param>
        /// <param name="from">A string representing the language code of the translation text. en = english, de = german etc...</param>
        /// <param name="to">A string representing the language code to translate the text into.</param>
        /// <returns></returns>
        public string Translate(string text, string from, string to)
        {
            string value;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(string.Format(cApiCallUrl, appId, text, from, to));
            request.ContentType = cWebRequestContentType;
            value = WebResponseToString(request.GetResponse());
            //Output will be simmilar to  the following
            //<string xmlns="http://schemas.microsoft.com/2003/10/Serialization/">Hola</string>
            return RemoveXmlFromResponse(value);
        }
        #endregion

        #region Private methods
        /// <summary>
        /// Since this is called multiple times, seperated this into its own function
        /// </summary>
        /// <param name="response">the webresponse object the http or web request returns</param>
        /// <returns>the output of the web request as a string</returns>
        private string WebResponseToString(WebResponse response)
        {
            string value;
            using (StreamReader reader = new StreamReader(response.GetResponseStream(), System.Text.UnicodeEncoding.Default))
            {
                value = reader.ReadToEnd();
            }
            return value;
        }
  
        /// <summary>
        /// a very simple stupid way to remove the xml string tags from the response
        /// </summary>
        /// <param name="request">value to strip tag from</param>
        /// <returns></returns>
        private string RemoveXmlFromResponse(string request)
        {
            if (string.IsNullOrWhiteSpace(request)) { return string.Empty; }
            int startIndex = request.IndexOf('>') + 1;
            return request.Substring(startIndex, request.IndexOf('<', startIndex) - startIndex);
        }
        #endregion

    }
}
