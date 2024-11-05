using Microsoft.EntityFrameworkCore;
using Models;
using System.Text.RegularExpressions;
namespace Validation;
/// <summary>
/// Validator class for all the data which is required form the user. 
/// </summary> 
public class EmpolyeeValidation : IBaseValidation
{
    private readonly string? _config;
    private readonly AppDbContext? _context;
    public EmpolyeeValidation(AppDbContext context, IConfiguration config)
    {
        _context = context;
        _config = config.GetConnectionString("DefaultConnection");
    }
    /// <summary>
    ///?                                                     IsValidName 
    /// method returns true if the user name is not empty otherwise returns false. 
    /// </summary>
    /// <param name="name"> this is the username which we are going to get from the User class  </param>
    /// <returns> true  if the user name is not null </returns>
    public bool IsValidName(string name)
    {
        if (string.IsNullOrEmpty(name))
        {
            throw new ArgumentException("You need to enter a valid name");
        }
        return true;
    }
    /// <summary>
    /// ~                                                       IsValidEmail
    /// using Regex to check if the email is valid 
    /// </summary>
    /// <param name="email"></param>
    /// <returns> true if the email match the emailRegex</returns>
    public bool IsValidEmail(string email)
    {
        if (string.IsNullOrEmpty(email))
        {
            throw new ArgumentException("You need to enter a valid email address");
        }
            string emailRegex = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return Regex.IsMatch(email, emailRegex);
    }
    /// <summary>
    ///!                                                IsPasswordValid
    /// the password need to bee more than 8 characters 
    /// </summary>
    /// <param name="password">this variable we get from user.password to check the user input </param>
    /// <returns> its return true if the password is 8 or more than 8 </returns>
    /// <exception cref="ArgumentException">Password must be at least 8 characters </exception> 
    /// </summary>
    public bool IsPasswordValid(string password)
    {
        if (password.Length < 8)
        {
            throw new ArgumentException("Password must be at least 8 characters");
        }
        return true;
    }
    /// <summary>
    ///^                                               IsTheNameUsed
    /// the think behind this method is to check if the user name is already in used by another user in this case the method will return false. 
    /// else is't should return true. But because of we have a lot to do in this curse so i chose to wait with it. its could be an improvement in the future.               
    /// </summary>
    /// <param name="name">user.Name</param>
    /// <returns> true if the username is not used by another user.</returns>
    public async Task<bool> IsTheNameUsed(string name)
    {
        var usedName = await _context?.Users.FirstOrDefaultAsync(e => e.Name == name)!;
        if (usedName != null)
        {
            throw new ArgumentException($"The name {name} is already used");
        }
        else
        {
            return true;
        }
    }
}
