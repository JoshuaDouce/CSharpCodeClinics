using Newtonsoft.Json.Linq;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Primitives;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using SixLabors.Primitives;
using System.Diagnostics;

namespace FaceRecognition
{
    class Program
    {
        private static string msg = "Pleas provide an ApiKey followed by the path to an Image"; 
        static void Main(string[] args)
        {
            //Command Line Args
            var apiKey = !string.IsNullOrWhiteSpace(args[0]) ? args[0] : throw new ArgumentException(msg, args[0]);
            var fileName = File.Exists(args[1]) ? args[1] : throw new FileNotFoundException(msg, args[1]);

            //Request
            var region = "westcentralus";
            var target = new Uri($"https://{region}.api.cognitive.microsoft.com/face/v1.0/detect/?subscription-key={apiKey}");
            var httpPost = CreateHttpRequest(target, "POST", "application/octet-stream");

            //Load Image
            using (var fs = File.OpenRead(fileName))
            {
                fs.CopyTo(httpPost.GetRequestStream());
            }

            //Submit Image
            string data = GetResponse(httpPost);

            //Inspect Response
            var rectangles = GetRectangles(data);

            //Draw on image (copy)
            var image = Image.Load(fileName);

            foreach (var rectangle in rectangles)
            {
                image.Mutate(a => a.DrawPolygon(Rgba32.Red, 20, rectangle));
            }

            var outputFileName = @"C:\Users\Doucej\Pictures\2-people-1-relationship-01-1024x857-modified.png";

            SaveImage(image, outputFileName);

            OpenWithDefaultApp(outputFileName);
        }

        private static void OpenWithDefaultApp(string outputFileName)
        {
            var si = new ProcessStartInfo()
            {
                FileName = "explorer.exe",
                Arguments = outputFileName,
                WindowStyle = ProcessWindowStyle.Hidden
            };
            Process.Start(si);
        }

        private static void SaveImage(Image<Rgba32> image, string outputFileName)
        {
            using (var fs = File.Create(outputFileName))
            {
                image.SaveAsPng(fs);
            }
        }

        private static IEnumerable<PointF[]> GetRectangles(string data)
        {
            var faces = JArray.Parse(data);

            foreach (var face in faces)
            {
                var id = (string)face["faceId"];

                var top = (int)face["faceRectangle"]["top"];
                var left = (int)face["faceRectangle"]["left"];
                var width = (int)face["faceRectangle"]["width"];
                var height = (int)face["faceRectangle"]["height"];

                var rectangle = new PointF[] {
                    new PointF(left, top),
                    new PointF(left + width, top),
                    new PointF(left + width, top + height),
                    new PointF(left, top + height)                   
                };

                yield return rectangle;
            }
        }

        private static string GetResponse(HttpWebRequest httpPost)
        {
            using (var response = httpPost.GetResponse())
            using (var sr = new StreamReader(response.GetResponseStream()))
            {
                return sr.ReadToEnd();
            }
        }

        private static HttpWebRequest CreateHttpRequest(Uri target, string httpMethod, string contentType)
        {
            var request = WebRequest.CreateHttp(target);
            request.Method = httpMethod;
            request.ContentType = contentType;
            return request;
        }
    }
}
