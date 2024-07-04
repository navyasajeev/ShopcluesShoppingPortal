using ShopcluesShoppingPortal.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;

namespace ShopcluesShoppingPortal.Data_Access
{
    public class ContactRepository
    { 
            private SqlConnection sqlConnection;
            private void Connection()
            {
                string connectionString = ConfigurationManager.ConnectionStrings["adoConnectionString"].ToString();
                sqlConnection = new SqlConnection(connectionString);
            }
        /// <summary>
        /// Add the details of a contact form
        /// </summary>
        /// <param name="contactForm"></param>
        /// <returns></returns>
            public bool SendMessage(ContactForm contactForm)
            {
                try
                {
                    Connection();
                    using (SqlCommand sqlCommand = new SqlCommand("SPI_ContactDetails", sqlConnection))
                    {
                        sqlCommand.CommandType = CommandType.StoredProcedure;
                        sqlCommand.Parameters.AddWithValue("@Name", contactForm.Name);
                        sqlCommand.Parameters.AddWithValue("@Email", contactForm.Email);
                        sqlCommand.Parameters.AddWithValue("@Message", contactForm.Message);

                        sqlConnection.Open();
                        int i = sqlCommand.ExecuteNonQuery();
                        sqlConnection.Close();
                        return i >= 1;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error in SendMessage method: " + ex.Message);
                    return false;
                }
               
            }
       

}
}