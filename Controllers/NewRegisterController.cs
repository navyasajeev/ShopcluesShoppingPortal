using ShopcluesShoppingPortal.Data_Access;
using ShopcluesShoppingPortal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShopcluesShoppingPortal.Controllers
{
    public class NewRegisterController : Controller
    {
        /// <summary>
        /// GET:Get the details of all new registered customer
        /// </summary>
        /// <returns>list</returns>
        public ActionResult GetAllCustomers()
        {
            Repository repository = new Repository();
            var UserList = repository.GetAllUsers();
            if (UserList.Count == 0)
            {
                ViewBag.Message = "Currently no  customers details in the database ";
            }
            return View(UserList);
        }

        /// <summary>
        /// GET: NewRegister/Details- Details of a particular customer
        /// </summary>
        /// <param name="id"></param>
        /// <returns>list</returns>
        public ActionResult Details(int id)
        {
            {
                Repository repository = new Repository();
                var user = repository.GetCustomerByID(id).FirstOrDefault(); ;
                if (user == null)
                {
                    // Handle scenario where user with provided id is not found
                    ViewBag.Message = "User not found with ID " + id;
                    return RedirectToAction("GetAllCustomers");
                }

                return View(user);
            }
        }

        /// <summary>
        /// GET: add new customer
        /// </summary>
        /// <returns></returns>
        public ActionResult AddCustomerDetails()
        {
            ViewBag.States = GetStates();
            ViewBag.Cities = GetCities();
            return View();
        }
      
        /// <summary>
        /// POST: Customer/Add a new customer Details
        /// </summary>
        /// <param name="registration"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AddCustomerDetails(Registration registration)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Repository repository = new Repository();
                    if (repository.InsertUserData(registration))
                    {
                        ViewBag.Message = "Employee details added successfully";
                    }
                }
                ViewBag.States = GetStates();
                ViewBag.Cities = GetCities();
                return View(registration);
            }
            catch
            {
                ViewBag.States = GetStates();
                ViewBag.Cities = GetCities();
                return View(registration);
            }
        }
        /// <summary>
        /// Get: get  a particular customer details for edit
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult EditCustomerDetails(int id)
        {
            Repository repository = new Repository();
            var customer = repository.GetAllUsers().Find(registration => registration.UserID == id);

            if (customer == null)
            {
                ViewBag.Message = "Customer not available with ID" + id.ToString();
                return RedirectToAction("GetAllCustomers");
            }

            ViewBag.States = GetStates(); // Ensure GetStates() returns List<SelectListItem>
            ViewBag.Cities = GetCities(); // Ensure GetCities() returns List<SelectListItem>

            return View(customer);
        }
        /// <summary>
        /// POST: edit a particular customer details
        /// </summary>
        /// <param name="id"></param>
        /// <param name="registration"></param>
        /// <returns></returns>
            
            [HttpPost]
        public ActionResult EditCustomerDetails(int id, Registration registration)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Repository repository = new Repository();
                    repository.UpdateCustomerData(registration);
                    ViewBag.States = GetStates();
                    ViewBag.Cities = GetCities();
                    return View(registration);
                }
                return View(registration);
            }        
            catch (Exception exception)
            {

                ViewBag.Message = exception.Message;
                return View();
            }
        }
        /// <summary>
        /// Delete a customer details by using its id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Delete(int id)
        {
            try
            {
                Repository repository = new Repository();
                if (repository.DeleteCustomer(id))
                {
                    ViewBag.AlertMsg = "Customer details deleted successfully";

                }
                return RedirectToAction("GetAllCustomers");

            }
            catch
            {
                return View();
            }
        }
        private List<SelectListItem> GetStates()
        {
            return new List<SelectListItem>
{
    new SelectListItem { Value = "Kerala", Text = "Kerala" },
    new SelectListItem { Value = "Tamilnadu", Text = "Tamilnadu" },
    new SelectListItem { Value = "Karnataka", Text = "Karnataka" },
    new SelectListItem { Value = "Bihar", Text = "Bihar" },
    new SelectListItem { Value = "Delhi", Text = "Delhi" },
};
        }
        private List<SelectListItem> GetCities()
        {
            return new List<SelectListItem>
{
    new SelectListItem { Value = "Kochi", Text = "Kochi" },
    new SelectListItem { Value = "Kozhikode", Text = "Kozhikode" },
    new SelectListItem { Value = "Thiruvanathapuram", Text = "Thiruvanathapuram" },
};
        }
    }
}
