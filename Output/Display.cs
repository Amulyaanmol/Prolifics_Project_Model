using System;
using Model;
using Domain;
using System.Text.RegularExpressions;

namespace Output
{
    public class Display
    {
        protected Display() { }

        public static int DisplayMainMenu()
        {
            Console.WriteLine("\n---SELECT ANY OPTION FROM BELOW---\n 1. Project Module\n 2. Employee Module \n 3. Role Module\n 4. Save Module\n 5. Quit\n");
            int option = 0;
            try
            {
                option = Convert.ToInt32(Console.ReadLine());
            }
            catch (Exception)
            {
                Console.WriteLine("Some Error Occured");
            }
            return option;
        }

        public static void MainCall(int option)
        {
            int option1;
            switch (option)
            {
                case 1:
                    try
                    {
                        Console.Write("\n---PROJECT MODULE---\n--------------------\n a. Add\n b. List All\n c. List By Id\n d. Delete\n e. Return to Main Menu\n\nEnter Your Choice - ");
                        char option2 = Convert.ToChar(Console.ReadLine());
                        switch (char.ToLower(option2))
                        {
                            case 'a':
                                AddProject();
                                break;
                            case 'b':
                                DisplayProjectList();
                                MainCall(1);
                                break;
                            case 'c':
                                DisplayProjectListById();
                                MainCall(1);
                                break;
                            case 'd':
                                DeleteProject();
                                break;
                            case 'e':
                                Console.WriteLine("Redirecting you to Main Menu...");
                                option1 = DisplayMainMenu();
                                MainCall(option1);
                                break;
                            default:
                                Console.WriteLine("Oops! Incorrect Choice...");
                                MainCall(1);
                                break;
                        }
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Oops! Incorrect Choice...");
                        MainCall(1);
                    }
                    break;
                case 2:
                    try
                    {
                        Console.Write("\n---EMPLOYEE MODULE---\n---------------------\n a. Add\n b. List All\n c. List By Id\n d. Delete\n e. Return to Main Menu\n\nEnter Your Choice - ");
                        char option2 = Convert.ToChar(Console.ReadLine());
                        switch (char.ToLower(option2))
                        {
                            case 'a':
                                AddEmployee();
                                break;
                            case 'b':
                                DisplayEmployeeList();
                                MainCall(2);
                                break;
                            case 'c':
                                DisplayEmployeeListById();
                                MainCall(2);
                                break;
                            case 'd':
                                DeleteEmployee();
                                break;
                            case 'e':
                                Console.WriteLine("Redirecting you to Main Menu...");
                                option1 = DisplayMainMenu();
                                MainCall(option1);
                                break;
                            default:
                                Console.WriteLine("Oops! Incorrect Choice...");
                                MainCall(2);
                                break;
                        }
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Oops! Incorrect Choice...");
                        MainCall(2);
                    }
                    break;
                case 3:
                    try
                    {
                        Console.Write("\n---ROLE MODULE---\n-----------------\n a. Add\n b. List All\n c. List By Id\n d. Delete\n e. Return to Main Menu\n\nEnter Your Choice - ");
                        char option2 = Convert.ToChar(Console.ReadLine());
                        switch (char.ToLower(option2))
                        {
                            case 'a':
                                AddRole();
                                break;
                            case 'b':
                                DisplayRoleList();
                                MainCall(3);
                                break;
                            case 'c':
                                DisplayRoleListById();
                                MainCall(3);
                                break;
                            case 'd':
                                DeleteRole();
                                break;
                            case 'e':
                                Console.WriteLine("Redirecting you to Main Menu...");
                                option1 = DisplayMainMenu();
                                MainCall(option1);
                                break;
                            default:
                                Console.WriteLine("Oops! Incorrect Choice...");
                                MainCall(3);
                                break;
                        }
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Oops! Incorrect Choice...");
                        MainCall(3);
                    }
                    break;
                case 4:
                    try
                    {
                        Console.Write("\n---SAVE MODULE---\n-----------------\n a. File\n b. DB-ADO\n c. DB-EF\n d. Return to Main Menu\n\nEnter Your Choice - ");
                        char option2 = Convert.ToChar(Console.ReadLine());
                        switch (char.ToLower(option2))
                        {
                            case 'a':
                                Console.Write("\n File Options:\n\t\t1. Text File\n\t\t2. JSON File\n\t\t3. XML File\n\nEnter Your Preferred File Type - ");
                                int file = Convert.ToInt32(Console.ReadLine());
                                if (file == 1)
                                {
                                    SaveAsFile();
                                    option1 = DisplayMainMenu();
                                    MainCall(option1);
                                }
                                else if(file == 2)
                                {
                                    SaveAsJSONFile();
                                    option1 = DisplayMainMenu();
                                    MainCall(option1);
                                }
                                else if(file == 3)
                                {
                                    SaveAsXmlFile();
                                    option1 = DisplayMainMenu();
                                    MainCall(option1);
                                }
                                else
                                {
                                    Console.WriteLine("Oops! Incorrect Choice...");
                                    option1 = DisplayMainMenu();
                                    MainCall(option1);
                                }
                                break;
                            case 'b':
                                DBAdo.GetConnection();
                                option1 = DisplayMainMenu();
                                MainCall(option1);
                                break;
                            case 'c':
                                SaveAsDB_EFFile();
                                option1 = DisplayMainMenu();
                                MainCall(option1);
                                break;
                            case 'd':
                                Console.WriteLine("Redirecting you to Main Menu...");
                                option1 = DisplayMainMenu();
                                MainCall(option1);
                                break;
                            default:
                                Console.WriteLine("Oops! Incorrect Choice...");
                                option1 = DisplayMainMenu();
                                MainCall(option1);
                                break;
                        }
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Oops! Incorrect Choice...");
                        option1 = DisplayMainMenu();
                        MainCall(option1);
                    }
                    break;
                case 5:
                    Console.WriteLine("Byeeeeeeeeee!!!!!!!!!");
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Invalid Input!!!!!!Re-Enter");
                    option1 = DisplayMainMenu();
                    MainCall(option1);
                    break;
            }
        }

        public static bool AddEmployeeToProject(int _projectId)
        {
            Employee employeeId = new();
            var projectIdResult = ProjectLogic.CheckProjectId(_projectId);
            try
            {
                if (projectIdResult.IsPositiveResult)
                {
                    Console.Write("Choose Employee Id (Only Numeric) for this Project - ");
                    employeeId.EmployeeId = Convert.ToInt32(Console.ReadLine());
                    var EmployeeIdResult = EmployeeLogic.CheckEmployeeId(employeeId);
                    if (EmployeeIdResult.IsPositiveResult)
                    {
                        var employeeRoleIdResult = EmployeeLogic.CheckEmployeeRoleId(employeeId);
                        employeeId.EmployeeRoleId = employeeRoleIdResult.EmployeeRoleId;
                        employeeId.EmployeeName = employeeRoleIdResult.EmployeeName;
                        var addEmployeeToProjectResult = ProjectLogic.AddEmployeeToProject(_projectId, employeeId);
                        if (!addEmployeeToProjectResult.IsPositiveResult)
                            Console.WriteLine("Adding Employee details by Role into Project not Successful\nProject id " + _projectId + " Already contains this Employee id - " + employeeId.EmployeeId);
                        else
                            Console.WriteLine("Employee Successfully added to the project");
                    }
                    else
                        Console.WriteLine("The given employee id Doesn't Exists - " + employeeId.EmployeeId);
                }
                else
                    Console.WriteLine("The given Project id Doesn't Exists - " + _projectId);
            }
            catch (Exception)
            {
                Console.WriteLine("Please provide correct Input....Redirecting you to main Menu");
                MainCall(1);
            }
            return projectIdResult.IsPositiveResult;
        }

        public static bool AddProject()
        {
            Project project = new(); 
            ProjectLogic projectLogic = new();
            EmployeeLogic employeeLogic = new();
            var displayEmployees = employeeLogic.DisplayAll();
            try
            {
                if (displayEmployees.IsPositiveResult)
                {
                    Console.Write("\nEnter following information to add new Project:\nEnter Project ID (Only Numeric) - ");
                    project.ProjectId = Convert.ToInt32(Console.ReadLine());
                    Console.Write("Enter Project Name - ");
                    project.ProjectName = Console.ReadLine();
                    while (int.TryParse(project.ProjectName, out _) || string.IsNullOrWhiteSpace(project.ProjectName))
                    {
                        if (string.IsNullOrWhiteSpace(project.ProjectName))
                        {
                            Console.Write("Project Name Can't be Empty or WhiteSpace...! Input Project Name again...\nEnter Project Name - ");
                            project.ProjectName = Console.ReadLine();
                        }
                        else
                        {
                            Console.Write("Project Name Can't be a Number...! Input Project Name again...\nEnter Project Name - ");
                            project.ProjectName = Console.ReadLine();
                        }
                    }
                    Console.Write("Enter Project Start Date - ");
                    project.OpenDate = Convert.ToDateTime(Console.ReadLine());
                    Console.Write("Enter Project End Date - ");
                    project.CloseDate = Convert.ToDateTime(Console.ReadLine());
                    Console.Write("Enter the Budget - ");
                    project.Budget = Convert.ToInt64(Console.ReadLine());
                    while (project.Budget < 0)
                    {
                        Console.Write("Budget Can't be Negative...! Input Project Name again...\nEnter the Budget - ");
                        project.Budget = Convert.ToInt64(Console.ReadLine());
                    }
                    Console.Write(@"Do you want to add Employee to the Project List? Y\N - ");
                    char choice = Console.ReadKey().KeyChar;
                    Console.WriteLine("\n");
                    switch (char.ToUpper(choice))
                    {
                        case 'Y':
                            Console.WriteLine("Available Employees in the List are --- \nID - Name\n------------");
                            foreach (Employee employeeProperties in displayEmployees.Results)
                                Console.WriteLine(employeeProperties.EmployeeId + " - " + employeeProperties.EmployeeName);
                            Console.Write("How Many Employees you want add to this Project - ");
                            int count = Convert.ToInt32(Console.ReadLine());
                            var countResult = EmployeeLogic.CheckCount(count);
                            if (countResult.IsPositiveResult)
                            {
                                var addProjectResult = projectLogic.Insert(project);
                                if (addProjectResult.IsPositiveResult)
                                {
                                    for (int i = 1; i <= count; i++)
                                        AddEmployeeToProject(project.ProjectId);
                                    Console.WriteLine(addProjectResult.Message + " With Employee details by Role\n");
                                }
                                else
                                    Console.WriteLine(addProjectResult.Message);
                            }
                            else
                                Console.WriteLine("\nAdding Project Details is not Successful\nEnter the Correct Count to add Employee...!!!!!");
                            break;
                        case 'N':
                            var addProjectResults = projectLogic.Insert(project);
                            if (addProjectResults.IsPositiveResult)
                                Console.WriteLine(addProjectResults.Message + " Without Employee details...");
                            else
                                Console.WriteLine("\nAdding Project Details is not Successful\nProject already exists with id - " + project.ProjectId);
                            break;
                        default:
                            Console.WriteLine("Some Error Occured!! Please select right option");
                            MainCall(1);
                            break;
                    }
                }
                else Console.WriteLine("Empty Employee List....\nAdding Project Details with Employee details is not Successful");
            }
            catch (Exception)
            {
                Console.WriteLine("Please provide correct Input....Redirecting you to Project Module");
                MainCall(1);
            }
            Console.Write(@"Do you want to add more Projects? Y\N : ");
            char choice1 = Console.ReadKey().KeyChar;
            Console.Write("\n");
            switch (char.ToUpper(choice1))
            {
                case 'Y':
                    AddProject();
                    break;
                case 'N':
                    MainCall(1);
                    break;
                default:
                    Console.WriteLine("Some Error Occured!! Please select right option");
                    MainCall(1);
                    break;
            }
            return displayEmployees.IsPositiveResult;
        }

        public static void DisplayProjectList()
        {
            ProjectLogic projectLogic = new();
            var displayProjects = projectLogic.DisplayAll();
            if (displayProjects.IsPositiveResult)
            {
                Console.WriteLine("\n--Projects Details with Employee Assigned by Role are--\n");
                foreach (Project projectProperties in displayProjects.Results)
                {
                    Console.Write("Project ID - Project Name -  Project Start Date - Project End Date - Project Budget\n--------------------------------------------------------------------------------------\n");
                    Console.WriteLine(projectProperties.ProjectId + "\t\t" + projectProperties.ProjectName + "\t\t" + projectProperties.OpenDate.ToShortDateString() + "\t" +
                                        projectProperties.CloseDate.ToShortDateString() + "\t" + projectProperties.Budget);
                    if (projectProperties.ListEmployee != null)
                    {
                        Console.WriteLine("\nEmployee Name - Employee Id - Role Id\n------------------------------------------");
                        foreach (Employee employeeProperties in projectProperties.ListEmployee)
                            Console.WriteLine(employeeProperties.EmployeeName + "\t\t" + employeeProperties.EmployeeId + "\t\t" + employeeProperties.EmployeeRoleId);
                        Console.WriteLine("--------------------------------------------------------------------------------------\n");
                    } 
                    else
                        Console.WriteLine("\nEmployee list is empty....");
                }
            }
            else
                Console.WriteLine("Noting to View in Project Details.....!!!!! ");
        }

        public static void DisplayProjectListById()
        {
            ProjectLogic projectLogic = new();
            try
            {
                var displayProjects = projectLogic.DisplayAll();
                if (displayProjects.IsPositiveResult)
                {
                    Console.WriteLine("Project ID\n---------------");
                    foreach (Project projectProperties in displayProjects.Results)
                        Console.WriteLine(projectProperties.ProjectId);
                    Console.Write("\nSelect Project Id to Display Full details of Respective Id from the List - ");
                    var projectId = Convert.ToInt32(Console.ReadLine());
                    var projectIdResult = ProjectLogic.CheckProjectId(projectId);
                    if (projectIdResult.IsPositiveResult)
                    {
                        var projectsByIdResult = projectLogic.DisplayById(projectId);
                        Console.WriteLine("\nProject ID - Project Name -  Project Start Date - Project End Date - Project Budget\n" +
                                              "--------------------------------------------------------------------------------------");
                        Console.WriteLine(projectsByIdResult.ProjectId + "\t\t" + projectsByIdResult.ProjectName + "\t\t" + projectsByIdResult.OpenDate.ToShortDateString() +
                         "\t" + projectsByIdResult.CloseDate.ToShortDateString() + "\t" + projectsByIdResult.Budget);
                        var getProjectIdResult = ProjectLogic.GetProjectId(projectId);
                        if (getProjectIdResult.ListEmployee != null)
                        {
                            Console.WriteLine("\n\nEmployee ID - Employee Name - Role Id\n-----------------------------------");
                            foreach (Employee employeeProperties in getProjectIdResult.ListEmployee)
                                Console.WriteLine(employeeProperties.EmployeeId + "\t\t" + employeeProperties.EmployeeName + "\t\t" + employeeProperties.EmployeeRoleId);
                        }
                        else
                            Console.WriteLine("\nEmployee list is empty....");
                    }
                    else
                        Console.WriteLine("Project Id Doesn't Exists....");
                }
                else
                    Console.WriteLine("\n.....Project List is Empty.....");
            }
            catch (Exception)
            {
                Console.WriteLine("Please provide correct Input....Redirecting you to main Menu");
                MainCall(1);
            }
        }

        public static bool DeleteProject()
        {
            Project project = new();
            ProjectLogic projectLogic = new();
            var displayProjects = projectLogic.DisplayAll();
            if (displayProjects.IsPositiveResult)
            {
                Console.WriteLine("List of Available Project details are...\nProject ID - Project Name\n" +
                "-----------------------------------");
                foreach (Project projectProperties in displayProjects.Results)
                    Console.WriteLine(projectProperties.ProjectId + "\t" + projectProperties.ProjectName);
                Console.Write("Input the Project Id (Only Numeric) to Delete - ");
                project.ProjectId = Convert.ToInt32(Console.ReadLine());
                var projectIdResult = ProjectLogic.CheckProjectId(project.ProjectId);
                if (projectIdResult.IsPositiveResult)
                {
                    var deleteProjectResult = projectLogic.Delete(project);
                    Console.WriteLine(deleteProjectResult.Message);
                }
                else
                    Console.WriteLine("Project Id Doesn't Exists....");
            }
            else
                Console.WriteLine("\n.....Project List is Empty.....\n");
            Console.Write(@"Do you want to Delete more Projects? Y\N : ");
            char choice = Console.ReadKey().KeyChar;
            Console.WriteLine("\n");
            switch (char.ToUpper(choice))
            {
                case 'Y':
                    DeleteProject();
                    break;
                case 'N':
                    MainCall(1);
                    break;
                default:
                    Console.WriteLine("Some Error Occured!! Please select right option");
                    MainCall(1);
                    break;
            }
            return displayProjects.IsPositiveResult;
        }

        public static bool AddEmployee()
        {
            Employee employee = new();
            RoleLogic roleLogic = new();
            try
            {
                Console.WriteLine("\nEnsuring Role List is there or not before adding an Employee to the Employee List...");
                var displayRoles = roleLogic.DisplayAll();
                if (displayRoles.IsPositiveResult)
                {
                    Console.WriteLine("Available Roles in the List are --- \nID - Name\n------------");
                    foreach (Role roleProperties in displayRoles.Results)
                        Console.WriteLine(roleProperties.RoleId + " - " + roleProperties.RoleName);
                    Console.Write("\nEnter following information to add new Employee:");
                    Console.Write("\nEnter Employee Name - ");
                    employee.EmployeeName = Console.ReadLine();
                    while (int.TryParse(employee.EmployeeName, out _) || string.IsNullOrWhiteSpace(employee.EmployeeName))
                    {
                        if (string.IsNullOrWhiteSpace(employee.EmployeeName))
                        {
                            Console.Write("Employee Name Can't be Empty or WhiteSpace...! Input Employee Name again...\nEnter Employee Name - ");
                            employee.EmployeeName = Console.ReadLine();
                        }
                        else
                        {
                            Console.Write("Employee Name Mustn't be a Number...! Input Employee Name again...\nEnter Employee Name - ");
                            employee.EmployeeName = Console.ReadLine();
                        }
                    }
                    Console.Write("Enter Employee ID (Only Numeric) - ");
                    employee.EmployeeId = Convert.ToInt32(Console.ReadLine());
                    Console.Write("Enter 10 digit Mobile Number - ");
                    employee.Contact = Console.ReadLine();
                    if (employee.Contact != null)
                    {
                        while ((!Regex.Match(employee.Contact, @"^\d[0-9]{10}\d$").Success) && (!Regex.Match(employee.Contact, @"^\+?(\d[\d-. ]+)?(\([\d-. ]+\))?[\d-. ]+\d$").Success))
                        {
                            Console.WriteLine("Invalid Mobile number...");
                            Console.Write("Enter 10 digit Mobile Number - ");
                            employee.Contact = Console.ReadLine();
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid Mobile number...");
                        Console.Write("Enter 10 digit Mobile Number - ");
                        employee.Contact = Console.ReadLine();
                    }
                    Console.Write("Enter Role id (Only Numeric) from above Role list - ");
                    employee.EmployeeRoleId = Convert.ToInt32(Console.ReadLine());
                }
                else
                    Console.Write("Empty Role List....");
            }
            catch (Exception)
            {
                Console.WriteLine("Please provide correct Input....Redirecting you to main Menu");
                MainCall(2);
            }
            EmployeeLogic employeeLogic = new();
            var employeeClassRoleIdResult = RoleLogic.CheckEmployeeClassRoleId(employee);
            if (employeeClassRoleIdResult.IsPositiveResult)
            {
                var addEmployeeResult = employeeLogic.Insert(employee);
                if (!addEmployeeResult.IsPositiveResult)
                    Console.WriteLine("\nAdding Employee Details is not Successful\nEmployee already exists with id - " + employee.EmployeeId);
                else
                    Console.WriteLine(addEmployeeResult.Message);
            }
            else
                Console.WriteLine("\nAdding Employee Details is not Successful\nThe given Role id Doesn't Exists - " + employee.EmployeeRoleId);
            Console.Write(@"Do you want to add more Employee? Y\N : ");
            char choice = Console.ReadKey().KeyChar;
            Console.Write("\n");
            switch (char.ToUpper(choice))
            {
                case 'Y':
                    AddEmployee();
                    break;
                case 'N':
                    MainCall(2);
                    break;
                default:
                    Console.WriteLine("Some Error Occured!! Please select right option");
                    MainCall(2);
                    break;
            }
            return employeeClassRoleIdResult.IsPositiveResult;
        }

        public static void DisplayEmployeeList()
        {
            EmployeeLogic employeeLogic = new();
            var displayEmployees = employeeLogic.DisplayAll();
            if (displayEmployees.IsPositiveResult)
            {
                Console.WriteLine("--Employees Details are--\nEmployee ID - Employee Name - Employee Contact - Employee Role Id\n" +
                                  "----------------------------------------------------------------------");
                foreach (Employee employeeProperties in displayEmployees.Results)
                    Console.WriteLine(employeeProperties.EmployeeId + "\t\t" + employeeProperties.EmployeeName + "\t\t" + employeeProperties.Contact + "\t\t" + employeeProperties.EmployeeRoleId);
            }
            else
                Console.WriteLine("\n.....Employee List is Empty.....");
        }

        public static void DisplayEmployeeListById()
        {
            Employee employee = new();
            EmployeeLogic employeeLogic = new();
            try
            {
                var displayEmployees = employeeLogic.DisplayAll();
                if (displayEmployees.IsPositiveResult)
                {
                    Console.WriteLine("Employee ID\n------------");
                    foreach (Employee employeeProperties in displayEmployees.Results)
                        Console.WriteLine(employeeProperties.EmployeeId);
                    Console.Write("\nSelect Employee Id to Display Full details of Respective Id from the List - ");
                    employee.EmployeeId = Convert.ToInt32(Console.ReadLine());
                    var employeeIdResult = EmployeeLogic.CheckEmployeeId(employee);
                    if (employeeIdResult.IsPositiveResult)
                    {
                        var employeesByIdResult = employeeLogic.DisplayById(employee.EmployeeId);
                        Console.WriteLine("\nEmployee ID - Employee Name - Employee Contact - Employee Role Id\n------------------------------------------------------------------------------");
                        Console.WriteLine(employeesByIdResult.EmployeeId + "\t\t" + employeesByIdResult.EmployeeName + "\t\t" + employeesByIdResult.Contact + "\t\t" + employeesByIdResult.EmployeeRoleId);
                    }
                    else
                        Console.WriteLine("Employee Id Doesn't Exists....");
                }
                else
                    Console.WriteLine("\n.....Employee List is Empty.....");
            }
            catch (Exception)
            {
                Console.WriteLine("Please provide correct Input....Redirecting you to main Menu");
                MainCall(2);
            }
        }

        public static bool DeleteEmployee()
        {
            Employee employee = new();
            EmployeeLogic employeeLogic = new();
            var displayEmployees = employeeLogic.DisplayAll();
            if (displayEmployees.IsPositiveResult)
            {
                Console.WriteLine("List of Available Employee details are...\nEmployee ID - Employee Name\n-----------------------------------");
                foreach (Employee employeeProperties in displayEmployees.Results)
                    Console.WriteLine(employeeProperties.EmployeeId + "\t" + employeeProperties.EmployeeName);
                Console.Write("Input the Employee Id (Only Numeric) to Delete - ");
                employee.EmployeeId = Convert.ToInt32(Console.ReadLine());
                var employeeIdResult = EmployeeLogic.CheckEmployeeId(employee);
                if (employeeIdResult.IsPositiveResult)
                {
                    var deleteemployeeResult = employeeLogic.Delete(employee);
                    DeleteEmployeeFromProject(employee.EmployeeId);
                    Console.WriteLine(deleteemployeeResult.Message);
                }
                else
                    Console.WriteLine("Employee Id Doesn't Exists....");
            }
            else
                Console.WriteLine("\n.....Employee List is Empty.....\n");
            Console.Write(@"Do you want to Delete more Employees? Y\N : ");
            char choice = Console.ReadKey().KeyChar;
            Console.WriteLine("\n");
            switch (char.ToUpper(choice))
            {
                case 'Y':
                    DeleteEmployee();
                    break;
                case 'N':
                    MainCall(2);
                    break;
                default:
                    Console.WriteLine("Some Error Occured!! Please select right option");
                    MainCall(2);
                    break;
            }
            return displayEmployees.IsPositiveResult;
        }

        public static bool DeleteEmployeeFromProject(int _employeeId)
        {
            ProjectLogic projectLogic = new();
            var displayProjects = projectLogic.DisplayAll();
            if (displayProjects.IsPositiveResult)
            {
                foreach (Project projectProperties in displayProjects.Results)
                {
                    var getProjectIdResult = ProjectLogic.GetProjectId(projectProperties.ProjectId);
                    if (getProjectIdResult.ListEmployee != null)
                    {
                        Console.Write("List of Available Employees in the given project id - " + projectProperties.ProjectId + "\nEmployee ID - Employee Name\n-----------------------------------\n");
                        foreach (Employee employeeProperties in getProjectIdResult.ListEmployee)
                            Console.WriteLine(employeeProperties.EmployeeId + "\t" + employeeProperties.EmployeeName);
                        var deleteEmployeeFromProjectResult = ProjectLogic.DeleteEmployeeFromProject(_employeeId, projectProperties.ProjectId);
                        if (deleteEmployeeFromProjectResult.IsPositiveResult)
                            Console.WriteLine("\nSuccessfully deleted Employee id is - " + _employeeId);
                        else
                            Console.WriteLine("\nProject id - " + projectProperties.ProjectId + " Doesn't contain the given Employee id - " + _employeeId);
                    }
                    else
                        Console.WriteLine("\nThe given Project id Doesn't Contain Employee Details to Delete - " + _employeeId);
                }
            }
            else
                Console.WriteLine("Noting to Delete in Project List.....!!!!!");
            return displayProjects.IsPositiveResult;
        }

        public static bool AddRole()
        {
            Role role = new();
            try
            {
                Console.Write("\nEnter following information to add new Role:\n");
                Console.Write("Enter Role Name - ");
                role.RoleName = Console.ReadLine();
                while (int.TryParse(role.RoleName, out _) || string.IsNullOrWhiteSpace(role.RoleName))
                {
                    if (string.IsNullOrWhiteSpace(role.RoleName))
                    {
                        Console.Write("Role Name Can't be Empty or WhiteSpace...! Input Role name again...\nEnter Role Name - ");
                        role.RoleName = Console.ReadLine();
                    }
                    else
                    {
                        Console.Write("Role Name Mustn't be Number.. Input Role name again...\nEnter Role Name - ");
                        role.RoleName = Console.ReadLine();
                    }
                }
                Console.Write("Enter Role ID (Only Numeric) - ");
                role.RoleId = Convert.ToInt32(Console.ReadLine());
            }
            catch (Exception)
            {
                Console.WriteLine("Please provide correct Input....Redirecting you to main Menu");
                MainCall(3);
            }
            RoleLogic roleLogic = new();
            var addRoleResult = roleLogic.Insert(role);
            if (!addRoleResult.IsPositiveResult)
                Console.WriteLine("\nAdding Role Details is not Successful\nRole Id or Role Name Already Exists");
            else
                Console.WriteLine(addRoleResult.Message);
            Console.Write(@"Do you want to add more Roles? Y\N : ");
            char choice = Console.ReadKey().KeyChar;
            Console.WriteLine("\n");
            switch (char.ToUpper(choice))
            {
                case 'Y':
                    AddRole();
                    break;
                case 'N':
                    MainCall(3);
                    break;
                default:
                    Console.WriteLine("Some Error Occured!! Please select right option");
                    MainCall(3);
                    break;
            }
            return addRoleResult.IsPositiveResult;
        }

        public static void DisplayRoleList()
        {
            RoleLogic roleLogic = new();
            var displayRoles = roleLogic.DisplayAll();
            if (displayRoles.IsPositiveResult)
            {
                Console.WriteLine("--Roles Details are--\nRole ID - Role Name \n----------------------");
                foreach (Role roleProperties in displayRoles.Results)
                    Console.WriteLine(roleProperties.RoleId + "\t\t" + roleProperties.RoleName);
            }
            else
                Console.WriteLine("\n.....Role List is Empty.....");
        }

        public static void DisplayRoleListById()
        {
            try
            {
                RoleLogic roleLogic = new();
                var displayRoles = roleLogic.DisplayAll();
                if (displayRoles.IsPositiveResult)
                {
                    Console.WriteLine("Role ID\n---------");
                    foreach (Role roleProperties in displayRoles.Results)
                        Console.WriteLine(roleProperties.RoleId);
                    Console.Write("\nSelect Role Id to Display Full details of Respective Id from the List - ");
                    var roleId = Convert.ToInt32(Console.ReadLine());
                    var roleIdResult = RoleLogic.CheckRoleId(roleId);
                    if (roleIdResult.IsPositiveResult)
                    {
                        var rolesByIdResult = roleLogic.DisplayById(roleId);
                        Console.WriteLine("\nRole ID - Role Name \n----------------------");
                        Console.WriteLine(rolesByIdResult.RoleId + "\t\t" + rolesByIdResult.RoleName);
                    }
                    else
                        Console.WriteLine("Role Id Doesn't Exists....");
                }
                else
                    Console.WriteLine("\n.....Role List is Empty.....");
            }
            catch (Exception)
            {
                Console.WriteLine("Please provide correct Input....Redirecting you to main Menu");
                MainCall(3);
            }
        }

        public static bool DeleteRole()
        {
            Role role = new();
            RoleLogic roleLogic = new();
            var displayRoles = roleLogic.DisplayAll();
            if (displayRoles.IsPositiveResult)
            {
                Console.WriteLine("List of Available Role details are...\nRole ID - Role Name\n--------------------------");
                foreach (Role roleProperties in displayRoles.Results)
                    Console.WriteLine(roleProperties.RoleId + "\t" + roleProperties.RoleName);
                Console.Write("Input the Role Id (Only Numeric) to Delete - ");
                role.RoleId = Convert.ToInt32(Console.ReadLine());
                var roleIdResult = RoleLogic.CheckRoleId(role.RoleId);
                if (roleIdResult.IsPositiveResult)
                {
                    var roleResult = EmployeeLogic.CheckRoleInEmployee(role.RoleId);
                    if (!roleResult.IsPositiveResult)
                    {
                        var result = roleLogic.Delete(role);
                        Console.WriteLine(result.Message);
                    }
                    else
                        Console.WriteLine(roleResult.Message);
                }
                else
                    Console.WriteLine("Role Id Doesn't Exists....");
            }
            else
                Console.WriteLine("\n.....Role List is Empty.....\n");
            Console.Write(@"Do you want to Delete more Roles? Y\N : ");
            char choice = Console.ReadKey().KeyChar;
            switch (char.ToUpper(choice))
            {
                case 'Y':
                    DeleteRole();
                    break;
                case 'N':
                    MainCall(3);
                    break;
                default:
                    Console.WriteLine("Some Error Occured!! Please select right option");
                    MainCall(3);
                    break;
            }
            return displayRoles.IsPositiveResult;
        }

        public static void SaveAsXmlFile()
        {
            var employeeSerialize = EmployeeLogic.SerializeXMLFile(@"C:\Users\91707\source\Prolifics_Project_Model\Model\AppData\Employee.xml");
            var projectSerialize=ProjectLogic.SerializeXMLFile(@"C:\Users\91707\source\Prolifics_Project_Model\Model\AppData\Project.xml");
            var roleSerialize = RoleLogic.SerializeXMLFile(@"C:\Users\91707\source\Prolifics_Project_Model\Model\AppData\Role.xml");
            if(employeeSerialize.IsPositiveResult||projectSerialize.IsPositiveResult||roleSerialize.IsPositiveResult)
                Console.WriteLine("\n-----PPM Details Saved Successfully-----");
            else
                Console.WriteLine("\nEmpty PPM!!!!...\n-----Could not be Saved Successfully-----");
        }

        private static void SaveAsJSONFile()
        {
            var roleSerialize = RoleLogic.SerializeJSONFile(@"C:\Users\91707\source\Prolifics_Project_Model\Model\AppData\Role.json");
            var projectSerialize = ProjectLogic.SerializeJSONFile(@"C:\Users\91707\source\Prolifics_Project_Model\Model\AppData\Project.json");
            var employeeSerialize = EmployeeLogic.SerializeJSONFile(@"C:\Users\91707\source\Prolifics_Project_Model\Model\AppData\Employee.json");
            if (employeeSerialize.IsPositiveResult || projectSerialize.IsPositiveResult || roleSerialize.IsPositiveResult)
                Console.WriteLine("\n-----PPM Details Saved as JSON File Successfully-----");
            else
                Console.WriteLine("\nEmpty PPM!!!!...\n-----Could not be Saved as JSON File-----");
        }

        private static void SaveAsDB_EFFile()
        {

            var roleSerialize = RoleLogic.InsertRole();
            if (roleSerialize.IsPositiveResult)
                Console.WriteLine("\n-----Role details inserted into role table successfully-----");
            else
                Console.WriteLine("\nEmpty PPM!!!!...\n-----Could not be Saved as Text File-----");
        }

        private static void SaveAsFile()
        {
            var employeeSerialize = EmployeeLogic.SerializeTextFile(@"C:\Users\91707\source\Prolifics_Project_Model\Model\AppData\Employee.txt");
            var projectSerialize = ProjectLogic.SerializeTextFile(@"C:\Users\91707\source\Prolifics_Project_Model\Model\AppData\Project.txt");
            var roleSerialize = RoleLogic.SerializeTextFile(@"C:\Users\91707\source\Prolifics_Project_Model\Model\AppData\Role.txt");
            if (employeeSerialize.IsPositiveResult || projectSerialize.IsPositiveResult || roleSerialize.IsPositiveResult)
                Console.WriteLine("\n-----PPM Details Saved as Text File Successfully-----");
            else
                Console.WriteLine("\nEmpty PPM!!!!...\n-----Could not be Saved as Text File-----");
        }

    }
}
