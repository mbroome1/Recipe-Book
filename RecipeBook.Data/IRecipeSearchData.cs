using RecipeBook.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RecipeBook.Data
{
    public interface IRecipeSearchData
    {
        //Task<RecipeSearch> GetAll();
        Task<RecipeSearch> Get(string searchQuery, int? numberOfRecords, int? offset);
    }
}
