using System;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using HtmlAgilityPack;
using System.IO;
using System.IO.Compression;
namespace WebConnections
{
    public class Website
    {
        private static string _cookies = string.Empty;
        public string URL { set; get; }
        public string Title { set; get; }
        public Website()
        { }
        public Website(string sURL, string sTitle)
        {
            Title = sTitle;
            URL = sURL;
        }
        public Website(string sURL)
        {
            URL = sURL;
        }
        public List<string> GetImageURLs()
        {
            if (URL.ToLower().Contains("ninemanga"))
            {
                return GetFromNinemanga();
            }
            return null;
        }
        private List<string> GetFromNinemanga()
        {
            List<string> imgURLs = new List<string>();
            HtmlWeb web = new HtmlWeb();
            int iRat;
            if (int.TryParse(URL.TrimEnd('/').Split('/').Last().Split('.').FirstOrDefault().Split('-').FirstOrDefault(), out iRat))
            {


                using (CustomWebClient.WebClient client = new CustomWebClient.WebClient())
                {
                    HtmlDocument doc = new HtmlDocument();

                    doc.LoadHtml(client.DownloadStringGzip(URL));

                    Uri oURI = new Uri(URL);

                    string sNext = "";
                    Title = doc.DocumentNode.SelectSingleNode(@"//*[@id=""chapter""]").ChildNodes.FirstOrDefault(x => x.Attributes.Any(y => y.Name == "selected")).NextSibling.InnerText;
                    while (doc.DocumentNode.SelectSingleNode(@"/html/head/title").InnerText.Contains(Title))
                    {
                        foreach (HtmlNode oNode in doc.DocumentNode.Descendants("img").Where(d => d.Attributes.Contains("class") && d.Attributes["class"].Value.Contains("manga_pic")))
                        {
                            imgURLs.Add(oNode.Attributes.FirstOrDefault(x => x.Name == "src").Value);
                        }
                        sNext = oURI.Scheme + @"://" + oURI.Authority + doc.DocumentNode.SelectNodes("/html/body/div[3]/div[2]/div[3]/a[1]")[0].Attributes[0].Value;
                        doc.LoadHtml(client.DownloadStringGzip(sNext));
                    }
                }
            }
            else
            {
                HtmlDocument doc = web.Load(URL);
                Title = "Collection";
                foreach (HtmlNode oNode in doc.DocumentNode.SelectNodes("/html/body/div[2]/div/div[3]/div[2]")[0].ChildNodes.Where(x => x.Attributes.Any(y => y.Name == "class" && y.Value == "sub_vol_ul")))
                {
                    imgURLs.Add("http://es.ninemanga.com" + oNode.SelectSingleNode("/em/a[1]").Attributes.FirstOrDefault(x => x.Name == "href").Value);
                }
            }
            return imgURLs;
        }
    }

    public class Websites : List<Website>
    {
        public static List<string> GetSitesFromFeed(string sURL)
        {
            List<string> sTemp = new List<string>();
            XElement oRoot = XElement.Load(sURL);
            foreach (XElement oItem in oRoot.Descendants("channel").FirstOrDefault().Descendants("item"))
            {
                sTemp.Add(oItem.Element("link").Value);
            }
            return sTemp;
        }
        public static List<string> GetSitesFromFeed(string sRss, DateTime dFrom)
        {
            List<string> sTemp = new List<string>();
            XElement oRoot = XElement.Parse(sRss.Replace("&", "&amp;"));
            foreach (XElement oItem in oRoot.Descendants("channel").FirstOrDefault().Descendants("item"))
            {
                DateTime mPubDate;
                if (DateTime.TryParse(oItem.Element("pubDate").Value, out mPubDate))
                {
                    if (mPubDate > dFrom)
                        sTemp.Add(oItem.Element("link").Value);
                }
                else
                {
                    int iHours = 0, iTemp, iAux = 1;
                    while (int.TryParse(oItem.Element("pubDate").Value.Substring(0, iAux), out iTemp))
                    {
                        iHours = iTemp;
                        iAux += 1;
                    }
                    if (DateTime.Now.AddHours(iHours * -1) > dFrom)
                        sTemp.Add(oItem.Element("link").Value);
                }
            }
            return sTemp;
        }
        public Websites() { }



        public void GetByURL(string URL, string HTML)
        {
            if (URL.ToLower().Contains("ninemanga"))
            {
                GetFromNinemanga(HTML, URL);
            }
        }
        private void GetFromNinemanga(string HTML, string URL)
        {
            HtmlDocument doc = new HtmlDocument();
            Uri oURI = new Uri(URL);
            doc.LoadHtml(HTML);
            if (int.TryParse(URL.TrimEnd('/').Split('/').Last().Split('.').FirstOrDefault().Split('-').FirstOrDefault(), out int iRat))
            {
                Add(new Website(URL/*URL.Substring(0, URL.IndexOf(iRat.ToString())) + iRat.ToString() + "-1000000-1.html"*/, doc.DocumentNode.SelectSingleNode("/html/body/div[3]/div[2]/div[1]").ChildNodes[1].ChildNodes.FirstOrDefault(x => x.Attributes.Any(y => y.Name == "selected")).NextSibling.InnerText));
            }
            else
            {

                foreach (HtmlNode oLNode in doc.DocumentNode.SelectNodes("/html/body/div[2]/div/div[3]/div[2]")[0].ChildNodes.Where(x => x.Attributes.Any(y => y.Name == "class" && y.Value == "sub_vol_ul")))
                {
                    foreach (HtmlNode oNode in oLNode.ChildNodes.Where(x => x.Name == "li"))
                    {
                        Add(new Website((oURI.Scheme + @"://" + oURI.Authority + oNode.SelectSingleNode("em/a[1]").Attributes.FirstOrDefault(x => x.Name == "href").Value), oNode.SelectSingleNode("a").InnerText));
                    }
                }
            }
        }
    }

}
