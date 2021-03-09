namespace KijijiAdNotify {
    public class SearchParameters {
        //TODO add location.alberta.whatever the fuck support by deserializing json library in the repo

        //Mandartory parameters
        public int LocationId { get; set; }

        public int CategoryId { get; set; }

        //Extra field for KijijiScraperInterface. Specifys output filename
        public string Output { get; set; } = "KijijiListings";

        //Some known parameters available when using either the "api" (default) or "html" scraperType
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
        public string AdType { get; set; }

        //Some known parameters available when using the "api" (default) scraperType
        // ReSharper disable once CommentTypo
        //TODO change Q to query this dumb fucking code reeeeeeeEEEEEEEEEEEEEEEEEEEE
        // ReSharper disable once CommentTypo
        //honestly tho what fag cares its Q intead of query? idk man
        public string Q { get; set; }
        public string SortType { get; set; }
        public int? Distance { get; set; }
        public string PriceType { get; set; }

        //Some known parameters available when using the "html" scraperType
        public string ScraperType { get; set; }
        public string Keywords { get; set; }
        public string SortByName { get; set; }

        //SearchParameters merged here because its simpler
        //and doesn't make sense to have seperated
        public int? PageDelayMs { get; set; }
        public int? MinResults { get; set; }
        public int? MaxResults { get; set; }
        public bool? ScrapeResultDetails { get; set; }
        public int? ResultDetailsDelayMs { get; set; }
    }
}
