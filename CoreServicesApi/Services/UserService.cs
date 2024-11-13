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
        public User GetUserById(string userEmail)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM Users WHERE Email = @name";
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@name", userEmail);
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
            if (!validationEmployee.IsValidName(user.Name!))
            {
                throw new ArgumentException("Invalid Username: Must start with a letter, contain only letters and numbers, and be 6-12 characters long.");
            }
            if (!validationEmployee.IsValidEmail(user.Email!))
            {
                throw new ArgumentException("Invalid Email: You need to enter a valid email address.");
            }
            if (!validationEmployee.IsPasswordValid(user.Password!))
            {
                throw new ArgumentException("Invalid Password: Password must be at least 8 characters.");
            }
            user.Password = _hashAlgorithm!.HashPassword(user, user.Password!);
            _context!.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }
        public async Task<User> UpdateUser(User user)
        {
            if (!validationEmployee.IsValidName(user.Name!) || !validationEmployee.IsValidEmail(user.Email!) || !validationEmployee.IsPasswordValid(user.Password!))
            {
                throw new ArgumentException("Invalid user data");
            }
            var existingUser = await _context!.Users.FindAsync(user.Id);
            if (existingUser == null)
            {
                throw new ArgumentException("User not found");
            }
            existingUser.Name = user.Name;
            existingUser.Email = user.Email;
            existingUser.Password = user.Password;
            existingUser.Password = _hashAlgorithm!.HashPassword(user, user.Password!);
            _context.Users.Update(existingUser);
            await _context.SaveChangesAsync();
            return existingUser;
        }
        public async Task<string> DeleteUser(User user)
        {
            var existingUser = _context!.Users.Find(user.Name);
            if (existingUser == null)
            {
                throw new ArgumentException("Employee not found");
            }
            existingUser.Name = user.Name;
            existingUser.Email = user.Email;
            existingUser.Password = user.Password;
            _context!.Users.Remove(user);
            await _context.SaveChangesAsync();
            return "Employee deleted successfully";
        }
        public bool VerifyPassword(User user, string password)
        {
            return _hashAlgorithm!.VerifyHashedPassword(user, user.Password!, password) != PasswordVerificationResult.Failed;
        }
        public async Task<User?> SignInQuery(string email, string password)
        {
            var user = await _context!.Users.FirstOrDefaultAsync(u => u.Email == email);

            if (user == null) return null;

            var passwordHasher = new PasswordHasher<User>();
            var result = passwordHasher.VerifyHashedPassword(user, user.Password, password);

            return result == PasswordVerificationResult.Success ? user : null;
        }

    }
}