using System;
using System.Net;
using System.Linq;
using System.Threading;

namespace Imgget
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            string userName = GetUserName();

            OpenAccountPage(userName);
            WriteInitialInstructions();

            var imageLinks = GetImageLinks();

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

                    var downloadPath = $"{userName}/{fileName}";

                    if (!System.IO.File.Exists(downloadPath))
                    {
                        webClient.DownloadFile(imageLinks[i], downloadPath);

                        Thread.Sleep(500);

                        if (i % 50 == 0)
                        {
                            Console.WriteLine("50 images has been downloaded. Cool down time.");
                            Thread.Sleep(10000);
                        }
                    }

                }
            }
        }

        private static string[] GetImageLinks()
        {
            return Console.ReadLine().Split(',');
        }

        private static string GetUserName()
        {
            Console.WriteLine("Enter username");
            return Console.ReadLine();
        }

        private static void WriteInitialInstructions()
        {
            Console.WriteLine("Scroll to the bottom of the page. Press F12 on the page.");
            Console.WriteLine("");
            Console.WriteLine("Copy and paste this JS code into the developer javascript console window:");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("var imgElements = document.getElementsByTagName('article')[0].querySelectorAll('img');var imgSrcArray = [];for (var i = 0; i < imgElements.length; i++){imgSrcArray.push(imgElements[i].getAttribute('src'));}imgSrcArray.toString();");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("Now copy and paste the long string of links into this window:");
        }

        public static void OpenAccountPage(string userName) {
            System.Diagnostics.Process.Start("https://www.instagram.com/"+userName);
        }

        public static string GetFileName(string fileUrl){
            var startIndex = fileUrl.LastIndexOf('/');

            return fileUrl.Substring(startIndex);
        }
    }
}
