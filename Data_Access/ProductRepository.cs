using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Configuration;
using ShopcluesShoppingPortal.Models;

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

        public bool AddProduct(ProductDetail productDetail)
        {
           
                Connection();
                SqlCommand sqlCommand = new SqlCommand("SP_AddNewProductDetails", sqlConnection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@ProductName", productDetail.ProductName);
                sqlCommand.Parameters.AddWithValue("@CategoryName", productDetail.CategoryName);
                sqlCommand.Parameters.AddWithValue("@Description", productDetail.Description);
                sqlCommand.Parameters.AddWithValue("@Stock", productDetail.Stock);
                sqlCommand.Parameters.AddWithValue("@CreatedDate", productDetail.CreatedDate);
                sqlCommand.Parameters.AddWithValue("@ProductImage", productDetail.ProductImage);
                sqlCommand.Parameters.AddWithValue("@Price", productDetail.Price);
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
                                   Stock = dr["Stock"] == DBNull.Value ? 0 : Convert.ToInt32(dr["Stock"]),
                                   CreatedDate = Convert.ToDateTime(dr["CreatedDate"]),
                                   ProductImage = Convert.ToString(dr["ProductImage"]),
                                   Price = dr["Price"] == DBNull.Value ? 0 : Convert.ToInt32(dr["Price"])
                               }).ToList();
                return ProductList;
           
        }

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
                        Stock = Convert.IsDBNull(row["Stock"]) ? 0 : Convert.ToInt32(row["Stock"]),
                        CreatedDate = Convert.ToDateTime(row["CreatedDate"]),
                        ProductImage = Convert.ToString(row["ProductImage"]),
                        Price = Convert.IsDBNull(row["Price"]) ? 0 : Convert.ToInt32(row["Price"])
                    };
                    
                }
                return product;
            }
            

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
                sqlCommand.Parameters.AddWithValue("@ProductImage", productDetail.ProductImage);
                sqlCommand.Parameters.AddWithValue("@Price", productDetail.Price);
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






        public void AddToCart(int userID, int productID, int quantity)
        {
            Connection();
            SqlCommand sqlCommand = new SqlCommand("SP_AddToCart", sqlConnection);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("@UserID", userID);
            sqlCommand.Parameters.AddWithValue("@ProductID", productID);
            sqlCommand.Parameters.AddWithValue("@Quantity", quantity);

            sqlConnection.Open();
            sqlCommand.ExecuteNonQuery();
            sqlConnection.Close();
        }

        // Place an order
        public void PlaceOrder(int userID, DateTime orderDate, decimal totalAmount, string orderStatus, List<CartItem> cartItems)
        {
            Connection();
            SqlCommand sqlCommand = new SqlCommand("SP_PlaceOrder", sqlConnection);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("@UserId", userID);
            sqlCommand.Parameters.AddWithValue("@OrderDate", orderDate);
            sqlCommand.Parameters.AddWithValue("@TotalAmount", totalAmount);
            sqlCommand.Parameters.AddWithValue("@OrderStatus", orderStatus);

            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("ProductId", typeof(int));
            dataTable.Columns.Add("Quantity", typeof(int));

            foreach (var cartItem in cartItems)
            {
                dataTable.Rows.Add(cartItem.ProductID, cartItem.Quantity);
            }

            SqlParameter parameter = sqlCommand.Parameters.AddWithValue("@OrderItems", dataTable);
            parameter.SqlDbType = SqlDbType.Structured;
            parameter.TypeName = "dbo.OrderItemType"; // Assuming dbo.OrderItemType is a user-defined table type

            sqlConnection.Open();
            sqlCommand.ExecuteNonQuery();
            sqlConnection.Close();
        }
    }
}

