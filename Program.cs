using System;
using Jint;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Text;
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

            var parameters = new ScrapeParameters();

            parameters.q = "iphone";
            parameters.output = "listings.js";
            parameters.categoryId = 0;
            parameters.locationId = 1700199;

            KjijijScraperInterface scraper = new KjijijScraperInterface();

            var cts = new CancellationTokenSource();
            Console.CancelKeyPress += (_, e) => {
                e.Cancel = true; // keeps the app from terminating just by Ctrl+C
                cts.Cancel();
            };

            KijijiFloater eh = new KijijiFloater();
            eh.alert(null, parameters, cts.IsCancellationRequested);

        }
    }
}
