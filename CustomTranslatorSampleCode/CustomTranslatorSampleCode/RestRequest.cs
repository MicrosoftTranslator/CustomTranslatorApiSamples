using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Http;

namespace CustomTranslatorSampleCode
{
    public class RestRequest
    {
        Dictionary<string, string> headers = new Dictionary<string, string>();
        string authtoken = "";
        public void AddHeader(string name, string value)
        {
            headers.Add(name, value);
        }

        bool IsAuthTokenSet()
        {
            if (!headers.TryGetValue("Authorization", out authtoken))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public async Task<string> GetRestRequest(string uri, bool isfiledownload = false)
        {
            string result = "";

            if (!IsAuthTokenSet())
            {
                return "Subscription Key is must";
            }

            //Create HTTP Client 
            using (HttpClient client = new HttpClient())
            {
                //Add Authrization Header
                foreach (KeyValuePair<string, string> item in headers)
                {
                    client.DefaultRequestHeaders.Add(item.Key, item.Value);
                }

                try
                {
                    if (isfiledownload)
                    {
                        HttpResponseMessage response = await client.GetAsync(uri, HttpCompletionOption.ResponseHeadersRead);
                        Helper helper = new Helper();
                        string downloadpath = await helper.DownloadFile(response);
                        return "File downloaded at " + downloadpath;
                    }
                    else
                    {
                        HttpResponseMessage response = await client.GetAsync(uri);
                        response.EnsureSuccessStatusCode();
                        // NOTE: A successful response is returned in JSON. 
                        result = await response.Content.ReadAsStringAsync();
                    }
                }
                catch (Exception e)
                {
                    result = e.Message + ". " + e.InnerException;
                }
            }
            return result;
        }

        public async Task<string> PostRestRequest(string uri, HttpContent content)
        {
            string result = "";

            if (!IsAuthTokenSet())
            {
                return "Subscription Key is must";
            }

            //Create HTTP Client 
            using (HttpClient client = new HttpClient())
            {
                //Add Authrization Header
                foreach (KeyValuePair<string, string> item in headers)
                {
                    client.DefaultRequestHeaders.Add(item.Key, item.Value);
                }
                try
                {
                    HttpResponseMessage response = await client.PostAsync(uri, content);
                    result = await response.Content.ReadAsStringAsync();
                    response.EnsureSuccessStatusCode();
                    result = response.Headers.Location.ToString();
                }
                catch (Exception e)
                {
                    result += e.Message + ". " + e.InnerException;
                }
            }
            return result;
        }

        public async Task<string> PutRestRequest(string uri, HttpContent content)
        {
            string result = "";

            if (!IsAuthTokenSet())
            {
                return "Subscription Key is must";
            }

            //Create HTTP Client 
            using (HttpClient client = new HttpClient())
            {
                //Add Authrization Header
                foreach (KeyValuePair<string, string> item in headers)
                {
                    client.DefaultRequestHeaders.Add(item.Key, item.Value);
                }

                try
                {
                    HttpResponseMessage response = await client.PutAsync(uri, content);
                    response.EnsureSuccessStatusCode();
                    result = await response.Content.ReadAsStringAsync();
                }
                catch (Exception e)
                {
                    result = e.Message + ". " + e.InnerException;
                }
            }
            return result;
        }

        public async Task<string> DeleteRestRequest(string uri)
        {
            string result = "";

            if (!IsAuthTokenSet())
            {
                return "Subscription Key is must";
            }

            //Create HTTP Client 
            using (HttpClient client = new HttpClient())
            {
                //Add Authrization Header
                foreach (KeyValuePair<string, string> item in headers)
                {
                    client.DefaultRequestHeaders.Add(item.Key, item.Value);
                }

                try
                {
                    HttpResponseMessage response = await client.DeleteAsync(uri);
                    result = await GenerateTextResposeForHttpStatusCode(response);
                }
                catch (Exception e)
                {
                    result = e.Message + ". " + e.InnerException;
                }
            }
            return result;
        }

        async Task<string> GenerateTextResposeForHttpStatusCode(HttpResponseMessage response)
        {
            string result = "";

            if (response.StatusCode.ToString().ToUpper() == "OK")
            {
                result = "successful";
            }
            else if (response.StatusCode.ToString().ToUpper() == "NOTFOUND")
            {
                result = "not found";
            }
            else if (response.StatusCode.ToString().ToUpper() == "BADREQUEST")
            {
                result = "bad request" + "\r\n";
                result += await response.Content.ReadAsStringAsync();
            }
            else
            {
                result = "cannot perform operation";
            }
            return result;
        }
    }
}
