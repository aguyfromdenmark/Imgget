using System;
using System.Net;
using System.Linq;
using System.Threading;
using System.IO;

namespace Imgget
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            Console.SetIn(new StreamReader(Console.OpenStandardInput(214748364), Console.InputEncoding, false, 214748364));

            string userName = GetUserName();

            OpenAccountPage(userName);
            WriteInitialInstructions();

            var imageLinks = GetImageLinks();

            Directory.CreateDirectory(userName);

            Console.WriteLine("Processing");
            DownloadFiles(userName, imageLinks);
        }

        private static void DownloadFiles(string userName, string[] imageLinks)
        {
            using (var webClient = new WebClient())
            {

                var imageLinksList = imageLinks.ToList();
                imageLinksList.Reverse();

                for (int i = 0; i < imageLinksList.Count; i++)
                {

                    var fileName = GetFileName(imageLinksList[i]);

                    var downloadPath = userName + "/" + fileName;

                    if (!File.Exists(downloadPath))
                    {
                        Console.WriteLine($"Downloading {i} of {imageLinksList.Count}...");
                        try
                        {
                            webClient.DownloadFile(imageLinks[i], downloadPath);

                        }
                        catch (Exception)
                        {
                            continue;
                        }

                        Thread.Sleep(500);

                        if (i > 0 && i % 50 == 0)
                        {
                            Console.WriteLine("50 images has been downloaded. Cool down time. Resume in 10 seconds.");
                            Thread.Sleep(10000);
                        }
                    }

                }

                Console.WriteLine("Job's done! Press any key to quit.");
                Console.ReadKey();
            }
        }

        private static string[] GetImageLinks()
        {
            return Console.ReadLine().Split(',');
        }

        private static string GetUserName()
        {
            Console.WriteLine("Enter username (IG Account will open in your browser. Chrome recommended)");
            return Console.ReadLine();
        }

        private static void WriteInitialInstructions()
        {
            Console.WriteLine("Scroll to the bottom of the page. Press F12 on the page.");
            Console.WriteLine("");
            Console.WriteLine("Copy and paste this JS code into the developer javascript console window:");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("var imgSrcArray = [];var interval = setInterval(function() {window.scrollTo(0, document.body.scrollHeight);var imgElements = document.getElementsByTagName('article')[0].querySelectorAll('img');for (var i = 0; i < imgElements.length; i++){imgSrcArray.push(imgElements[i].getAttribute('src'));}},300);");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("Now copy and paste this jscode into the developer javascript console window. Copy the full result.");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("var uniqueArray = imgSrcArray.filter(function(item, pos) {return imgSrcArray.indexOf(item) == pos;});uniqueArray.toString();");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("Now copy and paste the long string of links into this window:");
        }

        public static void OpenAccountPage(string userName) {
            System.Diagnostics.Process.Start("https://www.instagram.com/"+userName);
        }

        public static string GetFileName(string fileUrl){
            var startIndex = fileUrl.LastIndexOf('/');

            return fileUrl.Substring(startIndex + 1);
        }
    }
}
