using System;
using System.Threading.Tasks;
using System.IO;
using System.Net.Http;

namespace CustomTranslatorSampleCode
{
    public class Helper
    {
        public async Task<string> DownlaodFile(HttpResponseMessage response)
        {
            string filedownloadpath = "";
            string downloadedfilename = "";
            System.Net.Http.HttpContent content = response.Content;
            filedownloadpath = Path.GetTempPath();
            downloadedfilename = content.Headers.ContentDisposition.FileName;
            try
            {
                using (Stream streamToReadFrom = await response.Content.ReadAsStreamAsync())
                {
                    using (Stream streamToWriteTo = File.Open(Path.Combine(filedownloadpath, downloadedfilename), FileMode.Create))
                    {
                        await streamToReadFrom.CopyToAsync(streamToWriteTo);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message + ". " + e.InnerException);
            }
            return filedownloadpath + downloadedfilename;
        }
    }
}
