using AutoMapper;
using RecipeBook.API.Models;
using RecipeBook.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecipeBook.API.Profiles
{
    public class RecipeProfile : Profile
    {
        public RecipeProfile()
        {
            CreateMap<Recipe, RecipeDto>();
            CreateMap<Extendedingredient, ExtendedingredientDto>();
            CreateMap<Analyzedinstruction, AnalyzedinstructionDto>();
            CreateMap<Measures, MeasuresDto>();
            CreateMap<Us, UsDto>();
            CreateMap<Metric, MetricDto>();
            CreateMap<Step, StepDto>();
            CreateMap<Ingredient, IngredientDto>();
            CreateMap<Length, LengthDto>();

        }
    }
}
