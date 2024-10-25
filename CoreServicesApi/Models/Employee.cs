using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Models
{
    public class Employee
    {
        [Key]
        public int Id {get;set;}
        public string? EmployeeName {get; set; }
        public string? EmployeeEmail {get; set; }
        public string? Password {get; set; }
        public string? Role {get; set; }
        public decimal Salary {get; set; }
        [ForeignKey("ManagerId")]
        public int? ManagerId {get; set; }
        public Employee? Manager { get; set; }
        public DateTime? CreatedAt {get; set; }
    }
}