using Model;
using Model.Action;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Xml;
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

        public static ActionResult CheckRoleInEmployee(int roleId)
        {
            ActionResult roleResult = new () { IsPositiveResult = true };
            int count = 0;
            if (employeeDetails.Count > 0)
            {
                foreach (Employee employeeProperties in employeeDetails)
                    if (employeeProperties.EmployeeRoleId == roleId)
                        count++;
                if (count > 0)
                    roleResult.Message = "Role Id Exists in Employee List";
                else
                {
                    roleResult.Message = "Role is not present in Employee List!";
                    roleResult.IsPositiveResult = false;
                }
            }
            else
            {
                roleResult.IsPositiveResult = false;
                roleResult.Message = "Empty Employee List!";
            }
            return roleResult;
        }

        public static ActionResult SerializeXMLFile(string filename)
        {
            ActionResult serializeCollectioneResult = new() { IsPositiveResult = true };
            try
            {
                if (employeeDetails.Count > 0)
                {
                    //using var stream = new FileStream(filename, FileMode.CreateNew)
                    //var xmlSerilizer = new XmlSerializer(typeof(List<Employee>))
                    //xmlSerilizer.Serialize(stream, employeeDetails)

                    // TextWriter writer = new StreamWriter(filename)
                    //xsSubmit.Serialize(writer, employeeDetails)
                    //XmlSerializer  x= new (typeof(MyObject))
                    //var subReq = new MyObject()
                    XmlSerializer xsSubmit = new(typeof(List<Employee>));
                    var xml = "";
                    using XmlWriter writer = XmlWriter.Create(filename);
                    xsSubmit.Serialize(writer, employeeDetails);
                    //TextWriter writer1 = new StreamWriter(filename)
                    xml = filename.ToString();
                    //try
                    //
                    //    var xmlserializer = new XmlSerializer(typeof(T))
                    //    var stringWriter = new StringWriter()
                    //    using (var writer = XmlWriter.Create(stringWriter))
                    //    
                    //        xmlserializer.Serialize(writer, value)
                    //        return stringWriter.ToString()
                    //    
                    //
                    //catch Exception ex
                    //
                    //    throw new Exception("An error occurred", ex)
                    //
                }
                else
                    serializeCollectioneResult.IsPositiveResult = false;
            }
            catch (Exception)
            {
                serializeCollectioneResult.Message = "Error at Employee Serialization";
                serializeCollectioneResult.IsPositiveResult = false;
            }
            return serializeCollectioneResult;
        }

        public static ActionResult SerializeTextFile(string filename)
        {
            ActionResult serializeTestFileResult = new() { IsPositiveResult = true };
            try
            {
                if (employeeDetails.Count > 0)
                {
                    using TextWriter writer = new StreamWriter(filename);
                    writer.WriteLine("----Employee Module----\n\nEmployee Id - Employee Name - Employee Contact - Employee Role\n--------------------------------------------------------------");
                    foreach (var item in employeeDetails)      
                        writer.WriteLine(string.Format("{0}\t\t{1}\t\t{2}\t\t{3}", item.EmployeeId, item.EmployeeName,item.Contact,item.EmployeeRoleId));
                }
                else
                    serializeTestFileResult.IsPositiveResult = false;
            }
            catch (Exception)
            {
                serializeTestFileResult.IsPositiveResult = false;
                serializeTestFileResult.Message = "Error Occured at Employee File Serialization";
            }
            return serializeTestFileResult;
        }

        public static ActionResult SerializeJSONFile(string filename)
        {
            ActionResult serializeADOFileResult = new() { IsPositiveResult = true };
            try
            {
                if (employeeDetails.Count > 0)
                {
                    var options = new JsonSerializerOptions { WriteIndented = true };
                    string jsonString = JsonSerializer.Serialize(employeeDetails,options);
                    File.WriteAllText(filename, jsonString);
                    Console.WriteLine(File.ReadAllText(filename));
                }
                else
                    serializeADOFileResult.IsPositiveResult = false;
            }
            catch (Exception)
            {
                serializeADOFileResult.IsPositiveResult = false;
                serializeADOFileResult.Message = "Error Occured at Employee File Serialization";
            }
            return serializeADOFileResult;
        }

        public static ActionResult SerializeADOFile(string dbName)
        {
            ActionResult serializaAdoResult = new() { IsPositiveResult = true };
            try
            {
                string connectionString = $"Server=(localdb)\\ProjectsV13; Database = {dbName};Integrated security=True;Trusted_Connection=yes";
                string queryString = "INSERT INTO dbo.Employee(EmployeeId,Name,Contact,EmployeeRole) VALUES (@EmployeeId,@Name,@Contact,@EmployeeRole)";
                using SqlConnection sqlConnection = new(connectionString);
                sqlConnection.Open();
                foreach (Employee employeeProperties in employeeDetails)
                {
                    using SqlCommand sqlCommand = new(queryString, sqlConnection);
                    sqlCommand.Parameters.AddWithValue("@EmployeeId", SqlDbType.Int).Value = employeeProperties.EmployeeId;
                    sqlCommand.Parameters.AddWithValue("@Name", SqlDbType.VarChar).Value = employeeProperties.EmployeeName;
                    sqlCommand.Parameters.AddWithValue("@Contact", SqlDbType.VarChar).Value = employeeProperties.Contact;
                    sqlCommand.Parameters.AddWithValue("@EmployeeRole", SqlDbType.Int).Value = employeeProperties.EmployeeRoleId;
                    int result = sqlCommand.ExecuteNonQuery();
                    if (result < 0)
                        serializaAdoResult.Message = "Error inserting data into Database!";
                    else
                        serializaAdoResult.Message = "Employee rows Updated Successfully!";
                }
                sqlConnection.Close();
                serializaAdoResult.IsPositiveResult = true;
            }
            catch (Exception e)
            {
                serializaAdoResult.IsPositiveResult = false;
                Debug.WriteLine(e.Message);
            }
            return serializaAdoResult;
        }
    }
}
