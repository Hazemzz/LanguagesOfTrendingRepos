using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Trending_Repos__backend_coding_challenge_.Models;

namespace Trending_Repos__backend_coding_challenge_.Controllers
{
    [Route("api/TrendingRepos")]
    [ApiController]
    public class TrendingReposLanguagesController : ControllerBase
    {
        [HttpGet("LanguagesOfnTrendingRepos")]
        public async Task<JsonResult> GetLanguageOfTrendingRepos()
        {
            var date = DateTime.Now.AddDays(-30).ToString("yyyy-MM-ddThh:mm:ssZ");

            string trendingReposLanguagesUrl = @"https://api.github.com/search/repositories?q=created:>" + (date) + "&sort=stars&order=desc&per_page=100";
            HttpWebRequest request = WebRequest.CreateHttp(trendingReposLanguagesUrl);
            request.UseDefaultCredentials = true;
            request.Proxy.Credentials = CredentialCache.DefaultCredentials;

            //Required for making response with out facing *(403) Forbidden.* error 
            request.Accept = "application/json";
            request.UserAgent = "request";

            WebResponse response = await request.GetResponseAsync();
            Stream data = response.GetResponseStream();
            StreamReader reader = new StreamReader(data ?? throw new InvalidOperationException());
            var readerResult = await reader.ReadToEndAsync();
            Root jObj = JsonConvert.DeserializeObject<Root>(readerResult);

            var result = jObj.Items
                .GroupBy(x => x.Language)
                .Select(group => new
                {
                    Language = group.Key,
                    Repos = group.Select(x => new { x.Id, x.Name, x.Full_Name }).ToList(),
                    total_count = group.Count()
                })
                .ToArray();

            return new JsonResult(result);
        }
    }
}
