using Microsoft.Extensions.Configuration;
using RecipeBook.Core.Entities;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace RecipeBook.Data
{
    public class SpoonacularRecipeSearchData : IRecipeSearchData
    {
        private readonly IHttpClientFactory clientFactory;
        private readonly IConfiguration config;

        public SpoonacularRecipeSearchData(IHttpClientFactory clientFactory, IConfiguration config)
        {
            this.clientFactory = clientFactory;
            this.config = config;
        }


        public async Task<RecipeSearch> Get(string searchQuery)
        {
            var searchResult = new RecipeSearch();

            var key = config["ExternalServices:SpoonacularApiKey"];
            var url = $@"https://api.spoonacular.com/recipes/complexSearch?number=5&apiKey={key}&query={searchQuery}";

            var request = new HttpRequestMessage(HttpMethod.Get, url);
            var client = clientFactory.CreateClient();

            HttpResponseMessage response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                searchResult = await response.Content.ReadFromJsonAsync<RecipeSearch>();
                return searchResult;
            }
            else if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                throw new Exception("Connection to external api failed.");
            }
            else
            {
                throw new Exception("Something Went wrong.");
            }

        }
    }
}
