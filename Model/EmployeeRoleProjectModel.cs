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

       // public Employee(string name, int id, string contact, int roleid)
        
            //this.EmployeeName = name
            //this.EmployeeId = id
            //this.Contact = contact
            //this.EmployeeRoleId = roleid
        
    }
    public class Role
    {
        public string RoleName { get; set; }
        public int RoleId { get; set; }

        //public Role(string name, int id)
        
            //this.RoleName = name
           // this.RoleId = id
        
    }
    public class Project
    {
        public int ProjectId { get; set; }
        public string ProjectName { get; set; }
        public DateTime OpenDate { get; set; }
        public DateTime CloseDate { get; set; }
        public decimal Budget { get; set; }
        public List<Employee> ListEmployee { get; set; }

        //public Project(int id, string name, DateTime Sdate, DateTime Edate, decimal budget, List<Employee> listEmployee)
        
           // this.ProjectId = id
            //this.ProjectName = name
            //this.OpenDate = Sdate
           // this.CloseDate = Edate
           // this.Budget = budget
           // this.ListEmployee = listEmployee
        

    }
}
