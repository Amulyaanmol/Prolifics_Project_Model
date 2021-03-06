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
            catch (Exception exception)
            {
                addRoleResult.IsPositiveResult = false;
                addRoleResult.Message = "\nSome Error Occured!! Please select right option\n" + exception.ToString();
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
            catch (Exception exception)
            {
                employeeClassRoleIdResult.IsPositiveResult = false;
                employeeClassRoleIdResult.Message = "\nSome Error Occured!! Please select right option\n" + exception.ToString();
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
            catch (Exception exception)
            {
                roleIdResult.IsPositiveResult = false;
                roleIdResult.Message = "\nSome Error Occured!! Please select right option\n" + exception.ToString();
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
            catch (Exception exception)
            {
                serializeCollectioneResult.Message = "Error at SerializeCollection\n" + exception.ToString();
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
            catch(Exception exception)
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
            catch (Exception exception)
            {
                serializeADOFileResult.IsPositiveResult = false;
                serializeADOFileResult.Message = "Error Occured at Employee File Serialization\n" + exception.ToString();
            }
            return serializeADOFileResult;
        }

        public static ActionResult SerializeADOFile(string database)
        {
            ActionResult serializaAdoResult = new() { IsPositiveResult = true };
            try
            {
                string connectionString = $"Server=(localdb)\\ProjectsV13; Database = {database};Integrated security=True;Trusted_Connection=yes";
                string queryString = "INSERT INTO dbo.Role(RoleId,RoleName) VALUES (@RoleId,@RoleName)";
                using SqlConnection sqlConnection = new(connectionString);
                sqlConnection.Open();
                foreach (Role roleProperties in roleDetails)
                {
                    using SqlCommand sqlCommand = new(queryString, sqlConnection);
                    sqlCommand.Parameters.AddWithValue("@RoleId", SqlDbType.Int).Value = roleProperties.RoleId;
                    sqlCommand.Parameters.AddWithValue("@RoleName", SqlDbType.VarChar).Value = roleProperties.RoleName;
                    int result = sqlCommand.ExecuteNonQuery();
                    if (result < 0)
                        serializaAdoResult.Message = "Error inserting data into Database!";
                    else
                        serializaAdoResult.Message = "Role rows Updated Successfully!";
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

        public static ActionResult SerializeEf()
        {
            ActionResult serializeEfResult = new() { IsPositiveResult = false };
            using var db = new PpmContext();
            try
            {
                List<Role> roles = db.RoleEf.ToList();
                foreach (Role role in roleDetails)
                {
                    if(roles.Exists(roleProperties=>roleProperties.RoleId==role.RoleId))
                    {
                        var removeId = roles.SingleOrDefault(roleProperties => roleProperties.RoleId == role.RoleId);
                        db.RoleEf.Remove(removeId);
                        db.SaveChanges();
                        db.RoleEf.Add(role);
                        db.SaveChanges();
                    }
                    else
                    {
                        db.RoleEf.Add(role);
                        db.SaveChanges();
                    }
                }
                serializeEfResult.IsPositiveResult = true;
                
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            return serializeEfResult;
        }

    }
}
