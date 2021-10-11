using System;
using System.Data;
using System.Data.SqlClient;

namespace Domain
{
    public static class DBAdo
    {
        public static bool CheckDB(string dbName)
        {
            bool result = false;
            try
            {
                SqlConnection myConn = new(@"Server=(localdb)\ProjectsV13;Integrated security=True;Trusted_Connection=yes");
                string sqlCheckDBQuery = string.Format($"SELECT DB_ID('{dbName}')", myConn);
                using (myConn)
                {
                    using SqlCommand sqlCmd = new(sqlCheckDBQuery, myConn);
                    myConn.Open();
                    object resultObj = sqlCmd.ExecuteScalar();
                    int databaseID = 0;
                    if (resultObj != null)
                        _ = int.TryParse(resultObj.ToString(), out databaseID);
                    myConn.Close();
                    result = (databaseID > 0);
                }
            }
            catch (Exception ex)
            {
                result = false;
                Console.WriteLine(ex.Message);
            }
            return result;
        }
        public static void GetConnection()
        {
            SqlConnection myConn = new(@"Server=(localdb)\ProjectsV13;Integrated security=True;Trusted_Connection=yes");
            try
            {
                Console.Write("Give Database Name - ");
                string userName = Console.ReadLine();
                var result = CheckDB(userName);
                if (result)
                {
                    Console.WriteLine("DataBase Already Exists with this Name : " + userName);
                    Console.Write(@"Do you want to use Existing Database? Y\N : ");
                    char choice1 = Console.ReadKey().KeyChar;
                    Console.Write("\n");
                    switch (char.ToUpper(choice1))
                    {
                        case 'Y':
                            CreateTable(userName);
                            break;
                        case 'N':
                            myConn.Open();
                            string drop = $"DROP DATABASE {userName}";
                            SqlCommand cmd = new(drop, myConn);
                            cmd.Prepare();
                            object r = cmd.ExecuteScalar();
                            if (r == null)
                            {
                                Console.WriteLine("Dropped DB - " + userName);
                                GetConnection();
                            }
                            break;
                        default:
                            Console.WriteLine("Some Error Occured!! Please select right option");
                            break;
                    }
                }
                else
                {
                    var createDbResult = CreateDB(userName);
                    if (createDbResult)
                        Console.WriteLine("Database Creatation is Success");
                    else
                        Console.WriteLine("Problem while Creating Database..!");
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                if (myConn.State == ConnectionState.Open)
                     myConn.Close();
            }
        }
        public static bool CreateDB(string dbName)
        {
            bool result = false;
            String str;
            SqlConnection myConn = new(@"Server=(localdb)\ProjectsV13;Integrated security=True;Trusted_Connection=yes");
            str = $"CREATE DATABASE {dbName}";
            Console.WriteLine("Getting Connection ...");
            SqlCommand myCommand = new(str, myConn);
            try
            {
                myConn.Open();
                object command = myCommand.ExecuteScalar();
                if (command == null)
                {
                    CreateTable(dbName);
                    return true;
                }
                else
                    return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                if (myConn.State == ConnectionState.Open)
                    myConn.Close();
            }
            return result;
        }
        public static bool CreateTable(string database)
        {
            SqlConnection myCon = new($"Server=(localdb)\\ProjectsV13; Database = {database};Integrated security=True;Trusted_Connection=yes");
            try
            {
                myCon.Open();
                string dropRole = "DROP TABLE IF EXISTS Role";
                SqlCommand roleDrop = new(dropRole, myCon);
                roleDrop.Prepare();
                object roleDropResult = roleDrop.ExecuteScalar();
                if (roleDropResult == null)
                {
                    SqlCommand createCommand = new("CREATE TABLE Role (RoleId int NOT NULL,RoleName varchar(50),PRIMARY KEY (RoleId))", myCon);
                    object roleCreate = createCommand.ExecuteNonQuery();
                    if (roleCreate != null)
                    {
                        var serializeResult = RoleLogic.SerializeADOFile(database);
                        if (serializeResult.IsPositiveResult)
                            Console.WriteLine(serializeResult.Message);
                        else
                            Console.WriteLine(serializeResult.Message+"");
                    }
                    else
                        Console.WriteLine("Role not Created");
                }
                string dropEmlpoyee = "DROP TABLE IF EXISTS Employee";
                SqlCommand employeeDrop = new(dropEmlpoyee, myCon);
                employeeDrop.Prepare();
                object employeeDropResult = employeeDrop.ExecuteScalar();
                if (employeeDropResult == null)
                {
                    SqlCommand createCommand = new("CREATE TABLE Employee (EmployeeId int NOT NULL,Name varchar(50),Contact varchar(50),EmployeeRole int NOT NULL,PRIMARY KEY (EmployeeId))", myCon);
                    object employeeCreate = createCommand.ExecuteNonQuery(); 
                    if (employeeCreate != null)
                    {
                        var serializeResult = EmployeeLogic.SerializeADOFile(database);
                        if (serializeResult.IsPositiveResult)
                            Console.WriteLine(serializeResult.Message);
                        else
                            Console.WriteLine(serializeResult.Message + "");
                    }
                    else
                        Console.WriteLine("Employee not Created");
                }
                string dropProject = "DROP TABLE IF EXISTS Project";
                SqlCommand projectDrop = new(dropProject, myCon);
                projectDrop.Prepare();
                object projectDropResult = projectDrop.ExecuteScalar();
                if (projectDropResult == null)
                {
                    SqlCommand createCommand = new("CREATE TABLE Project (ProjectId int NOT NULL,ProjectName varchar(50),OpenDate Date, CloseDate Date,Budget Decimal,EmployeeId int,EmployeeName varchar(50),EmployeeRole int NOT NULL)", myCon);
                    object projectCreate = createCommand.ExecuteNonQuery();
                    if (projectCreate != null)
                    {
                        var serializeResult = ProjectLogic.SerializeADOFile(database);
                        if (serializeResult.IsPositiveResult)
                            Console.WriteLine(serializeResult.Message);
                        else
                            Console.WriteLine(serializeResult.Message + "");
                    }
                    else
                        Console.WriteLine("Project not Created");
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                if (myCon.State == ConnectionState.Open)
                    myCon.Close();
            }
            return true;
        }
    }
}
