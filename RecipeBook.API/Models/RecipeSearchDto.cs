using System;
using System.Collections.Generic;
using System.Text;

namespace RecipeBook.Core.Entities
{
    public class RecipeSearchDto
    {
        public ResultDto[] results { get; set; }
        public int offset { get; set; }
        public int number { get; set; }
        public int totalResults { get; set; }
    }
}
