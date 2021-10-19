using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using Model.Action;
using System.Xml.Serialization;
using System.IO;
using System.Xml;
using System.Text.Json;
using System.Data.SqlClient;
using System.Data;
using System.Diagnostics;

namespace Domain
{
    public class ProjectLogic : IEntityOperation<Project>
    {
        static readonly List<Project> projectDetails = new();

        public ActionResult Insert(Project projectProperty)
        {
            ActionResult addProjectresults = new() { IsPositiveResult = true };
            try
            {
                if (projectDetails.Count > 0)
                {
                    if (projectDetails.Exists(projectProperties => projectProperties.ProjectId == projectProperty.ProjectId) || projectDetails.Exists(projectProperties => projectProperties.ProjectName == projectProperty.ProjectName))
                    {
                        addProjectresults.IsPositiveResult = false;
                        addProjectresults.Message = "\nAdding Project Details is not Successful\nProject Id or Project Name already exists";
                    }
                    else
                    {
                        projectDetails.Add(projectProperty);
                        addProjectresults.Message = "\nProject Details Added Successfully";
                        addProjectresults.IsPositiveResult = true;
                    }
                }
                else
                {
                    projectDetails.Add(projectProperty);
                    addProjectresults.Message = "\nProject Details Created Successfully";
                    addProjectresults.IsPositiveResult = true;
                }
            }
            catch (Exception exception)
            {
                addProjectresults.IsPositiveResult = false;
                addProjectresults.Message = "\nSome Error Occured!! Please select right option\n" + exception.ToString();
            }
            return addProjectresults;
        }

        public DataResults<Project> DisplayAll()
        {
            DataResults<Project> displayProjectResults = new() { IsPositiveResult = true };
            if (projectDetails.Count > 0)
                displayProjectResults.Results = projectDetails.OrderBy(projectProperties => projectProperties.ProjectId);
            else
                displayProjectResults.IsPositiveResult = false;
            return displayProjectResults;
        }

        public Project DisplayById(int id)
        {
            Project project = new();
            if (projectDetails.Exists(projectProperties => projectProperties.ProjectId == id))
                project = projectDetails.Single(projectProperties => projectProperties.ProjectId == id);
            return project;
        }

        public static ActionResult CheckProjectId(int _projectId)
        {
            ActionResult projectIdResult = new() { IsPositiveResult = true };
            try
            {
                if (projectDetails.Exists(projectProperties => projectProperties.ProjectId == _projectId))
                    projectIdResult.IsPositiveResult = true;
                else
                    projectIdResult.IsPositiveResult = false;
            }
            catch (Exception exception)
            {
                projectIdResult.IsPositiveResult = false;
                projectIdResult.Message = "\nSome Error Occured!! Please select right option\n"+ exception.ToString();
            }
            return projectIdResult;
        }

        public ActionResult Delete(Project ProjectIdProperty)
        {
            ActionResult deleteProjectResult = new() { IsPositiveResult = true };
            projectDetails.RemoveAll(projectProperties => projectProperties.ProjectId == ProjectIdProperty.ProjectId);
            deleteProjectResult.Message = "\nSuccessfully deleted Project id is - " + ProjectIdProperty.ProjectId;
            return deleteProjectResult;
        }

        public static Project GetProjectId(int _projectId)
        {
            Project project = new();
            if (projectDetails.Exists(projectProperties => projectProperties.ProjectId == _projectId))
                project.ListEmployee = projectDetails.Single(projectProperties => projectProperties.ProjectId == _projectId).ListEmployee;
            return project;
        }

        public static ActionResult DeleteEmployeeFromProject(int _employeeId, int _projectId)
        {
            ActionResult deleteEmployeeFromProjectResult = new() { IsPositiveResult = true };
            try
            {
                if (projectDetails.Single(projectProperties => projectProperties.ProjectId == _projectId).ListEmployee.Exists(employeeProperties => employeeProperties.EmployeeId == _employeeId))
                {
                    var removableItem = projectDetails.Single(projectProperties => projectProperties.ProjectId == _projectId).ListEmployee.Single(employeeProperties => employeeProperties.EmployeeId == _employeeId);
                    projectDetails.Single(projectProperties => projectProperties.ProjectId == _projectId).ListEmployee.Remove(removableItem);

                }
                else
                    deleteEmployeeFromProjectResult.IsPositiveResult = false;
            }
            catch (Exception exception) 
            { 
                deleteEmployeeFromProjectResult.IsPositiveResult = false; deleteEmployeeFromProjectResult.Message = "\nSome Error Occured!! Please select right option\n" + exception.ToString(); 
            }
            return deleteEmployeeFromProjectResult;
        }

        public static ActionResult AddEmployeeToProject(int _projectId, Employee employeeIdProperty)
        {
            ActionResult addEmployeeToProjectResult = new() { IsPositiveResult = true };
            try
            {
                if (projectDetails.Single(projectProperties => projectProperties.ProjectId == _projectId).ListEmployee == null)
                    projectDetails.Single(projectProperties => projectProperties.ProjectId == _projectId).ListEmployee = new List<Employee>();
                if (projectDetails.Single(projectProperties => projectProperties.ProjectId == _projectId).ListEmployee.Exists(e => e.EmployeeId == employeeIdProperty.EmployeeId))
                    addEmployeeToProjectResult.IsPositiveResult = false;
                else
                {
                    addEmployeeToProjectResult.IsPositiveResult = true;
                    projectDetails.Single(projectProperties => projectProperties.ProjectId == _projectId).ListEmployee.Add(employeeIdProperty);
                }
            }
            catch (Exception exception)
            { 
                addEmployeeToProjectResult.IsPositiveResult = false; addEmployeeToProjectResult.Message = "\nSome Error Occured!! Please select right option\n" + exception.ToString();
            }
            return addEmployeeToProjectResult;
        }

        public static ActionResult SerializeXMLFile(string filename)
        {
            ActionResult serializeCollectioneResult = new() { IsPositiveResult = true };
            try
            {
                if (projectDetails.Count > 0)
                {
                    XmlSerializer xsSubmit = new(typeof(List<Project>));
                    var xml = "";
                    using XmlWriter writer = XmlWriter.Create(filename);
                    xsSubmit.Serialize(writer, projectDetails);
                    //TextWriter writer1 = new StreamWriter(filename)
                    xml = filename.ToString();
                }
                else
                    serializeCollectioneResult.IsPositiveResult = false;
            }
            catch (Exception exception)
            {
                serializeCollectioneResult.IsPositiveResult = false;
                serializeCollectioneResult.Message = "Error at Project Serialization\n" + exception.ToString();
            }
            return serializeCollectioneResult;
        }

        public static ActionResult SerializeTextFile(string filename)
        {
            ActionResult serializeTestFileResult = new() { IsPositiveResult = true };
            //bool append = false
            try
            {
                if (projectDetails.Count > 0)
                {
                    //XmlSerializer xsSubmit = new(typeof(List<Project>))
                    //var xml = ""
                    //using XmlWriter writer = XmlWriter.Create(filename)
                    //xsSubmit.Serialize(writer, projectDetails)
                    //TextWriter writer1 = new StreamWriter(filename)
                    //xml = filename.ToString()
                    using TextWriter writer = new StreamWriter(filename);
                    writer.WriteLine("-----PROJECT MODULE-----");
                    foreach (var item in projectDetails)
                    {
                        writer.WriteLine("\nProject Id - Project Name - Start Date - End Date - Budget\n----------------------------------------------------------");
                        writer.WriteLine(string.Format("{0} \t{1} \t{2} \t{3} \t{4}\n", item.ProjectId, item.ProjectName, item.OpenDate.ToShortDateString(), item.CloseDate.ToShortDateString(), item.Budget));
                        if (item.ListEmployee != null)
                        {
                            writer.WriteLine("-----EMPLOYEE DETAILS -----");
                            foreach (var listitem in item.ListEmployee)
                                writer.WriteLine(string.Format("Employee Name - {0} Employee Id - {1} Role Id - {2}", listitem.EmployeeName, listitem.EmployeeId, listitem.EmployeeRoleId));
                        }
                    }
                }
                else
                    serializeTestFileResult.IsPositiveResult = false;
            }
            catch (Exception exception)
            {
                serializeTestFileResult.IsPositiveResult = false;
                serializeTestFileResult.Message = "Error Occured at Employee File Serialization\n" + exception.ToString();
            }
            return serializeTestFileResult;
        }

        public static ActionResult SerializeJSONFile(string filename)
        {
            ActionResult serializeADOFileResult = new() { IsPositiveResult = true };
            try
            {
                if (projectDetails.Count > 0)
                {
                    var options = new JsonSerializerOptions { WriteIndented = true };
                    string jsonString = JsonSerializer.Serialize(projectDetails, options);
                    File.WriteAllText(filename, jsonString);
                    Console.WriteLine(File.ReadAllText(filename));
                }
                else
                    serializeADOFileResult.IsPositiveResult = false;
            }
            catch (Exception exception)
            {
                serializeADOFileResult.IsPositiveResult = false;
                serializeADOFileResult.Message = "Error Occured at Employee File Serialization\n" + exception.ToString();
            }
            return serializeADOFileResult;
        }

        public static ActionResult SerializeADOFile(string dbName)
        {
            ActionResult serializaAdoResult = new() { IsPositiveResult = true };
            try
            {
                string connectionString = $"Server=(localdb)\\ProjectsV13; Database = {dbName};Integrated security=True;Trusted_Connection=yes";
                string addProjectValues = "INSERT INTO dbo.Project(ProjectId,ProjectName,OpenDate,CloseDate,Budget,EmployeeId,EmployeeName,EmployeeRole) VALUES (@ProjectId,@ProjectName,@OpenDate,@CloseDate,@Budget,@EmployeeId,@EmployeeName,@EmployeeRole)";
                SqlConnection sqlConnection = new(connectionString);
                sqlConnection.Open();
                foreach (Project projectProperties in projectDetails)
                {
                    if (projectProperties.ListEmployee != null)
                    {
                        foreach (Employee employeeProperties in projectProperties.ListEmployee)
                        {
                            SqlCommand sqlProjectCommand = new(addProjectValues, sqlConnection);
                            sqlProjectCommand.Parameters.AddWithValue("@ProjectId", SqlDbType.Int).Value = projectProperties.ProjectId;
                            sqlProjectCommand.Parameters.AddWithValue("@ProjectName", SqlDbType.VarChar).Value = projectProperties.ProjectName;
                            sqlProjectCommand.Parameters.AddWithValue("@OpenDate", SqlDbType.Date).Value = projectProperties.OpenDate;
                            sqlProjectCommand.Parameters.AddWithValue("@CloseDate", SqlDbType.Date).Value = projectProperties.CloseDate;
                            sqlProjectCommand.Parameters.AddWithValue("@Budget", SqlDbType.Decimal).Value = projectProperties.Budget;
                            sqlProjectCommand.Parameters.AddWithValue("@EmployeeId", SqlDbType.Int).Value = employeeProperties.EmployeeId;
                            sqlProjectCommand.Parameters.AddWithValue("@EmployeeName", SqlDbType.VarChar).Value = employeeProperties.EmployeeName;
                            sqlProjectCommand.Parameters.AddWithValue("@EmployeeRole", SqlDbType.Int).Value = employeeProperties.EmployeeRoleId;
                            sqlProjectCommand.ExecuteNonQuery();
                        }
                        serializaAdoResult.Message = "Project details Added with Employee Details....!!!";
                    }
                    else
                        Console.WriteLine("\nEmployee list is empty....");
                }
                sqlConnection.Close();
                serializaAdoResult.IsPositiveResult = true;
            }
            catch (Exception e)
            {
                serializaAdoResult.IsPositiveResult = false;
                Debug.WriteLine(e.ToString());
            }
            return serializaAdoResult;
        }

    }
}
