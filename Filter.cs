using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualBasic.CompilerServices;

namespace KijijiAdNotify {
    public class Filter {
        public static bool iPhonePriceFilter1(Listing listing, bool isPlusModel) {
            string[] plusModelKeywords = {"plus","+"};
            bool listingIsPlusModel = plusModelKeywords.Any(keyword => listing.Title.Contains(keyword));

            string[] forRepairKeywords = {"for parts", "cracked", "repair"};
            bool listingIsForRepair = forRepairKeywords.Any(keyword => listing.Title.Contains(keyword) || listing.Description.Contains(keyword));
            return listing.Attributes.PhoneBrand != null &&
                   isPlusModel == listingIsPlusModel ||
                   listingIsForRepair;
        }
    }
}
  