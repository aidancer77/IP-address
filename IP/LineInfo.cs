using System;

namespace IP
{
    public class LineInfo
    {
        public string Admin { get; set; }
        public string Server { get; set; }
        public int Timer { get; set; }
        public int LineId { get; set; }
        public string LineName { get; set; }

        public LineInfo()
        {
            Admin = "220832";
            Server = "192.168.77.74:8181";
            Timer = 5;
            LineId = 0;
            LineName = "";
        }
    }
}