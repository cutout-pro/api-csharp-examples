using matting_api_request.httpUtils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace matting_api_request.api
{
    class ApiStyleTransferRequest
    {
        //Account key
        private readonly string ApiKey;

        //Request address
        private readonly string RqeuestUrl;

        public ApiStyleTransferRequest(string apiKey, string requestUrl)
        {
            this.ApiKey = apiKey;
            this.RqeuestUrl = requestUrl;
        }

        public void StyleTranferRequest(string imagePath, string styleImagePath, string outPutPath)
        {
            Console.WriteLine("Request address:" + RqeuestUrl + "\n");
            try
            {
                //Picture of Base64
                byte[] bytes = File.ReadAllBytes(imagePath);
                string contentBase64 = Convert.ToBase64String(bytes);
                //Style Base64
                byte[] styleByte = File.ReadAllBytes(styleImagePath);
                string styleBase64 = Convert.ToBase64String(styleByte);

                //Request header
                var header = new Dictionary<string, string>
                {
                    {"APIKEY", ApiKey}
                };

                //Request parameter
                var parameters = new Dictionary<string, object>
                {
                    {"contentBase64", contentBase64},
                    {"styleBase64", styleBase64}
                };
                string result = HttpClientUtil.PostRequest(RqeuestUrl, parameters, header);
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
                Console.WriteLine("Style transfer API request failed:" + ex.Message);
            }
        }
    }
}
