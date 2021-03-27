
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

        public async Task<RecipeSearch> Get(string searchQuery)
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

            //Create a new result object array from the search query string
            var filteredResults = search.results
                    .Where(t => t.title.ToLower().Contains(searchQuery.Trim().ToLower()))
                    .ToArray();

            //Assign values needed for a new return object
            var filteredResultsCount = filteredResults.Count();
            var offset = search.offset;
            var number = search.number;

            //Build the new object to return back
            var filteredSearch = new RecipeSearch()
            {
                results = filteredResults,
                totalResults = filteredResultsCount,
                number = filteredResultsCount,
                offset = offset
            };

            return filteredSearch;
        }
    }
}
