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
        public ActionResult UserDashBoard()
        {
            var products = productRepository.GetAllProduct();
            return View(products);
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

        public ActionResult ContactUs()
        {
            return View();
        }

        // POST: Contact/SendMessage
        [HttpPost]
        [ValidateAntiForgeryToken] // Helps prevent CSRF attacks
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

                // If ModelState is not valid or sending message failed, return to the form view
                return View("ContactUs", model);
            }
            catch (Exception ex)
            {
             
                ViewBag.ErrorMessage = "An error occurred while processing your request.";
                Console.WriteLine("Error in SendMessage action: " + ex.Message);
                return View("ContactUs", model);
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
                Session["userEmail"] = login.EmailAddress;
                return RedirectToAction("UserDashBoard", "Home"); // Redirect to user dashboard
               
            }
            else
            {
                ModelState.AddModelError("", "Invalid username or password."); // Add error message if login fails
                return View("NewLogin", login); // Return to login page with errors
            }
        }

         
    }
}







