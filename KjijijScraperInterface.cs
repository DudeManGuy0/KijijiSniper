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

        private string ObjectFieldsToCla(object options) {
            //Made specifically to work with Minimist
            //TODO make it not make cla for empty fields
            
            if (options == null) {
                return "";
            }

            //i don't know how this works it just does
            IEnumerable<string> collection = options.GetType()
                .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(p => p.GetValue(options)!=null)
                .Select(p => $"--{char.ToLowerInvariant(p.Name[0]) + p.Name.Substring(1) +"="+ p.GetValue(options)}");

            return string.Join(" ", collection);
        }


        public List<Listing> ScrapeKijiji(ScrapeArgs args) {
            var options = args.ScrapeOptions;
            var parameters = args.ScrapeParameters;

            string paramsCla = ObjectFieldsToCla(parameters);
            string optionsCla = ObjectFieldsToCla(options);
            string cla = paramsCla + " " + optionsCla;

            string strCmdText = " queryKijiji.js " + cla;
            Process proc = new Process {
                StartInfo = {CreateNoWindow = true, FileName = "node.exe", Arguments = strCmdText}
            };
            proc.Start();
            proc.WaitForExit();

            string json = File.ReadAllText(parameters.Output +".json");
            File.Delete(parameters.Output+".json");
            return JsonConvert.DeserializeObject<List<Listing>>(json);
        }   
    }
}
            


         
