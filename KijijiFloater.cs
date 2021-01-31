using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;

namespace KijijiAdNotify {
    class KijijiFloater : KjijijScraperInterface {
        //I thought floater would represent how this bot
        //floats on kjiji scraping and filtering to notify
        //the user. probably gonna rename later.
        //TODO add filtering for price after getting listing

        public void alert(SearchParameters parameters, bool cancelRequested, Action<Listing> notify, Func<Listing, decimal?> filter) {
            List<Listing> listA = ScrapeKijiji(parameters);

            while (!cancelRequested) {
                Thread.Sleep(60000);

                List<Listing> listB = ScrapeKijiji(parameters);
                List<Listing> listC = SubtractLists(listA, listB);

                foreach (Listing listing in listC) {
                    filter.Invoke(listing);
                }

                listA = listB;
            }

        }

        public void alert2(SearchParameters parameters, bool cancelRequested, Action<Listing> action) {
            while (!cancelRequested) {
                int chefBoyaredee = 0;
                List<Listing> list = ScrapeKijiji(parameters);

                foreach (Listing listing in list) {

                    if (Notify.PostedAgo(listing) < new TimeSpan(0, 0, 5, 0))
                        action.Invoke(listing);

                    if (Notify.PostedAgo(listing) == null) {
                        Console.WriteLine("el faggotee");
                        Console.WriteLine(listing.Url);
                    }
                }

                Thread.Sleep(60000);
            }
        }

        public void alert2(List<SearchParameters> listArgs, bool cancelRequested, Action<Listing> action) {
            while (!cancelRequested) {
                foreach (SearchParameters args in listArgs) {
                    List<Listing> list = ScrapeKijiji(args);

                    foreach (Listing listing in list.Where(listing =>
                        Notify.PostedAgo(listing) < new TimeSpan(0, 0, 1, 30))) {
                        action.Invoke(listing);

                    }
                    Thread.Sleep(60000/listArgs.Count);
                }

                
            }
        }

        private List<Listing> SubtractLists(List<Listing> listA, List<Listing> listB) {
            ///Takes in two lists, listB is the one we are scanning for new listings.
            ///Then it compares the two to find if listB has any new listings.
            var listC = new List<Listing>();
            //Scan for new listings using URL as an ID 
            foreach (Listing elemB in listB) {
                bool isMatch = false;
                foreach (Listing elemA in listA.Where(elemA => elemA.Url == elemB.Url)) {
                    isMatch = true;
                }

                if (!isMatch) listC.Add(elemB);
            }

            return listC;
        }
    }
}

