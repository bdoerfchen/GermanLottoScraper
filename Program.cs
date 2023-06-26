using LottoScraper.Model;
using System.Diagnostics;
using System.Text.Json;

namespace LottoScraper
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Start StopWatch
            Stopwatch stopwatch = Stopwatch.StartNew();

            //Get all available Lotto years
            LottoDatesClient datesClient = new LottoDatesClient();
            var years = datesClient.GetAllYears();
            long msDates = stopwatch.ElapsedMilliseconds;
            stopwatch.Restart();

            //Get all draws for each year
            LottoDrawsClient drawsClient = new LottoDrawsClient();
            int days = 0;
            int yearsDone = 0;
            foreach (var year in years)
            {
                Console.Write($"\rRequesting {year.Days.Length} games from year {year.Year.Year} ({yearsDone++}/{years.Length})");
                Parallel.ForEach(year.Days, day =>
                {
                    day.GameInfo = drawsClient.GetGameByDate(day.GetDateTime());
                    days++;
                });
            }
            Console.Write("\rRequested all games\r\n");
            stopwatch.Stop();
            var msDraws = stopwatch.ElapsedMilliseconds;

            //Printing times
            Console.WriteLine($"Requesting dates took {msDates} ms for {years.Length} years.");
            Console.WriteLine($"Requesting draws took {msDraws} ms for {days} days.");

            //Convert data to json and output
            string data = JsonSerializer.Serialize(years);
            string? path = args.Length > 0 ? args[0] : null;
            if (path is null)
            {
                Console.Write("File path: ");
                path = Console.ReadLine();
            }

            if (path is not null)
            {
                File.WriteAllText(path, data);
                Console.WriteLine($"{data.Length} Bytes were written to path '{path}'");
            }
        }
    }
}