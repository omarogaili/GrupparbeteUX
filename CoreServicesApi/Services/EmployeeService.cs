using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Models;
using Validation;
namespace Services;

/// <summary>
/// this class contains all the logic for saving and adding all the employees for the system we are working on. we are using PasswordHaser to hash the password. 
/// and EmployeeValidation class to validate the inputs like the name of the employee, the password and the email. 
///! why creating this class?
/// in this class we have all the logic for the system, which we are going to use in Umbraco later. when we send the endpoint for each one of this services. 
/// that will give as much flexibility and safety.So the Boss or those which have an administrator work can get all the workers information. if its needed. 
/// </summary>

public class EmployeeService : IEmployeeService
{
    private readonly AppDbContext? _context;
    private readonly string? _connectionString;
    private readonly PasswordHasher<Employee>? emplyeeHaser;
    public readonly EmpolyeeValidation validationEmployee;
    public EmployeeService(AppDbContext context, IConfiguration connectionString)
    {
        _context = context;
        _connectionString = connectionString.GetConnectionString("DefaultConnection");
        emplyeeHaser = new PasswordHasher<Employee>();
        validationEmployee = new EmpolyeeValidation(context, connectionString);
    }
    public Task<List<Employee>> GetEmployees()
    {
        return _context!.Employees.ToListAsync();
    }
    public async Task<Employee> GetEmployeeById(string employeesName)
    {
        var employee= await _context!.Employees.FirstOrDefaultAsync(x=> x.EmployeeName == employeesName);
        if (employee == null)
        {
            throw new ArgumentException("Employee not found");
        }
        return employee;
    }
    public async Task<Employee> AddEmployee(Employee employee)
    {
        if(!validationEmployee.IsValidName(employee.EmployeeName!)|| !validationEmployee.IsValidEmail(employee.EmployeeEmail!)|| !validationEmployee.IsPasswordValid(employee.Password!))
        {
            throw new ArgumentException("Invalid employee name");
        }
        await _context!.Employees.AddAsync(employee);
        _context.SaveChanges();
        return employee;
    }
    public async Task<Employee> UpdateEmployee(Employee employee)
    {
        if(!validationEmployee.IsValidName(employee.EmployeeName!)||!validationEmployee.IsValidEmail(employee.EmployeeEmail!)||!validationEmployee.IsPasswordValid(employee.Password!))
        {
            throw new ArgumentException("Invalid employee name");
        }
        _context!.Employees.Update(employee);
        await _context.SaveChangesAsync();
        return employee;
    }
    public async Task<string> DeleteEmployee(string employeeName)
    {
        var employee = _context!.Employees.Find(employeeName);
        if(employee == null)
        {
            throw new ArgumentException("Employee not found");
        }
        _context!.Employees.Remove(employee);
        await _context.SaveChangesAsync();
        return "Employee deleted successfully";
    }
}
