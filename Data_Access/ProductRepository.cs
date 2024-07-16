using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Configuration;
using ShopcluesShoppingPortal.Models;
using System.IO;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace ShopcluesShoppingPortal.Data_Access
{
    public class ProductRepository
    {
        private SqlConnection sqlConnection;
        private static List<OrderDetail> OrderHistory = new List<OrderDetail>();
        private void Connection()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["adoConnectionString"].ToString();
            sqlConnection = new SqlConnection(connectionString);
        }
        
        /// <summary>
        /// Add new Product
        /// </summary>
        /// <param name="productDetail"></param>
        /// <returns></returns>
        public bool AddProduct(ProductDetail productDetail, HttpPostedFileBase file)
        {

            try
            {
                Connection();
                SqlCommand sqlCommand = new SqlCommand("SP_AddNewProductDetails", sqlConnection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlConnection.Open();
                sqlCommand.Parameters.AddWithValue("@ProductName", productDetail.ProductName);
                sqlCommand.Parameters.AddWithValue("@CategoryName", productDetail.CategoryName);
                sqlCommand.Parameters.AddWithValue("@Description", productDetail.Description);
                sqlCommand.Parameters.AddWithValue("@Stock", productDetail.Stock);
                sqlCommand.Parameters.AddWithValue("@CreatedDate", productDetail.CreatedDate);
                sqlCommand.Parameters.AddWithValue("@Price", productDetail.Price);
                sqlCommand.Parameters.AddWithValue("@ProductImage", productDetail.ProductImage);
                int i = sqlCommand.ExecuteNonQuery();
                if (i >= 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            finally
            {
                if (sqlConnection != null && sqlConnection.State == ConnectionState.Open)
                {
                    sqlConnection.Close();
                }
            }
        }
        /// <summary>
        /// Get the details of all products
        /// </summary>
        /// <returns></returns>
        public List<ProductDetail> GetAllProduct()
        {
            try
            {
                Connection();
                List<ProductDetail> ProductList = new List<ProductDetail>();
                SqlCommand sqlCommand = new SqlCommand("SP_GetProducts", sqlConnection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
                DataTable dataTable = new DataTable();
                sqlConnection.Open();
                sqlDataAdapter.Fill(dataTable);
                ProductList = (from DataRow dr in dataTable.Rows
                               select new ProductDetail()
                               {
                                   ProductID = Convert.ToInt32(dr["ProductID"]),
                                   ProductName = Convert.ToString(dr["ProductName"]),
                                   CategoryName = Convert.ToString(dr["CategoryName"]),
                                   Description = Convert.ToString(dr["Description"]),
                                   Stock = Convert.ToInt32(dr["Stock"]),
                                   CreatedDate = Convert.ToDateTime(dr["CreatedDate"]),
                                   Price = Convert.ToInt32(dr["Price"]),
                                   ProductImage = Convert.ToString(dr["ProductImage"]),
                               }).ToList();
                return ProductList;
            }
            finally
            {
                if (sqlConnection != null && sqlConnection.State == ConnectionState.Open)
                {
                    sqlConnection.Close();
                }
            }

        }
        /// <summary>
        /// Get the details of the product by using id
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        public ProductDetail GetProductById(int productId)
        {
            try
            {

                ProductDetail product = null;
                Connection();
                SqlCommand sqlCommand = new SqlCommand("SPS_GetProductByID", sqlConnection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@ProductID", productId);
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
                DataTable dataTable = new DataTable();
                sqlConnection.Open();
                sqlDataAdapter.Fill(dataTable);
                if (dataTable.Rows.Count > 0)
                {
                    DataRow row = dataTable.Rows[0];

                    product = new ProductDetail
                    {
                        ProductID = Convert.ToInt32(row["ProductID"]),
                        ProductName = Convert.ToString(row["ProductName"]),
                        CategoryName = Convert.ToString(row["CategoryName"]),
                        Description = Convert.ToString(row["Description"]),
                        Stock = Convert.ToInt32(row["Stock"]),
                        CreatedDate = Convert.ToDateTime(row["CreatedDate"]),
                        Price = Convert.ToInt32(row["Price"]),
                        ProductImage = Convert.ToString(row["ProductImage"]),
                    };

                }
                return product;
            }
            finally
            {
               
                if (sqlConnection != null && sqlConnection.State == ConnectionState.Open)
                {
                    sqlConnection.Close();
                }
            }
        }

        /// <summary>
        /// Update the existing product
        /// </summary>
        /// <param name="productDetail"></param>
        /// <returns></returns>
        public bool UpdateProduct(ProductDetail productDetail)
        {

            try
            {
                Connection();
                SqlCommand sqlCommand = new SqlCommand("SPU_ProductDetails", sqlConnection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@ProductID", productDetail.ProductID);
                sqlCommand.Parameters.AddWithValue("@ProductName", productDetail.ProductName);
                sqlCommand.Parameters.AddWithValue("@CategoryName", productDetail.CategoryName);
                sqlCommand.Parameters.AddWithValue("@Description", productDetail.Description);
                sqlCommand.Parameters.AddWithValue("@Stock", productDetail.Stock);
                sqlCommand.Parameters.AddWithValue("@CreatedDate", productDetail.CreatedDate);
                sqlCommand.Parameters.AddWithValue("@Price", productDetail.Price);
                sqlCommand.Parameters.AddWithValue("@ProductImage", productDetail.ProductImage);
                sqlConnection.Open();
                int id = sqlCommand.ExecuteNonQuery();
                if (id > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            finally
            {
                if (sqlConnection != null && sqlConnection.State == ConnectionState.Open)
                {
                    sqlConnection.Close();
                }
            }
        }
        /// <summary>
        /// Deleting the existing product
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        public bool DeleteProduct(int productId)
        {
            try
            {
                Connection();
                SqlCommand sqlCommand = new SqlCommand("SPD_Product", sqlConnection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@ProductId", productId);
                sqlConnection.Open();
                int i = sqlCommand.ExecuteNonQuery();
               
                if (i >= 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            finally
            {
                if (sqlConnection != null && sqlConnection.State == ConnectionState.Open)
                {
                    sqlConnection.Close();
                }
            }
        }
       /// <summary>
       /// Method to add a new order
       /// </summary>
       /// <param name="orderDetail"></param>
       /// <returns></returns>
        public bool AddOrder(OrderDetail orderDetail)
        {
            try
            {
                Connection();
                SqlCommand sqlCommand = new SqlCommand("SP_AddNewOrder", sqlConnection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@ProductName", orderDetail.ProductName);
                sqlCommand.Parameters.AddWithValue("@Quantity", orderDetail.Quantity);
                sqlCommand.Parameters.AddWithValue("@EmailAddress", orderDetail.EmailAddress);
                sqlCommand.Parameters.AddWithValue("@OrderDate", orderDetail.OrderDate);
                sqlCommand.Parameters.AddWithValue("@TotalAmount", orderDetail.TotalAmount);
                sqlCommand.Parameters.AddWithValue("@Address", orderDetail.Address);
                sqlCommand.Parameters.AddWithValue("@Pincode", orderDetail.Pincode);
                sqlCommand.Parameters.AddWithValue("@PhoneNumber", orderDetail.PhoneNumber);
                sqlCommand.Parameters.AddWithValue("@Status", orderDetail.Status);

                sqlConnection.Open();
                int id = Convert.ToInt32(sqlCommand.ExecuteScalar());
                orderDetail.OrderID = OrderHistory.Count + 1; // Simulate auto-increment ID
                OrderHistory.Add(orderDetail);
                if (id > 0)
                {
                    orderDetail.OrderID = id;
                    return true;
                }
                else
                {
                    return false;
                }
            }
            finally
            {
                if (sqlConnection != null && sqlConnection.State == ConnectionState.Open)
                {
                    sqlConnection.Close();
                }
            }
        }
        /// <summary>
        /// Method to get the details of all orders
        /// </summary>
        /// <returns></returns>
        public List<OrderDetail> GetAllOrder()
        {
            try
            {
                Connection();
                List<OrderDetail> ProductList = new List<OrderDetail>();
                SqlCommand sqlCommand = new SqlCommand("SP_GetOrders", sqlConnection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
                DataTable dataTable = new DataTable();
                sqlConnection.Open();
                sqlDataAdapter.Fill(dataTable);
                ProductList = (from DataRow dr in dataTable.Rows
                               select new OrderDetail()
                               {
                                   OrderID = Convert.ToInt32(dr["OrderID"]),
                                   ProductName = Convert.ToString(dr["ProductName"]),
                                   Quantity = Convert.ToInt32(dr["Quantity"]),
                                   EmailAddress = Convert.ToString(dr["EmailAddress"]),
                                   OrderDate = Convert.ToDateTime(dr["OrderDate"]),
                                   TotalAmount = Convert.ToInt32(dr["TotalAmount"]),
                                   Address = Convert.ToString(dr["Address"]),
                                   Pincode = Convert.ToInt32(dr["Pincode"]),
                                   PhoneNumber = Convert.ToInt32(dr["PhoneNumber"]),
                                   Status = Convert.ToString(dr["Status"])


                               }).ToList();
                return ProductList;
            }
            finally
            {
                if (sqlConnection != null && sqlConnection.State == ConnectionState.Open)
                {
                    sqlConnection.Close();
                }
            }

        }
        /// <summary>
        /// Method to get the details of a particular product
        /// </summary>
        /// <param name="orderID"></param>
        /// <returns></returns>
        public OrderDetail GetOrderById(int orderID)
        {
            try
            {
                OrderDetail order = null;
                Connection();
                SqlCommand sqlCommand = new SqlCommand("SPS_GetOrderByID", sqlConnection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@OrderID", orderID);
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
                DataTable dataTable = new DataTable();
                sqlConnection.Open();
                sqlDataAdapter.Fill(dataTable);

                if (dataTable.Rows.Count > 0)
                {
                    DataRow row = dataTable.Rows[0];
                    order = new OrderDetail
                    {
                        OrderID = Convert.ToInt32(row["OrderID"]),
                        ProductName = Convert.ToString(row["ProductName"]),
                        Quantity = Convert.ToInt32(row["Quantity"]),
                        EmailAddress = Convert.ToString(row["EmailAddress"]),
                        OrderDate = Convert.ToDateTime(row["OrderDate"]),
                        TotalAmount = Convert.ToInt32(row["TotalAmount"]),
                        Address = Convert.ToString(row["Address"]),
                        Pincode = Convert.ToInt32(row["Pincode"]),
                        PhoneNumber = Convert.ToInt32(row["PhoneNumber"]),
                        Status = Convert.ToString(row["Status"])

                    };

                }
                return order;
            }
            finally
            {
                if (sqlConnection != null && sqlConnection.State == ConnectionState.Open)
                {
                    sqlConnection.Close();
                }
            }
        }
        /// <summary>
        /// Update the stock after every order placed
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="quantity"></param>
        /// <returns></returns>
        public bool UpdateStock(int productId, int quantity)
        {
            try
            {
                Connection();
                SqlCommand sqlCommand = new SqlCommand("SP_UpdateStock", sqlConnection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@ProductID", productId);
                sqlCommand.Parameters.AddWithValue("@Quantity", quantity);
                sqlConnection.Open();
                int rowsAffected = sqlCommand.ExecuteNonQuery();
                return rowsAffected > 0;
            }
            finally
            {
                if (sqlConnection != null && sqlConnection.State == ConnectionState.Open)
                {
                    sqlConnection.Close();
                }
            }
        }
        /// <summary>
        /// Update the status of the order
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public bool UpdateOrderStatus(int orderId, string status)
        {
            try
            {
                Connection();
                string updateQuery = "UPDATE Orders SET Status = @Status WHERE OrderID = @OrderID";

                SqlCommand command = new SqlCommand(updateQuery, sqlConnection);
                command.Parameters.AddWithValue("@Status", status);
                command.Parameters.AddWithValue("@OrderID", orderId);

                sqlConnection.Open();
                int rowsAffected = command.ExecuteNonQuery();
                
                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                // Handle exceptions appropriately
                throw ex;
            }
            finally
            {
                if (sqlConnection != null && sqlConnection.State == ConnectionState.Open)
                {
                    sqlConnection.Close();
                }
            }
        }

        /// <summary>
        /// Get all the order details of a particular user obtained by its email address
        /// </summary>
        /// <param name="emailAddress"></param>
        /// <returns></returns>
        public static List<OrderDetail> GetOrderHistory(string emailAddress)
        {
            return OrderHistory.Where(o => o.EmailAddress == emailAddress).ToList();
        }
    }
}