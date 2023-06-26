using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LottoScraper.Model
{
    internal class LottoYear
    {
        public DateTime Year { get; set; }
        public LottoDay[] Days { get; set; }

    }
}
