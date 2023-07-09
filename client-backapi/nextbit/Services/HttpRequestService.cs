//using System.Net;
//using System.Text;

//namespace nextbit.Services
//{
//    public static class HttpRequestService
//    {
//        public static string HttpRequest(string url, string data, WebHeaderCollection headers = null, string contextType = "application/x-www-form-urlencoded")
//        {
//            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
//            request.Method = WebRequestMethods.Http.Get;
//            request.ContentType = contextType;
//​
//        if (headers != null) request.Headers = headers;
//​
//        if (!string.IsNullOrEmpty(data))
//            {
//                request.Method = WebRequestMethods.Http.Post;
//​
//            byte[] bytes = Encoding.UTF8.GetBytes(data);
//                request.ContentLength = bytes.Length;
//​
//            using (Stream reqStream = request.GetRequestStream())
//                {
//                    reqStream.Write(bytes, 0, bytes.Length);
//                }
//            }
//​
//        string responseText = string.Empty;
//​
//        try
//            {
//                using (HttpWebResponse resp = (HttpWebResponse)request.GetResponse())
//                {
//                    Stream respStream = resp.GetResponseStream();
//                    using (StreamReader sr = new StreamReader(respStream))
//                    {
//                        responseText = sr.ReadToEnd();
//                    }
//                }
//            }
//            catch (WebException ex)
//            {
//                using (var stream = ex.Response.GetResponseStream())
//                using (var reader = new StreamReader(stream))
//                {
//                    responseText = reader.ReadToEnd();
//                }
//            }
//            catch (Exception ex)
//            {
//                responseText = ex.Message;
//            }
//​
//        return responseText;
//        }
//    }
//}
