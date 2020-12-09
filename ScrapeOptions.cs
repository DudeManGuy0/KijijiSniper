
using System;
using System.Collections.Generic;
using System.Text;

namespace KijijiAdNotify {
    public class ScrapeOptions {
        public int pageDelayMs { get; set; }
        public int minResults { get; set; }
        public int maxResults { get; set; }
        public bool scrapeResultDetails { get; set; }
        public int resultDetailsDelayMs { get; set; }

    }
}
