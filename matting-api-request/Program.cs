using matting_api_request.api;
using System;
using System.IO;
namespace matting_api_request
{
    class Program
    {
        //Account key
        private const string APIKEY = "Enter the APIKEY for the account";
        //Request the address
        private const string REQUEST_URL = "https://picupapi.tukeli.net/api/v1";

        //Read path (this path is the picture folder path under the current project)
        private static readonly string READ_IMAGE_PATH;

        //Matting (including portrait, object, general, avatar, beautification, animation)
        private static readonly ApiMattingRqeuest apiMattingRqeuest;

        //Image URL
        private static readonly string ImageUrl;
        private static readonly string ImageUrl2;
        private static readonly string ImageUrl3;

        static Program()
        {
            READ_IMAGE_PATH = Path.GetFullPath("../../..") + @"\images\";
            apiMattingRqeuest = new ApiMattingRqeuest(APIKEY);
            ImageUrl = "https://pics6.baidu.com/feed/574e9258d109b3de9ff792ddf1564f87810a4c2d.jpeg?token=4a333e5c57d8a35cbe23682083316697&s=58898F5566027355008448A80300E00A";
            ImageUrl2 = "http://images.news18.com/ibnlive/uploads/2018/09/Ducati-Panigale-959-Corsa.jpg";
            ImageUrl3 = "http://wdpicup.oss-cn-hangzhou.aliyuncs.com/matting_original/2021/04/25/f1d95be6da2e490588e55e0757ac34eb.jpg?Expires=1619936560&OSSAccessKeyId=LTAIzt3dzL2GfSyG&Signature=kgXZ00EDzaTipKf8ABGMc%2BMGA%2FI%3D";
        }

        static void Main(string[] args)
        {
            //IdPhoto();
            //MattingPortrait();
            //MattingBody();
            //MattingUniversal();
            //MattingAvatar();
            //MattingBeautify();
            //ImageFix();
            //MattingAnime();
            //StyleTransfer();
            Console.ReadKey();
        }


        //Id photo API request
        private static void IdPhoto()
        {
            //Output path (easy to display results, you can customize the output path)
            string outPutPath = GetOutPutPath() + @"\id_photo.txt";
            string IdPhotoRqeuestUri = REQUEST_URL + "/idphoto/printLayout";
            var mattingRequest = new ApiIdPhotoRequest(APIKEY, IdPhotoRqeuestUri);
            mattingRequest.IdPhoto(READ_IMAGE_PATH + "boy.png", outPutPath);
        }

        //Portrait cutout（mattingType=1）
        private static void MattingPortrait()
        {
            //Crop
            string crop = "";
            //Background color
            string bgcolor = "";
            //Picture path
            string imagePath = READ_IMAGE_PATH + "goddess.jpeg";
            //Picture name
            string imageFileName = Path.GetFileName(imagePath);
            //Output path (convenient to display the results, you can customize the output path)
            string outPutPath = GetOutPutPath();
            //Return binary file stream
            apiMattingRqeuest.MattingReturnsByteRequest(REQUEST_URL + "/matting?mattingType=1", imagePath, imageFileName, crop, bgcolor, outPutPath + @"\portrait_byte.png");
            //Return base64 string
            apiMattingRqeuest.MattingReturnsBase64Request(REQUEST_URL + "/matting2?mattingType=1", imagePath, imageFileName, crop, bgcolor, outPutPath + @"\portrait_base64.png");
            //Return a base64 string through the image URL
            apiMattingRqeuest.MattingByUrlRequest(REQUEST_URL + "/mattingByUrl", "1", ImageUrl, crop, bgcolor, outPutPath + @"\portrait_url.png");
        }

        //Object Cutout（mattingType=2）
        private static void MattingBody()
        {
            //Crop
            string crop = "";
            //Background color
            string bgcolor = "";
            //Picture path
            string imagePath = READ_IMAGE_PATH + "ducati.jpg";
            //Picture name
            string imageFileName = Path.GetFileName(imagePath);
            //Output path (convenient to display the results, you can customize the output path)
            string outPutPath = GetOutPutPath();
            //Return binary file stream
            apiMattingRqeuest.MattingReturnsByteRequest(REQUEST_URL + "/matting?mattingType=2", imagePath, imageFileName, crop, bgcolor, outPutPath + @"\body_byte.png");
            //Return base64 string
            apiMattingRqeuest.MattingReturnsBase64Request(REQUEST_URL + "/matting2?mattingType=2", imagePath, imageFileName, crop, bgcolor, outPutPath + @"\body_base64.png");
            //Return a base64 string through the image URL
            apiMattingRqeuest.MattingByUrlRequest(REQUEST_URL + "/mattingByUrl", "2", ImageUrl2, crop, bgcolor, outPutPath + @"\body_url.png");
        }

        //Universal Cutout（mattingType=6）
        private static void MattingUniversal()
        {
            //Crop
            string crop = "";
            //Background color
            string bgcolor = "";
            //Picture path
            string imagePath = READ_IMAGE_PATH + "goddess.jpeg";
            //Picture name
            string imageFileName = Path.GetFileName(imagePath);
            //Output path (convenient to display the results, you can customize the output path)
            string outPutPath = GetOutPutPath();
            //Return binary file stream
            apiMattingRqeuest.MattingReturnsByteRequest(REQUEST_URL + "/matting?mattingType=6", imagePath, imageFileName, crop, bgcolor, outPutPath + @"\universal_byte.png");
            //Return base64 string
            apiMattingRqeuest.MattingReturnsBase64Request(REQUEST_URL + "/matting2?mattingType=6", imagePath, imageFileName, crop, bgcolor, outPutPath + @"\universal_base64.png");
            //Return a base64 string through the image URL
            apiMattingRqeuest.MattingByUrlRequest(REQUEST_URL + "/mattingByUrl", "6", ImageUrl, crop, bgcolor, outPutPath + @"\universal_url.png");
        }

        //Avatar cutout（mattingType=3）
        private static void MattingAvatar()
        {
            //Crop
            string crop = "";
            //Background color
            string bgcolor = "";
            //Picture path
            string imagePath = READ_IMAGE_PATH + "child.jpg";
            //Picture name
            string imageFileName = Path.GetFileName(imagePath);
            //Output path (convenient to display the results, you can customize the output path)
            string outPutPath = GetOutPutPath();
            //Return binary file stream
            apiMattingRqeuest.MattingReturnsByteRequest(REQUEST_URL + "/matting?mattingType=3", imagePath, imageFileName, crop, bgcolor, outPutPath + @"\avatar_byte.png");
            //Return base64 string
            apiMattingRqeuest.MattingReturnsBase64Request(REQUEST_URL + "/matting2?mattingType=3", imagePath, imageFileName, crop, bgcolor, outPutPath + @"\avatar_base64.png");
            //Return a base64 string through the image URL
            apiMattingRqeuest.MattingByUrlRequest(REQUEST_URL + "/mattingByUrl", "3", ImageUrl3, crop, bgcolor, outPutPath + @"\avatar_url.png");
        }

        //beautify
        private static void MattingBeautify()
        {
            //Picture path
            string imagePath = READ_IMAGE_PATH + "goddess.jpeg";
            //Picture name
            string imageFileName = Path.GetFileName(imagePath);
            //Output path (convenient to display the results, you can customize the output path)
            string outPutPath = GetOutPutPath();
            //Return binary file stream
            apiMattingRqeuest.MattingReturnsByteRequest(REQUEST_URL + "/matting?mattingType=4", imagePath, imageFileName, "", "", outPutPath + @"\beautify_byte.png");
            //Return base64 string
            apiMattingRqeuest.MattingReturnsBase64Request(REQUEST_URL + "/matting2?mattingType=4", imagePath, imageFileName, "", "", outPutPath + @"\beautify_base64.png");
        }

        //Image restoration
        private static void ImageFix()
        {
            //image
            string fix_image = READ_IMAGE_PATH + "image_fix.jpg";
            //mask
            string mask_image = READ_IMAGE_PATH + "mask.jpg";
            //Output path (convenient to display the results, you can customize the output path)
            string outPutPath = GetOutPutPath() + @"\image_fix.txt";
            var apiImageFixRequest = new ApiImageFixRequest(APIKEY, REQUEST_URL + "/imageFix");
            apiImageFixRequest.ImageFixRequest(fix_image, mask_image, outPutPath);
        }

        //Animation
        private static void MattingAnime()
        {
            //Picture path
            string imagePath = READ_IMAGE_PATH + "goddess.jpeg";
            //Picture name
            string imageFileName = Path.GetFileName(imagePath);
            //Output path (convenient to display the results, you can customize the output path)
            string outPutPath = GetOutPutPath();
            //Return binary file stream
            apiMattingRqeuest.MattingReturnsByteRequest(REQUEST_URL + "/matting?mattingType=11", imagePath, imageFileName, "", "", outPutPath + @"\anime_byte.png");
            //Return base64 string
            apiMattingRqeuest.MattingReturnsBase64Request(REQUEST_URL + "/matting2?mattingType=11", imagePath, imageFileName, "", "", outPutPath + @"\anime_base64.png");
            //Return a base64 string through the image URL
            apiMattingRqeuest.MattingByUrlRequest(REQUEST_URL + "/mattingByUrl", "11", ImageUrl, null, null, outPutPath + @"\anime_url.png");
        }

        //Style transfer
        private static void StyleTransfer()
        {
            //Picture path
            string imagePath = READ_IMAGE_PATH + "goddess.jpeg";
            //Style picture path
            string styleImagePath = READ_IMAGE_PATH + "style.jpg";
            //Output path (convenient to display the results, you can customize the output path)
            string outPutPath = GetOutPutPath() + @"\style_transfer.txt";
            var apiStyleTransfer = new ApiStyleTransferRequest(APIKEY, REQUEST_URL + "/styleTransferBase64");
            apiStyleTransfer.StyleTranferRequest(imagePath, styleImagePath, outPutPath);
        }

        //Output path (only for the convenience of displaying the results)
        private static string GetOutPutPath()
        {
            string outPutPath = Environment.CurrentDirectory + @"\result";
            if (!Directory.Exists(outPutPath))
            {
                Directory.CreateDirectory(outPutPath);
            }
            return outPutPath;
        }
    }
}
