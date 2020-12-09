using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace KijijiAdNotify {
    public class Listing {
        [JsonProperty("title")]
        public string Title { get; set; }
        [JsonProperty("description")]
        public string Description { get; set; }
        [JsonProperty("date")]
        public DateTime? Date { get; set; }
        [JsonProperty("image")]
        public string Image { get; set; }
        [JsonProperty("images")]
        public string[] Images { get; set; }
        [JsonProperty("attributes")]
        public Attributes Attributes { get; set; }
        [JsonProperty("url")]
        public string Url { get; set; }
        


        [JsonProperty("locationId")]
        public int LocationId { get; set; }
        [JsonProperty("categoryId")]
        public int CategoryId { get; set; }
        [JsonProperty("sortByName")]
        public string SortByName { get; set; }
        [JsonProperty("q")]
        public string Q { get; set; }

    }
    public class Attributes {
        [JsonProperty("location")]
        public string Location { get; set; }
        [JsonProperty("type")]
        public string Type { get; set; }
        [JsonProperty("forsaleby")]
        public string Forsaleby { get; set; }
        [JsonProperty("fulfillment")]
        public string Fulfillment { get; set; }
        [JsonProperty("payment")]
        public string Payment { get; set; }
        [JsonProperty("price")]
        public decimal Price { get; set; }
        [JsonProperty("petofferedby")]
        public string Petofferedby { get; set; }
        [JsonProperty("moreinfo")]
        public string Moreinfo { get; set; }
        [JsonProperty("animalwas")]
        public string Animalwas { get; set; }
    }
}
