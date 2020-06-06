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

namespace DSMPatchReportingGUI.Controller
{
    public class APIDataController : IDataController
    {
        private String _loginURLAppendix = "/api/Auth/login";

        public virtual JWT Login(string BaseURL, UserForLoginDto user)
        {
            HttpClientHandler handler = new HttpClientHandler();

            HttpClient client = new HttpClient(handler);
            client.BaseAddress = new Uri(BaseURL);

            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            var response = client.PostAsJsonAsync(_loginURLAppendix, user)
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
    }
}
