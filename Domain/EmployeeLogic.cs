using Model;
using Model.Action;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;

namespace Domain
{
    public class EmployeeLogic : IEntityOperation<Employee>
    {
        static readonly List<Employee> employeeDetails = new();

        public ActionResult Insert(Employee employeeProperty)
        {
            ActionResult addEmployeeResult = new() { IsPositiveResult = true };
            try
            {
                if (employeeDetails.Count > 0)
                {
                    if (employeeDetails.Exists(employeeProperties => employeeProperties.EmployeeId == employeeProperty.EmployeeId))
                        addEmployeeResult.IsPositiveResult = false;
                    else
                    {
                        addEmployeeResult.IsPositiveResult = true;
                        employeeDetails.Add(employeeProperty);
                        addEmployeeResult.Message = "\nEmployee Details Added Successfully";
                    }
                }
                else
                {
                    addEmployeeResult.IsPositiveResult = true;
                    employeeDetails.Add(employeeProperty);
                    addEmployeeResult.Message = "\nEmployee Details Created Successfully";
                }
            }
            catch (Exception)
            {
                addEmployeeResult.IsPositiveResult = false;
                addEmployeeResult.Message = "\nSome Error Occured!! Please select right option";
            }
            return addEmployeeResult;
        }

        public DataResults<Employee> DisplayAll()
        {
            DataResults<Employee> displayEmployeeResults = new() { IsPositiveResult = true };
            if (employeeDetails.Count > 0)
                displayEmployeeResults.Results = employeeDetails.OrderBy(employeeProperties => employeeProperties.EmployeeId);
            else
                displayEmployeeResults.IsPositiveResult = false;
            return displayEmployeeResults;
        }

        public Employee DisplayById(int id)
        {
            Employee employee = new();
            if (employeeDetails.Exists(employeeProperties => employeeProperties.EmployeeId == id))
                employee = employeeDetails.Single(employeeProperties => employeeProperties.EmployeeId == id);
            return employee;
        }

        public ActionResult Delete(Employee EmployeeIdProperty)
        {
            ActionResult deleteEmployeeResult = new() { IsPositiveResult = true };
            employeeDetails.RemoveAll(employeeProperties => employeeProperties.EmployeeId == EmployeeIdProperty.EmployeeId);
            deleteEmployeeResult.Message = "\nSuccessfully deleted Employee id is - " + EmployeeIdProperty.EmployeeId;
            return deleteEmployeeResult;
        }

        public static ActionResult DeleteRoleFromEmployee(int _roleId, int _employeeId)
        {
            ActionResult deleteRoleFromEmployeeResult = new() { IsPositiveResult = true };
            try
            {

                if (employeeDetails.Single(employeeProperties => employeeProperties.EmployeeId == _employeeId).EmployeeRoleId == _roleId)
                    deleteRoleFromEmployeeResult.IsPositiveResult = true;
                else
                    deleteRoleFromEmployeeResult.IsPositiveResult = false;
            }
            catch (Exception)
            {
                deleteRoleFromEmployeeResult.IsPositiveResult = false;
                deleteRoleFromEmployeeResult.Message = "\nSome Error Occured!! Please select right option";
            }
            return deleteRoleFromEmployeeResult;
        }

        public static ActionResult CheckCount(int count)
        {
            ActionResult countResult = new() { IsPositiveResult = true };
            try
            {
                if (count <= employeeDetails.Count)
                    countResult.IsPositiveResult = true;
                else
                    countResult.IsPositiveResult = false;
            }
            catch (Exception)
            {
                countResult.IsPositiveResult = false;
                countResult.Message = "\nSome Error Occured!! Please select right option";
            }
            return countResult;
        }

        public static Employee GetEmployeeId(int _employeeId)
        {
            Employee employee = new();
            if (employeeDetails.Exists(employeeProperties => employeeProperties.EmployeeId == _employeeId))
                employee.EmployeeRoleId = employeeDetails.Single(employeeProperties => employeeProperties.EmployeeId == _employeeId).EmployeeRoleId;
            return employee;
        }

        public static Employee CheckEmployeeRoleId(Employee EmployeeIdProperty)
        {
            Employee employee = new();
            employee.EmployeeRoleId = employeeDetails.Single(employeeProperties => employeeProperties.EmployeeId == EmployeeIdProperty.EmployeeId).EmployeeRoleId;
            employee.EmployeeName = employeeDetails.Single(employeeProperties => employeeProperties.EmployeeId == EmployeeIdProperty.EmployeeId).EmployeeName;
            return employee;
        }

        public static ActionResult CheckEmployeeId(Employee employeeIdProperty)
        {
            ActionResult EmployeeIdResult = new() { IsPositiveResult = true };
            try
            {
                if (employeeDetails.Exists(employeeProperties => employeeProperties.EmployeeId == employeeIdProperty.EmployeeId))
                    EmployeeIdResult.IsPositiveResult = true;
                else
                    EmployeeIdResult.IsPositiveResult = false;
            }
            catch (Exception)
            {
                EmployeeIdResult.IsPositiveResult = false;
                EmployeeIdResult.Message = "\nSome Error Occured!! Please select right option";
            }
            return EmployeeIdResult;
        }

        public static ActionResult SerializeCollection(string filename)
        {
            ActionResult serializeCollectioneResult = new() { IsPositiveResult = true };
            if (employeeDetails.Count > 0)
            {
                using var stream = new FileStream(filename, FileMode.CreateNew);
                var xmlSerilizer = new XmlSerializer(typeof(List<Employee>));
                xmlSerilizer.Serialize(stream, employeeDetails);
            }
            else
                serializeCollectioneResult.IsPositiveResult = false;
            return serializeCollectioneResult;
        }

    }
}
