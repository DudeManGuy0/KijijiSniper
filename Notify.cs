using System;

namespace KijijiAdNotify {
    public class Notify {

        //public time
        //will have notification optionn implemented for the future

        public static string TimePassed(Listing listing) {
            if (listing.Date == null) return "";
            TimeSpan msPassed = new TimeSpan(listing.Date.Value.Ticks - DateTime.Now.Ticks);

            if (msPassed.TotalDays >= 1)
                return GetString((int)msPassed.TotalDays, "day");

            if (msPassed.TotalHours >= 1)
                return GetString((int)msPassed.TotalHours, "hour");

            if (msPassed.TotalSeconds >= 10)
                return GetString((int)msPassed.TotalSeconds, "second");

            return "Just now";

            static string GetString(int amount, string unit) => amount == 1 ? $"1 {unit} ago" : $"{amount} {unit}s ago";
        }

        public static TimeSpan? PostedAgo(Listing listing) {
            if (!listing.Date.HasValue) return null;
            DateTime? listingDateTime = listing.Date;
            DateTime currentDateTime = DateTime.Now;
            TimeSpan timePassed = (TimeSpan)(currentDateTime - listingDateTime);
            return timePassed;
        }

        public static void Notify1(Listing listing) {
            if (listing.Date != null) {
                TimeSpan timePassed = new TimeSpan(listing.Date.Value.Ticks - DateTime.Now.Ticks);
                if (timePassed.Milliseconds < 300000) {
                    Console.WriteLine(listing.Title);
                    Console.WriteLine(listing.Description);
                    Console.WriteLine(Notify.TimePassed(listing));
                    Console.WriteLine("$" + listing.Attributes.Price);
                    Console.WriteLine(listing.Url);
                    Console.WriteLine();
                }
            }
        }
    }
}
