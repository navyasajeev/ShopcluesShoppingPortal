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

        /// <summary>
        /// GET: Place an Order 
        /// </summary>
        /// <param name="productId"></param>
        /// <returns>details of a particular product</returns>
        public ActionResult PlaceOrder(int productId)
        {
            try
            {
                var product = productRepository.GetProductById(productId);
                if (product != null && product.Stock > 0)
                {
                    var orderDetail = new OrderDetail
                    {

                        Product = product,
                        ProductName = product.ProductName,
                        Quantity = 1,
                        OrderDate = DateTime.Now,
                        TotalAmount = product.Price,
                        EmailAddress = Session["userEmail"] as string,
                        Status = "Pending"

                    };
                    productRepository.UpdateStock(productId, 1);

                    return View(orderDetail);

                }
                return View();
            }
            catch
            {
                return View("Error");
            }
        }
        /// <summary>
        /// Post:Place the order
        /// </summary>
        /// <param name="orderDetail"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PlaceOrder(OrderDetail orderDetail,int orderid)
        {
            try
            {
                if (ModelState.IsValid)
                {
                


                    if (productRepository.AddOrder(orderDetail))
                    {
                        
                        ViewBag.Message = "Order placed successfully!";                     
                        return RedirectToAction("Message", new { orderId = orderDetail.OrderID });
                    }
                    else
                    {
                        ViewBag.ErrorMessage = "Failed to place the order.";
                    }
                }
            }
            catch 
            {
                ViewBag.ErrorMessage = "Error occurred while processing your request.";
            }
            return View(orderDetail);
        }
        /// <summary>
        /// Display a message shown related to the order placed
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public ActionResult Message(int orderId)
        {
            var product = productRepository.GetOrderById(orderId);
            return View(product);
        }
      }
}
