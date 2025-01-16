using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.IO;
using System.Threading.Tasks;
using Expenzo.Model;
using Expenzo.Services.Interface;

namespace Expenzo.Services
{
    public class UserService : IUserService
    {
        private readonly string usersFilePath = Path.Combine(AppContext.BaseDirectory, "Users.json");

        public async Task SaveUserAsync(User user)
        {
            try
            {
                var users = await GetAllUsersAsync();

                // User id
                int usersCount = users.Count();
                user.UserId = usersCount + 1;
                // Hash the user's password
                user.Password = HashPassword(user.Password);

                users.Add(user);
                await WriteUsersToJson(users);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving user: {ex.Message}");
                throw;
            }
        }

        public async Task<List<User>> GetAllUsersAsync()
        {
            try
            {
                if (!File.Exists(usersFilePath))
                {
                    return new List<User>();
                }

                var json = await File.ReadAllTextAsync(usersFilePath);
                return JsonSerializer.Deserialize<List<User>>(json) ?? new List<User>();
            }
            catch (JsonException jsonEx)
            {
                Console.WriteLine($"JSON deserialization error: {jsonEx.Message}");
                return new List<User>();
            }
            catch (IOException ioEx)
            {
                Console.WriteLine($"I/O error while loading users: {ioEx.Message}");
                return new List<User>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error while loading users: {ex.Message}");
                return new List<User>();
            }
        }

        //public async Task<User> GetUserAsync(string username)
        //{
        //    var existingUsers = await GetAllUsersAsync();

        //    foreach ()
        //    throw new NotImplementedException();
        //}

        private async Task WriteUsersToJson(List<User> users)
        {
            try
            {
                var json = JsonSerializer.Serialize(users, new JsonSerializerOptions { WriteIndented = true });

                await File.WriteAllTextAsync(usersFilePath, json);
            }
            catch (IOException ioEx)
            {
                Console.WriteLine($"I/O error while loading users: {ioEx.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error while saving users: {ex.Message}");
                throw;
            }
        }

        // Method to hash the password using SHA-256
        private string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var bytes = Encoding.UTF8.GetBytes(password);
                var hash = sha256.ComputeHash(bytes);
                return Convert.ToBase64String(hash);
            }
        }
    }
}
