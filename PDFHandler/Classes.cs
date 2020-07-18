using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PdfSharp.Pdf;
using PdfSharp.Drawing;
using System.IO;
using System.Xml.Linq;
using CustomWebClient;
namespace PDFHandler
{
    public class Documents : List<Document>
    {
        public Documents() { }
        public Documents(XElement oRaw)
        {
            foreach (XElement xRow in oRaw.Elements())
            {
                Add(new Document(xRow));
            }
        }
        public XElement toXMLElement()
        {
            XElement oElement = new XElement("Documents");
            foreach (Document oDoc in this)
            {
                oElement.Add(oDoc.toXMLElement());
            }
            return oElement;
        }
    }
    public class Replace
    {
        public string ThisValue { set; get; }
        public string WithThis { set; get; }
        public Replace()
        { }
        public Replace(string sThis, string sWThis)
        {
            ThisValue = sThis;
            WithThis = sWThis;
        }
        public Replace(XElement oRaw)
        {
            ThisValue = oRaw.Element("ThisValue").Value;
            WithThis = oRaw.Element("WithThis").Value;
        }
        public XElement toXMLElement()
        {
            XElement oElement = new XElement("Replace");
            oElement.SetElementValue("ThisValue", ThisValue);
            oElement.SetElementValue("WithThis", WithThis);
            return oElement;
        }
    }
    public class Replaces : List<Replace>
    {
        public Replaces() { }
        public Replaces(XElement oRaw)
        {
            foreach (XElement xRow in oRaw.Elements())
            {
                Add(new Replace(xRow));
            }
        }
        public XElement toXMLElement()
        {
            XElement oElement = new XElement("Replaces");
            foreach (Replace oDoc in this)
            {
                oElement.Add(oDoc.toXMLElement());
            }
            return oElement;
        }
        public List<string> ReplaceString(string sInput)
        {
            return this.Where(x => sInput.Contains(x.ThisValue)).Select(y => sInput.Replace(y.ThisValue, y.WithThis)).ToList();
        }
    }
    public class Document
    {
        public string URL { set; get; }
        public List<String> ImageURLs { set; get; }
        public string Title { set; get; }
        public string Author { set; get; }
        public string Subject { get { return "Created by KNDLComic"; } }
        public DateTime LastModified { set; get; }
        public enum status
        {
            New,
            Normal,
            Pending,
            Downloaded
        };

        public status Status { set; get; }
        public Document()
        {
            ImageURLs = new List<string>();
        }
        public Document(XElement oRaw)
        {
            Title = oRaw.Element("Title").Value;
            Author = oRaw.Element("Author").Value;
            URL = oRaw.Element("URL").Value;
            DateTime mLastMod;
            if (DateTime.TryParse(oRaw.Element("LastMod").Value, out mLastMod))
                LastModified = mLastMod;
            Status = (status)Enum.Parse(typeof(status), oRaw.Element("Status").Value);
            ImageURLs = new List<string>();
            foreach (XElement oURL in oRaw.Element("URLs").Elements())
            {
                ImageURLs.Add(oURL.Value);
            }
        }
        public MemoryStream toPDF(Replaces oReplaces)
        {
            PdfDocument mDocument = new PdfDocument();
            Replace ToValue = null;

            using (WebClient wc = new WebClient())
            {
                string sImageUrl = ImageURLs.FirstOrDefault(x => oReplaces.Exists(y => x.Contains(y.ThisValue)));
                if (sImageUrl != null)
                {
                    MemoryStream ms = new MemoryStream(wc.DownloadData(sImageUrl));
                    foreach (Replace oRpl in oReplaces.Where(x => sImageUrl.Contains(x.ThisValue)))//.Select(y => sImageURL.Replace(y.ThisValue, y.WithThis)).ToList())
                    {
                        try
                        {
                            if (ms.Length != new MemoryStream(wc.DownloadData(sImageUrl.Replace(oRpl.ThisValue, oRpl.WithThis))).Length)
                            {
                                ToValue = oRpl;
                            }
                        }
                        catch
                        {
                            //nichts
                        }
                    }
                }
                if (ToValue != null)
                {
                    List<string> sNURLS = ImageURLs.Select(y => y.Replace(ToValue.ThisValue, ToValue.WithThis)).ToList();
                    ImageURLs = sNURLS;
                }
                foreach (string sImageURL in ImageURLs)
                {
                    try
                    {
                        PdfPage page = mDocument.AddPage();
                        XGraphics gfx = XGraphics.FromPdfPage(page);
                        System.Drawing.Image oImage = System.Drawing.Image.FromStream(new MemoryStream(wc.DownloadData(sImageURL)));


                        MemoryStream ms = new MemoryStream();
                        oImage.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                        oImage.Dispose();
                        XImage image = XImage.FromStream(ms);

                        double dProporcion = 1;
                        //rezise calcs
                        if (image.PixelWidth * dProporcion > page.Width)
                        {
                            dProporcion = page.Width / image.PixelWidth;
                        }
                        if (image.PixelHeight * dProporcion > page.Height)
                        {
                            dProporcion = page.Height / image.PixelHeight;
                        }
                        gfx.DrawImage(image, (page.Width - image.PixelWidth * dProporcion) / 2, (page.Height - image.PixelHeight * dProporcion) / 2, image.PixelWidth * dProporcion, image.PixelHeight * dProporcion);
                    }
                    catch
                    {
                        //do nothing, try next
                    }
                }
            }
            MemoryStream fs = new MemoryStream();
            mDocument.Save(fs, false);
            return fs;
        }

        public XElement toXMLElement()
        {
            XElement oElement = new XElement("Document");
            oElement.SetElementValue("Title", Title);
            oElement.SetElementValue("URL", URL);
            oElement.SetElementValue("Author", Author);
            oElement.SetElementValue("LastMod", LastModified);
            oElement.SetElementValue("Status", Status);
            XElement xURLS = new XElement("URLs");
            ImageURLs.ForEach(x => xURLS.Add(new XElement("ImgURL", x)));
            oElement.Add(xURLS);
            return oElement;
        }
    }
    public class Feeds : List<Feed>
    {
        public Feeds(XElement oRaw)
        {
            foreach (XElement xRow in oRaw.Elements())
            {
                Add(new Feed(xRow));
            }
        }
        public XElement toXMLElement()
        {
            XElement oElement = new XElement("Feeds");
            foreach (Feed oDoc in this)
            {
                oElement.Add(oDoc.toXMLElement());
            }
            return oElement;
        }
    }
    public class Feed
    {
        public string Address { set; get; }
        public DateTime LastRefresh { set; get; }
        public Feed(XElement oRaw)
        {
            Address = oRaw.Element("Address").Value;
            DateTime mLastMod;
            if (DateTime.TryParse(oRaw.Element("LastRefresh").Value, out mLastMod))
                LastRefresh = mLastMod;
        }
        public Feed(string sAddress)
        {
            Address = sAddress;
            LastRefresh = new DateTime();
        }
        public XElement toXMLElement()
        {
            XElement oElement = new XElement("Feed");
            oElement.SetElementValue("Address", Address);
            oElement.SetElementValue("LastRefresh", LastRefresh);
            return oElement;
        }
    }
}
