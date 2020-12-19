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
                Q = "iphone",
                Output = "listings",
                CategoryId = 0,
                LocationId = 1700199
            };

            var options = new ScrapeOptions {
                MaxResults = -1,
                MinResults = 80
            };

            var args_ = new ScrapeArgs(parameters, options);
            KijijiFloater eh = new KijijiFloater();
            eh.alert(args_, cts.IsCancellationRequested, Notify);

            static void Notify(Listing listing) {
                Console.WriteLine(listing.Title);
                Console.WriteLine(listing.Description);
                Console.WriteLine("$"+listing.Attributes.Price);
                Console.WriteLine(listing.Url);
                Console.WriteLine();
            }

        }
    }
}
