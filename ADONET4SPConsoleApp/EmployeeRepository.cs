using MySql.Data.MySqlClient;
using System;

namespace ADONET4SPConsoleApp
{
    public class EmployeeRepository
    {
        readonly string connectionString = "Data Source=127.0.0.1;Initial Catalog=EMP;UID=root;Password=Jayram007@;Integrated Security=True";

        public void CreateEmployee()
        {
            Console.Write("Name : ");
            string name = Console.ReadLine();

            Console.Write("Address : ");
            string address = Console.ReadLine();

            Console.Write("Phone : ");
            string phoneNo = Console.ReadLine();

            Console.Write("BirthDate yyyy-MM-dd: ");
            DateTime birthDate = DateTime.Parse(Console.ReadLine());

            Console.Write("IsActive true/false: ");
            bool isActive = Boolean.Parse(Console.ReadLine());

            using (MySqlConnection con = new MySqlConnection(connectionString))
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand("CreateEmployee", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("Name", name);
                cmd.Parameters.AddWithValue("Address", address);
                cmd.Parameters.AddWithValue("PhoneNo", phoneNo);
                cmd.Parameters.AddWithValue("BirthDate", birthDate);
                cmd.Parameters.AddWithValue("IsActive", isActive);

                cmd.ExecuteNonQuery();

                Console.WriteLine("Employee Added Successfully");
            }
        }

        public void ReadEmployees()
        {
            using (MySqlConnection con = new MySqlConnection(connectionString))
            {
                con.Open();
                MySqlCommand cmdEmployee = new MySqlCommand("ReadEmployees", con);
                cmdEmployee.CommandType = System.Data.CommandType.StoredProcedure;            

                using (MySqlDataReader reader = cmdEmployee.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Console.WriteLine($"Id: {reader["Id"]}, Name: {reader["Name"]}, Address: {reader["Address"]}, Phone: {reader["PhoneNo"]}, BirthDate: {reader["BirthDate"]}, IsActive: {reader["IsActive"]}");
                    }                    
                    reader.Close();
                    con.Close();
                }
            }
        }

        public void ReadEmployee()
        {
            Console.Write("Id : ");
            int p_Id = int.Parse(Console.ReadLine());           

            using (MySqlConnection con = new MySqlConnection(connectionString))
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand("ReadEmployee", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_Id", p_Id);

                using (MySqlDataReader readerEmployee1 = cmd.ExecuteReader())
                {
                    if (readerEmployee1.Read())
                    {
                        Console.WriteLine($"Id: {readerEmployee1["Id"]}");
                        Console.WriteLine($"Name: {readerEmployee1["Name"]}");
                        Console.WriteLine($"Address: {readerEmployee1["Address"]}");
                        Console.WriteLine($"PhoneNo: {readerEmployee1["PhoneNo"]}");
                        Console.WriteLine($"BirthDate: {readerEmployee1["BirthDate"]}");
                        Console.WriteLine($"IsActive: {readerEmployee1["IsActive"]}");
                    }
                    else
                        Console.WriteLine("No record found with Id : " + p_Id);
                }
            }
        }

        public void UpdateEmployee()
        {
            Console.Write("Id : ");
            int p_Id = int.Parse(Console.ReadLine());

            using (MySqlConnection con = new MySqlConnection(connectionString))
            {
                con.Open();
                MySqlCommand cmdEmployee = new MySqlCommand("ReadEmployee", con);                
                cmdEmployee.CommandType = System.Data.CommandType.StoredProcedure;
                cmdEmployee.Parameters.AddWithValue("p_Id", p_Id);
                MySqlDataReader readerEmployee = cmdEmployee.ExecuteReader();
                if (readerEmployee.HasRows == true)
                {
                    readerEmployee.Close();

                    Console.Write("Name : ");
                    string Name = Console.ReadLine();

                    Console.Write("Address : ");
                    string Address = Console.ReadLine();

                    Console.Write("Phone : ");
                    string PhoneNo = Console.ReadLine();

                    Console.Write("BirthDate yyyy-MM-dd: ");
                    DateTime BirthDate = DateTime.Parse(Console.ReadLine());

                    Console.Write("IsActive true/false: ");
                    bool IsActive = Boolean.Parse(Console.ReadLine());

                    MySqlCommand cmd = new MySqlCommand("UpdateEmployee", con);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("p_Id", p_Id);
                    cmd.Parameters.AddWithValue("Name", Name);
                    cmd.Parameters.AddWithValue("Address", Address);
                    cmd.Parameters.AddWithValue("PhoneNo", PhoneNo);
                    cmd.Parameters.AddWithValue("BirthDate", BirthDate);
                    cmd.Parameters.AddWithValue("IsActive", IsActive);

                    cmd.ExecuteNonQuery();

                    Console.WriteLine("Employee Updated Successfully");
                }
                else
                    Console.WriteLine("No record found with Id: " + p_Id);
            }
        }

        public void DeleteEmployee()
        {
            Console.Write("Id : ");
            int p_Id = int.Parse(Console.ReadLine());

            using (MySqlConnection con = new MySqlConnection(connectionString))
            {
                con.Open();
                string getEmployeeQuery = "SELECT * FROM Employee WHERE Id=" + p_Id;
                MySqlCommand cmdEmployee = new MySqlCommand(getEmployeeQuery, con);
                MySqlDataReader readerEmployee = cmdEmployee.ExecuteReader();

                if (readerEmployee.HasRows == true)
                {
                    readerEmployee.Close();

                    MySqlCommand cmd = new MySqlCommand("DeleteEmployee", con);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("p_Id", p_Id);

                    cmd.ExecuteNonQuery();
                    Console.WriteLine("Employee Deleted Successfully");
                }
                else
                    Console.WriteLine("No record found with Id : " + p_Id);                
            }
        }
    }
}
