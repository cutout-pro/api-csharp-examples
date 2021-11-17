using matting_api_request.httpUtils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;

namespace matting_api_request.api
{
    class ApiMattingRqeuest
    {
        //API key of the account
        private readonly string ApiKey;

        public ApiMattingRqeuest(string apiKey)
        {
            this.ApiKey = apiKey;
        }

        /**
         * Cutout returns the binary file stream
         * imagePath Picture path
         * crop Whether to cut
         * bgColor background color
         * outPutPath Output path
         */
        public void MattingReturnsByteRequest(string uri, string imageFilePath, string imageFileName, string crop, string bgColor, string outPutPath)
        {
            Console.WriteLine("Request address:" + uri + "\n");
            try
            {
                //Request header
                var heder = GetHeader();
                //upload picture
                var multipart = new MultipartFormDataContent()
                {
                    {new ByteArrayContent(File.ReadAllBytes(imageFilePath)), "file", imageFileName},
                    //Crop
                    {new StringContent(crop), "crop"},
                    //Picture background
                    {new StringContent(bgColor), "bgcolor"}
                };
                byte[] bytes = HttpClientUtil.PostRequestReturnsByte(uri, multipart, heder);
                if (bytes != null)
                {
                    Console.WriteLine("success-----------\n");
                    File.WriteAllBytes(outPutPath, bytes);
                    Console.WriteLine("Output path:" + outPutPath);
                    Console.WriteLine("----------------------------------\n");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Cutout failed, error message:" + ex.Message);
            }

        }


        /**
         * Cutout returns a base64 string
         * imagePath  Picture path
         * crop Whether to cut
         * bgColor background color
         * outPutPath Output path
         */
        public void MattingReturnsBase64Request(string uri, string imageFilePath, string imageFileName, string crop, string bgColor, string outPutPath)
        {
            Console.WriteLine("Request address:" + uri + "\n");
            try
            {
                //Request header
                var heder = GetHeader();
                //upload picture
                var multipart = new MultipartFormDataContent()
                {
                    {new ByteArrayContent(File.ReadAllBytes(imageFilePath)), "file", imageFileName},
                    //Crop
                    {new StringContent(crop), "crop"},
                    //Picture background
                    {new StringContent(bgColor), "bgcolor"}
                };
                string result = HttpClientUtil.PostRequestReturnsStr(uri, multipart, heder);
                if (result != null)
                {
                    var resultData = JsonConvert.DeserializeObject<Dictionary<string, object>>(result);
                    string code = resultData["code"].ToString();
                    if (code.Equals("0"))
                    {
                        Console.WriteLine("success----------\n");
                        var data = JsonConvert.DeserializeObject<Dictionary<string, object>>(resultData["data"].ToString());
                        string imageBase64 = data["imageBase64"].ToString();
                        byte[] bytes = Convert.FromBase64String(imageBase64);
                        File.WriteAllBytes(outPutPath, bytes);
                        Console.WriteLine("Output path:" + outPutPath);
                        Console.WriteLine("----------------------------------\n");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Cutout failed, error message:" + ex.Message);
            }

        }

        /**
         * Return Base64 result through picture Url
         */
        public void MattingByUrlRequest(string uri, string mattingType, string imageUrl, string crop, string bgColor, string outPutPath)
        {
            try
            {
                //Request header
                var header = GetHeader();
                //Request parameter
                var parameters = new Dictionary<string, string>
                {
                    //Cutout type
                    {"mattingType", mattingType},
                    //Crop
                    {"crop", crop},
                    //background color
                    {"bgcolor", bgColor},
                    //Picture URL
                    {"url", imageUrl}
                };
                string result = HttpClientUtil.GetRequest(uri, parameters, header);
                if (result != null)
                {
                    var resultData = JsonConvert.DeserializeObject<Dictionary<string, object>>(result);
                    string code = resultData["code"].ToString();
                    if (code.Equals("0"))
                    {
                        Console.WriteLine("success----------\n");
                        var data = JsonConvert.DeserializeObject<Dictionary<string, object>>(resultData["data"].ToString());
                        string imageBase64 = data["imageBase64"].ToString();
                        byte[] bytes = Convert.FromBase64String(imageBase64);
                        File.WriteAllBytes(outPutPath, bytes);
                        Console.WriteLine("Output path:" + outPutPath);
                        Console.WriteLine("----------------------------------\n");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Cutout failed, error message:" + ex.Message);
            }

        }

        /**
         * Request header
         */
        private Dictionary<string, string> GetHeader()
        {
            var header = new Dictionary<string, string>
            {
                {"APIKEY", ApiKey}
            };
            return header;
        }
    }
}
