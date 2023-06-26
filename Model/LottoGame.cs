using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace LottoScraper.Model
{
    internal class LottoGame
    {
        public int Id { get; set; }
        public long DrawDate { get; set; }
        public float? GameAmount { get; set; }
        public int? SuperNumber { get; set; }
        public Currency? Currency { get; set; }
        public GameType? GameType { get; set; }
        public NumberDraw[]? DrawNumbersCollection { get; set; }

    }

    public class Currency
    {
        public int Id { get; set; }
        public string? Name { get; set; }

        public override string ToString() => Name ?? string.Empty;
    }


    public class GameType
    {
        public int Id { get; set; }
        public string? Name { get; set; }

        public override string ToString() => Name ?? string.Empty;
    }


    public class NumberDraw
    {
        public int Id { get; set; }
        public int Index { get; set; }
        public int DrawNumber { get; set; }

        public override string ToString() => DrawNumber.ToString();
    }
}
