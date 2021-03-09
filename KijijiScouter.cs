using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace KijijiAdNotify {
    public class KijijiScouter : KjijijScraperInterface {
        //I thought floater would represent how this bot
        //floats on kjiji scraping and filtering to notify
        //the user. probably gonna rename later.
        //TODO if a bunch of new listings get posted and the entire query if new, do another deeper query until nnew listings run out
        //TODO implement async ness sos you can run 2 thingy one time
        //TODO fix minresults high but only 20 results issue thing
        //TODO log results at end
        //TODO Implement scout function with list of search parameters to run and conclude
        //TODO test different scout methods
        //TODO bind List<SearchParameters> and Filter in MultiScout()

        public async Task RepostAd(Listing ad) {
            var handler = new HttpClientHandler {AutomaticDecompression = ~DecompressionMethods.All};

            using (var httpClient = new HttpClient(handler)) {
                using (var request = new HttpRequestMessage(new HttpMethod("POST"), "https://www.kijiji.ca/p-submit-ad.html")) {
                    request.Headers.TryAddWithoutValidation("authority", "www.kijiji.ca");
                    request.Headers.TryAddWithoutValidation("cache-control", "max-age=0");
                    request.Headers.TryAddWithoutValidation("upgrade-insecure-requests", "1");
                    request.Headers.TryAddWithoutValidation("origin", "https://www.kijiji.ca");
                    request.Headers.TryAddWithoutValidation("user-agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/88.0.4324.192 Safari/537.36");
                    request.Headers.TryAddWithoutValidation("accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/avif,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.9");
                    request.Headers.TryAddWithoutValidation("sec-gpc", "1");
                    request.Headers.TryAddWithoutValidation("sec-fetch-site", "same-origin");
                    request.Headers.TryAddWithoutValidation("sec-fetch-mode", "navigate");
                    request.Headers.TryAddWithoutValidation("sec-fetch-user", "?1");
                    request.Headers.TryAddWithoutValidation("sec-fetch-dest", "document");
                    request.Headers.TryAddWithoutValidation("referer", "https://www.kijiji.ca/p-admarkt-post-ad.html?categoryId=760&adTitle=Test+listing");
                    request.Headers.TryAddWithoutValidation("accept-language", "en-US,en;q=0.9");
                    request.Headers.TryAddWithoutValidation("cookie", "machId=5b64816ca52821ccca51733a8433d57345558fe14e9f583d15eaac929730c5d849c6c8ae15f9a97b7805074af308ded9774b9be49741c85b4db2dd9dfc1a5b04; GCLB=CJ2J-7mRn7bzbw; AMCVS_AE159CC25A134C820A495C97^%^40AdobeOrg=1; fab=0; ab=1; loginDetails=^%^7B^%^22sS^%^22^%^3A^%^22None^%^22^%^7D; siteLocale=en_CA; suid_1543500759=arlr2j6t; suid_1544069877=2pfpr3bwy; ssid=MTAxNzI2ODgyM3xleUowZVhBaU9pSktWMVFpTENKaGJHY2lPaUpJVXpVeE1pSjkuZXlKbGVIQWlPakUyTVRZeU9EazFOVElzSW1saGRDSTZNVFl4TVRFd05UVTFNaXdpYzNWaUlqb2lNVEF4TnpJMk9EZ3lNeUlzSW1WdFlXbHNJam9pYTJ0cGQyazROVFZBWjIxaGFXd3VZMjl0SW4wLnRCLVpuaWRZZHdNRXNVTjNKZzU4N3ZhVl9Ec1dNX1NadHJTYnZvVmc5X0hsREJNdF84Q25YTkpxMDE0V2Naa1dCOXY4T2I5eEc4RW0yejdMZ1BqZGlB; suid_1546726200=zu591vyl; suid_1545539150=32a7pqm9z; suid_1546952647=2ufe5u8hv; suid_1549717867=20oh0qxel; suid_1549430967=2s67qyu2v; ad_ids_v=38; suid_1549415258=2d0ow81il; suid_1549290005=26vrs9qp8; suid_1548318428=2m57bnbcl; suid_1550776335=14ubv4ies; suid_1550789978=1y84booc8; suid_1505808975=1fyohavc8; aam_uuid=53467244227215854714232985078754263229; _ga=GA1.2.1639009228.1613362779; _fbp=fb.1.1613362779319.1303931073; cguid=Vcguid=8026f8171770a6e5b2a3caddff6002b5^^doml=true^^brl=true; optimizelyEndUserId=oeu1613362780107r0.4854807038058757; suid_1551116638=sdx79pzw; suid_1505811478=gzr84mr9; suid_1505808973=1hzs1m9et; suid_1551120404=29mzfhu60; suid_1550110976=pwiasmsi; suid_1543566912=2t3fb0ds5; suid_1551234812=1gs03fnab; suid_1551254041=2mmx8qc2i; gmk=QUl6YVN5RFZaV2prS3p5cTJHY2FJcEdNc0xmTVZTanVzRUVVYTQw; suid_1543263550=87s3qb2r; suid_1550987115=2n77rv5ic; suid_1542025494=3gzf8nbue; kjses=0267c1ae-baf9-46b8-a6ed-2b4b8fd02642^^aL6umDS5mGvA/lfakrqrMw==; suid_1550481005=b7d4yiuv; suid_1552499266=2x9f7uwiy; suid_1552344406=1fte0xtnf; suid_1253197939=27bjr18vf; suid_1552177141=2bxtzwj43; suid_1549507470=114aw5dyp; suid_1552641339=38pwutaid; suid_1552595582=39uordav2; suid_1552738531=1wcbr7lhz; suid_1552693691=1lep824ua; suid_1331691394=bederem8; suid_1552481039=3hu30x6jw; suid_1551378281=2gmsscd01; suid_1505808978=2u4du0fpx; suid_1553098375=2798z8dwy; suid_1553102671=2c8cfzu0z; suid_1548803010=hsolbrd8; suid_1553072857=1mglwge7h; suid_1550216494=2x5ceqmhk; suid_1550986774=33ivsvofz; suid_1548067287=27fewj8c9; ad_ids_s=Vsaved_s=1548606277^#1614311666999&1548596594^#1614311666999&1537152847^#1614311666999&1548077423^#1614311666999&1530963758^#1614311666999; suid_1553231301=33ji9ibk9; suid_1553198471=28lvvi89q; suid_1553153933=1zw6nfsm1; suid_1553178634=yazkg220; suid_1552793682=vogt3vgi; suid_1553418714=1ncygb4f; suid_1550214888=30hpvig8e; suid_1553460885=2q7dwm8ot; kjrva=1553460885^%^2C1550214888^%^2C1553418714^%^2C1546101289^%^2C1552793682^%^2C1553178634^%^2C1553153933^%^2C1553198471^%^2C1553136484^%^2C1548067287; up=^%^7B^%^22ln^%^22^%^3A^%^22264817672^%^22^%^2C^%^22ls^%^22^%^3A^%^22l^%^3D1700199^%^26sv^%^3DLIST^%^26sf^%^3DdateDesc^%^22^%^2C^%^22rva^%^22^%^3A^%^221548803010^%^2C1553072857^%^2C1550216494^%^2C1550986774^%^2C1548067287^%^2C1553134559^%^2C1553135336^%^2C1553160180^%^2C1553136484^%^2C1553233359^%^2C1553231301^%^2C1553198471^%^2C1553153933^%^2C1553242698^%^2C1553178634^%^2C1552793682^%^2C1546101289^%^2C1553418714^%^2C1550214888^%^2C1553460885^%^22^%^7D; fcmk=QUl6YVN5RGR1cFl2OHN4TkpoV1BCaklJczgyOXVkdDNMYmdsLXhv; algoliaToken=eyJhcGlLZXkiOiJORE14WlRoalpqUTRaVE16WVRNeFpEa3hOalZpWVdNMVpEazROMkkxWkdWbFl6UTBZV0kzWkdFME5EQmxaV1psTXpFMlkyWmtNR0U0T0RnNE1HRXpaSFpoYkdsa1ZXNTBhV3c5TVRZeE5UQXdOVFU1TnpFNU1RPT0iLCJhcHBJZCI6IjNJRDc4WU1PV1UiLCJpbmRleCI6WyJrY2FfcHJvZF9zdWdnZXN0Iiwia2NhX3Byb2RfbXZfY3AiXSwidmFsaWRVbnRpbCI6MTYxNTAwNTU5NzE5MX0=; JSESSIONID=948CBD7E294960FADFE2E509412C6BA8; AMCV_AE159CC25A134C820A495C97^%^40AdobeOrg=-432600572^%^7CMCIDTS^%^7C18692^%^7CMCMID^%^7C53969295449629789524208696156433200394^%^7CMCOPTOUT-1614998289s^%^7CNONE^%^7CvVersion^%^7C4.5.2^%^7CMCAAMLH-1613985887^%^7C9^%^7CMCAAMB-1614919195^%^7Cj8Odv6LonN4r3an7LhD3WZrU1bUpAkFkkiY1ncBR96t2PTI");

                    request.Content = new StringContent("ca.kijiji.xsrf.token=1614991087087.50733126b7b9a64d2557686638bd8a67ae0692857427465993f8e7954b33b1a7&images=&postAdForm.fraudToken=9e5dd274e56bf8507ad4151790dea92d&uuid=&adId=&postAdForm.galleryImageIndex=&postAdForm.geocodeLat=51.1715&postAdForm.geocodeLng=-114.15918&postAdForm.city=Calgary&postAdForm.province=AB&PostalLat=&PostalLng=&categoryId=760&postAdForm.locationId=1700199&postAdForm.mapAddress=Calgary^%^2C+AB+T3R+0S5&postAdForm.postalCode=T3R+0S5&postAdForm.showLocationOnMap=false&postAdForm.saveLocation=true&postAdForm.mapRadius=1810.220141836028&postAdForm.adType=OFFER&postAdForm.attributeMap^%^5Bphonebrand_s^%^5D=apple&postAdForm.attributeMap^%^5Bfulfillment_s^%^5D=&postAdForm.attributeMap^%^5Bpayment_s^%^5D=&postAdForm.attributeMap^%^5Bphonecarrier_s^%^5D=unlck&postAdForm.attributeMap^%^5Bforsaleby_s^%^5D=ownr&postAdForm.title=Test+listing&postAdForm.description=Test+lissting+desc&postAdForm.tagsInput=&postAdForm.tags=testlisintg1&file=&postAdForm.youtubeVideoURL=&postAdForm.priceType=FIXED&postAdForm.priceAmount=12345&postAdForm.phoneNumber=&featuresForm.topAdDuration=7&submitType=saveAndCheckout");
                    request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/x-www-form-urlencoded");

                    var response = await httpClient.SendAsync(request);
                }
            }
        }

        public void Scout(SearchParameters parameters,TimeSpan ScoutInterval, Action<Listing> notify, Func<Listing, bool> filter) {
            var timeStarted = DateTime.Now;
            List<Listing> alreadyScrapedListings = ScrapeKijiji(parameters);
            while (true) {
                Thread.Sleep(ScoutInterval);
                var latestScrape = ScrapeKijiji(parameters);
                //For each new listing, if its been posted after we started looking for new listings and we haven't
                //already picked it up, add it to found listings and if it passes the filter, call notify
                foreach (Listing newListing in latestScrape) {
                    if (newListing.Date >= timeStarted && alreadyScrapedListings.All(oldListing => oldListing.Url != newListing.Url)) {
                        alreadyScrapedListings.Add(newListing);
                        if(filter(newListing)){
                            notify.Invoke(newListing);
                        }
                    }
                }
            }
        }
        public void Scout(SearchParameters parameters, TimeSpan ScoutInterval, Action<List<Listing>> notify, Func<Listing, bool> filter) {
            var timeStarted = DateTime.Now;
            List<Listing> alreadyScrapedListings = ScrapeKijiji(parameters);
            while (true) {
                Thread.Sleep(ScoutInterval);
                var latestScrape = ScrapeKijiji(parameters);
                //For each new listing, if its been posted after we started looking for new listings and we haven't
                //already picked it up, add it to found listings and if it passes the filter, call notify
                List<Listing> passedListings = new List<Listing>();
                foreach (Listing newListing in latestScrape) {
                    if (newListing.Date >= timeStarted && alreadyScrapedListings.All(oldListing => oldListing.Url != newListing.Url)) {
                        alreadyScrapedListings.Add(newListing);
                        if (filter(newListing)) {
                            passedListings.Add(newListing);
                        }
                    }
                }

                notify.Invoke(passedListings);
            }
        }

        public void ScoutMultiple(List<SearchParameters> listParameters, TimeSpan scoutInterval, Action<Listing> notify, Func<Listing, bool> filter) {
            TimeSpan timeWait = scoutInterval / listParameters.Count;
            var timeStarted = DateTime.Now;
            var link = new Dictionary<SearchParameters, List<Listing>>();
            //Initialization phase
            foreach (SearchParameters parameters in listParameters) {
                //Wait to avoid spamming requests/causing lag spike
                Thread.Sleep(500);
                link.Add(parameters, ScrapeKijiji(parameters));
            }

            int i = 0;
            while (true) {
                Thread.Sleep(timeWait);
                var searchParameters = link.ElementAt(i).Key;
                var alreadyScrapedListings = link.ElementAt(i).Value;
                var latestScrape = ScrapeKijiji(searchParameters);

                 foreach (Listing newListing in latestScrape) {
                    if (newListing.Date >= timeStarted && alreadyScrapedListings.All(oldListing => oldListing.Url != newListing.Url)) {
                        alreadyScrapedListings.Add(newListing);
                        if (filter(newListing)) {
                            notify.Invoke(newListing);
                        }
                    }
                }

                link[searchParameters] = alreadyScrapedListings;

                i++;
                if (i == link.Count) i = 0;
            }

        }
        public void ScoutMultiple(List<SearchParameters> listParameters, TimeSpan scoutInterval, Action<List<Listing>> notify, Func<Listing, bool> filter) {
            TimeSpan timeWait = scoutInterval / listParameters.Count;
            var timeStarted = DateTime.Now;
            var link = new Dictionary<SearchParameters, List<Listing>>();
            //Initialization phase
            foreach (SearchParameters parameters in listParameters) {
                //Wait to avoid spamming requests/causing lag spike
                Thread.Sleep(500);
                link.Add(parameters, ScrapeKijiji(parameters));
            }

            int i = 0;
            var newListings = new List<Listing>();
            while (true) {
                Thread.Sleep(timeWait);
                var searchParameters = link.ElementAt(i).Key;
                var alreadyScrapedListings = link.ElementAt(i).Value;
                var latestScrape = ScrapeKijiji(searchParameters);

                foreach (Listing newListing in latestScrape) {
                    if (newListing.Date >= timeStarted && alreadyScrapedListings.All(oldListing => oldListing.Url != newListing.Url)) {
                        alreadyScrapedListings.Add(newListing);
                        if (filter(newListing)) {
                            newListings.Add(newListing);
                        }
                    }
                }
                notify.Invoke(newListings);
                link[searchParameters] = alreadyScrapedListings;

                i++;
                if (i == link.Count) i = 0;
            }

        }

        //public void ScoutMultiple(KeyValuePair<, TimeSpan scoutInterval, Action<List<Listing>> notify, Func<Listing, bool> filter) {

        //}
    }
}

