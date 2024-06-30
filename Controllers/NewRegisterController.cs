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
        // GET: NewRegister
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

        // GET: NewRegister/Details/5
        public ActionResult Details(int id)
        {
            {
                Repository repository = new Repository();
                var user = repository.GetCustomerByID(id);
                if (user == null)
                {
                    // Handle scenario where user with provided id is not found
                    ViewBag.Message = "User not found with ID " + id;
                    return RedirectToAction("GetAllCustomers");
                }

                return View(user);
            }
        }

        // GET: Employee/AddEmployeeDetails
        public ActionResult AddCustomerDetails()
        {
            ViewBag.States = GetStates();
            ViewBag.Cities = GetCities();
            return View();
        }
      


        // POST: Employee/AddEmployeeDetails
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

        // GET: NewRegister/Edit/5
        /*public ActionResult EditCustomerDetails(int id)
                {
                    Repository repository = new Repository();
                    var customer = repository.GetCustomersByID(id).FirstOrDefault();
                    if (customer == null)
                    {
                        ViewBag.Message = "Customer not available with ID" + id.ToString();
                        return RedirectToAction("GetAllCustomers");
                    }
                    ViewBag.States = GetStates(); // Populate states dropdown
                    ViewBag.Cities = GetCities();

                    return View(customer);
                }
        */


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
            // POST: NewRegister/Edit/5
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
                // TODO: Add update logic here


            
            catch (Exception exception)
            {

                ViewBag.Message = exception.Message;
                return View();
            }
        }
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
           // POST: NewRegister/Delete/5
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
