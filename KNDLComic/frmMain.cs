using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PDFHandler;
using System.IO;
using WebConnections;
using EmailHandler;
using System.Xml.Linq;
using System.Text.RegularExpressions;

namespace KNDLComic
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        Documents oDocs;
        Feeds oFeeds;
        Replaces oReps;
        private void cmdAdd_Click(object sender, EventArgs e)
        {
            if (txtNewLink.Text.Length > 0)
            {
                AddWebsite(txtNewLink.Text, false);
            }
        }
        private void AddWebsite(string sUrl, bool bSend)
        {
            Websites oWeb = new Websites();
            oWeb.GetByURL(sUrl, GetHTMLFromBrowser(sUrl));
            if (oWeb.Count == 0)
            {
                MessageBox.Show("Invalid Link");
            }
            else
            {
                foreach (Website oIWeb in oWeb)
                {
                    Document oDoc = oDocs.FirstOrDefault(x => x.Title == oIWeb.Title);
                    if (oDoc == null)
                    {
                        //create
                        oDoc = new Document();
                        oDoc.Author = "KNDLComic";
                        oDoc.Title = oIWeb.Title;
                        lvwDownloadLinks.Items.Add(new ListViewItem(new[] { oIWeb.Title, Document.status.New.ToString() }));
                        oDocs.Add(oDoc);
                    }
                    else
                        lvwDownloadLinks.FindItemWithText(oIWeb.Title).SubItems[1].Text = Document.status.New.ToString();
                    if (oIWeb.URL != oDoc.URL)
                    {
                        oDoc.URL = oIWeb.URL;
                        if (bSend)
                        {
                            SendDocToEmail(oDoc, txtEmail.Text);
                            oDoc.Status = Document.status.Downloaded;
                        }
                        else
                            oDoc.Status = Document.status.New;
                        oDoc.LastModified = DateTime.Now;
                    }
                }
            }
        }
        private void frmMain_Load(object sender, EventArgs e)
        {
            LoadStateFromXML("Comix.xml");
        }

        private void cmdSend_Click(object sender, EventArgs e)
        {
            if (chkOneDoc.Checked)
            {
                Document oDoc = new Document();
                oDoc.Author = "KNDLComic";
                oDoc.Title = txtDocName.Text;
                foreach (ListViewItem oLI in lvwDownloadLinks.CheckedItems)
                {
                    Document oIndDoc = oDocs.SingleOrDefault(x => x.Title == oLI.Text);
                    if (oIndDoc.Status == Document.status.New)
                    {
                        Website oComicWeb = new Website(oIndDoc.URL);
                        oIndDoc.ImageURLs = oComicWeb.GetImageURLs();
                        oIndDoc.Status = Document.status.Downloaded;
                        oIndDoc.LastModified = DateTime.Now;
                    }
                    oDoc.ImageURLs.AddRange(oIndDoc.ImageURLs);
                }
                Email oMail = new Email();
                oMail.To = txtEmail.Text;
                oMail.AttachmentFileName = oDoc.Title;
                bool bRetry = true;
                while (bRetry)
                {
                    try
                    {
                        oMail.Attachment = oDoc.toPDF(oReps);
                        oMail.SendEmail();
                        foreach (ListViewItem oLI in lvwDownloadLinks.CheckedItems)
                        {
                            oLI.SubItems[1].Text = Document.status.Downloaded.ToString();
                        }
                        bRetry = false;
                    }
                    catch (Exception err)
                    {
                        if (MessageBox.Show(err.Message, "Error", MessageBoxButtons.AbortRetryIgnore) != System.Windows.Forms.DialogResult.Retry)
                        {
                            bRetry = false;
                        }
                    }
                }
            }
            else
            {
                switch (cmbSend.SelectedItem.ToString())
                {
                    case "":
                        break;
                    case "Checked":
                        foreach (ListViewItem oLI in lvwDownloadLinks.CheckedItems)
                        {
                            Document oDoc = oDocs.FirstOrDefault(x => x.Title == oLI.Text);
                            SendDocToEmail(oDoc, txtEmail.Text);

                        }
                        break;
                    case "Selected":
                        foreach (ListViewItem oLI in lvwDownloadLinks.SelectedItems)
                        {
                            Document oDoc = oDocs.FirstOrDefault(x => x.Title == oLI.Text);
                            SendDocToEmail(oDoc, txtEmail.Text);
                        }
                        break;
                    case "New":
                        foreach (Document oDoc in oDocs.Where(y => y.Status == Document.status.New))
                        {
                            SendDocToEmail(oDoc, txtEmail.Text);
                        }
                        break;

                }



            }
        }
        private void SendDocToEmail(Document oDoc, string sEmail)
        {
            Email oMail = new Email();
            oMail.To = sEmail;
            if (oDoc.Status == Document.status.New)
            {
                frmBrowser oFBR = new frmBrowser();
                oFBR.URL = oDoc.URL;
                oFBR.ShowDialog();

                oDoc.ImageURLs = oFBR.imgURLs;
                oDoc.Status = Document.status.Downloaded;
                oDoc.LastModified = DateTime.Now;
            }
            oMail.AttachmentFileName = oDoc.Title;
            bool bRetry = true;
            while (bRetry)
            {
                try
                {
                    oMail.Attachment = oDoc.toPDF(oReps);
                    oMail.SendEmail();
                    lvwDownloadLinks.FindItemWithText(oDoc.Title).SubItems[1].Text = oDoc.Status.ToString();
                    bRetry = false;
                    oMail.Attachment.Dispose();
                }
                catch (Exception err)
                {
                    if (MessageBox.Show(err.Message, "Error", MessageBoxButtons.RetryCancel) != System.Windows.Forms.DialogResult.Retry)
                    {
                        bRetry = false;
                    }
                }
            }
        }
        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveStateInXML("Comix.xml");
        }

        private void chkOneDoc_CheckedChanged(object sender, EventArgs e)
        {
            txtDocName.Enabled = chkOneDoc.Checked;
        }
        private void SaveStateInXML(string sFile)
        {
            XElement oRoot = new XElement("KNDLComic");
            oRoot.Add(oDocs.toXMLElement());
            oRoot.Add(oFeeds.toXMLElement());
            oRoot.Add(oReps.toXMLElement());
            oRoot.Save(sFile);
        }
        private void LoadStateFromXML(string sFile)
        {
            try
            {
                XElement xDocs = XElement.Load(sFile);
                oFeeds = new Feeds(xDocs.Descendants("Feeds").First());
                foreach (Feed oFeed in oFeeds)
                {
                    lvwFeeds.Items.Add(new ListViewItem(new[] { oFeed.Address }));
                }
                oDocs = new Documents(xDocs.Descendants("Documents").First());
                foreach (Document oDoc in oDocs.OrderBy(x => x.Title))
                {
                    lvwDownloadLinks.Items.Add(new ListViewItem(new[] { oDoc.Title, oDoc.Status.ToString() }));
                }
                oReps = new Replaces(xDocs.Descendants("Replaces").First());
                ListViewItem oItem;
                foreach (Replace oRep in oReps.OrderBy(x => x.ThisValue))
                {
                    oItem = new ListViewItem(new[] { oRep.ThisValue, oRep.WithThis });
                    oItem.Tag = oRep;
                    lswReplaces.Items.Add(oItem);
                }
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message + Environment.NewLine);
                oDocs = new Documents();
            }
        }

        private void cmdAddFeed_Click(object sender, EventArgs e)
        {
            oFeeds.Add(new Feed(txtNewLink.Text));
            lvwFeeds.Items.Add(new ListViewItem(new[] { txtNewLink.Text }));
        }
        private string GetHTMLFromBrowser(string URL)
        {
            frmBrowser fBrowser = new frmBrowser() { RSS = URL };
            fBrowser.ShowDialog();
            return fBrowser.RSS;
        }
        private string AdaptRSSHTML(string sHTML)
        {

            Regex regex = new Regex(@"(?<=<channel>)(.*)(?=</channel>)", RegexOptions.Singleline);
            return $"<root><channel>{regex.Match(sHTML).Groups[1].Value}</channel></root>";
        }
        private void RefreshFeed(Feed oFeed)
        {
            List<string> sSites = Websites.GetSitesFromFeed(AdaptRSSHTML(GetHTMLFromBrowser(oFeed.Address)), oFeed.LastRefresh);
            foreach (string sSite in sSites)
            {
                AddWebsite(sSite, false);
            }
            oFeed.LastRefresh = DateTime.Now;
        }
        private void cmdChangeState_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem oLI in lvwDownloadLinks.CheckedItems)
            {
                Document oDoc = oDocs.FirstOrDefault(x => x.Title == oLI.Text);
                if (oDoc.Status == Document.status.Downloaded)
                    oDoc.Status = Document.status.New;
                else
                    oDoc.Status = Document.status.Downloaded;
                oLI.SubItems[1].Text = oDoc.Status.ToString();
            }
        }

        private void cmdRefresh_Click(object sender, EventArgs e)
        {
            foreach (Feed oFeed in oFeeds)
            {
                RefreshFeed(oFeed);
            }
        }

        private void cmdAddReplace_Click(object sender, EventArgs e)
        {
            if (txtReplaces.Text.Split(';').Count() > 2)
            {
                MessageBox.Show("Too many ;s");
            }
            else
            {
                if (!txtReplaces.Text.Contains(';'))
                {
                    MessageBox.Show("separate two values with ;");
                }
                else
                {
                    Replace oRep = new Replace(txtReplaces.Text.Split(';')[0], txtReplaces.Text.Split(';')[1]);
                    oReps.Add(oRep);
                    ListViewItem oItem = new ListViewItem(new[] { oRep.ThisValue, oRep.WithThis });
                    oItem.Tag = oRep;
                    lswReplaces.Items.Add(oItem);
                }
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem oRPL in lswReplaces.SelectedItems)
            {
                oReps.Remove((Replace)oRPL.Tag);
                lswReplaces.Items.Remove(oRPL);
            }
        }
    }
}
