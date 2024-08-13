using Product.Domain;

namespace Infrastructure;

public static class Seed
{
    public static async Task SeedCategories(ApplicationDbContext context)
    {
        if (!context.Categories.Any())
        {
            context.Categories.Add(new Category()
            {
                Id = 1,
                Name = "Beer"
            });
            context.Categories.Add(new Category()
            {
                Id = 2,
                Name = "Drinks"
            });
            context.Categories.Add(new Category()
            {
                Id = 3,
                Name = "Food"
            });
            await context.SaveChangesAsync();
        }
    }
}