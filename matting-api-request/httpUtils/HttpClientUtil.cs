using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Text;

namespace matting_api_request.httpUtils
{
    class HttpClientUtil
    {

        /* 
         *Post request returns string (synchronous)
         */
        public static string PostRequest(string uri, Dictionary<string, object> parameters, Dictionary<string, string> headers)
        {
            string result = null;
            using (var client = new HttpClient())
            {
                try
                {
                    string json = JsonConvert.SerializeObject(parameters);
                    StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                    if (headers != null)
                    {
                        foreach (KeyValuePair<string, string> header in headers)
                        {
                            client.DefaultRequestHeaders.Add(header.Key, header.Value);
                        }
                    }
                    //Waiting for the execution result
                    var response = client.PostAsync(uri, content).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        //Waiting for results
                        result = response.Content.ReadAsStringAsync().Result;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            return result;
        }

        /**
         * Psot picture or file request, return binary stream
         *  
         */
        public static byte[] PostRequestReturnsByte(string uri, MultipartFormDataContent multipart, Dictionary<string, string> headers)
        {
            byte[] bytes = null;
            using (var client = new HttpClient())
            {
                try
                {
                    if (headers != null)
                    {
                        foreach (KeyValuePair<string, string> header in headers)
                        {
                            client.DefaultRequestHeaders.Add(header.Key, header.Value);
                        }
                    }
                    var response = client.PostAsync(uri, multipart).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        //The response header format is image/png, which means the request is successful
                        if (response.Content.Headers.ContentType.MediaType.Equals("image/png"))
                        {
                            bytes = response.Content.ReadAsByteArrayAsync().Result;
                        }
                        else
                        {
                            //Image processing failure returns JSON error message
                            string result = response.Content.ReadAsStringAsync().Result;
                            Console.WriteLine("Image processing failed: " + result);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            return bytes;
        }

        /**
         * Psot picture or file request, return characters
         *  
         */
        public static string PostRequestReturnsStr(string uri, MultipartFormDataContent multipart, Dictionary<string, string> headers)
        {
            string result = null;
            using (var client = new HttpClient())
            {
                try
                {
                    if (headers != null)
                    {
                        foreach (KeyValuePair<string, string> header in headers)
                        {
                            client.DefaultRequestHeaders.Add(header.Key, header.Value);
                        }
                    }
                    var response = client.PostAsync(uri, multipart).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        result = response.Content.ReadAsStringAsync().Result;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            return result;
        }

        /**
         * Get request
         */
        public static string GetRequest(string uri, Dictionary<string, string> parameters, Dictionary<string, string> headers)
        {
            string result = null;
            using (var client = new HttpClient())
            {
                try
                {
                    var builder = new UriBuilder(uri);
                    //Request parameter
                    if (parameters != null)
                    {
                        var query = new FormDataCollection(parameters).ReadAsNameValueCollection().ToString();
                        builder.Query = query;
                    }
                    //Request header
                    if (headers != null)
                    {
                        foreach (KeyValuePair<string, string> header in headers)
                        {
                            client.DefaultRequestHeaders.Add(header.Key, header.Value);
                        }
                    }
                    string url = builder.ToString();
                    Console.WriteLine("Request address:" + url + "\n");
                    var response = client.GetAsync(url).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        result = response.Content.ReadAsStringAsync().Result;
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            return result;
        }
    }
}
