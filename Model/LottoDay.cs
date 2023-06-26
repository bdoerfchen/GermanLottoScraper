using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LottoScraper.Model
{
    internal class LottoDay
    {
        public string? Date { get; set; }

        public DateTime GetDateTime()
        {
            if (Date is not null)
            {
                int year = int.Parse(Date[0..4]);
                int month = int.Parse(Date[5..7]);
                int day = int.Parse(Date[8..10]);

                return new DateTime(year, month, day);
            }
            else throw new InvalidOperationException($"{nameof(Date)} must not be null to be parsed");
        }

        public LottoGame? GameInfo { get; set; }
    }
}
