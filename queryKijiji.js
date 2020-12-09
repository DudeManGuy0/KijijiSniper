//The job of this file is to be executed with query arguments passed via command line
//and to output a json file with the listings with its name specified in cmd line args

const kijiji = require("kijiji-scraper");
const fs = require('fs');
const args = require('minimist')(process.argv.slice(2));

/*const options = {
    minResults: 20
};

const params = {
    locationId: 1700199,
    categoryId: 0, 
    sortByName: "priceAsc",
    q: "brother"
};*/

const options = {
    minResults: args.minResults
};

const params = {
    locationId: args.locationId || args.l,
    categoryId: args.categoryId || args.c,
    sortByName: args.sortByName || args.s,
    q: args.q
};

kijiji.search(params, options, writeAdsToFile);
function writeAdsToFile(err, ads) {
    if (!err) {
        var x = JSON.stringify(ads);
        var filename = args.output || args.o;
        fs.writeFile(filename + ".json", x, 'utf8', ()=>{});
    }
    if(err){
        fs.writeFile('halp',"i errored :(" + err.stringify, 'utf8', ()=>{});
    }
}