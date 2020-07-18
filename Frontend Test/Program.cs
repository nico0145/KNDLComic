using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PDFHandler;
using System.IO;
using WebConnections;
using EmailHandler;
namespace Frontend_Test
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Please enter your Kindle's email adress [Default: nscavone_76@kindle.com]");
            string sMailTo = Console.ReadLine();
            if (sMailTo.Length == 0)
                sMailTo = "nscavone_76@kindle.com";
            Console.WriteLine("Please enter the Website address");
            foreach (string sLink in Console.ReadLine().Split(';'))
            {
                Document oDoc = new Document();
                oDoc.Author = "Reddit";
                oDoc.Title = "Test";
                Email oMail = new Email();
                oMail.To = sMailTo;
                Website oWeb = new Website(sLink);
                oDoc.ImageURLs = oWeb.GetImageURLs();
                oMail.Attachment = oDoc.ToPDF();
                oMail.AttachmentFileName = oWeb.Title;
                oMail.SendEmail();
            }
        }
    }
}
