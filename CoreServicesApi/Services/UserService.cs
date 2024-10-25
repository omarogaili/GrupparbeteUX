using System.Collections.Generic;
using System.Configuration;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Models;
using Validation;
using MySqlConnector;
namespace Services
{
    public class UserService : IUseService
    {
        private readonly AppDbContext? _context;
        private readonly string? _connectionString;
        private readonly PasswordHasher<User>? _hashAlgorithm;
        public readonly EmpolyeeValidation validationEmployee;
        public UserService(AppDbContext context, IConfiguration connectionString)
        {
            _context = context;
            _connectionString = connectionString.GetConnectionString("DefaultConnection");
            _hashAlgorithm = new PasswordHasher<User>();
            validationEmployee = new EmpolyeeValidation(context, connectionString);
        }

        public User GetUserById(string userName)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM Users WHERE Name = @name";
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@email", userName);
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new User
                            {
                                Id = reader.GetInt32(0),
                                Name = reader.GetString(1),
                                Email = reader.GetString(2),
                            };
                        }
                    }
                }
            }
            return null!;
        }
        public async Task<User> AddUser(User user)
        {
            if (!validationEmployee.IsValidName(user.Name!) || !validationEmployee.IsValidEmail(user.Email!) || !validationEmployee.IsPasswordValid(user.Password!))
            {
                throw new ArgumentException("Invalid employee name");
            }
            await _context!.Users.AddAsync(user);
            _context.SaveChanges();
            return user;
        }
        public async Task<User> UpdateUser(User user)
        {
            if (!validationEmployee.IsValidName(user.Name!) || !validationEmployee.IsValidEmail(user.Name!) || !validationEmployee.IsPasswordValid(user.Password!))
            {
                throw new ArgumentException("Invalid employee name");
            }
            _context!.Users.Update(user);
            await _context.SaveChangesAsync();
            return user;
        }
        public async Task<string> DeleteUser(User user)
        {
            var users = _context!.Users.Find(user.Name);
            if (users == null)
            {
                throw new ArgumentException("Employee not found");
            }
            _context!.Users.Remove(user);
            await _context.SaveChangesAsync();
            return "Employee deleted successfully";
        }
        public bool VerifyPassword(User user, string password)
        {
            return _hashAlgorithm!.VerifyHashedPassword(user, user.Password!, password) != PasswordVerificationResult.Failed;
        }
        public int? SignInQuery(string userName, string password)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                string query = "SELECT Id, Password FROM Users WHERE Name = @name";
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@name", userName);
                    try
                    {
                        connection.Open();
                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                var storedPasswordHash = reader["Password"].ToString();
                                var userId = (int)reader["Id"];
                                if (VerifyPassword(new User { Password = storedPasswordHash! }, password))
                                {
                                    return userId;
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }
            Console.WriteLine("Login Failed");
            return null;
        }
    }
}