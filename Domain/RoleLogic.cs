using Model;
using Model.Action;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

        public  ActionResult Delete(Role RoleIdProperty)
        {
            ActionResult deleteRoleResult = new() { IsPositiveResult = true };
            roleDetails.RemoveAll(roleProperties => roleProperties.RoleId == RoleIdProperty.RoleId);
            deleteRoleResult.Message = "\nSuccessfully deleted Role id is - " + RoleIdProperty.RoleId;
            return deleteRoleResult;
        }

        public static ActionResult SerializeCollection(string filename)
        {
            ActionResult serializeCollectioneResult = new() { IsPositiveResult = true };
            if (roleDetails.Count > 0)
            {
                // Note that only the collection is serialized -- not the
                // CollectionName or any other public property of the class.
                XmlSerializer x = new(typeof(List<Role>));
                TextWriter writer = new StreamWriter(filename);
                x.Serialize(writer, roleDetails);
            }
            else
                serializeCollectioneResult.IsPositiveResult = false;
            return serializeCollectioneResult;
        }
    }

}
