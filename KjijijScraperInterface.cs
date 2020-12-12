using System.Collections.Generic;
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
            
            if (options == null) {
                return "";
            }
            //i don't know how this works it just does
            IEnumerable<string> collection = options.GetType()
                .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(p => p.GetValue(options)!=null)
                .Select(p => $"--{p.Name} {p.GetValue(options)}");
            return string.Join(" ", collection);
        }


        public List<Listing> ScrapeKijiji(ScrapeParameters parameters, ScrapeOptions options = null) {
            string ParamsCLA = ObjectFieldsToCla(parameters);
            string OptionsCLA = ObjectFieldsToCla(options);
            string CLA = ParamsCLA + " " + OptionsCLA;

            string strCmdText = " queryKijiji.js " + CLA;
            var proc = new System.Diagnostics.Process();
            proc.StartInfo.CreateNoWindow = true;
            proc.StartInfo.FileName = "node.exe";
            proc.StartInfo.Arguments = strCmdText;
            proc.Start();
            proc.WaitForExit();

            string json = File.ReadAllText(parameters.output +".json");
            File.Delete(parameters.output+".json");
            return JsonConvert.DeserializeObject<List<Listing>>(json);
        }   
    }
}
            


         
