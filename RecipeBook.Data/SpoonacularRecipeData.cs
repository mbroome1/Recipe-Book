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
    public class SpoonacularRecipeData : IRecipeData
    {
        private readonly IHttpClientFactory clientFactory;
        private readonly IConfiguration config;

        public SpoonacularRecipeData(IHttpClientFactory clientFactory, IConfiguration config)
        {
            this.clientFactory = clientFactory;
            this.config = config;
        }

        public async Task<Recipe> Get(int id)
        {
            var searchResult = new Recipe();

            var key = config["ExternalServices:SpoonacularApiKey"];
            var url = $@"https://api.spoonacular.com/recipes/{id}/information?apiKey={key}";

            var request = new HttpRequestMessage(HttpMethod.Get, url);
            var client = clientFactory.CreateClient();

            HttpResponseMessage response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                searchResult = await response.Content.ReadFromJsonAsync<Recipe>();
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
