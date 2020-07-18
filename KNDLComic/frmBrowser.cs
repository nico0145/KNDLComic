using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using HtmlAgilityPack;
namespace KNDLComic
{
    public partial class frmBrowser : Form
    {
        private bool bLoading;
        public string URL { set; get; }
        public string Title { set; get; }
        public List<string> imgURLs { set; get; }
        private string[] NextButton = { "Próximo", ">>" };
        private string[] PrevButton = { "Anterior", "<<" };
        public string RSS { set; get; }
        public frmBrowser()
        {
            InitializeComponent();
        }

        private void frmBrowser_Load(object sender, EventArgs e)
        {
            HtmlWeb web = new HtmlWeb();
            brsMain.ScriptErrorsSuppressed = true;
            if (RSS != null)
            {
                RSS = GetFromBrowser(RSS);
            }
            else
            {
                imgURLs = new List<string>();

                int iRat;

                if (int.TryParse(URL.TrimEnd('/').Split('/').Last().Split('.').FirstOrDefault().Split('-').FirstOrDefault(), out iRat))
                {
                    HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
                    string sImgUR;
                    string sNext = URL;
                    bool bRetry = true;
                    doc.LoadHtml(GetFromBrowser(sNext));
                    Title = doc.DocumentNode.SelectSingleNode(@"//*[@id=""chapter""]").ChildNodes.FirstOrDefault(x => x.Attributes.Any(y => y.Name == "selected")).NextSibling.InnerText;
                    while (bRetry)
                    {
                        try
                        {
                            while (doc.DocumentNode.SelectSingleNode(@"/html/head/title").InnerText.Contains(Title))
                            {
                                foreach (HtmlNode oNode in doc.DocumentNode.Descendants("img").Where(d => d.Attributes.Contains("class") && d.Attributes["class"].Value.Contains("manga_pic")))
                                {
                                    sImgUR = oNode.Attributes.FirstOrDefault(x => x.Name == "src")?.Value;
                                    using (CustomWebClient.WebClient wc = new CustomWebClient.WebClient())
                                    {
                                        if (wc.DoesFileExist(sImgUR))
                                        {
                                            imgURLs.Add(sImgUR);
                                        }
                                        else
                                        {
                                            throw new Exception("404 file not found");
                                        }
                                    }

                                }
                                doc.LoadHtml(ClickonLink(NextButton));
                                if (brsMain.Document.Body.InnerHtml.ToString().Contains("all_imgs_url"))
                                {
                                    imgURLs = GetAllImagesFromJSVariable();
                                    if (imgURLs.Any())
                                    {
                                        this.Close();
                                        return;
                                    }
                                }
                            }
                            bRetry = false;
                        }
                        catch
                        {
                            try
                            {
                                if (imgURLs.Count == 0)
                                {
                                    ClickonLink(NextButton);
                                    doc.LoadHtml(ClickonLink(PrevButton));
                                }
                                else
                                {
                                    ClickonLink(PrevButton);
                                    doc.LoadHtml(ClickonLink(NextButton));
                                }
                                bRetry = true;
                            }
                            catch
                            {
                                bRetry = false;
                            }
                        }
                    }
                }
                else
                {
                    HtmlAgilityPack.HtmlDocument doc = web.Load(URL);
                    Title = "Collection";
                    foreach (HtmlNode oNode in doc.DocumentNode.SelectNodes("/html/body/div[2]/div/div[3]/div[2]")[0].ChildNodes.Where(x => x.Attributes.Any(y => y.Name == "class" && y.Value == "sub_vol_ul")))
                    {
                        imgURLs.Add("http://es.ninemanga.com" + oNode.SelectSingleNode("/em/a[1]").Attributes.FirstOrDefault(x => x.Name == "href").Value);
                    }
                }
            }
            this.Close();

        }
        private List<string> GetAllImagesFromJSVariable()
        {
            Regex regex = new Regex(@"all_imgs_url: \[(.*?)\]", RegexOptions.Singleline);
            string sChunks = regex.Match(brsMain.Document.Body.InnerHtml.ToString()).Groups[1].Value;
            return sChunks.Split('"').Where(x => x.StartsWith("http")).ToList();
        }
        private string GetFromBrowser(string sURL)
        {
            brsMain.Navigate(sURL);
            while (brsMain.ReadyState != WebBrowserReadyState.Complete)
            {
                Application.DoEvents();
            }

            return brsMain.DocumentText;

        }
        private string ClickonLink(string[] innerText)
        {
            string sOldTitle = brsMain.DocumentTitle;
            HtmlElement oLink = FindLink(innerText);
            oLink.InvokeMember("Click");
            oLink.RaiseEvent("onclick");
            bLoading = true;
            while (bLoading)
            {
                Application.DoEvents();
                while (brsMain.ReadyState != WebBrowserReadyState.Complete)
                {
                    Application.DoEvents();
                }
                if (brsMain.StatusText == "Done" && brsMain.DocumentTitle != sOldTitle)
                    bLoading = false;
            }
            return brsMain.DocumentText;
        }

        HtmlElement FindLink(string[] innerText)
        {
            foreach (HtmlElement link in brsMain.Document.GetElementsByTagName("A"))
            {
                if (innerText.Any(x => link?.InnerText?.Equals(x) == true))
                {
                    return link;
                }
            }
            return null;
        }
        private void brsMain_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            bLoading = false;
        }

        private void brsMain_ProgressChanged(object sender, WebBrowserProgressChangedEventArgs e)
        {
            bLoading = true;
        }

    }
}
