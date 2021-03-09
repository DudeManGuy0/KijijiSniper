using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using NetCoreAudio;

namespace KijijiAdNotify {
    public class Notify {

        //public time
        //will have notification optionn implemented for the future

        public static string TimePassed(Listing listing) {
            //Returns string representation of time passed since current time from listing
            if (listing.Date == null) return "";
            TimeSpan msPassed = new TimeSpan(DateTime.Now.Ticks - listing.Date.Value.Ticks);
            if (msPassed.TotalDays >= 1)
                return GetString((int)msPassed.TotalDays, "day");

            if (msPassed.TotalHours >= 1)
                return GetString((int)msPassed.TotalHours, "hour");

            if (msPassed.TotalMinutes >= 1)
                return GetString((int)msPassed.TotalMinutes, "minute");

            if (msPassed.TotalSeconds >= 10)
                return GetString((int)msPassed.TotalSeconds, "second");

            return "Just now";

            static string GetString(int amount, string unit) => amount == 1 ? $"1 {unit} ago" : $"{amount} {unit}s ago";
        }

        public static void Notify1(Listing listing) {
            Console.WriteLine(listing.Title);
            Console.WriteLine(listing.Description);
            Console.WriteLine(TimePassed(listing));
            Console.WriteLine($"Scraped: {DateTime.Now}");
            Console.WriteLine($"Published: {listing.Date}");
            Console.WriteLine("$" + listing.Attributes.Price);
            Console.WriteLine(listing.Url);
            Console.WriteLine();
        }

        public static void Notify2(List<Listing> listings) {
            Console.Clear();
            foreach (Listing listing in listings) {
                //iPhone x
                //256gb mint condition.Always protected in case. Life proof case included.No charger or headphones. ...
                //27 minutes ago
                //Scraped: 2021-02-20 6:49:18 PM
                //Published: 2021-02-20 6:48:18 PM
                //$460
                //https://www.kijiji.ca/v-cell-phone/calgary/iphone-x/1552031439
                string output = $"{listing.Title}\n" +
                                $"{listing.Description}\n" +
                                $"{TimePassed(listing)}\n" +
                                $"Scraped: {DateTime.Now}\n" +
                                $"Published: {listing.Title}\n" +
                                $"${listing.Attributes.Price}\n" +
                                $"{listing.Url}\n";
                Console.Write(output);

                //if(listing == listings.Last())
                    File.AppendAllText("log.txt", output);

                Player player = new Player();
                player.Play("notification.mp3");
            }

        }
    }
}
