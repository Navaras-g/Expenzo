using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Expenzo.Model;
using Expenzo.Services.Interface;

namespace Expenzo.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly string categoriesFilePath = Path.Combine(AppContext.BaseDirectory, "Categories.json");

        public async Task<List<Category>> GetAllCategoriesAsync()
        {
            try
            {
                if (!File.Exists(categoriesFilePath))
                {
                    return new List<Category>();
                }

                var json = await File.ReadAllTextAsync(categoriesFilePath);
                return JsonSerializer.Deserialize<List<Category>>(json) ?? new List<Category>();
            }
            catch (JsonException jsonEx)
            {
                Console.WriteLine($"JSON deserialization error: {jsonEx.Message}");
                return new List<Category>();
            }
            catch (IOException ioEx)
            {
                Console.WriteLine($"I/O error while loading categories: {ioEx.Message}");
                return new List<Category>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error while loading categories: {ex.Message}");
                return new List<Category>();
            }
        }

        public async Task SaveCategoryAsync(Category category)
        {
            try
            {
                var categories = await GetAllCategoriesAsync();

                categories.Add(category);
                await WriteCategoriesToJson(categories);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving category: {ex.Message}");
                //throw;
            }
        }

        private async Task WriteCategoriesToJson(List<Category> categories)
        {
            try
            {
                var json = JsonSerializer.Serialize(categories, new JsonSerializerOptions { WriteIndented = true });

                await File.WriteAllTextAsync(categoriesFilePath, json);
            }
            catch (IOException ioEx)
            {
                Console.WriteLine($"I/O error while loading categories: {ioEx.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error while saving categories: {ex.Message}");
                //throw;
            }
        }
    }
}
