using Newtonsoft.Json;
using RecipeBook.Core.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace RecipeBook.Data
{
    public class JsonFileRecipeData : IRecipeData
    {
        public JsonFileRecipeData()
        {
            
        }
        public async Task<Recipe> Get(int id)
        {
            using(StreamReader r = new StreamReader("SearchById.json"))
            {
                string json = await r.ReadToEndAsync();
                r.Close();
                var result =  JsonConvert.DeserializeObject<Recipe>(json);

                return result;
            }
        }
    }
}
