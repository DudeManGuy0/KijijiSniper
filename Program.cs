using System;
using System.Threading;

namespace KijijiAdNotify {
    public class Program {
        static void Main(string[] args) {
            //TODO add user interface
            //TODO make use of logging events

            int appVerbosityLevel = 1;

            void Log(string txt, int messageVerbosityLevel) {
                if (appVerbosityLevel >= messageVerbosityLevel) {
                    Console.WriteLine(txt);
                }
            }

            var cts = new CancellationTokenSource();
            Console.CancelKeyPress += (_, e) => {
                e.Cancel = true; // keeps the app from terminating just by Ctrl+C
                cts.Cancel();
            };

            var parameters = new ScrapeParameters {
                q = "iphone",
                output = "listings.js",
                categoryId = 0,
                locationId = 1700199
            };

            KijijiFloater eh = new KijijiFloater();
            eh.alert(null, parameters, cts.IsCancellationRequested, notify);

            void notify(Listing listing) {
                Console.WriteLine(listing.Title);
                Console.WriteLine(listing.Description);
                Console.WriteLine(listing.Attributes.Price);
                Console.WriteLine(listing.Url);
                Console.WriteLine();
            }

        }
    }
}
