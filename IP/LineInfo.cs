using Newtonsoft.Json;
using System;

namespace IP
{
    internal class LineInfo
    {
        public string Admin { get; set; }
        public string Server { get; set; }
        public int Timer { get; set; }
        public int LineId { get; set; }
        public string LineName { get; set; }

        // Пустой конструктор для десериализации
        public LineInfo() { }

        // Конструктор с параметрами
        public LineInfo(string admin, string server, int timer, int lineId, string lineName)
        {
            Admin = admin;
            Server = server;
            Timer = timer;
            LineId = lineId;
            LineName = lineName;
        }
    }
}