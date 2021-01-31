const kijiji = require("kijiji-scraper");

const options = {
    minResults: 20000,
    maxResults: -1
};

const params = {
    q: "iphone",
    locationId: 1700199,  // Same as kijiji.locations.ONTARIO.OTTAWA_GATINEAU_AREA.OTTAWA
    categoryId: 0,  // Same as kijiji.categories.CARS_AND_VEHICLES
    sortByName: "priceAsc"  // Show the cheapest listings first
};

kijiji.search(params, options).then(ads => {
    for (let i = 0; i < ads.length; ++i) {
        console.log(ads[i].attributes.price)
        console.log(ads[i].title);
        console.log(ads[i].description)
        console.log("\n")
    }
}).catch(console.error);
