using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using System.IO.Compression;
namespace CustomWebClient
{
    public class WebClient : System.Net.WebClient
    {

        public string DownloadStringGzip(string sUrl)
        {

            try
            {
                byte[] b = DownloadData(sUrl);

                MemoryStream output = new MemoryStream();
                using (GZipStream g = new GZipStream(new MemoryStream(b), CompressionMode.Decompress))
                {
                    g.CopyTo(output);
                }

                return Encoding.UTF8.GetString(output.ToArray());
            }
            catch
            {
                //niente
            }
            return null;
        }
        public bool DoesFileExist(string sURL)
        {
            bool result = false;

            WebRequest webRequest = WebRequest.Create(sURL);
            webRequest.Timeout = 1200; // miliseconds
            webRequest.Method = "HEAD";

            HttpWebResponse response = null;

            try
            {
                response = (HttpWebResponse)webRequest.GetResponse();
                result = true;
            }
            catch (WebException webException)
            {
                //nada
            }
            finally
            {
                if (response != null)
                {
                    response.Close();
                }
            }

            return result;
        }

        public byte[] DownloadDataGzip(string sUrl)
        {

            try
            {
                byte[] b = DownloadData(sUrl);

                MemoryStream output = new MemoryStream();
                using (GZipStream g = new GZipStream(new MemoryStream(b), CompressionMode.Decompress))
                {
                    g.CopyTo(output);
                }

                return output.ToArray();
            }
            catch
            {
                //nothing
            }
            return null;
        }

        public WebClient()
        {
            this.Headers[HttpRequestHeader.UserAgent] = @"Mozilla/5.0 (X11; Linux x86_64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/51.0.2704.103 Safari/537.36";
            this.Headers[HttpRequestHeader.Upgrade] = "1";
            this.Headers[HttpRequestHeader.Accept] = @"text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8";
            this.Headers["DNT"] = "1";
            this.Headers[HttpRequestHeader.AcceptEncoding] = "gzip, deflate";
            this.Headers[HttpRequestHeader.AcceptLanguage] = "es,en;q=0.8";
            this.Headers[HttpRequestHeader.Cookie] = @"__cfduid=d2a1f390a6e96504bb2f605b4d12a5b921487257094; ninemanga_manga_juan_views_55830=1; ninemanga_country_code=US; PHPSESSID=083kpjkai09l42vro7a45v0bv7; ninemanga_history_list=182%23%23%23%3Cli%3E%3Cdl%3E%3Cdt%3E%3Ca%20href%3D%22%23%22%20class%3D%22close_cell%22%20cur%3D%22182%22%20onclick%3D%22return%20false%22%3E%3C/a%3E%3C/dt%3E%3Cdd%3E%3Ca%20href%3D%22http%3A//es.ninemanga.com/manga/Boku%2520no%2520Hero%2520Academia.html%22%3EBoku%20no%20Hero%20Academia%3C/a%3E%3C/dd%3E%3Cdd%3E%3Cspan%3EBoku%20no%20Hero%20Academia%20Cap%EDtulo%20159%20p%E1gina%204%3C/span%3E%3Ca%20href%3D%22http%3A//es.ninemanga.com/chapter/Boku%2520no%2520Hero%2520Academia/613590-4.html%22%3Ego%20on%3C/a%3E%3C/dd%3E%3C/dl%3E%3C/li%3E%7C%7C%7C49%23%23%23%3Cli%3E%3Cdl%3E%3Cdt%3E%3Ca%20href%3D%22%23%22%20class%3D%22close_cell%22%20cur%3D%2249%22%20onclick%3D%22return%20false%22%3E%3C/a%3E%3C/dt%3E%3Cdd%3E%3Ca%20href%3D%22http%3A//es.ninemanga.com/manga/Nanatsu%2520no%2520Taizai.html%22%3ENanatsu%20no%20Taizai%3C/a%3E%3C/dd%3E%3Cdd%3E%3Cspan%3ENanatsu%20no%20Taizai%20Cap%EDtulo%20238%20p%E1gina%201%3C/span%3E%3Ca%20href%3D%22http%3A//es.ninemanga.com/chapter/Nanatsu%2520no%2520Taizai/610047-1.html%22%3Ego%20on%3C/a%3E%3C/dd%3E%3C/dl%3E%3C/li%3E; pop_ads_count_key=5; __unam=3855076-15a476e3c1f-552cb48-715; Hm_lvt_234e86251ffdf39196872b04a27c6f72=1509926915; Hm_lpvt_234e86251ffdf39196872b04a27c6f72=1510430995; __utma=127180185.1879213979.1487257104.1510426108.1510430547.37; __utmc=127180185; __utmz=127180185.1487257104.1.1.utmcsr=(direct)|utmccn=(direct)|utmcmd=(none)";

            this.Encoding = Encoding.UTF8;
        }

    }
}
