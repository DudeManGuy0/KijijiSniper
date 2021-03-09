using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading;
using Newtonsoft.Json;

namespace KijijiAdNotify {
    public class Program {
        static void Main() {
            //TODO add user interface
            //TODO make use of logging events


            var iPhone8 = new SearchParameters {
                Q = "iphone 8",
                CategoryId = 760,
                LocationId = 1700199,
                SortType = "DATE_DESCENDING",
                MinResults = 100
            };

            var iPhone7 = new SearchParameters {
                Q = "iphone 7",
                CategoryId = 760,
                LocationId = 1700199,
                SortType = "DATE_DESCENDING",
                MinResults = 100
            };

            var iPhoneX = new SearchParameters {
                Q = "iphone X",
                CategoryId = 760,
                LocationId = 1700199,
                SortType = "DATE_DESCENDING",
                MinResults = 100
            };

            List<SearchParameters> searchParameters = new List<SearchParameters>{iPhoneX,iPhone7,iPhone8};
            TimeSpan timeSpan = new TimeSpan(0,0,5,0);
            KijijiScouter eh = new KijijiScouter();
            eh.ScoutMultiple(searchParameters,timeSpan,Notify.Notify1, (listing) => true);

        }
    }
}
