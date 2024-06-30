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

namespace ShopcluesShoppingPortal.Controllers
{
    public class HomeController : Controller
    {
        ProductRepository productRepository = new ProductRepository();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult MainDashBoard()
        {
            return View();
        }
        public ActionResult MainDash()
        {

          
            var products = productRepository.GetAllProduct();
            return View(products);
            
        }
        public ActionResult ProductDetails(int id)
        {
            var product = productRepository.GetProductById(id);
            return View(product);
        }

        [HttpPost]
        public ActionResult AddToCart(int productId, int quantity)
        {
            // Example: Get current user ID (replace with your logic)
            int userId = 1;

            productRepository.AddToCart(userId, productId, quantity);

            return RedirectToAction("MainDash");
        }

        [HttpPost]
        public ActionResult PlaceOrder(List<CartItem> cartItems)
        {
            //  Get current user ID 
            int userId = 1;

            //  Calculate total amount 
            decimal totalAmount = cartItems.Sum(ci => ci.Quantity * GetProductPrice(ci.ProductID));

            // Set order status 
            string orderStatus = "Pending";

            productRepository.PlaceOrder(userId, DateTime.Now, totalAmount, orderStatus, cartItems);

            // Clear cart after placing order 
            // Example: Clear session["Cart"] or delete from CartItems table

            return RedirectToAction("MainDash");
        }

        // Example: Method to get product price (replace with your logic)
        private decimal GetProductPrice(int productId)
        {

           
                var product = productRepository.GetProductById(productId);
                if (product != null)
                {
                    return product.Price; // Adjust according to your schema
                }
                else
                {
                    // Handle case where product with given ID is not found
                    throw new ArgumentException("Product not found for the given ID.");
                }
          
        }






        public ActionResult Contactus()
        {
            return View();
        }

        // POST: Contact/SendMessage (Handle form submission)
        [HttpPost]
        [ValidateAntiForgeryToken] // Helps prevent CSRF attacks
        public ActionResult SendMessage(ContactForm model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Configure and send email
                    var fromAddress = new MailAddress("your-email@gmail.com", "Your Name");
                    var toAddress = new MailAddress("recipient@example.com", "Recipient Name");
                    const string fromPassword = "your-password";
                    const string subject = "New message from contact form";
                    string body = $"Name: {model.Name}\n";
                    body += $"Email: {model.Email}\n";
                    body += $"Message:\n{model.Message}";

                    var smtp = new SmtpClient
                    {
                        Host = "smtp.gmail.com",
                        Port = 587,
                        EnableSsl = true,
                        DeliveryMethod = SmtpDeliveryMethod.Network,
                        UseDefaultCredentials = false,
                        Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
                    };

                    using (var message = new MailMessage(fromAddress, toAddress)
                    {
                        Subject = subject,
                        Body = body
                    })
                    {
                        smtp.Send(message);
                    }

                    ViewBag.Message = "Your message has been sent successfully.";
                    return View("Contactus"); // Redirect to a thank you page or display success message
                }
                catch (Exception ex)
                {
                    ViewBag.Error = $"Error sending email: {ex.Message}";
                    return View("Contactus", model); // Return to the form with an error message
                }
            }
            else
            {
                // Model validation failed, return to the form with errors
                return View("Contactus", model);
            }
        }
        [HttpGet]
        public ActionResult NewLogin()
        {

            return View();
        }

        [HttpPost]
        public ActionResult NewLogin(Login login)
        {


            Repository repository = new Repository();
            bool isAuthenticated = repository.LoginDetails(login);
            if (login.EmailAddress == "admin@gmail.com" && login.Password == "Admin123")
            {
                return RedirectToAction("AdminDashBoard", "Admin"); // Redirect to admin dashboard
            }
            else if (isAuthenticated)
            {
                return RedirectToAction("MainDash", "Home"); // Redirect to user dashboard
            }
            else
            {
                ModelState.AddModelError("", "Invalid username or password."); // Add error message if login fails
                return View("NewLogin", login); // Return to login page with errors
            }
        }

         
    }
}







