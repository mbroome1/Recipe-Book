namespace RecipeBook.API.Models
{
    public class StepDto
    {
        public int number { get; set; }
        public string step { get; set; }
        public IngredientDto[] ingredients { get; set; }
        public LengthDto length { get; set; }
    }
}
