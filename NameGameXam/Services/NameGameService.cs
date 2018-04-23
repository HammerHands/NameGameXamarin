using NameGameXam.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace NameGame.Services
{
    public class NameGameService
    {
        private const string DataUrl = "https://www.willowtreeapps.com/api/v1.0/profiles";

        public async Task<Profile[]> GetProfiles()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(DataUrl);

            //Get Response as JSON
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await client.GetAsync(client.BaseAddress);
            if (response.IsSuccessStatusCode)
            {
                // convert IEnumerable to Array
                var dataProfiles = response.Content.ReadAsAsync<IEnumerable<Profile>>().Result;
                return dataProfiles.ToArray();
            }
            return null;
        }
    }
}