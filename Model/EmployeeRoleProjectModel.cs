using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace Model
{
    public class Employee
    {
        
        [Required]
        public string EmployeeName { get; set; }
        public int EmployeeId { get; set; }
        [Phone]
        public string Contact { get; set; }
        public int EmployeeRoleId { get; set; }

    }
    public class Role
    {
        public string RoleName { get; set; }
        public int RoleId { get; set; }

    }
    public class Project
    {
        public int ProjectId { get; set; }
        public string ProjectName { get; set; }
        public DateTime OpenDate { get; set; }
        public DateTime CloseDate { get; set; }
        public decimal Budget { get; set; }
        public List<Employee> ListEmployee { get; set; }

    }
}
