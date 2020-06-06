using DSMPatchReportingGUI.Interfaces;
using DSMPatchReportingGUI.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DSMPatchReportingGUI.Controller
{
    public class APIDataController : IDataController
    {
        private String _patchesURL = "/api/PatchData";
        private String _LastError = "";

        public virtual List<DateOpenClosedStatDto> GetDateOpenClosedStats(string BaseURL, JWT jwt, DataFilter filter = null)
        {
            HttpClientHandler handler = new HttpClientHandler();
            handler.PreAuthenticate = true;
            handler.UseDefaultCredentials = true;

            HttpClient client = new HttpClient(handler);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwt.token);
            client.BaseAddress = new Uri(BaseURL);

            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            string url = "/api/Status";
            if (filter != null)
                url = "/api/Status" + "/" + filter.ComputerName.ToLower();

            HttpResponseMessage response = client.GetAsync(url).Result;

            List<DateOpenClosedStatDto> entries = new List<DateOpenClosedStatDto>();

            if (response.IsSuccessStatusCode)
            {
                var posts = response.Content.ReadAsAsync<IEnumerable<DateOpenClosedStatDto>>().Result;
                entries = posts.ToList<DateOpenClosedStatDto>();
            }
            else
            {
                _LastError = response.ReasonPhrase;
                Console.WriteLine("Error Code" + response.StatusCode + " : Message - " + response.ReasonPhrase);
            }

            return entries;
        }

        public virtual List<String> GetComputers(string BaseURL, JWT jwt)
        {
            HttpClientHandler handler = new HttpClientHandler();
            handler.PreAuthenticate = true;
            handler.UseDefaultCredentials = true;

            HttpClient client = new HttpClient(handler);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwt.token);
            client.BaseAddress = new Uri(BaseURL);

            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = client.GetAsync("/api/Status/computers").Result;

            List<String> entries = new List<String>();

            if (response.IsSuccessStatusCode)
            {
                var posts = response.Content.ReadAsAsync<IEnumerable<String>>().Result;
                entries = posts.ToList<String>();
            }
            else
            {
                _LastError = response.ReasonPhrase;
                Console.WriteLine("Error Code" + response.StatusCode + " : Message - " + response.ReasonPhrase);
            }

            return entries;
        }

        public virtual List<PatchCountOfComplianceDto> GetMostSecurityIssues(string BaseURL, JWT jwt, DataFilter filter = null)
        {
            HttpClientHandler handler = new HttpClientHandler();
            handler.PreAuthenticate = true;
            handler.UseDefaultCredentials = true;

            HttpClient client = new HttpClient(handler);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwt.token);
            client.BaseAddress = new Uri(BaseURL);

            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            string url = "/api/Status/mergeddetails/notFixed";
            if (filter != null && filter.ComputerName != null) 
                url = "/api/Status/mergeddetails/notFixed" + "/" + filter.ComputerName.ToLower();

            HttpResponseMessage response = client.GetAsync(url).Result;

            List<PatchCountOfComplianceDto> entries = new List<PatchCountOfComplianceDto>();

            if (response.IsSuccessStatusCode)
            {
                var posts = response.Content.ReadAsAsync<IEnumerable<PatchCountOfComplianceDto>>().Result;
                entries = posts.ToList<PatchCountOfComplianceDto>();

            }
            else
            {
                _LastError = response.ReasonPhrase;
                Console.WriteLine("Error Code" + response.StatusCode + " : Message - " + response.ReasonPhrase);
            }

            return entries;
        }

        public virtual List<ComputerOpenClosedStatDto> GetFixedInPercent(string BaseURL, JWT jwt, DataFilter filter = null)
        {
            HttpClientHandler handler = new HttpClientHandler();
            handler.PreAuthenticate = true;
            handler.UseDefaultCredentials = true;

            HttpClient client = new HttpClient(handler);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwt.token);
            client.BaseAddress = new Uri(BaseURL);

            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            if (filter != null)
            {
                string url = "/api/Status/detailsPercent/" + filter.SpecificDate.Value.ToString("yyyy-MM-dd");
                if (filter.ComputerName != null)
                    url = "/api/Status/detailsPercent/" + filter.SpecificDate.Value.ToString("yyyy-MM-dd") + "/" + filter.ComputerName.ToLower();

                HttpResponseMessage response = client.GetAsync(url).Result;

                List<ComputerOpenClosedStatDto> entries = new List<ComputerOpenClosedStatDto>();

                if (response.IsSuccessStatusCode)
                {
                    var posts = response.Content.ReadAsAsync<IEnumerable<ComputerOpenClosedStatDto>>().Result;
                    entries = posts.ToList<ComputerOpenClosedStatDto>();

                }
                else
                {
                    _LastError = response.ReasonPhrase;
                    Console.WriteLine("Error Code" + response.StatusCode + " : Message - " + response.ReasonPhrase);
                }
                return entries;
            }
            MessageBox.Show("Datum ist notwendig");

            return new List<ComputerOpenClosedStatDto>();
        }

        public virtual List<PatchData> GetPatchDataByDate(string BaseURL, JWT jwt, DataFilter filter = null)
        {
            HttpClientHandler handler = new HttpClientHandler();
            handler.PreAuthenticate = true;
            handler.UseDefaultCredentials = true;

            HttpClient client = new HttpClient(handler);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwt.token);
            client.BaseAddress = new Uri(BaseURL);

            if (filter == null)
            {
                filter = new DataFilter();
            }

            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            string url = "/api/PatchData/date/" + DateTime.Now.ToString("yyyyMMdd");
            if (filter != null)
                url = "/api/PatchData/date/" + filter.SpecificDate.Value.ToString("yyyyMMdd");

            HttpResponseMessage response = client.GetAsync(url).Result;

            List<PatchData> entries = new List<PatchData>();

            if (response.IsSuccessStatusCode)
            {
                var posts = response.Content.ReadAsAsync<IEnumerable<PatchData>>().Result;
                entries = posts.ToList<PatchData>();

            }
            else
            {
                _LastError = response.ReasonPhrase;
                Console.WriteLine("Error Code" + response.StatusCode + " : Message - " + response.ReasonPhrase);
            }

            List<PatchData> returnEntries = new List<PatchData>();
            if (filter.ComputerName == "" || filter.ComputerName == null)
            {
                returnEntries = entries;
            }
            else
            {
                returnEntries = entries.Where(x => x.Computer == filter.ComputerName).ToList();
            }

            return returnEntries;
        }

        public virtual List<PatchData> GetLatestSecurityIssues(string BaseURL, JWT jwt, DataFilter filter = null)
        {
            HttpClientHandler handler = new HttpClientHandler();
            handler.PreAuthenticate = true;
            handler.UseDefaultCredentials = true;

            HttpClient client = new HttpClient(handler);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwt.token);
            client.BaseAddress = new Uri(BaseURL);

            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            string url = "/api/PatchData/latest" + "/1";
            if (filter != null)
                url = "/api/PatchData/latest" + "/" + filter.DataSetCount.ToString();

            HttpResponseMessage response = client.GetAsync(url).Result;

            List<PatchData> entries = new List<PatchData>();

            if (response.IsSuccessStatusCode)
            {
                var posts = response.Content.ReadAsAsync<IEnumerable<PatchData>>().Result;
                entries = posts.ToList<PatchData>();

            }
            else
            {
                _LastError = response.ReasonPhrase;
                Console.WriteLine("Error Code" + response.StatusCode + " : Message - " + response.ReasonPhrase);
            }

            return entries;
        }

        public virtual List<PatchData> GetPatches(string BaseURL, JWT jwt)
        {
            HttpClientHandler handler = new HttpClientHandler();
            handler.PreAuthenticate = true;
            handler.UseDefaultCredentials = true;

            HttpClient client = new HttpClient(handler);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwt.token);
            client.BaseAddress = new Uri(BaseURL);

            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = client.GetAsync("/api/PatchData").Result;

            List<PatchData> entries = new List<PatchData>();

            if (response.IsSuccessStatusCode)
            {
                var posts = response.Content.ReadAsAsync<IEnumerable<PatchData>>().Result;
                entries = posts.ToList<PatchData>();

            }
            else
            {
                _LastError = response.ReasonPhrase;
                Console.WriteLine("Error Code" + response.StatusCode + " : Message - " + response.ReasonPhrase);
            }

            return entries;
        }

        public virtual JWT Login(string BaseURL, UserForLoginDto user)
        {
            try
            {
                HttpClientHandler handler = new HttpClientHandler();

                HttpClient client = new HttpClient(handler);
                client.BaseAddress = new Uri(BaseURL);

                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));

                var response = client.PostAsJsonAsync("/api/Auth/login", user)
                        .ConfigureAwait(false)
                        .GetAwaiter()
                        .GetResult();

                if (!response.IsSuccessStatusCode)
                    return null;

                string result = response.Content.ReadAsStringAsync()
                        .ConfigureAwait(false)
                        .GetAwaiter()
                        .GetResult();

                var jwt = JsonConvert.DeserializeObject<JWT>(result);

                return jwt;
            }
            catch {
                return null;
            }
            
        }

        public List<String> GetUsers(string BaseURL, JWT jwt)
        {
            HttpClientHandler handler = new HttpClientHandler();
            handler.PreAuthenticate = true;
            handler.UseDefaultCredentials = true;

            HttpClient client = new HttpClient(handler);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwt.token);
            client.BaseAddress = new Uri(BaseURL);

            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = client.GetAsync("/api/Auth/users").Result;

            List<String> entries = new List<String>();

            if (response.IsSuccessStatusCode)
            {
                var users = response.Content.ReadAsAsync<IEnumerable<String>>().Result;
                entries = users.ToList<String>();
            }
            else
            {
                _LastError = response.ReasonPhrase;
                Console.WriteLine("Error Code" + response.StatusCode + " : Message - " + response.ReasonPhrase);
            }

            return entries;
        }

        public bool DeleteUser(string BaseURL, JWT jwt, string username)
        {
            try
            {
                HttpClientHandler handler = new HttpClientHandler();
                handler.PreAuthenticate = true;
                handler.UseDefaultCredentials = true;

                HttpClient client = new HttpClient(handler);
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwt.token);
                client.BaseAddress = new Uri(BaseURL);

                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));

                var response = client.DeleteAsync("/api/Auth/user" + "/" + username)
                        .ConfigureAwait(false)
                        .GetAwaiter()
                        .GetResult();

                var responseString = response.Content.ReadAsAsync<String>().Result;
                _LastError = response.ReasonPhrase + " -> " + responseString;

               return response.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }

        public bool RegisterUser(string BaseURL, JWT jwt, UserForRegisterDto user)
        {
            try
            {
                HttpClientHandler handler = new HttpClientHandler();
                handler.PreAuthenticate = true;
                handler.UseDefaultCredentials = true;

                HttpClient client = new HttpClient(handler);
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwt.token);
                client.BaseAddress = new Uri(BaseURL);

                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));

                var response = client.PostAsJsonAsync("/api/Auth/register", user)
                       .ConfigureAwait(false)
                       .GetAwaiter()
                       .GetResult();

                var responseString = response.Content.ReadAsAsync<String>().Result;
                _LastError = response.ReasonPhrase + " -> " + responseString;

                return response.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }

        public bool ChangePassword(string BaseURL, JWT jwt, UserForRegisterDto userForRegisterDto, string password)
        {
            try
            {
                HttpClientHandler handler = new HttpClientHandler();
                handler.PreAuthenticate = true;
                handler.UseDefaultCredentials = true;

                HttpClient client = new HttpClient(handler);
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwt.token);
                client.BaseAddress = new Uri(BaseURL);

                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));

                var response = client.PostAsJsonAsync("/api/Auth/change", userForRegisterDto)
                       .ConfigureAwait(false)
                       .GetAwaiter()
                       .GetResult();

                var responseString = response.Content.ReadAsAsync<String>().Result;
                _LastError = response.ReasonPhrase + " -> " + responseString;

                return response.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }

        public String GetLastError()
        {
            return _LastError;
        }
    }
}
