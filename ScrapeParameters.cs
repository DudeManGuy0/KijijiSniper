namespace KijijiAdNotify {
    public class ScrapeParameters {
        //TODO add location.alberta.whatever the fuck support by deserializing json library in the repo

        //Mandartory parameters
        public int locationId { get; set; }
        public int categoryId { get; set; }
        //Extra field for KijijiScraperInterface. Specifys output filename
        public string output { get; set; } = "KijijiListings"; 

        //Some known parameters available when using either the "api" (default) or "html" scraperType
        public decimal minPrice { get; set; }
        public decimal maxPrice { get; set; }
        public string adType { get; set; }

        //Some known parameters available when using the "api" (default) scraperType
        public string q { get; set; }
        public string sortType { get; set; }
        public int distance { get; set; }
        public string priceType { get; set; }

        //Some known parameters available when using the "html" scraperType
        public string keywords { get; set; }
        public string sortByName { get; set; }

    }
    public class ScrapeArgs  {
        public ScrapeParameters parameters;
        public ScrapeOptions Options;
    }
}
