using Models;

namespace Services;

public interface IEmployeeService
{
    public Task<List<Employee>> GetEmployees(); // get all the employees
    public Task<Employee> GetEmployeeById(string employeesName); //! searching for an employee by the name 
    public Task<Employee> AddEmployee(Employee employee); //? adding new employee
    public Task<Employee> UpdateEmployee(Employee employee); 
    public Task<string> DeleteEmployee(string employeeName);
}
