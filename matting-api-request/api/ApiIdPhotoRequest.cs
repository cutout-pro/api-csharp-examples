using matting_api_request.httpUtils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace matting_api_request.api
{
    class ApiIdPhotoRequest
    {
        //Account key
        private readonly string ApiKey;

        //Request address
        private readonly string RqeustUri;


        public ApiIdPhotoRequest(string apikey, string uri)
        {
            this.ApiKey = apikey;
            this.RqeustUri = uri;
        }

        //ID Photo API
        //imagePath: Picture path
        //outPutPath: Output image path
        public void IdPhoto(string imagePath, string outPutPath)
        {
            Console.WriteLine("Request address:" + RqeustUri + "\n");
            try
            {
                //Base64 of the requested image
                byte[] byes = File.ReadAllBytes(imagePath);
                string imageBase64 = Convert.ToBase64String(byes);

                //Request header
                var header = new Dictionary<string, string> {
                    {"APIKEY", ApiKey}
                };

                //Request body
                var parameters = new Dictionary<string, object>
                {
                    //Picture of Base64
                    {"base64", imageBase64},
                    //background color
                    {"bgColor", "FFFFFF"},
                    //ID photo printing DPI
                    {"dpi", 300},
                    //Physical height of ID photo
                    {"mmHeight", 35},
                    //Physical width of ID photo
                    {"mmWidth", 25},
                    //Typographic background color
                    {"printBgColor", "FFFFFF"},
                     //The height of the printed layout size, in millimeters
                    {"printMmHeight", 210},
                    //The width of the printed layout size, in millimeters
                    {"printMmWidth", 150},
                    //Change the parameters, fill in this parameter to deduct an extra point
                    //{"dress", ""}
                };
                string result = HttpClientUtil.PostRequest(RqeustUri, parameters, header);
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
            catch (Exception e)
            {
                Console.WriteLine("ID photo API request error, error message:" + e.Message);
            }
        }
    }
}
