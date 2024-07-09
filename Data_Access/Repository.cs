using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Configuration;
using ShopcluesShoppingPortal.Models;
using System.Drawing;
using ShopcluesShoppingPortal.Common;

namespace ShopcluesShoppingPortal.Data_Access
{
    public class Repository
    {
        private SqlConnection sqlConnection;
        private void Connection()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["adoConnectionString"].ToString();
            sqlConnection = new SqlConnection(connectionString);
        }
        /// <summary>
        /// Insert the details of the user
        /// </summary>
        /// <param name="registration"></param>
        /// <returns></returns>
        public bool InsertUserData(Registration registration)
        {
                Connection();
                Password EncryptData = new Password();
                
                SqlCommand sqlCommand = new SqlCommand("SPI_UserRegistration", sqlConnection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@FirstName", registration.FirstName);
                sqlCommand.Parameters.AddWithValue("@LastName", registration.LastName);
                sqlCommand.Parameters.AddWithValue("@Gender", registration.Gender);
                sqlCommand.Parameters.AddWithValue("@DateOfBirth", registration.DateOfBirth);
                sqlCommand.Parameters.AddWithValue("@PhoneNumber", registration.PhoneNumber);
                sqlCommand.Parameters.AddWithValue("@Address", registration.Address);
                sqlCommand.Parameters.AddWithValue("@State", registration.State);
                sqlCommand.Parameters.AddWithValue("@City", registration.City);
                sqlCommand.Parameters.AddWithValue("@EmailAddress", registration.EmailAddress);
                sqlCommand.Parameters.AddWithValue("@Password", EncryptData.Encode(registration.Password));
                sqlCommand.Parameters.AddWithValue("@ConfirmPassword", EncryptData.Encode(registration.ConfirmPassword));
                sqlConnection.Open();
                int id = sqlCommand.ExecuteNonQuery();
                sqlConnection.Close();
                if (id > 0)
                 {
                      return true;
                 }
                         else
                 {
                return false;
            }

        }
        /// <summary>
        /// Get all the details of the user
        /// </summary>
        /// <returns></returns>
        public List<Registration> GetAllUsers()
        {
            List<Registration> UserList = new List<Registration> ();            
                Connection();
                SqlCommand sqlCommand = new SqlCommand("SPS_GetUserDetails", sqlConnection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
              
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
                DataTable dataTable = new DataTable();
                sqlConnection.Open();
                sqlDataAdapter.Fill(dataTable);
                sqlConnection.Close();
                UserList = (from DataRow dr in dataTable.Rows
                            select new Registration()
                            {
                                UserID = Convert.ToInt32(dr["UserID"]),
                                FirstName = Convert.ToString(dr["FirstName"]),
                                LastName = Convert.ToString(dr["LastName"]),
                                Gender = Convert.ToChar(dr["Gender"]),
                                DateOfBirth = Convert.ToDateTime(dr["DateOfBirth"]),
                                PhoneNumber = Convert.ToString(dr["PhoneNumber"]),
                                Address = Convert.ToString(dr["Address"]),
                                State = Convert.ToString(dr["State"]),
                                City = Convert.ToString(dr["City"]),
                                EmailAddress = Convert.ToString(dr["EmailAddress"]),
                                Password = Convert.ToString(dr["Password"]),
                                ConfirmPassword = Convert.ToString(dr["ConfirmPassword"]),
                            }).ToList();
            return UserList;
        }
        /// <summary>
        /// Get customer details  by id
        /// </summary>
        /// <param name="UserID"></param>
        /// <returns></returns>
       public List<Registration> GetCustomerByID(int UserID)
        {
            List<Registration> customerList = new List<Registration>();
            Connection();
            SqlCommand sqlCommand = new SqlCommand("SPS_GetUserByID", sqlConnection);
            sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@UserID", UserID);
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
                DataTable dataTable = new DataTable();
                sqlConnection.Open();
                sqlDataAdapter.Fill(dataTable);
                sqlConnection.Close();
                customerList = (from DataRow dr in dataTable.Rows
                                select new Registration()
                                {
                                    UserID = Convert.ToInt32(dr["UserID"]),
                                    FirstName = Convert.ToString(dr["FirstName"]),
                                    LastName = Convert.ToString(dr["LastName"]),
                                    Gender = Convert.ToChar(dr["Gender"]),
                                    DateOfBirth = Convert.ToDateTime(dr["DateOfBirth"]),
                                    PhoneNumber = Convert.ToString(dr["PhoneNumber"]),
                                    Address = Convert.ToString(dr["Address"]),
                                    State = Convert.ToString(dr["State"]),
                                    City = Convert.ToString(dr["City"]),
                                    EmailAddress = Convert.ToString(dr["EmailAddress"]),
                                    Password = Convert.ToString(dr["Password"]),
                                    ConfirmPassword = Convert.ToString(dr["ConfirmPassword"]),
                                }).ToList();       
            return customerList;
        }
        /// <summary>
        /// Update the details of the user
        /// </summary>
        /// <param name="registration"></param>
        /// <returns></returns>
        public bool UpdateCustomerData(Registration registration)
        {
            Connection();
            SqlCommand sqlCommand = new SqlCommand("SPU_UserDetails", sqlConnection);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("@UserID", registration.UserID);
            sqlCommand.Parameters.AddWithValue("@FirstName", registration.FirstName);
            sqlCommand.Parameters.AddWithValue("@LastName", registration.LastName);
            sqlCommand.Parameters.AddWithValue("@Gender", registration.Gender);
            sqlCommand.Parameters.AddWithValue("@DateOfBirth", registration.DateOfBirth);
            sqlCommand.Parameters.AddWithValue("@PhoneNumber", registration.PhoneNumber);
            sqlCommand.Parameters.AddWithValue("@Address", registration.Address);
            sqlCommand.Parameters.AddWithValue("@State", registration.State);
            sqlCommand.Parameters.AddWithValue("@City", registration.City);
            sqlCommand.Parameters.AddWithValue("@EmailAddress", registration.EmailAddress);
            sqlCommand.Parameters.AddWithValue("@Password", registration.Password);
            sqlCommand.Parameters.AddWithValue("@ConfirmPassword", registration.ConfirmPassword);
            sqlConnection.Open();
            int id = sqlCommand.ExecuteNonQuery();
            sqlConnection.Close();
            if (id > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// Deleting the details of the user
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public bool DeleteCustomer(int Id)
        {
            Connection();
            SqlCommand command = new SqlCommand("SPD_CustomerDetails", sqlConnection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@UserID", Id);
            sqlConnection.Open();
            int i = command.ExecuteNonQuery();
            sqlConnection.Close();
            if (i >= 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// get the login details of the user
        /// </summary>
        /// <param name="login"></param>
        /// <returns>Boolean</returns>
        public bool LoginDetails(Login login)
        {
            Connection();
            Password EncryptData = new Password();

            SqlCommand sqlCommand = new SqlCommand("SPS_LOGIN", sqlConnection);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("@EmailAddress", login.EmailAddress);
            if (!string.IsNullOrEmpty(login.Password))
            {
                sqlCommand.Parameters.AddWithValue("@Password", EncryptData.Encode(login.Password));
            }
           
            sqlConnection.Open();           
            bool isValidUser = (bool)sqlCommand.ExecuteScalar(); 
            sqlConnection.Close();
            return isValidUser;
                     
        }
        






















        /// <summary>
        /// Insert all the details of the user
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        public bool InsertLoginDetails(Login login)
        {
            Connection();
            SqlCommand sqlCommand = new SqlCommand("SPI_LoginDetails", sqlConnection);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("@EmailAddress", login.EmailAddress);
            sqlCommand.Parameters.AddWithValue("@Password", login.Password);
            sqlConnection.Open();
            int id = sqlCommand.ExecuteNonQuery();
            sqlConnection.Close();
            if (id > 0)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        public List<Login> GetAllLoginDetails()
        {
            List<Login> UserList = new List<Login>();

            Connection();
            SqlCommand sqlCommand = new SqlCommand("SPS_LoginDetails", sqlConnection);
            sqlCommand.CommandType = CommandType.StoredProcedure;

            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
            DataTable dataTable = new DataTable();
            sqlConnection.Open();
            sqlDataAdapter.Fill(dataTable);
            sqlConnection.Close();
            UserList = (from DataRow dr in dataTable.Rows
                        select new Login()
                        {
                          
                            EmailAddress = Convert.ToString(dr["EmailAddress"]),
                            Password = Convert.ToString(dr["Password"]),
                           
                        }).ToList();
            return UserList;
        }
        /// <summary>
        /// 
        /// Get customer details  by id
        /// </summary>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public List<Login> GetLoginDetailsByID(int UserID)
        {
            List<Login> customerList = new List<Login>();
            Connection();
            SqlCommand sqlCommand = new SqlCommand("SPS_LoginDetailsByID", sqlConnection);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("@UserID", UserID);
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
            DataTable dataTable = new DataTable();
            sqlConnection.Open();
            sqlDataAdapter.Fill(dataTable);
            sqlConnection.Close();
            customerList = (from DataRow dr in dataTable.Rows
                            select new Login()
                            {
                              
                                EmailAddress = Convert.ToString(dr["EmailAddress"]),
                                Password = Convert.ToString(dr["Password"]),
                              
                            }).ToList();

            return customerList;
        }
        //Update Customer
        public bool UpdateLoginDetails(Login login)
        {
            Connection();
            SqlCommand sqlCommand = new SqlCommand("SPU_LoginDetails", sqlConnection);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("@EmailAddress", login.EmailAddress);
            sqlCommand.Parameters.AddWithValue("@Password", login.Password);
        
            sqlConnection.Open();
            int id = sqlCommand.ExecuteNonQuery();
            sqlConnection.Close();
            if (id >=1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public string DeleteLoginDetails(int UserID)
        {
            string result = "";
            Connection();
            SqlCommand command = new SqlCommand("SPD_LoginDetails", sqlConnection);

            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@UserID", UserID);

            sqlConnection.Open();
            command.ExecuteNonQuery();
            result =command.Parameters.ToString();
            sqlConnection.Close();
            return result;
        }
    


    }
}