using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

namespace KijijiAdNotify {
    public class Program {
        static void Main() {
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

            var parameters = new SearchParameters {
                Q = "iphone x",
                CategoryId = 0,
                LocationId = 1700199
            };

            var parameters2 = new SearchParameters {
                Q = "iphone 7 plus",
                CategoryId = 0,
                LocationId = 1700199
            };
            KijijiFloater eh = new KijijiFloater();

            eh.alert(parameters, cts.IsCancellationRequested, Notify.Notify1);


        }
    }
}
