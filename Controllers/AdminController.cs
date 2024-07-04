using ShopcluesShoppingPortal.Data_Access;
using ShopcluesShoppingPortal.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web;
using System.Web.Mvc;

namespace ShopcluesShoppingPortal.Controllers
{
    public class AdminController : Controller
    {
        private SqlConnection sqlConnection;
        private void Connection()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["adoConnectionString"].ToString();
             sqlConnection = new SqlConnection(connectionString);
        }      
        public ActionResult AdminDashBoard()
        {
            return View();
        }
        /// <summary>
        /// Get the details of all product
        /// </summary>
        /// <returns></returns>
        public ActionResult GetAllProductDetails()
        {
            ProductRepository productRepository = new ProductRepository();
            ModelState.Clear();
            return View(productRepository.GetAllProduct());
        }
        /// <summary>
        /// Get:Add details of a new product
        /// </summary>
        /// <returns></returns>
        public ActionResult AddProductDetails()
        {
            return View();
        }
        /// <summary>
        /// 
        /// Post:Add the details of a new product
        /// </summary>
        /// <param name="productDetail"></param>
        /// <param name="file"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]      
        public ActionResult AddProductDetails(ProductDetail productDetail,HttpPostedFileBase file)
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
            if(file!=null &&file.ContentLength>0)
            {
                string filename = Path.GetFileName(file.FileName);
                string imgpath = Path.Combine(Server.MapPath("~/ProductImages/"), filename);
                file.SaveAs(imgpath);
            }
            sqlCommand.Parameters.AddWithValue("@ProductImage", "~/ProductImages/" + file.FileName);
            sqlCommand.ExecuteNonQuery();
            sqlConnection.Close();
            ModelState.Clear();
            ViewData["Message"] = "Product details " + productDetail.ProductName + " is saved successfully";
            return View();
            
        }
        /// <summary>
        /// Get:Get the details of a product using its id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult EditProductDetails(int id)
        {
            ProductRepository productRepository = new ProductRepository();

            return View(productRepository.GetAllProduct().Find(productDetail => productDetail.ProductID == id));
           
        }
        /// <summary>
        /// Post:Edit the particular details of a product
        /// </summary>
        /// <param name="productID"></param>
        /// <param name="productDetail"></param>
        /// <param name="file"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditProductDetails(int productID,ProductDetail productDetail, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    ProductRepository productRepository = new ProductRepository();
                        productRepository.UpdateProduct(productDetail);
                        if (file != null && file.ContentLength > 0)
                        {
                            string filename = Path.GetFileName(file.FileName);
                            string imgpath = Path.Combine(Server.MapPath("~/ProductImages/"), filename);
                            file.SaveAs(imgpath);
                            productDetail.ProductImage = "~/ProductImages/" + filename;
                        }
                        ViewData["Message"] = "Product details updated successfully";
                        return RedirectToAction("GetAllProductDetails", "Admin");                     
                }
                catch (Exception ex)
                {
                    ViewData["Message"] = "Error updating product details: " + ex.Message;
                    return View(productDetail); 
                }
            }
            else
            {
                return View(productDetail); 
            }
        }
        /// <summary>
        /// Get all the details of a particular product
        /// </summary>
        /// <param name="id"></param>
        /// <returns> details of particular id of a product </returns>
        public ActionResult ProductDetails(int id)
        {
            ProductRepository productRepository = new ProductRepository();
            var product = productRepository.GetProductById(id);

            

            return View(product);
        
        }
        /// <summary>
        /// Delete a particular product
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult DeleteProductDetails(int id)
        {
            try
            {
                ProductRepository productRepository = new ProductRepository();
                if (productRepository.DeleteProduct(id))
                {
                    ViewBag.AlertMsg = "Product details deleted successfully";
                }
                return RedirectToAction("GetAllProductDetails");
            }
            catch 
            {
                return View();
            }
        }
        /// <summary>
        /// Logout option
        /// </summary>
        /// <returns></returns>
        public ActionResult Logout()
        {
            return RedirectToAction("MainDash", "Home");
        }
        /// <summary>
        /// Get the details of all the orders
        /// </summary>
        /// <returns>list</returns>
        public ActionResult GetAllOrderDetails()
        {
            ProductRepository productRepository = new ProductRepository();
            ModelState.Clear();
            return View(productRepository.GetAllOrder());
        }
        /// <summary>
        /// Get the details of a particular order
        /// </summary>
        /// <param name="id"></param>
        /// <returns>list</returns>

        public ActionResult OrderDetails(int id)
        {
            ProductRepository productRepository = new ProductRepository();
            var product = productRepository.GetOrderById(id);



            return View(product);

        }
    }
}
