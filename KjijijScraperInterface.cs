using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json;

namespace KijijiAdNotify {
    public class KjijijScraperInterface {
        //provides code to interact with javascript that scrapes kijiji
        //might wanna use something related to this instead of running script
        //and parsing json output:
        //https://gist.github.com/elerch/5628117

        private static string ObjectFieldsToCla(object options) {
            //Made specifically to work with Minimist
            //TODO make it not make cla for empty fields
            //TODO implement protection against quotiation in object field
            if (options == null) return "";
            
            //i don't know how this works it just does
            IEnumerable<string> collection = options.GetType()
                .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(p => p.GetValue(options)!=null)
                .Select(p => $"--{char.ToLowerInvariant(p.Name[0])}{p.Name.Substring(1)}=\"{p.GetValue(options)}\"");

            return string.Join(" ", collection);
        }

        public  bool checkListingAvailability(Listing listing) {
            const string outputDir = "available";
            Process proc = new Process {
                StartInfo = { CreateNoWindow = true, FileName = "node.exe", Arguments = $" queryListingAvailability.js --url=\"{listing.Url}\" --output=\"{outputDir}\"" }
            };
            proc.Start();
            proc.WaitForExit();
            string result = File.ReadAllText(outputDir+".json");
            File.Delete($"{outputDir}.json");
            return result == "true";
        }

        public List<Listing> ScrapeKijiji(SearchParameters parameters) {
            string paramsCla = ObjectFieldsToCla(parameters);
            string strCmdText = "queryKijiji.js " + paramsCla;

            Process proc = new Process {
                StartInfo = {CreateNoWindow = true, FileName = "node.exe", Arguments = strCmdText}
            };
            proc.Start();
            proc.WaitForExit();

            string json = File.ReadAllText(parameters.Output +".json");
            File.Delete(parameters.Output+".json");
            
            //To avoid Json.Net converting timezones
            var jsonSerializerSettings = new JsonSerializerSettings {
                DateFormatHandling = DateFormatHandling.IsoDateFormat,
                DateTimeZoneHandling = DateTimeZoneHandling.Local,
                DateParseHandling = DateParseHandling.DateTimeOffset
            }; 
            return JsonConvert.DeserializeObject<List<Listing>>(json, jsonSerializerSettings);
        }   
    }
}
            


         
