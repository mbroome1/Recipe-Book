using AutoMapper;
using RecipeBook.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecipeBook.API.Profiles
{
    public class RecipeSearchProfile : Profile
    {
        public RecipeSearchProfile()
        {
            CreateMap<RecipeSearch, RecipeSearchDto>();
            CreateMap<Result, ResultDto>();
        }
    }
}
