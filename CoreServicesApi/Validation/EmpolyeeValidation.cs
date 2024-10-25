using Microsoft.EntityFrameworkCore;
using Models;
using System.Text.RegularExpressions;
namespace Validation;

public class EmpolyeeValidation : IBaseValidation
{
    private readonly string? _config;
    private readonly AppDbContext? _context;
    public EmpolyeeValidation(AppDbContext context, IConfiguration config)
    {
        _context = context;
        _config = config.GetConnectionString("DefaultConnection");
    }
    public bool IsValidName(string name)
    {
        if (name == null)
        {
            throw new AggregateException("You Need to enter a valid employee name and email address");
        }
        return true;
    }

    public bool IsValidEmail(string email)
    {
        if (email == null)
        {
            throw new ArgumentException("You need to enter a valid email address");
        }
        else
        {
            string emailRegex = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return Regex.IsMatch(email, emailRegex);
        }
    }
    public bool IsPasswordValid(string password)
    {
        if (password.Length > 8)
        {
            throw new ArgumentException("Password must be at least 8 characters");
        }
        else
        {
            var regexItem = new Regex("^[a-zA-Z0-9 ]*$");
            if (regexItem.IsMatch(password))
            {
                return true;
            }
            return password.Length <= 8;
        }
    }
    public async Task<bool> IsTheNameUsed(string name)
    {
        var usedName = await _context?.Employees.FirstOrDefaultAsync(e => e.EmployeeName == name)!;
        if(usedName != null)
        {
            throw new ArgumentException($"The name {name} is already used");
        }
        else
        {
            return true;
        }
    }
}
