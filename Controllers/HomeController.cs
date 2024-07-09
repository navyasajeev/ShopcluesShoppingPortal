using ShopcluesShoppingPortal.Data_Access;
using ShopcluesShoppingPortal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Web;
using System.Web.Mvc;
using static ShopcluesShoppingPortal.Data_Access.Repository;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace ShopcluesShoppingPortal.Controllers
{
    public class HomeController : Controller
    {
        ProductRepository productRepository = new ProductRepository();
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// About us page
        /// </summary>
        /// <returns></returns>
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Contact(ContactForm model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    ContactRepository contactRepository = new ContactRepository();
                    if (contactRepository.SendMessage(model))
                    {
                        ViewBag.Message = "Your message has been sent successfully.";
                        return RedirectToAction("Contact");
                    }
                    else
                    {
                        ViewBag.ErrorMessage = "Failed to send message. Please try again later.";
                    }
                }
                return View("Contact", model);
            }
            catch (Exception ex)
            {

                ViewBag.ErrorMessage = "An error occurred while processing your request.";
                Console.WriteLine("Error in SendMessage action: " + ex.Message);
                return View("ContactUs", model);
            }
        }

        public ActionResult MainDashBoard()
        {
            return View();
        }
        /// <summary>
        /// display all product in the user dashboard
        /// </summary>
        /// <returns></returns>
        public ActionResult UserDashBoard(string searchQuery)
        {
            var products = productRepository.GetAllProduct();
            if (!string.IsNullOrEmpty(searchQuery))
            {               
                ViewBag.SearchQuery = searchQuery;
                searchQuery = searchQuery.ToLower(); // Convert searchQuery to lowercase for case-insensitive search

                var filteredProducts = products.Where(p =>
                    p.ProductName.ToLower().Contains(searchQuery) ||
                    p.CategoryName.ToLower().Contains(searchQuery)
                ).ToList();

                return View(filteredProducts);
            }

            ViewBag.SearchQuery = "";
            return View(products);
        }
        public ActionResult MainDash()
        {

          
            var products = productRepository.GetAllProduct();
            return View(products);
            
        }
        /// <summary>
        /// Get the details of a product by using its id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult ProductDetails(int id)
        {
            var product = productRepository.GetProductById(id);
            return View(product);
        }

        public ActionResult ContactUs()
        {
            return View();
        }

        /// <summary>
        /// POST: Contact/SendMessage
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken] 
        public ActionResult ContactUs(ContactForm model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    ContactRepository contactRepository = new ContactRepository();
                    if (contactRepository.SendMessage(model))
                    {
                        ViewBag.Message = "Your message has been sent successfully.";
                        return RedirectToAction("ContactUs");
                    }
                    else
                    {
                        ViewBag.ErrorMessage = "Failed to send message. Please try again later.";
                    }
                }
                return View("ContactUs", model);
            }
            catch (Exception ex)
            {
             
                ViewBag.ErrorMessage = "An error occurred while processing your request.";
                Console.WriteLine("Error in SendMessage action: " + ex.Message);
                return View("ContactUs", model);
            }
        }
        /// <summary>
        ///Get the Login details
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult NewLogin()
        {

            return View();
        }
        /// <summary>
        /// Post:new login
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult NewLogin(Login login)
        {
            try
            {
                Repository repository = new Repository();
                if (string.IsNullOrEmpty(login.Password))
                {
                    return View("NewLogin", login);
                }
                bool isAuthenticated = repository.LoginDetails(login);
                if (login.EmailAddress == "admin@gmail.com" && login.Password == "Admin123")
                {
                    return RedirectToAction("AdminDashBoard", "Admin");
                }
                else if (isAuthenticated)
                {
                    Session["userEmail"] = login.EmailAddress;
                    return RedirectToAction("UserDashBoard", "Home");

                }
                else
                {
                    ModelState.AddModelError("", "Invalid username or password.");
                    return View("NewLogin", login);
                }
            }
            catch
            {
                return View();
            }
            
        }

        public ActionResult CartDetails()
        {
            var cart = Session["Cart"] as List<CartItem> ?? new List<CartItem>();
            return View(cart);
        }

        public ActionResult AddToCart(int productId)
        {
            var productRepository = new ProductRepository();
            var product = productRepository.GetProductById(productId);

            if (product != null)
            {
                var cart = Session["Cart"] as List<CartItem> ?? new List<CartItem>();

                // Check if the product is already in the cart
                var cartItem = cart.FirstOrDefault(item => item.Product.ProductID == productId);

                if (cartItem != null)
                {
                    cartItem.Quantity++;
                }
                else
                {
                    cart.Add(new CartItem { Product = product, Quantity = 1 });
                }

                Session["Cart"] = cart;
            }

            return RedirectToAction("CartDetails", "Home");
        }

        public ActionResult UpdateCart(int productId, int quantity)
        {
            var cart = Session["Cart"] as List<CartItem>;

            if (cart != null)
            {
                var cartItem = cart.FirstOrDefault(item => item.Product.ProductID == productId);

                if (cartItem != null)
                {
                    cartItem.Quantity = quantity;
                }

                Session["Cart"] = cart;
            }
           // return Json(new { success = true });
            return RedirectToAction("CartDetails", "Home");
        }
        public ActionResult RemoveFromCart(int productId)
        {
            var cart = Session["Cart"] as List<CartItem>;

            if (cart != null)
            {
                var cartItemToRemove = cart.FirstOrDefault(item => item.Product.ProductID == productId);

                if (cartItemToRemove != null)
                {
                    cart.Remove(cartItemToRemove);
                    Session["Cart"] = cart; // Update the session with the modified cart
                }
            }

            return RedirectToAction("CartDetails", "Home");
        }

        public ActionResult PlaceOrderForm()
        {
            // Assuming you have a form to collect additional order details
            return View();
        }

        [HttpPost]
        public ActionResult PlaceOrderForm(OrderDetail orderDetail)
        {
            var cart = Session["Cart"] as List<CartItem>;
            var productRepository = new ProductRepository();

            if (ModelState.IsValid)
            {
                // Loop through cart items and add orders
                foreach (var item in cart)
            {
                var order = new OrderDetail
                {
                    ProductName = item.Product.ProductName,
                    Quantity = item.Quantity,
                    EmailAddress = Session["userEmail"] as string,
                    OrderDate = DateTime.Now,
                    TotalAmount = item.Quantity * item.Product.Price,
                    Address = orderDetail.Address,
                    Pincode = orderDetail.Pincode,
                    PhoneNumber = orderDetail.PhoneNumber
                };

                productRepository.AddOrder(order);
            }

            // Clear the cart after placing order
            Session["Cart"] = null;

            ViewBag.Message = "Order placed successfully!";
            return RedirectToAction("ConfirmMessage", "Home"); // Redirect to home page or order summary page
            }

            // If the model state is not valid, return to the same view to correct errors
            return View(orderDetail);
        }
        public ActionResult ConfirmMessage()
        {
            return View();
        }
        public ActionResult OrderHistory()
        {
            string userEmail = Session["userEmail"] as string;
            var orders = ProductRepository.GetOrderHistory(userEmail);

            return View(orders);
        }

    }
}
    









