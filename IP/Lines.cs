using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IP
{
    internal class Lines
    {
        public string[] linesName = { "Боссар 1", "Боссар 2", "Боссар 3", "Боссар 4", "Боссар 5", "Боссар 6",
                "Боссар 7", "Боссар 8", "Боссар 9", "Боссар 10", "Боссар 11",
                "Боссар 12", "Боссар 13", "Волпак 1", "Волпак 2", "Волпак 3",
                "Волпак 4", "ЛМ1", "ЛМ2", "ЛМ4", "ЛК3", "Меспак", "Стеклобанка" };

        public static int GetLineIdFromName(string lineName)
        {
            if (string.IsNullOrEmpty(lineName))
                return 0;

            var lineMappings = new Dictionary<string, int>
    {
        { "Боссар 1", 1 },
        { "Боссар 2", 2 },
        { "Боссар 3", 3 },
        { "Боссар 5", 4 },
        { "Боссар 8", 5 },
        { "Боссар 9", 6 },
        { "Боссар 10", 7 },
        { "Боссар 11", 8 },
        { "Боссар 13", 9 },
        { "Волпак 3", 10 },
        { "Боссар 6", 11 },
        { "Боссар 7", 12 },
        { "Боссар 12", 13 },
        { "Меспак", 14 },
        { "Волпак 2", 15 },
        { "Волпак 1", 16 },
        { "Волпак 4", 17 },
        { "Боссар 4", 18 },
        { "Стеклобанка", 19 },
        { "ЛМ 1", 20 },
        { "ЛМ 3", 21 },
        { "ЛМ 4", 22 },
        { "ЛК 3", 23 }
    };

            return lineMappings.TryGetValue(lineName, out int lineId) ? lineId : 0;
        }
    }
}