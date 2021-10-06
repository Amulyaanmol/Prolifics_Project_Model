using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using Model.Action;
using System.Xml.Serialization;
using System.IO;
using System.Xml;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

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
            catch (Exception)
            {
                addProjectresults.IsPositiveResult = false;
                addProjectresults.Message = "\nSome Error Occured!! Please select right option";
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

        public  Project DisplayById(int id)
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
            catch (Exception)
            {
                projectIdResult.IsPositiveResult = false;
                projectIdResult.Message = "\nSome Error Occured!! Please select right option";
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
            catch (Exception)
            {
                deleteEmployeeFromProjectResult.IsPositiveResult = false;
                deleteEmployeeFromProjectResult.Message = "\nSome Error Occured!! Please select right option";
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
            catch (Exception)
            {
                addEmployeeToProjectResult.IsPositiveResult = false;
                addEmployeeToProjectResult.Message = "\nSome Error Occured!! Please select right option";
            }
            return addEmployeeToProjectResult;
        }

        public static ActionResult SerializeCollection(string filename)
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
            catch (Exception)
            {
                serializeCollectioneResult.IsPositiveResult = false;
                serializeCollectioneResult.Message = "Error at Project Serialization";
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
                    writer.WriteLine("-----PROJECT MODULE-----\n\nProject Id - Project Name - Start Date - End Date - Budget\n----------------------------------------------------------");
                    foreach (var item in projectDetails)      
                        writer.WriteLine(string.Format("{0}\t\t{1}\t\t{2}\t\t{3}\t\t{4}", item.ProjectId, item.ProjectName, item.OpenDate.ToShortDateString(), item.CloseDate.ToShortDateString(),item.Budget));
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
    }
}
