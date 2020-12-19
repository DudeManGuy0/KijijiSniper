using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace KijijiAdNotify {
    class KijijiFloater : KjijijScraperInterface {
        //I thought floater would represent how this bot
        //floats on kjiji scraping and filtering to notify
        //the user. probably gonna rename later.
        //TODO add filtering for price after getting listing



        public void alert(ScrapeArgs args, bool cancelRequested, Action<Listing> action) {
            List<Listing> listA = ScrapeKijiji(args);

            while (!cancelRequested) {
                Thread.Sleep(60000);

                List<Listing> listB = ScrapeKijiji(args);
                var listC = SubtractLists(listA,listB);

                foreach (Listing listing in listC) {
                    action.Invoke(listing);
                }

                listA = listB;
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

