
using Newtonsoft.Json;
using RecipeBook.Core.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace RecipeBook.Data
{
    public class JsonFileRecipeSearchData : IRecipeSearchData
    {
        private readonly IHttpClientFactory clientFactory;

        public JsonFileRecipeSearchData(IHttpClientFactory clientFactory)
        {
            this.clientFactory = clientFactory;
        }
        public async Task<RecipeSearch> GetAll()
        {
            //List<RecipeSearch> searchList = new List<RecipeSearch>();

            using (StreamReader r = new StreamReader("search.json"))
            {
                string json = await r.ReadToEndAsync();
                r.Close();
                var result = JsonConvert.DeserializeObject<RecipeSearch>(json);
                //searchList.Add(result);

                return result;
            }    
        }

        public async Task<RecipeSearch> Get(string searchQuery, int? numberOfRecords, int? offsetRecords)
        {
            var search = new RecipeSearch();
            
            using (StreamReader r = new StreamReader("search.json"))
            {
                //Load list of recipes
                string json = await r.ReadToEndAsync();
                r.Close();

                var result = JsonConvert.DeserializeObject<RecipeSearch>(json);
                search = result;
            }

            
            var searchResults = search.results
                    .Where(t => t.title.ToLower().Contains(searchQuery.Trim().ToLower()));

            //Create a new result object array from the search query string
            var filteredResults = searchResults
                .Skip(Convert.ToInt32(offsetRecords))
                .Take(Convert.ToInt32(numberOfRecords))
                    .ToArray();

            //Assign values needed for a new return object
            var filteredResultsCount = searchResults.Count();
            var filteredResultsOffset = Convert.ToInt32(offsetRecords);
            var filteredResultsNumber = Convert.ToInt32(numberOfRecords);

            //Build the new object to return back
            var filteredSearch = new RecipeSearch()
            {
                results = filteredResults,
                totalResults = filteredResultsCount,
                number = filteredResultsNumber,
                offset = filteredResultsOffset
            };

            return filteredSearch;
        }
    }
}
