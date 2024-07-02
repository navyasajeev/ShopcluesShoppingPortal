using ShopcluesShoppingPortal.Data_Access;
using ShopcluesShoppingPortal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShopcluesShoppingPortal.Controllers
{
    public class OrderController : Controller
    {
        private ProductRepository productRepository;

        public OrderController()
        {
            productRepository = new ProductRepository();
        }

        // GET: Order/Create
        public ActionResult PlaceOrder(int productId)
        {
            
            var product = productRepository.GetProductById(productId);
            var orderDetail = new OrderDetail
            {
                ProductID = productId,
                Product = product,
                ProductName = product.ProductName, 
                Quantity = 1, 
                OrderDate = DateTime.Now,
                TotalAmount = product.Price, 
                EmailAddress = Session["userEmail"] as string
            };
          

            return View(orderDetail);
        }

        // POST: Order/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PlaceOrder(OrderDetail orderDetail)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // Save order details to database
                    if (productRepository.AddOrder(orderDetail))
                    {
                        ViewBag.Message = "Order placed successfully!";
                        return RedirectToAction("Index", "Home"); // Redirect to home page or order list page
                    }
                    else
                    {
                        ViewBag.ErrorMessage = "Failed to place the order.";
                    }
                }
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Error occurred while processing your request.";
            }

            // If ModelState is not valid or if an error occurs, return to form with errors
            return View(orderDetail);
        }
    }
}
