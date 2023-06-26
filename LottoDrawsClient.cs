using LottoScraper.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace LottoScraper
{
    internal class LottoDrawsClient
    {

        const string DRAWS_URI = "https://www.lotto.de/api/stats/entities.lotto/draws/";

        private HttpClient httpClient = new HttpClient()
        {
            Timeout = new TimeSpan(0, 0, 10)
        };

        public LottoDrawsClient() 
        {
        }

        public LottoGame? GetGameByDate(DateTime date)
        {
            long timestamp = new DateTimeOffset(date, new TimeSpan(0)).ToUnixTimeMilliseconds();
            var httpTask = httpClient.GetFromJsonAsync<LottoGame[]>(DRAWS_URI + timestamp);
            httpTask.Wait();

            var gameData = httpTask.Result;
            if (gameData is null || gameData?.Length < 1)
                return null;
            else return gameData![0];
        }
    }
}
