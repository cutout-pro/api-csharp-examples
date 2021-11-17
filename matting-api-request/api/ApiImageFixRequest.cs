using matting_api_request.httpUtils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace matting_api_request.api
{
    class ApiImageFixRequest
    {
        //Account key
        private readonly string ApiKey;
        //Request address
        private readonly string RequestUrl;

        public ApiImageFixRequest(string apiKey, string requestUrl)
        {
            this.ApiKey = apiKey;
            this.RequestUrl = requestUrl;
        }

        /**
         * Picture repair (example uses maskBase64 to request)
         * imagePath Picture path
         * maskImagePath base64 of the mask image
         */
        public void ImageFixRequest(string imagePath, string maskImagePath, string outPutPath)
        {
            Console.WriteLine("Request address:" + RequestUrl + "\n");
            try
            {
                //Pictures that need to be repaired
                byte[] bytes = File.ReadAllBytes(imagePath);
                string base64 = Convert.ToBase64String(bytes);
                //mask picture
                byte[] mask = File.ReadAllBytes(maskImagePath);
                string maskBase64 = Convert.ToBase64String(mask);

                //Rectangular area array parameters (use a rectangular area without mask picture)
                //var rectangles = new List<Dictionary<string, object>>
                //{
                //    {
                //        new Dictionary<string, object>{
                //            {"x", 160},
                //            {"y",250},
                //            {"width",200},
                //            {"height",200}
                //        }
                //    }
                //};

                //Request body
                var parameters = new Dictionary<string, object>
                {
                    //Fix the base64 of the picture
                    {"base64", base64},
                    //base64 of the mask image
                    {"maskBase64", maskBase64}
                    //Rectangular area request parameters
                    //{"rectangles", rectangles }
                };
                //Request header
                var header = new Dictionary<string, string>
                {
                        {"APIKEY", ApiKey}
                };
                string result = HttpClientUtil.PostRequest(RequestUrl, parameters, header);
                if (result != null)
                {
                    var resultData = JsonConvert.DeserializeObject<Dictionary<string, object>>(result);
                    string code = resultData["code"].ToString();
                    if (code.Equals("0"))
                    {
                        Console.WriteLine("success----------\n");
                        File.AppendAllText(outPutPath, result);
                        Console.WriteLine("Output path:" + outPutPath);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Image repair API request error:" + ex.Message);
            }
        }
    }
}
