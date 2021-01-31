//The job of this file is to be executed with query arguments passed via command line
//and to output a json file with the listings with its name specified in cmd line args

const kijiji = require("kijiji-scraper");
const fs = require('fs');
const args = require('minimist')(process.argv.slice(2));



//https://github.com/mwpenny/kijiji-scraper/blob/master/README.md for documentation. Retreived Dec. 16 2020
const options = {
    pageDelayMs: args.pageDelayMs,
    minResults: args.minResults,
    maxResults: args.maxResults,
    scrapeResultDetails: args.scrapeResultDetails,
    resultDetailsDelayMs: args.resultDetailsDelayMs
};

const params = {
    locationId: args.locationId,
    categoryId: args.categoryId,
    minPrice: args.minPrice,
    maxPrice: args.maxPrice,
    adType: args.adType,
    q: args.q,
    sortType: args.sortType,
    distance: args.distance,
    priceType: args.priceType,
    keywords: args.keywords,
    sortByName: args.sortByName
};

function clean(obj) {
    for (var propName in obj) {
      if (obj[propName] === null || obj[propName] === undefined) {
        delete obj[propName];
      }
    }
}

clean(params);
clean(options);

kijiji.search(params, options, writeAdsToFile);

function writeAdsToFile(err, ads) {
    if (!err) {
        var x = JSON.stringify(ads);
        var filename = args.output;
        fs.writeFile(filename + ".json", x, 'utf8', ()=>{});
    }
    if(err){
        fs.writeFile('halp',"i errored :(" + err.message, 'utf8', ()=>{});
    }
}