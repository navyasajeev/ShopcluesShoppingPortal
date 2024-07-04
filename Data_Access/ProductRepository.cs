using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Configuration;
using ShopcluesShoppingPortal.Models;
using System.IO;

namespace ShopcluesShoppingPortal.Data_Access
{
    public class ProductRepository
    {
        private SqlConnection sqlConnection;

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
        /// Get the details of all products
        /// </summary>
        /// <returns></returns>
        public List<ProductDetail> GetAllProduct()
        {
            Connection();
            List<ProductDetail> ProductList = new List<ProductDetail>();
            SqlCommand sqlCommand = new SqlCommand("SP_GetProducts", sqlConnection);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
            DataTable dataTable = new DataTable();
            sqlConnection.Open();
            sqlDataAdapter.Fill(dataTable);
            sqlConnection.Close();
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
        /// <summary>
        /// Get the details of the product by using id
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        public ProductDetail GetProductById(int productId)
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
            sqlConnection.Close();

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

        /// <summary>
        /// Update the existing product
        /// </summary>
        /// <param name="productDetail"></param>
        /// <returns></returns>
        public bool UpdateProduct(ProductDetail productDetail)
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
        /// Deleting the existing product
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        public bool DeleteProduct(int productId)
        {
            Connection();
            SqlCommand sqlCommand = new SqlCommand("SPD_Product", sqlConnection);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("@ProductId", productId);
            sqlConnection.Open();
            int i = sqlCommand.ExecuteNonQuery();
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
       /// Method to add a new order
       /// </summary>
       /// <param name="orderDetail"></param>
       /// <returns></returns>
        public bool AddOrder(OrderDetail orderDetail)
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
            sqlConnection.Open();
            int id = Convert.ToInt32(sqlCommand.ExecuteScalar());
            sqlConnection.Close();
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
        /// <summary>
        /// Method to get the details of all orders
        /// </summary>
        /// <returns></returns>
        public List<OrderDetail> GetAllOrder()
        {
            Connection();
            List<OrderDetail> ProductList = new List<OrderDetail>();
            SqlCommand sqlCommand = new SqlCommand("SP_GetOrders", sqlConnection);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
            DataTable dataTable = new DataTable();
            sqlConnection.Open();
            sqlDataAdapter.Fill(dataTable);
            sqlConnection.Close();
            ProductList = (from DataRow dr in dataTable.Rows
                           select new OrderDetail()
                           {
                               OrderID = Convert.ToInt32(dr["OrderID"]),
                               ProductName = Convert.ToString(dr["ProductName"]),
                               Quantity = Convert.ToInt32(dr["Quantity"]),
                               EmailAddress = Convert.ToString(dr["EmailAddress"]),
                               OrderDate= Convert.ToDateTime(dr["OrderDate"]),
                               TotalAmount = Convert.ToInt32(dr["TotalAmount"]),
                               Address = Convert.ToString(dr["Address"]),
                               Pincode = Convert.ToInt32(dr["Pincode"]),
                               PhoneNumber= Convert.ToInt32(dr["PhoneNumber"])

                           }).ToList();
            return ProductList;

        }
        /// <summary>
        /// Method to get the details of a particular product
        /// </summary>
        /// <param name="orderID"></param>
        /// <returns></returns>
        public OrderDetail GetOrderById(int orderID)
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
            sqlConnection.Close();

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
                    PhoneNumber = Convert.ToInt32(row["PhoneNumber"])
                };

            }
            return order;
        }
    }
}