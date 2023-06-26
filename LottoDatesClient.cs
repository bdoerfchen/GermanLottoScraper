using LottoScraper.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace LottoScraper
{
    internal class LottoDatesClient
    {
        const string HISTORY_URI = "https://www.lotto.de/api/stats/entities.lotto/history/";
        const int START_YEAR = 1955;
        readonly int MAX_YEAR = DateTime.UtcNow.Year;

        HttpClient client = new HttpClient();


        public LottoYear[] GetAllYears()
        {
            int numberYears = MAX_YEAR + 1 - START_YEAR;
            LottoYear[] years = new LottoYear[numberYears];

            Parallel.For(START_YEAR, MAX_YEAR + 1, (year) =>
            {
                DateTime yearDate = new DateTime(year, 12, 31, 0, 0, 0, 0, DateTimeKind.Utc);
                long yearTicks = new DateTimeOffset(yearDate).ToUnixTimeMilliseconds();
                var httpTask = client.GetFromJsonAsync<LottoYear>(HISTORY_URI + yearTicks);
                httpTask.Wait();
                LottoYear? yearData = httpTask.Result;

                if (yearData is not null)
                {
                    yearData.Year = yearDate;
                    Array.Sort(yearData.Days, (day1, day2) => { return DateTime.Compare(day1.GetDateTime(), day2.GetDateTime()); });
                    years[year - START_YEAR] = yearData;
                }
            });

            return years;

        }
    }
}
