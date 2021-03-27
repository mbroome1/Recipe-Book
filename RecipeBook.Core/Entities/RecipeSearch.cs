using System;
using System.Collections.Generic;
using System.Text;

namespace RecipeBook.Core.Entities
{
    public class RecipeSearch
    {
        public Result[] results { get; set; }
        public int offset { get; set; }
        public int number { get; set; }
        public int totalResults { get; set; }
    }

    public class Result
    {
        public int id { get; set; }
        public string title { get; set; }
        public string image { get; set; }
        public string imageType { get; set; }
    }


}
