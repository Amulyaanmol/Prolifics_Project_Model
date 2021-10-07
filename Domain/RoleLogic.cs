﻿using Model;
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
    public class RoleLogic : IEntityOperation<Role>
    {
        static readonly List<Role> roleDetails = new();

        public  ActionResult Insert(Role roleProperty)
        {
            ActionResult addRoleResult = new() { IsPositiveResult = true };
            try
            {
                if (roleDetails.Count > 0)
                {
                    if (roleDetails.Exists(roleProperties => roleProperties.RoleId == roleProperty.RoleId) || roleDetails.Exists(roleProperties => roleProperties.RoleName == roleProperty.RoleName))
                        addRoleResult.IsPositiveResult = false;
                    else
                    {
                        addRoleResult.IsPositiveResult = true;
                        roleDetails.Add(roleProperty);
                        addRoleResult.Message = "\nRole Details Added Successfully";
                    }
                }
                else
                {
                    addRoleResult.IsPositiveResult = true;
                    roleDetails.Add(roleProperty);
                    addRoleResult.Message = "\nRole Details Created Successfully";
                }
            }
            catch (Exception)
            {
                addRoleResult.IsPositiveResult = false;
                addRoleResult.Message = "\nSome Error Occured!! Please select right option";
            }
            return addRoleResult;
        }

        public  DataResults<Role> DisplayAll()
        {
            DataResults<Role> displayRoleResults = new() { IsPositiveResult = true };
            if (roleDetails.Count > 0)
                displayRoleResults.Results = roleDetails.OrderBy(roleProperties => roleProperties.RoleId);
            else
                displayRoleResults.IsPositiveResult = false;
            return displayRoleResults;
        }

        public Role DisplayById(int id)
        {
            Role role = new();
            if (roleDetails.Exists(roleProperties => roleProperties.RoleId == id))
                role = roleDetails.Single(roleProperties => roleProperties.RoleId == id);
            return role;
        }

        public static ActionResult CheckEmployeeClassRoleId(Employee employeeClassRoleIdProperty)
        {
            ActionResult employeeClassRoleIdResult = new() { IsPositiveResult = true };
            try
            {
                if (roleDetails.Exists(roleProperties => roleProperties.RoleId == employeeClassRoleIdProperty.EmployeeRoleId))
                    employeeClassRoleIdResult.IsPositiveResult = true;
                else
                    employeeClassRoleIdResult.IsPositiveResult = false;
            }
            catch (Exception)
            {
                employeeClassRoleIdResult.IsPositiveResult = false;
                employeeClassRoleIdResult.Message = "\nSome Error Occured!! Please select right option";
            }
            return employeeClassRoleIdResult;
        }

        public static ActionResult CheckRoleId(int _roleId)
        {
            ActionResult roleIdResult = new() { IsPositiveResult = true };
            try
            {
                if (roleDetails.Exists(roleProperties => roleProperties.RoleId == _roleId))
                    roleIdResult.IsPositiveResult = true;
                else
                    roleIdResult.IsPositiveResult = false;
            }
            catch (Exception)
            {
                roleIdResult.IsPositiveResult = false;
                roleIdResult.Message = "\nSome Error Occured!! Please select right option";
            }
            return roleIdResult;
        }

        public ActionResult Delete(Role roleIdProperty)
        {
            ActionResult deleteRoleResult = new() { IsPositiveResult = true };
            roleDetails.RemoveAll(roleProperties => roleProperties.RoleId == roleIdProperty.RoleId);
            deleteRoleResult.Message = "\nSuccessfully deleted Role id is - " + roleIdProperty.RoleId;
            return deleteRoleResult;
        }

        public static ActionResult SerializeXMLFile(string filename)
        {
            ActionResult serializeCollectioneResult = new() { IsPositiveResult = true };
            try
            {
                if (roleDetails.Count > 0)
                {
                    //XmlSerializer x = new(typeof(List<Role>))
                    //x.Serialize(writer, roleDetails)
                    XmlSerializer xsSubmit = new(typeof(List<Role>));
                    var xml = "";
                    using XmlWriter writer = XmlWriter.Create(filename);
                    xsSubmit.Serialize(writer, roleDetails);
                    //TextWriter writer1 = new StreamWriter(filename)
                    xml = filename.ToString();
                }
                else
                    serializeCollectioneResult.IsPositiveResult = false;
            }
            catch (Exception)
            {
                serializeCollectioneResult.Message = "Error at SerializeCollection";
                serializeCollectioneResult.IsPositiveResult = false;
            }

            return serializeCollectioneResult;
        }
    
        public static ActionResult SerializeTextFile(string filename)
        {
            ActionResult serializeTestFileResult = new() { IsPositiveResult = true };
            try
            { 
                if(roleDetails.Count>0)
                {
                    //using (StreamWriter writer = new (filename))
                    //foreach (String s in Lists.verbList)
                    //tw.WriteLine(s)
                    // XmlSerializer xsSubmit = new(typeof(RoleLogic))
                    //var xml = ""
                    // using TextWriter writer = new StreamWriter(filename)
                    //writer.WriteLine(xsSubmit)
                    //TextWriter writer1 = new StreamWriter(filename)
                    //xml = filename.ToString()
                    //using Stream stream = File.Open(filename, FileMode.Create)
                    using TextWriter writer = new StreamWriter(filename);
                    writer.WriteLine("----Role Module----\n\nRole Id - Role Name\n-------------------");
                    foreach (var item in roleDetails)    
                        writer.WriteLine(string.Format("{0}\t\t{1}", item.RoleId, item.RoleName));
                }
                else
                    serializeTestFileResult.IsPositiveResult = false;
            }
            catch(Exception)
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
                if (roleDetails.Count > 0)
                {
                    var options = new JsonSerializerOptions { WriteIndented = true };
                    string jsonString = JsonSerializer.Serialize(roleDetails,options);
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
        public static ActionResult SerializeADOFile()
        {
            ActionResult serializaAdoResult = new() { IsPositiveResult = true };
            try
            {
                string connectionString = @"Data Source=(localdb)\ProjectsV13;Initial Catalog=Test;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
               
                string queryString = "INSERT INTO dbo.RoleTable(Id,RoleName) VALUES (@Id,@RoleName)";
                using SqlConnection sqlConnection = new(connectionString);
                sqlConnection.Open();
                foreach (Role roleProperties in roleDetails)
                {
                    using SqlCommand sqlCommand = new(queryString, sqlConnection);
                    sqlCommand.Parameters.AddWithValue("@Id", SqlDbType.Int).Value = roleProperties.RoleId;
                    sqlCommand.Parameters.AddWithValue("@RoleName", SqlDbType.VarChar).Value = roleProperties.RoleName;
                    int result = sqlCommand.ExecuteNonQuery();
                    if (result < 0)
                        serializaAdoResult.Message = "Error inserting data into Database!";
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
