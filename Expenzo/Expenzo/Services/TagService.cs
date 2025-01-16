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
    public class TagService : ITagService
    {

        private readonly string tagsFilePath = Path.Combine(AppContext.BaseDirectory, "Tags.json");

        public async Task<List<Tag>> GetAllTagsAsync()
        {
            try
            {
                if (!File.Exists(tagsFilePath))
                {
                    return new List<Tag>();
                }

                var json = await File.ReadAllTextAsync(tagsFilePath);
                return JsonSerializer.Deserialize<List<Tag>>(json) ?? new List<Tag>();
            }
            catch (JsonException jsonEx)
            {
                Console.WriteLine($"JSON deserialization error: {jsonEx.Message}");
                return new List<Tag>();
            }
            catch (IOException ioEx)
            {
                Console.WriteLine($"I/O error while loading categories: {ioEx.Message}");
                return new List<Tag>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error while loading categories: {ex.Message}");
                return new List<Tag>();
            }
        }

        public async Task SaveTagAsync(Tag tag)
        {
            try
            {
                var tags = await GetAllTagsAsync();

                tags.Add(tag);
                await WriteTagsToJson(tags);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving tag: {ex.Message}");
                //throw;
            }
        }

        private async Task WriteTagsToJson(List<Tag> tags)
        {
            try
            {
                var json = JsonSerializer.Serialize(tags, new JsonSerializerOptions { WriteIndented = true });

                await File.WriteAllTextAsync(tagsFilePath, json);
            }
            catch (IOException ioEx)
            {
                Console.WriteLine($"I/O error while loading tags: {ioEx.Message}");
                //throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error while saving tags: {ex.Message}");
                //throw;
            }
        }
    }
}
